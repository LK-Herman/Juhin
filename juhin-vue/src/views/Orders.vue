<template>
  <h2>Zamówienia</h2>
  <br>
  <div v-if="!error"  class="table-list">
            <div>
                <div id="orders-header" class="table-container table-header">
                    <div>
                        <p>NR ZAMÓWIENIA</p>
                    </div>
                    <div>
                        <p>SKRÓT</p>
                    </div>
                    <div>
                        <p>MOJE</p>
                    </div>
                    <div>
                        <p>DOSTAWCA</p>
                    </div>
                    
                    <div class="last-col">
                        <p></p>
                    </div>
                </div>
                <div v-for="order in orders" :key="order.orderId" >
                    
                        <div @click="handleLoadDeliveries(order.orderId)" id="orders-container" class="table-container">    
                            <div class="sub-table-list" >
                                <p>{{order.orderNumber}}</p>
                            </div>
                            <div>
                                <p>{{order.vendorShortName}}</p>
                            </div>
                            <div v-if="user.userId == order.userId">
                                <p><span class="material-icons">account_circle</span></p>
                            </div>
                            <div v-else>
                                <p ><span class="material-icons icon-group">group</span></p>
                            </div>
                            <div>
                                <p>{{order.vendorName}}</p>
                            </div>
                            <div class="last-col">
                                <router-link :to="{name:'DeliveryAdd', params:{
                                            vId:order.vendorId, 
                                            vendorName:order.vendorName, 
                                            orderNo:order.orderNumber,
                                            orderId:order.orderId}}" class="btn sub-btn">Dodaj dostawę
                                </router-link>
                            </div>
                        </div>

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
                        <span :class="{'active-page-no' : pageNo==page}">{{page}}</span>
                    </div>
                    <div> | </div>
                    <div v-if="lastPage>=pageNo" @click="handleGoToPage(lastPage)"> {{lastPage}}</div>
                    
                </div>
                <button v-if="pageNo<lastPage" @click="handleNextPage">dalej<span class="material-icons">keyboard_double_arrow_right</span></button>
                <button v-else >koniec<span class="material-icons">keyboard_double_arrow_right</span></button>
          </div>

      </div>
      <br>
      <div>
          <button @click="handleBack">Powrót</button>
      </div>
</template>

<script>
import { computed, onMounted, ref, watch, watchEffect } from '@vue/runtime-core'
import getOrders from '../composables/getOrders.js'
import urlHolder from '../composables/urlHolder.js'
import { useRouter } from 'vue-router'
import axios from 'axios'
import mainUrl from '../composables/urlHolder.js'
import {useStore} from 'vuex'

export default {
  components: {  },
  props:[],
  setup(props) {
    const router = useRouter()
    const url = urlHolder
    const store = useStore()
    const user = computed(()=> store.getters.getUser)
    const userToken = computed(()=> store.getters.getUserToken)

    const {orders, error, loadOrders, totalRecords} = getOrders(url, userToken.value)
    const pageNo = ref(1)
    const recordsPerPage = ref(10)
    const lastPage = ref(1)
     let counter = 1
    
    const calculatePageCount = (pageSize, totalCount) => {
        
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }

    onMounted(async () => {
        
        
        counter = 1
      await loadOrders(pageNo.value,recordsPerPage.value)
            .then(function(){
                  orders.value.forEach(item =>{item['counter']=counter++})  
            })
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    watch((orders), async () =>{
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            counter = 1
            await loadOrders(pageNo.value, recordsPerPage.value)
                .then(function(){
                    orders.value.forEach(item =>{item['counter']=counter++})  
                })
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
            counter = 1
        await loadOrders(pageNo.value, recordsPerPage.value)
            .then(function(){
                  orders.value.forEach(item =>{item['counter']=counter++})  
            })
    }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        counter = 1
        await loadOrders(pageNo.value, recordsPerPage.value)
            .then(function(){
                  orders.value.forEach(item =>{item['counter']=counter++})  
            })
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        counter = 1
        await loadOrders(pageNo.value, recordsPerPage.value)
        .then(function(){
                  orders.value.forEach(item =>{item['counter']=counter++})  
            })
    }

    const handleBack = () =>{
        router.back
    }
    const deliveries = ref(null)
    
    const handleLoadDeliveries = async (orderId) =>{
        await axios.get(mainUrl + "deliveries/byorder/"+ orderId,{
            'Accept':'*/*',
            'Authorization':'Bearer ' + userToken.value
        }).then(resp => deliveries.value = resp.data)

    }

    return {handleBack, user, deliveries, handleLoadDeliveries, orders, error, pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage }
  }
}
</script>

<style>
.table-orders{
    max-width: 800px;
}
.table-list .table-header#orders-header{
    background-color: #491980;
    grid-template-columns: 110px 130px 80px 300px  1fr;

}
.table-container#orders-container{
    grid-template-columns: 110px 130px 80px 300px  1fr;
    
}
.table-container .icon-group{
    color: rgb(97, 97, 97);
}
.table-list .dropdown{
    position: relative;
    /* display: inline-block; */
    
}

</style>