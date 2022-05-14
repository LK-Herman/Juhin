<template>
  <div>
      <form id="login-form" @submit.prevent="handleSubmit">
          <h4>JUHIN - SYSTEM ZARZĄDZANIA DOSTAWAMI</h4>
          <h2>Zarejestruj się</h2>

          <label >Adres Email</label>
          <input v-model="email" type="email" required>
          <label for="">Hasło</label>
          <input v-model="password" type="password" required>
          <label for="">Powtórz hasło</label>
          <input v-model="password2" type="password" required>
          <div class="login-links">
              <router-link :to="{name:'Login'}">
                <p>Posiadasz już konto - Zaloguj się</p>
              </router-link>
          </div>
          <div id="captcha">
            <VueRecaptcha
                :sitekey="siteKey"
                :load-recaptcha-script="true"
                @verify="handleSuccess"
                @error="handleError"
                theme="dark"
            ></VueRecaptcha>
          </div>

          <button id="login-btn">Rejestruj</button>
          <div v-if="error">
              <div class="error-msg" v-for="err in error" :key="err">
                  {{err}}
              </div>
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
import { VueRecaptcha } from 'vue-recaptcha';

export default {
    props: [],
    emits:['login-event'],
    components: {VueRecaptcha},
    setup(props, context){
        const mainUrl = urlHolder
        const router = useRouter()
        const email = ref('')
        const password =ref('')
        const password2 =ref('')
        const {getUser, user, error:getError} = getCurrentUser(mainUrl)
        const store = useStore()
        const {login, error} = loginUser(mainUrl)
        var captchaVerificationResult = false
        const siteKey = ref('6LeI_OkfAAAAAJKpXkpAxPqYyobYrwtaqpBeV6N3')
           

        const handleSuccess = (response) => 
        {
            console.log("Captcha verification succeeded")
            captchaVerificationResult = true
        };
        const handleError = () => 
        {
            console.log("Captcha error")
            captchaVerificationResult = false
        };

        const handleSubmit = async () =>
        {
            error.value = null
            if(captchaVerificationResult)
            {
                if(password.value == password2.value)
                {
                    let registerData = {
                        emailAddress : email.value,
                        password : password2.value
                    }

                    await axios.post(mainUrl+"accounts/Create", registerData,{
                        headers: {'Accept':'*/*'}
                        }).then(()=>{
                            login(email.value, password.value)
                                .then(()=>
                                {
                                    if(!error.value)
                                    {
                                        
                                        const userToken = computed(() => store.getters.getUserToken)
                                        getUser(userToken.value)
                                        if(!getError.value)
                                        {   
                                            router.push({name:"Main"})
                                        }
                                    }
                                })
                        }).catch(err => {
                                    if(err.response.data.length>0){
                                        let index = 0
                                        let errors = []
                                        err.response.data.forEach(element => {
                                            errors[index]=element.description
                                            index++
                                        });
                                        error.value = errors
                                    }else{
                                        let errors = [err.message]
                                        error.value = errors
                                    }
                                }
                                
                            )

                }else
                {
                    error.value = ["Hasła się nie zgadzają!"]
                }
            }else
            {
                error.value =["Uzupełnij wszystkie wymagane pola."]
            }  
        } 
        return { handleSubmit, error, email, password, password2, handleError, handleSuccess, siteKey}
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
#login-form #captcha{
    text-align: center;
    margin: 6px auto;
}
#login-form .error-msg{
    font-size: 13px;
    text-align: center;
}
#login-form .error-msg p{
    color: var(--warning);
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