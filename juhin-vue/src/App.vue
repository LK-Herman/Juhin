<template>
<div class="main-container">
    <div class="nav-container">
        <Navbar :isLogged="isLogged" @logout-event="handleLogout"/>
    </div>
    
    <div :class="{'sub-container':isLogged}">
        <div v-if="isLogged">
            <MenuBar />
        </div>
        <div class="body-container">
            <div v-if="!isLogged">
                <Login  @login-event="handleLogin" />
            </div>
            <div v-else>
                <router-view />
            </div>
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
import { ref } from '@vue/reactivity'
import urlHolder from './composables/urlHolder.js'
import { useRouter } from 'vue-router'
import Login from './views/Login.vue'
import { onBeforeMount, onMounted, watch } from '@vue/runtime-core'
import { locales } from 'moment'

export default {
    components: { MenuBar, Navbar, Endbar, Login},
    emits:["login-event", "logout-event", "user"],
    props: [],

    setup(props,context) {
        const isLogged = ref(false)
        const mainUrl = urlHolder
        const userEmail = ref('')
        const router = useRouter()
        const userToken = ref('')
        const user = ref(null)
        
        const handleLogin = (userCred) =>{
            isLogged.value = true
            localStorage.user = JSON.stringify(userCred.user)
            userToken.value = localStorage.token
            user.value = localStorage.user
            router.push({name:"Main"})
        }
        const handleLogout = () =>{
            isLogged.value = false
            router.push({name:'Main'})
        }
      
        onMounted(()=>{
            let actualDate = new Date()
            let storageDate = new Date(localStorage.expiration)
                
            if (localStorage.token !== '' && storageDate > actualDate ){
                isLogged.value= true
                userToken.value = localStorage.token
            }
        }) 
        watch(router, ()=>{
            let actualDate = new Date()
            let storageDate = new Date(localStorage.expiration)
                
            if (localStorage.token == '' && storageDate < actualDate ){
                isLogged.value= false
            }
        })
       
    return { isLogged, handleLogin, handleLogout, user, userEmail, userToken}
  }
}
</script>


<style>

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
