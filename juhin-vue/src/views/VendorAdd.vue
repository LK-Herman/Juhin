<template>
  <div>
      <form @submit.prevent="handleSubmit" class="vendor-form">
            <div id="vendor-addform-header">
                <img src="../assets/images/vendorIcon.png" alt="">
                <h2>Dodaj dostawcę</h2>
            </div>
            <div v-if="error" class="error">
                <p>{{error}}</p>
            </div>
            <label>Kod dostawcy</label>
            <input type="text" v-model="formVendorCode">
            <div v-if="responseErrors && responseErrors.hasOwnProperty('VendorCode')" class="error">
                <p>{{responseErrors.VendorCode[0]}}</p>
            </div>
            <label>Nazwa (forma krótka) </label>
            <input type="text" v-model="formShortName" >
            <div v-if="responseErrors && responseErrors.hasOwnProperty('ShortName')" class="error">
                <p>{{responseErrors.ShortName[0]}}</p>
            </div>
            <label>Nazwa pełna</label>
            <input type="text" v-model="formName">
            <div v-if="responseErrors && responseErrors.hasOwnProperty('Name')" class="error">
                <p>{{responseErrors.Name[0]}}</p>
            </div>
            <label>Adres</label>
            <input type="text" v-model="formAddress">
            <div v-if="responseErrors && responseErrors.hasOwnProperty('Address')" class="error">
                <p>{{responseErrors.Address[0]}}</p>
            </div>
            <label>Kraj</label>
            <input type="text" v-model="formCountry">
            <div v-if="responseErrors && responseErrors.hasOwnProperty('Country')" class="error">
                <p>{{responseErrors.Country[0]}}</p>
            </div>
            <label>Adres email</label>
            <input type="email" v-model="formEmail">
            <label>Numer telefonu</label>
            <input type="tel" v-model="formPhoneNumber">
           
            <div class="submit">
                <button>Dodaj</button>
            </div>
      </form>
  </div>

  <CreatedModal/>
</template>

<script>
import { ref } from '@vue/reactivity'
import addVendor from '../composables/addVendor.js'
import urlHolder from '../composables/urlHolder.js'
import { onUpdated, watch } from '@vue/runtime-core'
import CreatedModal from '../components/CreatedModal.vue'
export default {
    components:{CreatedModal},
    setup(){
        
        const mainUrl = urlHolder

        const user = localStorage.getItem('user')
        const userToken = localStorage.getItem('token')

        const formVendorCode = ref('')
        const formShortName = ref('')
        const formName = ref('')
        const formAddress = ref('')
        const formCountry = ref('')
        const formEmail = ref('')
        const formPhoneNumber = ref('')
        

        const {addNewVendor, error, responseErrors} = addVendor(mainUrl, userToken)

        const handleSubmit = async () =>{
                responseErrors.value = null
                error.value = null
            const vendorData = {
                VendorCode : formVendorCode.value,
                ShortName : formShortName.value,
                Name : formName.value,
                Address : formAddress.value,
                Country : formCountry.value,
                Email : formEmail.value,
                PhoneNumber : formPhoneNumber.value,
                IsActive : true
            }
            
            await addNewVendor(vendorData)
            if(!responseErrors.value && !error.value){
                document.getElementById("mycreated-modal").style.display="block"
                formVendorCode.value = ''
                formShortName.value =''
                formName.value = ''
                formAddress.value = ''
                formCountry.value = ''
                formEmail.value = ''
                formPhoneNumber.value = ''
            }
        }

        return {handleSubmit, formVendorCode, formShortName, formName, formAddress, formCountry, formEmail, formPhoneNumber, error, responseErrors}
    }

}
</script>
<style>
input[type='checkbox']{
    display: inline-block;
    width: 16px;
    position: relative;
    margin: 0 10px 0 0;
    top: 2px;
}

.vendor-form h2{
    text-align: center;
}
.vendor-form{
    max-width: 600px;
}
    .vendor-form .error p{
        font-size: 1.2em;
        color: var(--warning);
        text-align: center;
    }

.vendor-form #vendor-addform-header{
    text-align: center;
    background-color: var(--green);
    padding: 15px;
    margin: 0 0 15px 0  ;
}
</style>