<template>

     <div v-if="!venError" class="vendor-details-container">
        <div class="item-short">
            <h4>SKRÓT</h4>
            <h2>{{vendor.shortName}}</h2>
        </div>    
        <div class="item-head">
            <div class="vendor-header">
                <img src="../assets/images/vendorIcon.png" alt="" />
                <div>
                    <h4>PEŁNA NAZWA DOSTAWCY</h4>
                    <h2>{{vendor.name}}</h2>
                </div>
            </div>
        </div>
    </div>

    <div v-if="!error && isLoaded && !isEditing" class="item-container">
        <div class="item-label-first">
            <h4>NAZWA:</h4>
        </div>
        <div class="item-value-first">
            <h2>{{item.name}}</h2>
        </div>
        <div class="item-label-second">
            <h4>NR REWIZJI:</h4>
        </div>
        <div class="item-value-second">
            <p>{{item.revisionNumber}}</p>
        </div>
        <div class="item-label-first">
            <h4>OPIS:</h4>
        </div>
        <div class="item-value-first">
            <p>{{item.description}}</p>
        </div>
        <div class="item-label-second">
            <h4>CENA:</h4>
        </div>
        <div v-if="!curError" class="item-value-second">
            <p>{{item.price}} {{currency.code}}</p>
        </div>
        <div class="item-label-first">
            <h4>PODLEGA WSK</h4>
        </div>
        <div class="item-value-first">
            <p>{{item.isICP?"TAK":"NIE"}}</p>
        </div>
        <div class="item-label-second">
            <h4>MAKS. ILOŚĆ NA PALECIE ORYGINALNEJ</h4>
        </div>
        <div class="item-value-second">
            <p>{{item.palletQty}}</p>
        </div>
        <div class="item-label-first">
            <h4>KRAJ POCHODZENIA</h4>
        </div>
        <div class="item-value-first">
            <p>{{item.countryOfOrigin}}</p>
        </div>

        <div class="item-label-second">
            <h4>MAKSYMALNA ILOŚĆ NA PALECIE EURO</h4>
        </div>
        <div class="item-value-second">
            <p>{{item.maxEuroPalQty}}</p>
        </div>
        <div class="item-label-first">
            <h4>JEDNOSTKA</h4>
        </div>
        <div v-if="!uniError" class="item-value-first">
            <p>{{unit['shortName']}} / {{unit['name']}}</p>
        </div>
        <div class="item-label-second">
            <h4>AKTYWNA</h4>
        </div>
        <div class="item-value-second">
            <p>{{item.isActive?"TAK":"NIE" }}</p>
        </div>
        <div class="item-label-first">
            <h4>PALETA</h4>
        </div>
        <div class="item-value-first">
            <p>{{pallet.name}}</p>
        </div>
        <div class="item-label-second">
            <h4>WYMIARY PALETY</h4>
        </div>
        <div class="item-value-second">
            <p> {{pallet.xdimension}} / {{pallet.ydimension}}</p>
        </div>
        <div class="item-label-first">
            <h4>KOD HS:</h4>
        </div>
        <div class="item-value-first">
            <p>{{item.hsCode}}</p>
        </div>
        <div class="item-label-second">
            <h4>MAGAZYN PRZEZNACZENIA</h4>
        </div>
        <div v-if="!warError" class="item-value-second">
            <p>{{warehouse.shortName}} / {{warehouse.description}}</p>
        </div>
        <div class="item-label-first">
            <h4>OPIS KODU HS:</h4>
        </div>
        <div class="item-value-first">
            <p>{{item.hsCodeDescription}}</p>
        </div>
        
         <div class="item-label-second">
            <h4></h4>
        </div>
        <div class="item-value-second">
            <p> </p>
        </div>
        <div class="item-buttons">
            <button @click="handleBack" ><span class="material-icons">keyboard_backspace</span> Powrót</button>
            <button class="edit-btn" @click="handleEdit">Edytuj</button>
            <button class="delete-btn">Usuń</button>
        </div>
    </div>

    <div v-if="isEditing" class="edit-item-form-container">
        <form @submit.prevent="handleSubmit">
            <div class="triple314">
                <div>
                    <label>NAZWA</label>
                    <input type="text" v-model="formName" required>
                </div>
                <div>
                    <label>NR REWIZJI</label>
                    <input type="number" v-model="formRevision" required>
                </div>
                <div>
                    <label>OPIS</label>
                    <input type="text" v-model="formDescription" required>
                </div>
            </div>
            <div class="four2111">
                <div>
                    <label>KRAJ POCHODZENIA</label>
                    <input type="text" v-model="formCountry" required>
                </div>
                <div v-if="!uniListError">
                    <label>JEDNOSTKA</label>
                    <select v-model="formUnitId" required>
                        <option v-for="uni in units" :key="uni.unitId" :value="uni.unitId">{{uni.name}}</option>
                    </select>
                   
                </div>
                <div>
                    <label>CENA</label>
                    <input type="number" v-model="formPrice">
                </div>
                <div>
                    <label>WALUTA</label>
                    <select v-model="formCurrencyId">
                        <option v-for="curr in currencyList" :key="curr.currencyId" :value="curr.currencyId">{{curr.code}}</option>
                    </select>
                    
                </div>

            </div>
            <div class="double">
                <div class="checkboxes">
                    <input type="checkbox" checked v-model="formIsActive">
                    <label>CZY AKTYWNA?</label>
                </div>
                <div class="checkboxes">
                    <input type="checkbox"  v-model="formIsICP">
                    <label>CZY PODLEGA POD WSK?</label>
                </div>
            </div>
            <div class="double12">
                <div>
                    <label>KOD HS</label>
                    <input type="text" v-model="formHSnumber">
                </div>
                <div>
                    <label>OPIS KODU HS</label>
                    <input type="text" v-model="formHSdescription">
                </div>
            </div>
            <div class="double">
                <div>
                    <label>MAKS. ILOŚĆ NA PALECIE ORYGINALNEJ</label>
                    <input type="number" v-model="formMaxOri" required>
                </div>
                <div>
                    <label>MAKSYMALNA ILOŚĆ NA PALECIE EURO</label>
                    <input type="number" v-model="formMaxEur" required>
                </div>
            </div>
            <div class="double">
                <div v-if="!palletsError">
                    <label>RODZAJ PALETY</label>
                    <select v-model="formPalletId" required>
                        <option v-for="pal in pallets" :key="pal.palletId" :value="pal.palletId" >{{pal.name}}</option>
                    </select>
                </div>
                <div>
                    <label>MAGAZYN PRZEZNACZENIA</label>
                    <select v-model="formWarehouseId" required>
                        <option v-for="whouse in warehouses" :key="whouse.warehouseId" :value="whouse.warehouseId">{{whouse.shortName}} / {{whouse.description}}</option>
                    </select>
                </div>


            </div>

            <div class="item-buttons">
                <button class="btn" @click="isEditing=false"><span class="material-icons">close</span> Anuluj</button>
                <button class="edit-btn">Zapisz</button>

            </div>
        </form>
    </div>
    <CreatedModal/>
