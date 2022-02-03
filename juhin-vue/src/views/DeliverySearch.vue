<template>
    <div class="search-delivery-form">
        <h2 id="search-header">Wyszukiwanie dostaw</h2>
        <form @submit.prevent="handleSearchSubmit">
            <div class="onerow">
                <div>
                    <label>DOSTAWCA</label>
                    <input type="text">
                </div>
                <div>
                    <label>NR ZAMÓWIENIA</label>
                    <input type="text">
                </div>
                <div>
                    <label>NR CZĘŚCI / OPIS</label>
                    <input type="text">
                </div>
                <div>
                    <label>KRAJ POCHODZENIA</label>
                    <input type="text">
                </div>
                <div>
                    <label>DATA (ETA)</label>
                    <input type="date">
                </div>
            </div>
            <div class="onerow">
                
                <div>
                    <label>MAGAZYN DOCELOWY</label>
                    <select >
                        <option>RM</option>
                    </select>
                </div>
                <div>
                    <label>STATUS</label>
                    <select >
                        <option>Delivered</option>
                    </select>
                </div>
                <div class="switches">
                     <label>WSK</label>
                        <div class="icpSwich">
                            <input type="checkbox" name="icpSwich" class="icpSwich-cb" id="fs" >
                            <label class="icpSwich-label" for="fs">
                                <div class="icpSwich-inner"></div>
                                <div class="icpSwich-switch"></div>
                            </label>
                        </div>
                </div>
                <div class="switches">
                    <label>PRIORYTET</label>
                        <div class="flipswitch">
                            <input type="checkbox" name="flipswitch" class="flipswitch-cb" id="prio" >
                            <label class="flipswitch-label" for="prio">
                                <div class="flipswitch-inner"></div>
                                <div class="flipswitch-switch"></div>
                            </label>
                        </div>
                </div>
                <button><span class="material-icons">search</span>Szukaj</button>

            </div>
        </form>
    </div>


  
  <div v-if="!error"  class="table-list">
            <div>
                <div class="table-container table-header">
                    <div>
                        <p>NR ZAMÓWIENIA</p>
                    </div>
                    <div>
                        <p>DOSTAWCA</p>
                    </div>
                    <div>
                        <p>NR DOSTAWCY</p>
                    </div>
                    <div>
                        <p>UTWORZONO</p>
                    </div>
                    <div>
                        <p>ETA</p>
                    </div>
                    <div>
                        <p>PRZEWOŹNIK</p>
                    </div>
                    <div>
                        <p>OCENA</p>
                    </div>
                    <div>
                        <p>STATUS</p>
                    </div>
                    <div>
                        <p>PRIO</p>
                    </div>
                </div>
                <div v-for="delivery in deliveries" :key="delivery.deliveryId" >
                    <router-link :to="{name:'DeliveryDetails', params:{id:delivery.deliveryId}}">

                        <div class="table-container">    
                            <div class="sub-table-list" >
                                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                    <p>{{order.orderNumber}}</p>
                                </div>
                            </div>
                            <div>
                                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                    <p v-if="order.vendorData">{{order.vendorData.name}}</p>
                                </div>
                            </div>
                            <div>
                                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                    <p v-if="order.vendorData">{{order.vendorData.vendorCode}}</p>
                                </div>
                            </div>
                            <div>
                                <p>{{delivery.createdAt}}</p>
                            </div>
                            <div>
                                <p>{{delivery.etaDate}}</p>
                            </div>
                            <div>
                                <p v-if="delivery.forwarderName">{{delivery.forwarderName}}</p>
                            </div>
                            <div>
                                <p>{{delivery.rating}}</p>
                            </div>
                            <div>
                                <p v-if="delivery.statusName">{{delivery.statusName}}</p>
                            </div>
                            <div>
                                <p>{{delivery.isPriority?"TAK":"NIE"}}</p>
                            </div>

                        </div>
                    </router-link>
                </div>
          </div>
          <div class="table-buttons">
                
                <button @click="handlePages(10)"><span class="material-icons">list</span>10</button>
                <button @click="handlePages(20)"><span class="material-icons">list</span>20</button>
                <button @click="handlePages(50)"><span class="material-icons">list</span>50</button>
                <button v-if="pageNo==1"><span class="material-icons">keyboard_double_arrow_left</span>cofnij</button>
                <button v-else @click="handlePreviousPage"><span class="material-icons">keyboard_double_arrow_left</span>cofnij</button>

                <div class="table-page-numbers">
                     <div v-for="page in lastPage" :key="page" @click="handleGoToPage(page)">
                        <div :class="{'active-page-no' : pageNo==page}">{{page}}</div>
                    </div>
                    <div> | </div>
                    <div v-if="lastPage>=pageNo" @click="handleGoToPage(lastPage)"> {{lastPage}}</div>
                    
                </div>
                <button v-if="pageNo<lastPage" @click="handleNextPage">dalej<span class="material-icons">keyboard_double_arrow_right</span></button>
                <button v-else >koniec<span class="material-icons">keyboard_double_arrow_right</span></button>
          </div>

      </div>
