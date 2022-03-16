<template>
<div class="warehouse-view-ctnr">
    <div>
        <h2>Magazyny</h2>
        <div v-if="!error">
            <div v-for="warehouse in warehouses" :key="warehouse.warehouseId">
                <div class="warehouse-ctnr">
                    <div class="icon">
                        <p><span class="material-icons">warehouse</span></p>
                    </div>
                    <div class="labels">
                        <p>OPIS</p>
                    </div>
                    <div class="labels">
                        <p>SKRÓT</p>
                    </div>
                    <div class="labels">
                        <p>POJEMNOŚĆ</p>
                    </div>
                    <div>
                        <p>{{warehouse.description}}</p>
                    </div>
                    <div>
                        <p>{{warehouse.shortName}}</p>
                    </div>
                    <div>
                        <p>{{warehouse.maxPalletsQty}} PAL</p>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <div>
        <h2>Dodaj magazyn</h2>
        <div class="add-warehouse-form">
            <form @submit.prevent="handleSubmit">
                <label>OPIS - NAZWA MAGAZYNU</label>
                <p><span>(Pełna nazwa magazynu)</span></p>
                <input type="text" v-model="descriptionForm">
                <label>SKRÓT</label>
                <p><span>(od 2 do 5 znaków)</span></p>
                <input type="text" v-model="shortNameForm">
                <label>POJEMNOŚĆ</label> 
                <p><span>(maksymalna ilość miejsc paletowych)</span></p>
                <input type="number" min="1" v-model="maxPalletsQtyForm">
                <button>Dodaj</button>
                <div v-if="formError" class="error">
                    <p>{{formError}}</p>
                </div>
            </form>
            
        </div>
    </div>
</div>
<CreatedModal/>  
</template>

<script>
import getWarehouses from '../composables/getWarehouses.js'
import {useStore} from 'vuex'
import { computed, ref } from '@vue/reactivity'
import urlHolder from '../composables/urlHolder.js'
import { onMounted } from '@vue/runtime-core'
import axios from 'axios'
import CreatedModal from '../components/CreatedModal.vue'
export default {
    components:{CreatedModal},
    setup(){
        const store = useStore()
        const userToken = computed(()=>store.getters.getUserToken)
        const mainUrl = urlHolder
        const {loadWarehouses, warehouses, error} = getWarehouses(mainUrl, userToken.value)
        const descriptionForm = ref()
        const shortNameForm = ref()
        const maxPalletsQtyForm = ref(100)
        const formError = ref()

    onMounted(()=>{
        loadWarehouses(1,50)
    })        

    const handleSubmit = async ()=>{
        formError.value = ""
        console.log("submited")
        if (shortNameForm.value.length > 5 || shortNameForm.value.length < 1 )
        {
            formError.value = "Skrót powinien zawierać od 1 do 5 znaków"
        }
        if (maxPalletsQtyForm.value < 1)
        {
            formError.value = "Pojemność magazynu musi być większa od 0"
        }
        if(formError.value == "")
        {
            let warehouseData =
            {
                description : descriptionForm.value,
                shortName : shortNameForm.value,
                maxPalletsQty : maxPalletsQtyForm.value
            }
            var requestOptions = 
            {
            method: 'POST',
            headers: 
                {
                    "Accept":"*/*",
                    'Content-Type':'application/json',
                    "Access-Control-Allow-Origin": "*",
                    "Authorization":"Bearer " + userToken.value
                },
            mode:'cors',
            };
      
            try 
            {
                await axios.post( mainUrl + 'warehouses', warehouseData, requestOptions)
                .then(() =>{ document.getElementById("mycreated-modal").style.display="block"; },
                      reason =>{ resp = reason
                                 throw Error('Coś poszło nie tak.. ')
                                } )
                .finally(()=>{
                                descriptionForm.value = ""
                                shortNameForm.value = ""
                                maxPalletsQtyForm.value = ""
                                loadWarehouses(1,50)
                             })
                
            } 
            catch (err) 
            {
                formError.value = err.message
                console.log(error.value)
            }    
    }
        
    }

        return {warehouses, error, formError, handleSubmit, descriptionForm, shortNameForm, maxPalletsQtyForm}
    }

}
</script>

<style>
.warehouse-view-ctnr{
    display: grid;
    grid-template-columns:1fr 1fr;
    gap: 80px;
}


.warehouse-ctnr{
    display: grid;
    padding: 15px 12px;
    background-color: #444;
    margin: 16px 0;
    grid-template-columns: 70px 180px 60px 80px;
}
.warehouse-ctnr .icon{
    grid-row-start: 1;
    grid-row-end: 3;
    justify-self: center;
    align-self: center;
}
.warehouse-ctnr .icon p span{
    font-size: 40px;
    margin-right: 16px;
    margin-left: 6px;
    color: rgb(253, 194, 1);
    /* transform: translateX(-30px) translateY(20px); */
    
}
.warehouse-ctnr .labels p{
    color: #aaa;
    font-size: 10px;
    margin-bottom: 10px;
}

.add-warehouse-form form{
    margin:16px 0px;
    padding:0 0px;
    box-shadow: none;
    background-color: transparent;
    min-width: 240px;
    max-width: 340px;
}
.add-warehouse-form form p span{
    font-size: 11px;
}
.add-warehouse-form form button{
    margin-top: 24px;
}
.add-warehouse-form form .error p{
    color: rgb(189, 0, 0);
}
</style>