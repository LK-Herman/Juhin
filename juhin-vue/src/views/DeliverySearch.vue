<template>
    <div class="search-delivery-form">
        <h2 id="search-header">Wyszukiwanie dostaw</h2>
        <form @submit.prevent="handleSearchSubmit">
            <div class="onerow">
                <div>
                    <label >DOSTAWCA</label>
                     <div class="field-btn">
                        <input type="text" v-model="formVendor">
                        <div class="btn" @click="formVendor = ''"><span class="material-icons">backspace</span></div>
                    </div>
                </div>
                <div>
                    <label>NR ZAMÓWIENIA</label>
                     <div class="field-btn">
                            <input type="text" v-model="formPurchaseOrder">
                            <div class="btn" @click="formPurchaseOrder = ''"><span class="material-icons">backspace</span></div>
                        </div>
                </div>
                <div>
                    <label>NR CZĘŚCI / OPIS</label>
                     <div class="field-btn">
                        <input type="text" v-model="formItem">
                        <div class="btn" @click="formItem = ''"><span class="material-icons">backspace</span></div>
                     </div>
                </div>
                <div>
                    <label>KRAJ POCHODZENIA</label>
                    <div class="field-btn">
                        <input type="text" v-model="formCountry">
                        <div class="btn" @click="formCountry = ''"><span class="material-icons">backspace</span></div>
                    </div>
                </div>
                <div>
                    <label>DATA (ETA)</label>
                     <div class="field-btn">
                        <input type="date" v-model="formDate">
                        <div class="btn" @click="formDate = null"><span class="material-icons">backspace</span></div>
                     </div>
                </div>
            </div>
            <div class="onerow">
                
                <div>
                    <label>MAGAZYN DOCELOWY</label>
                    <div class="field-btn">
                        <select v-if="!warError" v-model="formWarehouseId">
                            <option v-for="war in warehouses" :key="war.warehouseId" :value="war.warehouseId">{{war.shortName}} / {{war.description}}</option>
                        </select>
                        <div class="btn" @click="formWarehouseId = 0"><span class="material-icons">backspace</span></div>
                    </div>
                </div>
                <div>
                    <label>STATUS</label>
                    <div class="field-btn">
                        <select v-if="!statusError" v-model="formStatusId">
                            <option v-for="status in statuses" :key="status.statusId" :value="status.statusId">{{status.name}}</option>
                        </select>
                        <div class="btn" @click="formStatusId = 0"><span class="material-icons">backspace</span></div>
                    </div>
                </div>
                <div class="switches">
                     <label>WSK</label>
                        <div class="icpSwich">
                            <input type="checkbox" name="icpSwich" class="icpSwich-cb" id="fs" v-model="formIsICP">
                            <label class="icpSwich-label" for="fs">
                                <div class="icpSwich-inner"></div>
                                <div class="icpSwich-switch"></div>
                            </label>
                        </div>
                </div>
                <div class="switches">
                    <label>PRIORYTET</label>
                        <div class="flipswitch">
                            <input type="checkbox" name="flipswitch" class="flipswitch-cb" id="prio" v-model="formIsPrio">
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

  <div>{{searchError}}</div> 
  
  <div v-if="!searchError"  class="table-list">
            <div>
                <div class="table-container table-header">
                    <div @click="filterDeliveries('orderNumber')">
                        <p>NR ZAMÓWIENIA</p>
                    </div>
                    <div @click="filterDeliveries('vendor')">
                        <p>DOSTAWCA</p>
                    </div>
                    <div @click="filterDeliveries('vendorNumber')">
                        <p>NR DOSTAWCY</p>
                    </div>
                    <div @click="filterDeliveries('created')">
                        <p>UTWORZONO</p>
                    </div>
                    <div @click="filterDeliveries('etaDate')">
                        <p>ETA</p>
                    </div>
                    <div @click="filterDeliveries('forwarder')">
                        <p>PRZEWOŹNIK</p>
                    </div>
                    <div @click="filterDeliveries('rating')">
                        <p>OCENA</p>
                    </div>
                    <div @click="filterDeliveries('status')" >
                        <p>STATUS</p>
                    </div>
                    <div @click="filterDeliveries('isPriority')" >
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
import { computed, onBeforeMount, onMounted, ref, watch } from '@vue/runtime-core'