</template>

<script>
import urlHolder from '../composables/urlHolder.js'
import { useStore } from 'vuex'
import { computed, ref } from '@vue/reactivity'
import getItemById from '../composables/getItemById.js'
import getVendorById from '../composables/getVendorById.js'
import { onBeforeMount, onMounted } from '@vue/runtime-core'
import getCurrency from '../composables/getCurrency.js'
import getCurrencyById from '../composables/getCurrencyById.js'
import getPalletById from '../composables/getPalletById.js'
import getPallets from '../composables/getPallets.js'
import getUnitById from '../composables/getUnitById.js'
import getUnits from '../composables/getUnits.js'
import getWarehouseById from '../composables/getWarehouseById.js'
import getWarehouses from '../composables/getWarehouses.js'
import editItemById from '../composables/editItemById.js'
import {useRouter} from 'vue-router'
import CreatedModal from '../components/CreatedModal.vue'

export default {
    components:{CreatedModal},
    props:['id'],
    setup(props){
        const mainUrl = urlHolder
        const store = useStore()
        const userToken = computed(()=> store.getters.getUserToken)
        const {item, error, loadItem} = getItemById(mainUrl, userToken.value)
        const {vendor, error:venError, loadVendor} = getVendorById(mainUrl, userToken.value)
        const {currency, error:curError, loadCurrencyById} = getCurrencyById(mainUrl, userToken.value)
        const {pallet, error:palError, loadPalletById} = getPalletById(mainUrl, userToken.value)
        const {unit, error:uniError, loadUnitById} = getUnitById(mainUrl, userToken.value)
        const {warehouse, error:warError, loadWarehouseById} = getWarehouseById(mainUrl, userToken.value)
        const {loadWarehouses, error:warsError, warehouses} = getWarehouses(mainUrl, userToken.value)
        const {loadCurrency, error:curListError, currencyList} = getCurrency(mainUrl, userToken.value)
        const {loadUnits, error:uniListError, units} = getUnits(mainUrl, userToken.value)
        const {loadPallets, error:palletsError, pallets} = getPallets(mainUrl, userToken.value)
        const {editItem, error:editItemError} = editItemById(mainUrl, userToken.value)

        const router = useRouter()

        const isLoaded = ref(false)
        const isEditing = ref(false)
        const formName = ref()
        const formRevision = ref()
        const formDescription = ref()
        const formCountry = ref()
        const formUnitId = ref()
        const formPrice = ref()
        const formCurrencyId = ref()
        const formIsActive = ref()
        const formIsICP = ref()
        const formHSnumber = ref()
        const formHSdescription = ref()
        const formMaxOri = ref()
        const formMaxEur = ref()
        const formPalletId = ref()
        const formWarehouseId = ref()

        const handleBack = ()=>{
            router.back()
        }
        
        onBeforeMount( ()=>
        {
            loadItem(props.id)
            .then(()=>
            {
                
                loadVendor(item.value.vendorId)
                loadPalletById(item.value.palletId)
                loadUnitById(item.value.unitId)
                loadWarehouseById(item.value.warehouseId)
                loadCurrencyById(item.value.currencyId)
                loadUnits(1,50)
                loadWarehouses(1,50)
                loadCurrency(1,50)
                loadPallets(1,50)
                .then(()=>
                    {
                        isLoaded.value = true

                    })
            })
        })
        const handleEdit = (()=>{
            formName.value = item.value.name
            formRevision.value = item.value.revisionNumber
            formDescription.value = item.value.description
            formCountry.value = item.value.countryOfOrigin
            formIsICP.value = item.value.isICP
            formIsActive.value = item.value.isActive
            formHSnumber.value = item.value.hsCode
            formHSdescription.value = item.value.hsCodeDescription
            formMaxEur.value = item.value.maxEuroPalQty
            formMaxOri.value = item.value.palletQty
            formWarehouseId.value = warehouse.value.warehouseId
            formPrice.value = item.value.price
            formUnitId.value = unit.value.unitId
            formCurrencyId.value = currency.value.currencyId
            formPalletId.value = pallet.value.palletId
            isEditing.value = true
        })
        const handleSubmit = (()=>{
            isLoaded.value = false
            let itemData =
            {
                name: formName.value,
                vendorId: vendor.value.vendorId,
                revisionNumber: formRevision.value,
                description: formDescription.value,
                countryOfOrigin: formCountry.value,
                isICP: formIsICP.value,
                isActive: formIsActive.value,
                hsCode: formHSnumber.value,
                hsCodeDescription: formHSdescription.value,
                maxEuroPalQty: formMaxEur.value,
                palletQty: formMaxOri.value,
                warehouseId: formWarehouseId.value,
                price: formPrice.value,
                unitId: formUnitId.value,
                currencyId: formCurrencyId.value,
                palletId: formPalletId.value

            }
            console.log(itemData)
            editItem(props.id, itemData)
                .then(()=>
                {
                    loadItem(props.id)
                        .then(()=>{
                            isEditing.value=false
                            isLoaded.value=true
                            document.getElementById("mycreated-modal").style.display="block";
                        })    
                })

        })

        return{
            handleSubmit,
            handleBack,
            handleEdit,
            isLoaded,
            isEditing,
            item,
            error,
            venError,
            vendor,
            currency,
            curError,
            pallet,
            palError,
            palletsError,
            pallets,
            unit,
            uniError,
            warError,
            warehouse,
            formName,
            formRevision,
            formDescription,
            formCountry,
            formUnitId,
            formPrice,
            formCurrencyId,
            formIsActive,
            formIsICP,
            formHSnumber,
            formHSdescription,
            formMaxOri,
            formMaxEur,
            formPalletId,
            formWarehouseId,
            warehouses,
            warsError,
            units,
            uniListError,
            currencyList 
        }
    }

}
</script>

