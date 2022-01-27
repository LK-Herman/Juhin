<template>

    <div class="form-and-header">
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
                <div><p>0:00 - 1:00</p></div>
                <div><p>1:00 - 2:00</p></div>
                <div><p>2:00 - 3:00</p></div>
                <div><p>3:00 - 4:00</p></div>
                <div><p>4:00 - 5:00</p></div>
                <div><p>5:00 - 6:00</p></div>
                <div><p>6:00 - 7:00</p></div>
                <div><p>7:00 - 8:00</p></div>
                <div><p>8:00 - 9:00</p></div>
                <div><p>9:00 - 10:00</p></div>
                <div><p>10:00 - 11:00</p></div>
                <div><p>11:00 - 12:00</p></div>
                <div><p>12:00 - 13:00</p></div>
                <div><p>13:00 - 14:00</p></div>
                <div><p>14:00 - 15:00</p></div>
                <div><p>15:00 - 16:00</p></div>
                <div><p>16:00 - 17:00</p></div>
                <div><p>17:00 - 18:00</p></div>
                <div><p>18:00 - 19:00</p></div>
                <div><p>19:00 - 20:00</p></div>
                <div><p>20:00 - 21:00</p></div>
                <div><p>21:00 - 22:00</p></div>
                <div><p>22:00 - 23:00</p></div>
                <div><p>23:00 - 00:00</p></div>
            </div>
        <div v-if="datesWithDeliveries" class="days-container hours-window">
            
                <div class="hours hour" v-for="day in datesWithDeliveries" :key="day.dateShort">
                    <div v-if="day" class="header day-header">
                        <h3>{{day.dayOfWeek}}</h3> 
                        <p>{{day.dateShort}}</p>
                    </div>

                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 0 && delivs.etaHour <1">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 1 && delivs.etaHour <2">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 2 && delivs.etaHour <3">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 3 && delivs.etaHour <4">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 4 && delivs.etaHour <5">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 5 && delivs.etaHour <6">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 6 && delivs.etaHour <7">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 7 && delivs.etaHour <8">
                                    <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 8 && delivs.etaHour <9">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 9 && delivs.etaHour <10">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 10 && delivs.etaHour <11">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 11 && delivs.etaHour <12">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 12 && delivs.etaHour <13">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 13 && delivs.etaHour <14">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 14 && delivs.etaHour <15">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 15 && delivs.etaHour <16">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 16 && delivs.etaHour <17">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 17 && delivs.etaHour <18">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 18 && delivs.etaHour <19">
                                   <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                    </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 19 && delivs.etaHour <20">
                                    <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 20 && delivs.etaHour <21">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 21 && delivs.etaHour <22">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 22 && delivs.etaHour <23">
                                     <router-link :to="{name:'DeliveryDetails', params:{id:delivs.deliveryId}}">
                                        <span v-for="order in delivs.purchaseOrders" :key="order.orderId">
                                            <p :class="{ora:delivs.isPriority}">{{order.vendorShortName}}</p>
                                        </span>
                                     </router-link>
                                </span>
                            </div>
                    </div>
                    <div  class="deliveries">
                            <div class="single-del" v-for="delivs in day.deliveries" :key="delivs">
                                <span v-if="delivs.etaHour >= 23">
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
        const endDate = ref(moment().add(14,'days').format("YYYY-MM-DD"))

        const { datesWithDeliveries, error, loadDeliveriesByDates } = getDeliveriesByDates(mainUrl, userToken.value)
        let datesRange = ref([{}])
        const handleSubmit = () =>{
            let startD = moment(startDate.value)
            let endD = moment(endDate.value)
                loadDeliveriesByDates(moment(startD).format("YYYY-MM-DD"),moment(endD).format("YYYY-MM-DD"))
        }
            onMounted(()=>
            {
                handleSubmit()
            })

        return { handleSubmit, datesWithDeliveries, error, startDate, endDate}
    }

}
</script>

<style>
.form-and-header{
    display: flex;
    flex-flow: row;
    justify-content: space-between;
    align-items: flex-end;
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
.form-and-header h2{
    margin-right: auto;
    padding-bottom: 15px ;
    
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
    background-color: rgb(98, 133, 33);
    padding: 4px 6px;
    /* border: solid 1px #222; */
    font-weight: 500;
    cursor: pointer;
    margin: 0 2px;
}
.days-container .hours div.deliveries div p:hover{
    background-color: var(--dark-blue)
}
.days-container .hours .deliveries .single-del .ora{
    background-color: var(--orange);
}



</style>