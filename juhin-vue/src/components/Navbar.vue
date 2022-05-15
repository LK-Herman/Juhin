<template>
<div class="navbar">
    <div class="logo">
        <p> 
        <router-link :to="{name:'Main'}"> JUHIN </router-link>
        <img id="helmet" src="../assets/images/helmet4.png" alt=""></p>
    </div>
    <div class="logobar-out">
        <div class="logobar-in">
            <div>
                <p id="sublogo"> SYSTEM ZARZĄDZANIA DOSTAWAMI </p>
            </div>
            <div class="logobar-links">
                
                <p id="yellowfont" v-if="userRolePL" class="links-item">{{userRolePL}} </p>
                <p id="yellowfont" v-if="isLogged && userRolePL.includes('Administrator')" class="links-item"><span class="material-icons md-48">manage_accounts</span></p>
                <p id="yellowfont" v-if="isLogged && !userRolePL.includes('Administrator')" class="links-item"><span class="material-icons md-48">person</span></p>
                <p v-if="isLogged" class="links-item" id="email-address">{{email}}</p>
                <router-link :to="{name: 'Register'}">
                    <p v-if="!isLogged" class="links-item">Zarejestruj się</p>
                </router-link>
                
                <a v-if="isLogged" class="links-item" @click="handleNavLogout">Wyloguj</a>
                <router-link v-else :to="{name:'Login'}" class="links-item">Zaloguj się</router-link>
            </div>
        </div>
    </div>
        
</div>
</template>

<script>
import logoutUser from '../composables/logoutUser.js'
import { useRouter } from 'vue-router'
import { computed, ref } from '@vue/reactivity'
import { onMounted, watch, watchEffect } from '@vue/runtime-core'
import urlHolder from '../composables/urlHolder'
import {useStore} from 'vuex'
import getCurrentUser from '../composables/getCurrentUser.js'
export default {
    
    emits:["logout-event","login-event","user"],
    setup(props, context){
        const router = useRouter()
        const mainUrl = urlHolder
        const store = useStore()
        const user = ref(null)
        const userRolePL = ref('')
        const email = ref('')
        const isLogged = ref(false)
        const {logout, error, logoutData} = logoutUser(mainUrl)
        const {getUser, error:getError} = getCurrentUser(mainUrl)

        
        const handleNavLogout = async () =>
        {
            await logout()
            .then(()=>
            {
                context.emit('logout-event')
                isLogged.value = false
                router.push({name:'Login'})
                email.value = ''
                userRolePL.value = ''
                store.commit('setIsLogged', false)
            })
        }
        onMounted(async ()=>
        {
            const isLogged = computed(()=> store.getters.getIsLogged)
            if(isLogged.value || window.location.pathname == '/reset' || window.location.pathname == '/resetpasswordconfirmation')
            {
                const us = computed(()=> store.getters.getUser)
                user.value = us.value
                if(user.value['userId'])
                {
                    email.value = user.value.emailAddress
                    if(user.value.isGuest) userRolePL.value = "Gość"
                    if(user.value.isWarehouseman) userRolePL.value = "Magazynier"
                    if(user.value.isSpecialist) userRolePL.value = "Logistyk"
                    if(user.value.isAdmin) userRolePL.value = "Administrator"
                    if(user.value.isSpecialist && user.value.isAdmin) userRolePL.value = "Logistyk i Administrator"
                }
                else
                {
                    const userToken = computed(()=> store.getters.getUserToken)
                    
                    await getUser(userToken.value)
                    if(!getError)
                    {
                        email.value = user.value.emailAddress
                        userRolePL.value = user.value.userRole
                    }
                }
            }else
            {   console.log(window.location.pathname)
                console.log(router.currentRoute.value)
                router.push({name:'Login'})   
            }
        })
        watch(store.state,()=>{
            
            let loginCheck = computed(()=> store.getters.getIsLogged)
            isLogged.value = loginCheck.value
            const us = computed(()=> store.getters.getUser)
            user.value = us.value
            if(user.value){
                email.value = user.value.emailAddress
                if(user.value.isGuest) userRolePL.value = "Gość"
                if(user.value.isWarehouseman) userRolePL.value = "Magazynier"
                if(user.value.isSpecialist) userRolePL.value = "Logistyk"
                if(user.value.isAdmin) userRolePL.value = "Administrator"
                if(user.value.isSpecialist && user.value.isAdmin) userRolePL.value = "Logistyk i Administrator"
            }
        })
        return {  handleNavLogout, userRolePL, email,isLogged, user}
    }

}
</script>

<style>
.navbar{
    display: grid;
    grid-template-rows: 60px 60px;
    column-gap: 0;
}
.navbar #helmet{
    position: absolute;
    margin: 0;
    width: 90px;
}

.navbar .logo
{
    z-index: 10;
    padding: 0px 0 0 18px;
    
}
.navbar .logo a{
    font-family: 'Amaranth', sans-serif;
    font-size: 61px;
    z-index: 11;
}
.navbar .logobar-out{
    display: grid;
    align-self: center;
    height: 60px;
    background-color: #2463b4;
    background-color: #536BAA ;
}

.navbar .logobar-in{
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 10px 0px;
    background-color: #2463b4;
    background-color: #536BAA ;
}
.navbar .logobar-in .logobar-links{
    display: flex;
    align-items: center;
    padding-right: 36px;
}
.navbar .logobar-in .logobar-links .links-item{
    padding-left: 36px;
    font-size: 14px;
    font-weight: 500;
    cursor: pointer;
}
.navbar .logobar-in .logobar-links .links-item#email-address{
    cursor: default;
}
.navbar .logobar-in .logobar-links .links-item#email-address:hover{
    cursor: default;
    color: var(--primary);
}
.navbar .logobar-in .logobar-links .links-item:hover{
    color: rgb(255, 182, 25);
}
.logobar-links .links-item#yellowfont span{
    font-size: 30px;
}
.logobar-links p.links-item#yellowfont:hover{
    color: rgb(218, 218, 218);
}
#sublogo{
    font-family: 'Roboto Condensed', sans-serif;
    font-weight: 400;
    font-size: 16px;
    padding-left: 24px;
}
.material-icons{
    vertical-align: middle;
    
}

</style>