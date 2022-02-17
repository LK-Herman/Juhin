<template>
    <h3>Administrowanie użytkownikami</h3>
    
   
    <div v-if="!error"  class="table-list">
            <div>
               
                <div id="users-header" class="table-container table-header">
                    <div>
                        <p>L.P.</p>
                    </div>
                    <div>
                        <p>UŻYTKOWNIK</p>
                    </div>
                    <div>
                        <p>GOŚĆ</p>
                    </div>
                    <div>
                        <p>MAGAZYNIER</p>
                    </div>
                    <div>
                        <p>SPECJALISTA</p>
                    </div>
                    <div>
                        <p>ADMINISTRATOR</p>
                    </div>
                    <div>
                        <p>USUŃ KONTO</p>
                    </div>
                </div>
                <div v-for="user in usersList" :key="user.emailAddress">
                        <div id="users-container" class="table-container">    
                            <div class="sub-table-list" >
                                <p>1</p>
                            </div>
                            <div>
                                <p>{{user.emailAddress}}</p>
                            </div>
                            <div class="userSwitch">
                                <input type="checkbox" name="userSwitch" class="userSwitch-cb" :id="user.emailAddress + 'guest'" v-model="user.isGuest">
                                <label class="userSwitch-label" :for="user.emailAddress + 'guest'">
                                <div class="userSwitch-inner"></div>
                                <div class="userSwitch-switch"></div>
                                </label>
                            </div>

                           
                            <div class="userSwitch">
                                <input type="checkbox" name="userSwitch" class="userSwitch-cb" :id="user.emailAddress + 'ware'" v-model="user.isWarehouseman">
                                <label class="userSwitch-label" :for="user.emailAddress + 'ware'">
                                <div class="userSwitch-inner"></div>
                                <div class="userSwitch-switch"></div>
                                </label>
                            </div>

                            <div class="userSwitch">
                                <input type="checkbox" name="userSwitch" class="userSwitch-cb" :id="user.emailAddress + 'spec'" v-model="user.isSpecialist">
                                <label class="userSwitch-label" :for="user.emailAddress + 'spec'">
                                <div class="userSwitch-inner"></div>
                                <div class="userSwitch-switch"></div>
                                </label>
                            </div>
                            <div class="userSwitch">
                                <input type="checkbox" name="userSwitch" class="userSwitch-cb" :id="user.emailAddress + 'admin'" v-model="user.isAdmin">
                                <label class="userSwitch-label" :for="user.emailAddress + 'admin'">
                                <div class="userSwitch-inner"></div>
                                <div class="userSwitch-switch"></div>
                                </label>
                            </div>
                            <div>
                                <button id="delUser">Usuń</button>
                               
                            </div>
                        </div>
                        <!-- </router-link> -->
                   
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
      <div v-if="!error">
          <p>{{usersList}}</p>

      </div>
</template>

<script>
import { computed, ref } from '@vue/reactivity'
import { useStore } from 'vuex'
import getUsers from '../composables/getUsers.js'
import getUserById from '../composables/getUserById.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted } from '@vue/runtime-core'
export default {
    setup(){
        const mainUrl = urlHolder  
        const store = useStore()
        const userToken = computed(()=>store.getters.getUserToken)
        // const currentUser = computed(()=>store.getters.getUser)
        const {userData, getUser, email, error:userError} = getUserById(mainUrl,userToken.value)
        const isAdmin = ref()
        const isWarehouseman = ref()
        const isGuest = ref()
        const isSpecialist = ref()

        const {loadUsers, error, usersList, totalRecords, lastPage} = getUsers(mainUrl, userToken.value)
        
        const getUserData = (id)=>{
            getUser(id)
            return email
        }

        onMounted(()=>{
            loadUsers(1,50)
        })

        return {userToken, usersList, error}
    }

}
</script>

<style>
.table-list .table-header#users-header{
    background-color: #175985;
    grid-template-columns: 50px 250px 100px 100px 100px 100px 150px ;
}
.table-container#users-container{
    grid-template-columns: 50px 250px 100px 100px 100px 100px 150px ;
    padding: 10px;
}
.table-container#users-container:active{
    transition: none;
    background-color: transparent;
}
.table-container#users-container:hover{
    transition: none;
    background-color: #252525;
    cursor: inherit;
    
}
.table-container#users-container #delUser{
    max-height: 26px ;
    margin:0;
}

.userSwitch {
    margin: 2px 0;
    position: relative;
    width: 56px;
}
.userSwitch input[type=checkbox] {
  display: none;
}
.userSwitch-label {
  display: block;
  overflow: hidden;
  cursor: pointer;
  border: 0px solid #8C8C8C;
  border-radius: 50px;
}
.userSwitch-inner {
  width: 200%;
  margin-left: -100%;
  transition: margin 0.2s ease-in 0s;
  
}
.userSwitch-inner:before, .userSwitch-inner:after {
  float: left;
  width: 50%;
  height: 20px;
  padding: 0;
  line-height: 20px;
  font-size: 11px;
  color: white;
  /* font-family: Trebuchet, Arial, sans-serif; */
  /* font-weight: bold; */
  box-sizing: border-box;
  
}
.userSwitch-inner:before {
  content: "TAK";
  padding-left: 10px;
  background-color: #4b8819;
  color: #FFFFFF;
  box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.switches .flipswitch-inner:after {
    content: "";

}
.userSwitch-inner:after {
    content: "NIE";
    padding-right: 10px;
    background-color: #636363;
    color: #E8E8E8;
    text-align: right;
    box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.userSwitch-switch {
  width: 20px;
  height: 20px;
  margin: 0px;
  background: #F5B727;
  box-shadow: 1px 1px 3px rgba(4,4,4,0.4), -1px -1px 3px rgba(4,4,4,0.4);
  border-radius: 50px;
  position: absolute;
  top: 0;
  bottom: 0;
  right: 34px;
  transition: all 0.2s ease-in 0s;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-inner {
  margin-left: 0;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-switch {
  right: 0;
}
</style>