<style>
.item-container{
    display: grid;
    grid-template-columns: 100px 100px 130px 120px 36px 100px 100px 130px 120px;
    align-items: stretch;
}
.item-container h4{
    color: #aaa;
    font-size: 14px;
}
.item-container p{
    color: #fff;
    font-size: 14px;
    
}
.item-label-first{
    display: flex;
    align-items: center;
    grid-column-start: 1;
    grid-column-end: 3;
    padding: 10px 20px;
    margin: 3px 0;
    background-color: var(--back-dark);
    
    min-height: 40px;
    
}
.item-value-first{
    display: flex;
    align-items: center;
    grid-column-start: 3;
    grid-column-end: 5;
    padding: 10px 20px;
    margin: 3px 0;
    background-color: var(--back-grey);
    
    min-height: 40px;
}
.item-label-second{
    display: flex;
    align-items: center;
    grid-column-start: 6;
    grid-column-end: 8;
    padding: 10px 20px;
    margin: 3px 0;
    background-color: var(--back-dark);
    min-height: 40px;
}
.item-value-second{
    display: flex;
    align-items: center;
    grid-column-start: 8;
    grid-column-end: 10;
    padding: 10px 20px;
    margin: 3px 0;
    background-color: var(--back-grey);
    min-height: 40px;
}
.item-buttons{
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    justify-content: space-between;
    grid-column-start: 1;
    grid-column-end: 10;
    margin: 15px 0px;
}
.item-buttons button{
    min-width: 140px;
    
}
.item-buttons button span{
    margin-right: 12px;
}