</template>

<script>
import { computed, onMounted, ref, watch } from '@vue/runtime-core'
import getDeliveriesList from '../composables/getDeliveriesList.js'
import urlHolder from '../composables/urlHolder.js'
import { useStore } from 'vuex'

export default {
  props:[],
  setup(props) {
    const url = urlHolder
    const store = useStore()
    
    const user = computed(()=> store.getters.getUser)
    const userToken = computed(()=> store.getters.getUserToken)

    const {deliveries, error, loadDeliveries, totalRecords} = getDeliveriesList(url, userToken.value)
    const pageNo = ref(1)
    const recordsPerPage = ref(10)
    const lastPage = ref(1)
    
    const calculatePageCount = (pageSize, totalCount) => {
        //console.log('RPP '+pageSize)
        //console.log('TR '+totalCount)
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }

    onMounted(async () => {
      await loadDeliveries(pageNo.value,recordsPerPage.value)
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        // console.log(lastPage.value)
        console.log(lastPage.value)
    })
  

    watch((deliveries), async () =>{
        
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        if(vendors.value){
             await handleGoToPage(lastPage.value)
         }
    })

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            await loadDeliveries(pageNo.value, recordsPerPage.value)
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
        await loadDeliveries(pageNo.value, recordsPerPage.value)
        }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        await loadDeliveries(pageNo.value, recordsPerPage.value)
    
        
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        await loadDeliveries(page, recordsPerPage.value)
    }

    return { deliveries, error, pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage }
  }
}
</script>

<style>
.search-delivery-form h2 #search-header{
    margin: 0px;
}
.search-delivery-form form{
    background-color: transparent;
    /* padding: 0; */
    margin: 24px 0 24px 0;
    border: 3px solid #555;
    box-shadow: none;
    /* box-shadow: inset 5px 5px 8px rgba(20,20,20,0.3); */
    max-width: none;
    /* border-radius: 30px; */

}
.search-delivery-form form .onerow{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr;
    grid-template-rows: auto;
    gap: 25px;
    align-items: end;
}
.search-delivery-form form .onerow .switches{
    justify-self: center;

}
.search-delivery-form form input{
    color: #ddd;
    font-size: 12px;
    max-height: 36px;
    background-color: #444;
}
.search-delivery-form form select{
    color: #ddd;
    font-size: 12px;
    max-height: 36px;
    background-color:#444;
}
.search-delivery-form form label{
    font-size: 12px;
    
}
.switches .flipswitch{
    margin: 16px 0;
}
.search-delivery-form form button{
    margin: 10px 0;
    justify-content: left;
    
}
.search-delivery-form form button span{
    margin-right: 40px;
}


.icpSwich {
    margin: 16px 0;
    position: relative;
    width: 98px;
}
.icpSwich input[type=checkbox] {
  display: none;
}
.icpSwich-label {
  display: block;
  overflow: hidden;
  cursor: pointer;
  border: 0px solid #8C8C8C;
  border-radius: 50px;
}
.icpSwich-inner {
  width: 200%;
  margin-left: -100%;
  transition: margin 0.3s ease-in 0s;
  
}
.icpSwich-inner:before, .icpSwich-inner:after {
  float: left;
  width: 50%;
  height: 22px;
  padding: 0;
  line-height: 22px;
  font-size: 12px;
  color: white;
  font-family: Trebuchet, Arial, sans-serif;
  font-weight: bold;
  box-sizing: border-box;
  
}
.icpSwich-inner:before {
  content: "WSK";
  padding-left: 11px;
  background-color: #e24f4f;
  color: #FFFFFF;
  box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.icpSwich-inner:after {
    content: "NIE WSK";
    padding-right: 11px;
    background-color: #636363;
    color: #E8E8E8;
    text-align: right;
    box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.icpSwich-switch {
  width: 29px;
  margin: -3.5px;
  background: #F5B727;
  box-shadow: 1px 1px 3px rgba(4,4,4,0.4), -1px -1px 3px rgba(4,4,4,0.4);
  border-radius: 50px;
  position: absolute;
  top: 0;
  bottom: 0;
  right: 73px;
  transition: all 0.3s ease-in 0s;
}
.icpSwich-cb:checked + .icpSwich-label .icpSwich-inner {
  margin-left: 0;
}
.icpSwich-cb:checked + .icpSwich-label .icpSwich-switch {
  right: 0;
}

</style>