import getSearchedDeliveries from '../composables/getSearchedDeliveries.js'
import getWarehouses from '../composables/getWarehouses.js'
import urlHolder from '../composables/urlHolder.js'
import { useStore } from 'vuex'
import getStatusues from '../composables/getStatuses.js'

export default {
  setup() {
    const url = urlHolder
    const store = useStore()
    
    const user = computed(()=> store.getters.getUser)
    const userToken = computed(()=> store.getters.getUserToken)
    const { deliveries, error:searchError, loadSearchedDeliveries, totalRecords } = getSearchedDeliveries(url, userToken.value)
    const { warehouses, error:warError, loadWarehouses } = getWarehouses(url, userToken.value)
    const { statuses, error:statusError, loadStatuses } = getStatusues(url, userToken.value)

    const pageNo = ref(1)
    const recordsPerPage = ref(100)
    const lastPage = ref(1)
    
    const formVendor = ref('')
    const formCountry = ref('')
    const formDate = ref()
    const formItem = ref('')
    const formIsICP = ref(false)
    const formIsPrio = ref(false)
    const formWarehouseId = ref(0)
    const formStatusId = ref(0)
    const formPurchaseOrder = ref('')
    const searchData = ref()
    const actualSorted = ref()
    const order = ref(true)

    const updateSearchData = (()=>{
        searchData.value =
        {
            isPrio : formIsPrio.value,
            isICP : formIsICP.value,
            page : pageNo.value,
            recordsPerPage : recordsPerPage.value
        }
            if(formDate.value != null) searchData.value['date'] = formDate.value
            if(formStatusId.value != 0) searchData.value['statusId'] = formStatusId.value
            if(formWarehouseId.value != 0) searchData.value['warehouseId'] = formWarehouseId.value
            if(formPurchaseOrder.value != '') searchData.value['orderNumber'] = formPurchaseOrder.value
            if(formVendor.value != '') searchData.value['vendorName'] = formVendor.value
            if(formItem.value != '') searchData.value['partDescription'] = formItem.value
            if(formCountry.value != '') searchData.value['country'] = formCountry.value
    })

    const filterDeliveries = (sortedBy) => 
    {
        
        order.value = actualSorted.value==sortedBy?!order.value:order.value 
        console.log(sortedBy + " / " + order.value)
        deliveries.value.sort( function(a, b) 
        {
            let compareResult = 0
            if (sortedBy == "vendor" )
            {
                    a['purchaseOrders'].forEach(poa => 
                    {
                        b['purchaseOrders'].forEach(pob =>
                        {   
                            if(order.value == true)
                            {
                                compareResult = poa['vendorName'].localeCompare(pob['vendorName'])
                                if (compareResult == 0) compareResult = a['etaDate'].localeCompare(b['etaDate'])    
                            }
                            else
                            {
                                compareResult = pob['vendorName'].localeCompare(poa['vendorName'])
                                if (compareResult == 0) compareResult = a['etaDate'].localeCompare(b['etaDate'])
                            }
                        })
                    });
            }
            if (sortedBy == "orderNumber" )
            {
                    a['purchaseOrders'].forEach(poa => 
                    {
                        b['purchaseOrders'].forEach(pob =>
                        {   
                            if(order.value == true)
                            {
                                compareResult = poa['orderNumber'].localeCompare(pob['orderNumber'])
                                if (compareResult == 0) compareResult = a['etaDate'].localeCompare(b['etaDate'])    
                            }
                            else
                            {
                                compareResult = pob['orderNumber'].localeCompare(poa['orderNumber'])
                                if (compareResult == 0) compareResult = a['etaDate'].localeCompare(b['etaDate'])
                            }
                        })
                    });
            }
            if (sortedBy == "vendorNumber" )
            {
                    a['purchaseOrders'].forEach(poa => 
                    {
                        b['purchaseOrders'].forEach(pob =>
                        {   
                            if(order.value == true)
                            {
                                compareResult = poa.vendorData['vendorCode'].localeCompare(pob.vendorData['vendorCode'])
                            }
                            else
                            {
                                compareResult = pob.vendorData['vendorCode'].localeCompare(poa.vendorData['vendorCode'])
                            }
                        })
                    });
            }
            if (sortedBy == "etaDate" )
            {
                if(order.value == true)
                { 
                    compareResult = a['etaDate'].localeCompare(b['etaDate'])
                }
                else
                {
                    compareResult = b['etaDate'].localeCompare(a['etaDate'])
                }
                
            }
            if (sortedBy == "created" )
            {
                if(order.value == true)
                { 
                    compareResult = a['createdAt'].localeCompare(b['createdAt'])
                }
                else
                {
                    compareResult = b['createdAt'].localeCompare(a['createdAt'])
                }
                
            }
            if (sortedBy == "rating" )
            {
                if(order.value == true)
                { 
                    compareResult = a['rating'] - b['rating']
                }
                else
                {
                    compareResult = b['rating']-a['rating']
                }
                
            }
            if (sortedBy == "status" )
            {
                if(order.value == true)
                { 
                    compareResult = a['statusId']-b['statusId']
                }
                else
                {
                    compareResult = b['statusId']-a['statusId']
                }
                
            }
            if (sortedBy == "isPriority" )
            {
                if(order.value == true)
                { 
                    compareResult = a['isPriority']-b['isPriority']
                }
                else
                {
                    compareResult = b['isPriority']-a['isPriority']
                }
                
            }
            if (sortedBy == "forwarder" )
            {
                if(order.value == true)
                { 
                    compareResult = a['forwarderName'].localeCompare(b['forwarderName'])
                }
                else
                {
                    compareResult = b['forwarderName'].localeCompare(a['forwarderName'])
                }
                
            }
         
            actualSorted.value = sortedBy
            return compareResult
        })
    }

    const handleSearchSubmit = async ()=>{
        pageNo.value = 1
        updateSearchData()
        await loadSearchedDeliveries(searchData.value)
    }
 
    const calculatePageCount = (pageSize, totalCount) => {
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }
    onBeforeMount( async () =>{
        loadWarehouses(1,50)
        loadStatuses(1,50)
    //   await loadDeliveries(pageNo.value,recordsPerPage.value)
        updateSearchData()
        await loadSearchedDeliveries(searchData.value)

    })
    onMounted(() => {
        filterDeliveries("vendor")
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        
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
            updateSearchData()
            await loadSearchedDeliveries(searchData.value)
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
            updateSearchData()
            await loadSearchedDeliveries(searchData.value)
        }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        updateSearchData()
        await loadSearchedDeliveries(searchData.value)
    
        
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        updateSearchData()
        await loadSearchedDeliveries(searchData.value)
    }

    return { handleSearchSubmit, deliveries, searchError, 
             formVendor, formCountry, formDate, formItem, formIsICP, formIsPrio, formWarehouseId, formStatusId, formPurchaseOrder,
             pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage,
             warehouses, warError, statuses, statusError,
             filterDeliveries
             }
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
.search-delivery-form form .field-btn{
    display: flex;
    flex-direction: row;
    margin:0;
}
.search-delivery-form form .field-btn .btn{
    margin: 10px 0;
    padding: 0 4px;
    max-height: 36px;
    min-width: 30px;
}
.search-delivery-form form .field-btn .btn span{
    color: #777;
}
.search-delivery-form form .field-btn .btn:hover{
    background-color: #444;
    }
.search-delivery-form form .field-btn .btn:hover span{
    color: #d6d6d6;
    
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
.search-delivery-form form option{
    color: #ddd;
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
.switches .flipswitch-inner:after {
    content: "";

}
.icpSwich-inner:after {
    content: "";
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