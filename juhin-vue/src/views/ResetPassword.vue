<template>
  <div>
      <form id="login-form" @submit.prevent="handleSubmit">
            <h4 id="rem-header">JUHIN - SYSTEM ZARZĄDZANIA DOSTAWAMI</h4>
            <p id="dontremember"><span class="material-icons">help</span></p>
            <h2 id="rem-header">Nie pamiętasz hasła?</h2>
            <p id="rem-desc">Na Twój adres email zostanie wysłany link aktywacyjny, który przeniesie Cię do formularza zmiany hasła.</p>
            <label >Adres Email</label>
            <input v-model="email" type="email" required placeholder="adres@email.pl">
            <div id="captcha">
                <VueRecaptcha
                    :sitekey="siteKey"
                    :load-recaptcha-script="true"
                    @verify="handleSuccess"
                    @error="handleError"
                    theme="dark"
                ></VueRecaptcha>
            </div>
            <button id="login-btn">Wyślij</button>
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
import { VueRecaptcha } from 'vue-recaptcha';
export default {
    props: [],
     components: {VueRecaptcha},
    emits:['login-event'],
    setup(props, context){
        const mainUrl = urlHolder
        const router = useRouter()
        const email = ref('')
        const userId = ref('')
        const store = useStore()
        const error = ref()
        const message = ref()
         var captchaVerificationResult = false
        const siteKey = ref('6LeI_OkfAAAAAJKpXkpAxPqYyobYrwtaqpBeV6N3')
           
        const handleSuccess = (response) => {
        console.log("Captcha verification succeeded")
        captchaVerificationResult = true
        };

        const handleError = () => {
        console.log("Captcha error")
        captchaVerificationResult = false
        };

        const handleSubmit = async () =>
        {
            if(captchaVerificationResult){
                axios.get(mainUrl+'accounts/User/byemail/' + email.value, {
                    headers: { 'Accept':'*/*'}})
                     .then(resp => 
                         {
                             userId.value = resp.data.userId
                             console.log(userId.value)
                             let url = window.location.href + 'passwordconfirmation'
                             console.log(url)
                             if(userId.value != ''){
                                 axios.post(mainUrl+'accounts/ResetTokenRequest?userId=' + userId.value + '&url='+ url)
                                 .then(res => 
                                     {
                                         console.log('Email send')
                                         message.value = "Link został wysłany na adres "+email.value
                                         console.log(res.data)
                                         
                                     })  
                                 .catch(err => error.value = err.message)
                             }
                    })
                .catch(err => error.value = err.message)
            }else
            {
                error.value = "Nie wszystkie wymagane pola zostały uzupełnione"
            }
            
        } 
        
        return { handleSubmit, error, email, message, handleError, handleSuccess, siteKey}
    }
}
</script>

<style>
form #dontremember{
    text-align: center;
}
form h2#rem-header{
    margin-bottom: 20px;
    padding: 0;
}
form p#rem-desc{
    margin-bottom: 35px;
    text-align: center;
}
form h4#rem-header{
    margin-bottom:20px;
}
form #dontremember span{
    color: #faaf00;
    font-size: 60px;
    margin: 0;

}


</style>