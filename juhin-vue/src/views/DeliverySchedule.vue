<template>
  <h2>Grafik dostaw</h2>

    <div>
        <form class="dates-form" @submit.prevent="handleSubmit">
            <div>
                <label>Data początkowa</label>
                <input type="datetime-local">
            </div>
            <div>
                <label>Data końcowa</label>
                <input type="datetime-local">
            </div>
            <button>OK</button>
        </form>
    </div>
    <div class="days-container">
  
            <div class="hours">
                <div class="header"><p>godzina</p></div>
                <div><p>0:00 - 1:00</p></div>
                <div><p>2:00 - 3:00</p></div>
                <div><p>3:00 - 4:00</p></div>
                <div><p>5:00 - 6:00</p></div>
                <div><p>6:00 - 7:00</p></div>
                <div><p>7:00 - 8:00</p></div>
                <div><p>9:00 - 10:00</p></div>
                <div><p>10:00 - 11:00</p></div>
                <div><p>11:00 - 12:00</p></div>
                <div><p>12:00 - 13:00</p></div>
                <div><p>13:00 - 14:00</p></div>
                <div><p>14:00 - 15:00</p></div>
                <div><p>15:00 - 16:00</p></div>
                <div><p>16:00 - 17:00</p></div>
                <div><p>17:00 - 17:00</p></div>
                <div><p>18:00 - 17:00</p></div>
                <div><p>19:00 - 17:00</p></div>
                <div><p>20:00 - 17:00</p></div>
                <div><p>21:00 - 17:00</p></div>
                <div><p>22:00 - 17:00</p></div>
                <div><p>23:00 - 00:00</p></div>
            </div>
        <div class="days-container hours-window">
            
                <div class="hours hour">
                    <div class="header day-header">
                        <h3>PON</h3> 
                        <p>12 STY</p>
                    </div>
                    <div class="deliveries">
                        <div><p>NKC</p></div>
                        <div><p>NKC</p></div>
                    </div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries">
                        <div><p>NKC</p></div>

                    </div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries">
                        <div>
                            <p>BFOREST</p>
                        </div>
                        <div>
                            <p>LSD</p>
                        </div>
                    </div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    <div class="deliveries"></div>
                    
                </div>
            












            <div class="hours hour">
                <div class="header day-header">
                    <h3>WTO</h3> 
                    <p>13 STY</p>
                </div>
                <div class="deliveries">
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div><p>CPPD</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                   
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div> <div class="hours hour">
                <div class="header day-header">
                    <h3>ŚRO</h3> 
                    <p>14 STY</p>
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div>
            <div class="hours hour">
                <div class="header day-header">
                    <h3>WTO</h3> 
                    <p>13 STY</p>
                </div>
                <div class="deliveries">
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div> <div class="hours hour">
                <div class="header day-header">
                    <h3>ŚRO</h3> 
                    <p>14 STY</p>
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries">
                    <div>
                        <p>JVK</p>
                    </div>

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div>
            <div class="hours hour">
                <div class="header day-header">
                    <h3>WTO</h3> 
                    <p>13 STY</p>
                </div>
                <div class="deliveries">
                </div>
                <div class="deliveries">
                    <div><p>LOOK</p></div>
                    

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div><p>VMI</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                   
                    <div>
                        <p>STX</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div> <div class="hours hour">
                <div class="header day-header">
                    <h3>ŚRO</h3> 
                    <p>14 STY</p>
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div>
            <div class="hours hour">
                <div class="header day-header">
                    <h3>WTO</h3> 
                    <p>13 STY</p>
                </div>
                <div class="deliveries">
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div>
                        <p>BFOREST</p>
                    </div>
                
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div> <div class="hours hour">
                <div class="header day-header">
                    <h3>ŚRO</h3> 
                    <p>14 STY</p>
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div>
            <div class="hours hour">
                <div class="header day-header">
                    <h3>WTO</h3> 
                    <p>13 STY</p>
                </div>
                <div class="deliveries">
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div>
                        <p>BFOREST</p>
                    </div>

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div> <div class="hours hour">
                <div class="header day-header">
                    <h3>ŚRO</h3> 
                    <p>14 STY</p>
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div>
            <div class="hours hour">
                <div class="header day-header">
                    <h3>WTO</h3> 
                    <p>13 STY</p>
                </div>
                <div class="deliveries">
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                    

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                    <div>
                        <p>BFOREST</p>
                    </div>
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div> <div class="hours hour">
                <div class="header day-header">
                    <h3>ŚRO</h3> 
                    <p>14 STY</p>
                </div>
                <div class="deliveries">
                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries">

                    <div><p>NKC</p></div>
                    <div><p>NKC</p></div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries">

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>

                </div>
                <div class="deliveries">
                    <div>
                        <p>BFOREST</p>
                    </div>
                </div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                <div class="deliveries"></div>
                
            </div>
    </div>
   </div>
</template>

<script>
import getDeliveriesByDates from '../composables/getDeliveriesByDates.js'
import urlHolder from '../composables/urlHolder.js'
import { useStore } from 'vuex'
import { computed, ref } from '@vue/reactivity'
export default {
    setup(){
        const mainUrl = urlHolder
        const store = useStore()
        const userToken = computed(()=> store.getters.getUserToken)
        const startDate = ref(Date())
        const endDate = ref(Date())

        const { deliveries, error, loadDeliveriesByDates } = getDeliveriesByDates(mainUrl, userToken.value)

        return { deliveries, error, startDate, endDate}
    }

}
</script>

<style>
form.dates-form{
    display: grid;
    grid-template-columns: 240px 240px 200px;

    background-color:transparent;
    border: none;
    box-shadow: none;
    margin: 0;
    padding: 10px 0;
    align-items: end;
    justify-items: start;
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
    /* display:flex;
    flex-direction: column; */
    
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
.days-container .hours div.deliveries{
    display: flex;
    flex-flow:row;
    flex-wrap: nowrap;
    padding: 0px 4px;
}
.days-container .hours.hour div{
        background-color: rgb(56, 56, 56);
    }
.days-container .hours div.deliveries div{
    background-color: rgb(98, 133, 33);
    padding: 2px 3px;
    margin: auto;
    min-height: auto;
    min-width: auto;
    width: 100%;
}
.days-container .hours div.deliveries div p{
    padding: 0;
    margin: 0;
}



</style>