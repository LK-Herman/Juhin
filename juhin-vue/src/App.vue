<template>
<div class="main-container">
    <div class="nav-container">
        <Navbar @logout-event="handleLogout"/>
    </div>
    
    <div :class="{'sub-container':isLogged}">
        <div v-if="isLogged">
            <MenuBar />
        </div>
        <div class="body-container">
            <router-view />
        </div>
    </div>
</div>
   
<div class="downbar">
    <Endbar />
</div>
  
</template>
<script>
import MenuBar from './components/MenuBar.vue'
import Navbar from './components/Navbar.vue'
import Endbar from './components/Endbar.vue'
import { computed, ref } from '@vue/reactivity'
import urlHolder from './composables/urlHolder.js'
import { useRouter } from 'vue-router'
import Login from './views/Login.vue'
import { onBeforeMount, onMounted, watch } from '@vue/runtime-core'
import {useStore} from 'vuex'

export default {
    components: { MenuBar, Navbar, Endbar, Login},
    emits:["login-event", "logout-event", "user"],
    props: [],

    setup(props,context) {
        const isLogged = ref(false)
        const mainUrl = urlHolder
        const userEmail = ref('')
        const router = useRouter()
        const store = useStore()
        
        const handleLogin = (userCred) =>{
            isLogged.value = true
            router.push({name:"Main"})
        }
        const handleLogout = () =>{
            isLogged.value = false
            router.push({name:'Login'})
        }
        
        onBeforeMount(()=>{
            
            if (localStorage['token']){
                isLogged.value = true
                store.commit('setIsLogged',true)
                store.commit('setUserToken', localStorage['token'])
                store.commit('setExpiration', new Date(localStorage['expiration']))
            }else{
                store.commit('setIsLogged',false)
                store.commit('clearUserToken')
                isLogged.value = false
                
            }
        }) 
         watch(store.state,()=>{
            
            let loginCheck  = computed(()=> store.getters.getIsLogged)
            isLogged.value = loginCheck.value

        })
       
    return { isLogged, handleLogin, handleLogout }
  }
}
</script>


<style>

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
