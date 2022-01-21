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
                <p id="sublogo"> WAREHOUSE MANAGEMENT </p>
            </div>
            <div class="logobar-links">
                
                <p v-if="userRolePL" class="links-item" id="email-address">{{userRolePL}} </p>
                <p v-if="isLogged" class="links-item" ><span class="material-icons md-48">manage_accounts</span></p>
                <p v-if="isLogged" class="links-item" id="email-address">{{email}}</p>
                <p v-if="!isLogged" class="links-item">Zarejestruj się</p>
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
import { ref } from '@vue/reactivity'
import { onMounted, watchEffect } from '@vue/runtime-core'
export default {
    props:['isLogged'],
    emits:["logout-event","login-event","user"],
    setup(props, context){
        const router = useRouter()
        
        const user = JSON.parse( localStorage.user )
        const userToken = localStorage.token

        const userRolePL = ref('')
        const email = ref('')
        const {logout, error, logoutData} = logoutUser()
        
        const handleNavLogout = async () =>{
            await logout()
            context.emit('logout-event')
            router.push({name:'Main'})
        }
    onMounted(()=>{
        if(props.isLogged){
            email.value = localStorage.email
            userRolePL.value = user.role
        }
    })         
    watchEffect(()=>{
        
        if(userRolePL.value){
            userRolePL.value = localStorage.userRole
            // console.log(userRolePL.value)
            switch (userRolePL.value) {
            case 'Admin':
                userRolePL.value = 'Administrator'
                break;
            case 'Specialist':
                userRolePL.value = 'Specjalista'
                break;
            case 'Warehouseman':
                userRolePL.value = 'Magazynier'
                break;
            case 'Guest':
                userRolePL.value = 'Gość'
                break;
            }
        }
        if (!props.isLogged){
            userRolePL.value = ''
        }
        
    })

        return {  handleNavLogout, userRolePL, email, user}
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
#sublogo{
    font-family: 'Amaranth', sans-serif;
    font-size: 16px;
    padding-left: 36px;
}
.material-icons{
    vertical-align: middle;
    
}

</style>