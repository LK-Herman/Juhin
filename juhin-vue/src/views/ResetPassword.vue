<template>
  <div>
      <form id="login-form" @submit.prevent="handleSubmit">
            <h4>JUHIN - SYSTEM ZARZĄDZANIA DOSTAWAMI</h4>
          <h2>Zresetuj hasło</h2>

          <label >Adres Email</label>
          <input v-model="email" type="email" required placeholder="adres@email.pl">
          
          <button id="login-btn">Reset</button>
          <div v-if="error">
              <div class="error-msg">{{error}}</div>
          </div>
          <div v-if="message" class="message-msg">
              <p >{{message}}</p>
          </div>
      </form>
      
  </div>
</template>

<script>
import { computed, ref } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import urlHolder from '../composables/urlHolder.js'
import loginUser from '../composables/loginUser.js'
import getCurrentUser from '../composables/getCurrentUser.js'
import { useStore } from 'vuex'
import axios from 'axios'

export default {
    props: [],
    emits:['login-event'],
    setup(props, context){
        const mainUrl = urlHolder
        const router = useRouter()
        const email = ref('')
        const userId = ref('')
        const store = useStore()
        const error = ref()
        const message = ref()
        
        const handleSubmit = async () =>
        {
           axios.get(mainUrl+'accounts/User/byemail/' + email.value, {
               headers: { 'Accept':'*/*'}})
                .then(resp => 
                    {
                        userId.value = resp.data.userId
                        console.log(userId.value)
           
                        if(userId.value != ''){
                            axios.post(mainUrl+'accounts/ResetTokenRequest?userId=' + userId.value)
                            .then(res => 
                                {
                                    console.log('Email send')
                                    message.value = res.data
                                })
                            .catch(err => error.value = err.message)
                        }
               })
           .catch(err => error.value = err.message)
            
        } 
        
        return { handleSubmit, error, email, message}
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
#login-form .message-msg p{
    text-align: center;
    font-size: 16px;
}
#login-form h4{
    font-family: 'Amaranth', sans-serif;
    font-size: 0.8em;
    font-weight: 300;
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