.edit-item-form-container form{
    max-width: 936px;
    background-color: transparent;
    box-shadow: none;
    padding: 0;
    margin: 0;
}
.edit-item-form-container form .double{
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 30px;
    margin:15px 0px;
}
.edit-item-form-container form .double12{
    display: grid;
    grid-template-columns: 1fr 2fr;
    gap: 30px;
    margin:15px 0px;
}
.edit-item-form-container form .double13{
    display: grid;
    grid-template-columns: 1fr 3fr;
    gap: 30px;
    margin:15px 0px;
}
.edit-item-form-container form .triple{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 30px;
    margin:15px 0px;

}
.edit-item-form-container form .four2111{
    display: grid;
    grid-template-columns: 2fr 1fr 1fr 1fr;
    gap: 30px;
    margin:15px 0px;

}
.edit-item-form-container form .triple311{
    display: grid;
    grid-template-columns: 3fr 1fr 1fr;
    gap: 30px;

}
.edit-item-form-container form .triple314{
    display: grid;
    grid-template-columns: 3fr 1fr 4fr;
    gap: 30px;

}
.edit-item-form-container form .double .checkboxes{
    display: flex;
    flex-direction: row;
    align-items: center ;
    
}
.edit-item-form-container form .double .checkboxes label{
    padding-left: 6px  ;
}
.edit-item-form-container form .double .checkboxes input[type=checkbox]{
    /* height: 20px; */
    
    padding: 20px;
}



</style>