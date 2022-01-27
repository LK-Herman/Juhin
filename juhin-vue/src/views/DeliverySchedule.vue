<template>
    <div class="form-and-header">
        <img src="@/assets/images/deliveryIcon.png"/>
        <h2>GRAFIK DOSTAW</h2>
        <form class="dates-form" @submit.prevent="handleSubmit">
            <div>
                <label>Data początkowa</label>
                <input type="date" v-model="startDate">
            </div>
            <div>
                <label>Data końcowa</label>
                <input type="date" v-model="endDate">
            </div>
            <button>OK</button>
        </form>
    </div>
    
    <div v-if="datesWithDeliveries" class="days-container">
        
        <div class="hours">
            <div class="header"><p>godzina</p></div>
            <div v-for="hour in hours" :key="hour"><p>{{hour}}:00 - {{hour + 1}}:00</p></div>
        </div>
        <div v-if="datesWithDeliveries" class="days-container hours-window">
            <div class="hours hour" v-for="day in datesWithDeliveries" :key="day.dateShort">
                <div v-if="day" class="header day-header">
                    <h3>{{day.dayOfWeek}}</h3> 
                    <p>{{day.dateShort}}</p>
                </div>

                <div class="deliveries" v-for="hour in hours" :key="hour">
                    <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                        <span v-if="delivs.etaHour >= hour && delivs.etaHour < hour + 1">
                                <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                    <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                </span>
                                </router-link>
                        </span>
                    </div>
                </div>
            </div>
        </div>

   </div>
  
</template>

<script>
import getDeliveriesByDates from '../composables/getDeliveriesByDates.js'
import urlHolder from '../composables/urlHolder.js'
import { useStore } from 'vuex'
import { computed, ref } from '@vue/reactivity'
import moment from 'moment'
import { onMounted } from '@vue/runtime-core'
export default {
    setup(){
        const mainUrl = urlHolder
        const store = useStore()
        const userToken = computed(()=> store.getters.getUserToken)
        const startDate = ref(moment().format("YYYY-MM-DD"))
        const endDate = ref(moment().add(20,'days').format("YYYY-MM-DD"))

        const { datesWithDeliveries, error, loadDeliveriesByDates } = getDeliveriesByDates(mainUrl, userToken.value)
        let hours = [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23]
        const handleSubmit = () =>{
            let startD = moment(startDate.value)
            let endD = moment(endDate.value)
                loadDeliveriesByDates(moment(startD).format("YYYY-MM-DD"),moment(endD).format("YYYY-MM-DD"))
        }
            onMounted(()=>
            {
                handleSubmit()
            })

        return { handleSubmit, datesWithDeliveries, error, startDate, endDate, hours}
    }

}
</script>

<style>
.form-and-header{
    display: flex;
    flex-flow: row;
    justify-content: space-between;
    align-items: flex-end;
    min-width: 1200px;
}

form.dates-form{
    display: grid;
    grid-template-columns: 200px 200px 120px;
    grid-template-rows: 60px auto ;
    background-color:transparent;
    border: none;
    box-shadow: none;
    margin: 0;
    padding: 10px 0;
    align-items: end;
    justify-items: end;
    margin-left: auto;
}
.form-and-header h2, img{
    /* margin-right: auto; */
    padding: 0 12px 15px 6px;
}

form.dates-form input{
    margin: 5px 0px;
    padding: 10px;
    min-height: 42px;
}
form.dates-form button{
    margin: 5px 0px;
    padding: 10px;
    min-height: 42px;
}

.days-container{
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    overflow:auto;
    max-width: 1200px;
    margin-bottom: 40px;
}
.days-container .hours{
    display:flex;
    flex-direction: column;
    
}
.days-container .hours-window{
    padding-bottom: 2px;
}
/* .days-container .hours div p{
    margin:auto;
} */
.days-container .hours div{
    display: flex;
    align-items: center;
    justify-content: center;
    min-width: 80px;
    background-color: var(--back-grey2);
    padding: 0 6px;
    font-size: 12px;
    border: 1px solid #252525;
    min-height: 30px;
    /* height: 100%; */
}
.days-container .hours .header{
    display: flex;
    min-height: 60px;
    align-items: center;
    justify-content: center;
    background-color: #666;
}

.days-container .hours .header.day-header{
    display: flex;
    flex-direction: column;
    background-color: rgb(72, 70, 168);
    color: #fff;
    justify-content: center;
    
}
.days-container .hours .deliveries{
    display: flex;
    flex-flow:row;
    flex-wrap: nowrap;
    padding: 0px 4px;
}
.days-container .hours .deliveries .single-del{
    display: grid;
    grid-template-columns: auto;
    flex-flow:row;
    flex-wrap: nowrap;
    
    /* background-color: rgb(98, 133, 33); */
    padding: 0;
    margin: 0;
    min-height: 0px;
    min-width: 0px;
    
    border: none;
    /* width: 100%; */
}

.days-container .hours.hour div{
    background-color: rgb(56, 56, 56);
    }
/* .days-container .hours div.deliveries div{
    background-color: rgb(98, 133, 33);
    padding: 2px 3px;
    margin: auto;
    min-height: auto;
    min-width: auto;
    width: 100%;
} */
.days-container .hours div.deliveries div{
    background-color: transparent;
    /* padding: 2px 3px; */
    margin: auto;
    min-height: auto;
    /* min-width: auto; */
    /* width: 100%; */
    border: none;
}
.days-container .hours div.deliveries div span{
    display: flex;
    padding: 0px 0px;
    margin: 0 ;
    max-height: 22px;
    align-items: center;
    /* min-height: auto; */
    /* min-width: auto;  */
    text-align: center;
    /* width: auto; */
    /* border: solid 1px #333; */
     /* inline-size: auto; */
}
.days-container .hours div.deliveries div p{
    /* background-color: var(--dark-blue); */
    background-color: rgb(98, 133, 33);
    padding: 6px 8px;
    /* border: solid 1px #222; */
    font-weight: 500;
    cursor: pointer;
    margin: 0 2px;
    transition:  160ms ease-in-out;
}

.days-container .hours div.deliveries div p:hover{
    transform: scale(1.5,1.5) ;
    transition:  160ms ease-in-out;
    box-shadow: 5px 5px 6px #222;
}
.days-container .hours .deliveries .single-del .ora{
    background-color: var(--orange);
}




</style>