<template>

    <div v-if="!error"  class="table-list">
            <div>
                <div class="page-header">
                    <h2>Lista dostawców</h2>

                    <form class="search-form" @submit.prevent="handleSearch">
                        <div class="search-container">
                            <div>
                                <label>Wyszukaj dostawcę:</label>
                                <input type="search" v-model="searchedName">
                            </div>
                            <div>
                                <button>Szukaj</button>
                            </div>
                        </div>
                    </form>
                </div>
                <div id="vendors-header" class="table-container table-header">
                    <div>
                        <p>KOD DOSTAWCY</p>
                    </div>
                    <div>
                        <p>SKRÓT</p>
                    </div>
                    <div>
                        <p>NAZWA</p>
                    </div>
                    <div>
                        <p>ADRES</p>
                    </div>
                    <div>
                        <p>KRAJ</p>
                    </div>
                  
                   
                    
                </div>
                <div v-for="vendor in vendors" :key="vendor.vendorId" >
                    <div @click="handleClick(vendor)">
                        <!-- <router-link :to="{name:'VendorDetails', params:{vId:vendor.vendorId}}" :userToken="userToken" :user="user"> -->
                        <div id="vendors-container" class="table-container">    
                            <div class="sub-table-list" >
                                <p>{{vendor.vendorCode}}</p>
                            </div>
                            <div>
                                <p>{{vendor.shortName}}</p>
                            </div>
                            <div>
                                <p>{{vendor.name}}</p>
                            </div>
                            <div>
                                <p>{{vendor.address}}</p>
                            </div>
                            <div>
                                <p>{{vendor.country}}</p>
                            </div>
                        </div>
                        <!-- </router-link> -->
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
      <CreatedModal/>  

</template>

<script>
import getVendors from '../composables/getVendors.js'
import urlHolder from '../composables/urlHolder.js'
import { computed, onMounted, onUpdated, ref, watch, watchEffect } from '@vue/runtime-core'
import {useRouter} from 'vue-router'
import CreatedModal from '../components/CreatedModal.vue'
import { useStore } from 'vuex'

export default {
  components: { CreatedModal },
  
  setup() {
    const url = urlHolder
    const store = useStore()

    const user = computed(()=>store.getters.getUser)
    const userToken = computed(()=>store.getters.getUserToken)

    const {vendors, error, loadVendors, totalRecords} = getVendors(url, userToken.value)
    const pageNo = ref(1)
    const recordsPerPage = ref(10)
    const lastPage = ref(1)
    const searchedName = ref('')
    const router = useRouter()
    
    const handleSearch =async () =>{
        recordsPerPage.value=10
        await loadVendors(pageNo.value, recordsPerPage.value ,searchedName.value)
    }

    const calculatePageCount = (pageSize, totalCount) => {
        
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }
    const handleClick = (vendor) =>{
        router.push({ name:'VendorDetails', 
                      params:{ vId:vendor.vendorId } 
        })
    }

    onMounted(async () => {
      await loadVendors(pageNo.value,recordsPerPage.value, searchedName.value)
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    watch((vendors), async () =>{
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            await loadVendors(pageNo.value, recordsPerPage.value, searchedName.value)
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
        await loadVendors(pageNo.value, recordsPerPage.value, searchedName.value)
    }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        await loadVendors(pageNo.value, recordsPerPage.value, searchedName.value)
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        await loadVendors(pageNo.value, recordsPerPage.value, searchedName.value)
    }

    return { 
            user,
            vendors, 
            error, 
            pageNo, 
            recordsPerPage, 
            handleNextPage, 
            handlePreviousPage, 
            handlePages, 
            handleGoToPage, 
            lastPage, 
            handleSearch, 
            searchedName, 
            handleClick 
            }
  }
}
</script>
<style >
.page-header{
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-right: 15px;
}
.table-list .table-header#vendors-header{
    background-color: #2e570c;
    grid-template-columns: 100px 100px 300px 350px 180px ;
}
.table-container#vendors-container{
    grid-template-columns: 100px 100px 300px 350px 180px ;
}
.table-container .icon-group{
    color: rgb(97, 97, 97);
}

.search-container{
    display: flex;
    align-items: flex-end;
}
.search-container div{
    margin: 0;

}
.search-container input{
    width: 300px;
    height: 40px;
    margin:15px 15px 15px 0px;
}
.search-container button{
    margin:15px 15px 15px 0px;
    height: 40px;
}
.search-container button:hover{
    background-color: var(--back-grey);
}
.search-form{
    padding: 10px 0;
    background-color: transparent;
    box-shadow: none;
    margin:0;
}
</style>>

