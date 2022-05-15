<template>
  <div>
      <form id="login-form" @submit.prevent="handleSubmit">
            <h4>JUHIN - SYSTEM ZARZĄDZANIA DOSTAWAMI</h4>
          <h2>Zaloguj się</h2>

          <label >Adres Email</label>
          <input v-model="email" type="email" required placeholder="adres@email.pl">
          <label for="">Hasło</label>
          <input v-model="password" type="password" required placeholder="* * * hasło * * *">
          <div class="login-links">
              <router-link :to="{name: 'Register'}">
                <p>Zarejestruj się</p>
              </router-link>
              <router-link :to="{name: 'ResetPassword'}">
                <p>Nie pamiętasz hasła?</p>
              </router-link>
          </div>
           <!-- <div v-if="isPending" class="lds-circle"><div></div></div> -->
          <button id="login-btn">Zaloguj</button>
          <div v-if="error">
              <div class="error-msg">{{error}}</div>
          </div>
      </form>
      <div v-if="isPending">
          <Spinner/>
      </div>
      
  </div>
</template>

<script>
import { computed, ref } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import urlHolder from '../composables/urlHolder.js'
import loginUser from '../composables/loginUser.js'
import getCurrentUser from '../composables/getCurrentUser.js'
import { useStore } from 'vuex'
import Spinner from '../components/Spinner.vue'

export default {
    props: [],
    
    emits:['login-event'],
    components:{Spinner},
    setup(props, context){
        const mainUrl = urlHolder
        const router = useRouter()
        const email = ref('')
        const password =ref('')
        const {getUser, user, error:getError} = getCurrentUser(mainUrl)
        const store = useStore()
        const {login, error, isPending} = loginUser(mainUrl)
        
        const handleSubmit = async () =>
        {
            await login(email.value, password.value)
                .then(()=>
                {
                    if(!error.value)
                    {
                        
                        const userToken = computed(() => store.getters.getUserToken)
                        getUser(userToken.value)
                        if(!getError.value)
                        {   
                            router.push({name:"Main"})
                            console.log('pushed to main')
                        }
                        // {
                        //     context.emit('login-event', {email: email.value, token:userToken.value, user:user.value })
                        // }
                    }

                })
            
        } 
        
        return { handleSubmit, error, email, password, isPending}
    }
}
</script>

<style>

#login-form{
    display: flex;
    flex-flow: column;
    background-image: linear-gradient(#282828, var(--back-grey), var(--back-grey2));
    max-width: 340px;
}
#login-form .error-msg{
    color: var(--warning);
    font-size: 12px;
    text-align: center;
}
#login-form h4{
   
    font-size: 0.8em;
    
    align-self: center;
    margin:5px 0 5px 0;
}
#login-form h2{
    font-weight: 400;
    align-self: center;
    margin:15px 0 25px 0;
}

#login-form .login-links{
    margin: 10px 0;
    display: flex;
    flex-flow: row;
    justify-content: space-between;
}
#login-form #login-btn{
    margin: 20px 5px;
    width: 40%;
    align-self: center;
    box-shadow: 2px 2px 4px rgba(10,10,10, 0.5);
}
#login-form #login-btn:hover{
    box-shadow: 0px 0px 0px rgba(10, 10, 10, 0.5);
    background-color: #6D8CDC;
}
</style>