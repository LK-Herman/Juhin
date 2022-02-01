<template>

     <div class="vendor-details-container">
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
    <div v-if="!error" class="item-container">
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
        <div class="item-value-second">
            <p v-if="!curError">{{item.price}} {{currency.code}}
            </p>
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
    </div>

</template>

<script>
import urlHolder from '../composables/urlHolder.js'
import { useStore } from 'vuex'
import { computed } from '@vue/reactivity'
import getItemById from '../composables/getItemById.js'
import getVendorById from '../composables/getVendorById.js'
import { onBeforeMount, onMounted } from '@vue/runtime-core'
import getCurrencyById from '../composables/getCurrencyById.js'
import getPalletById from '../composables/getPalletById.js'
import getUnitById from '../composables/getUnitById.js'
import getWarehouseById from '../composables/getWarehouseById.js'

export default {

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

        
        onBeforeMount(async ()=>
        {
            await loadItem(props.id)
            .then(()=>
            {
                loadCurrencyById(item.value.currencyId)
                loadVendor(item.value.vendorId)
                loadPalletById(item.value.palletId)
                loadUnitById(item.value.unitId)
                loadWarehouseById(item.value.warehouseId)
            })
        })

        return { item, error, venError, vendor, currency, curError, pallet, palError, unit, uniError, warError, warehouse }
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


</style>