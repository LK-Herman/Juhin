<template>
  <div v-if="!error">
        
        <div class="delivery-details-container">
            <div class="item-v">
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                    <div @click="handleGoToVendor(order.vendorId)" class="vendor-header" >
                        <img src="../assets/images/vendorIcon.png">
                        <h1 class="text-yellow">{{order.vendorName}}</h1>
                    </div>
                </div>
            </div>
            <div class="item-s" :class="{statusDelivered:delivery.statusId==3, statusInTransit:delivery.statusId==2, statusCreated:delivery.statusId==1}">
                <h3>{{delivery.status}}</h3>
            </div>

            <div v-if="!isEditing" class="item-o">
                <h4>NR ZMÓWIENIA</h4> 
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                        <p>{{order.orderNumber}}</p>
                </div>
            </div>

            <div v-if="!isEditing" class="item-po">
                <h4>UTWORZONA</h4> 
                <!-- <div v-for="order in delivery.purchaseOrders" :key="order.orderId"> -->
                    <p>{{delivery.createdAt}}</p>
                <!-- </div> -->
            </div>
            
            <div v-if="!isEditing" class="item-e">
                <h4>ETA</h4>
                <p>{{delivery.etaD}} {{delivery.etaT}}</p>
            </div>
            <div v-if="!isEditing" class="item-dd">
                <h4>DATA DOSTAWY</h4>
                <p v-if="delivery.statusId == 3" class="text-yellow">{{delivery.deliveryDate}}</p>
                <p v-else>(brak)</p>
            </div>
            


            <div  v-if="!isEditing" class="item-p">
                <div class="item-p-head">
                    <h4>LISTA TOWARÓW: </h4>    
                    
                    <div v-if="user.isSpecialist || user.isWarehouseman">
                        <button id="editlist" @click="handleEditList">Edytuj listę</button>
                    </div>
                </div>
                <div v-if="!error">
                    <div class="divTable blueTable">
                        <div class="divTableHeading">
                            <div class="divTableRow">
                                <div class="divTableHead firstCol center-text">POZYCJA</div>
                                <div class="divTableHead secondCol">NR TOWARU</div>
                                <div class="divTableHead thirdCol">OPIS TOWARU</div>
                                <div class="divTableHead fourthCol">ILOŚĆ</div>
                                <div class="divTableHead fifthCol center-text">JEDN.</div>
                                <div v-if="toggleEditList" class="divTableHead fifthCol center-text">USUŃ</div>
                            </div>
                        </div>
                        <div class="divTableBody">
                            <div class="divTableRow" v-for="item in delivery.packedItems" :key="item.itemId">
                                <div class="divTableCell firstCol center-text">{{item.counter}}</div>
                                <div class="divTableCell secondCol">{{ item.partNumber.toUpperCase() }}</div>
                                <div class="divTableCell thirdCol">{{ item.description.toUpperCase() }}</div>
                                <div class="divTableCell fourthCol">{{ item.quantity }}</div>
                                <div class="divTableCell fifthCol center-text">{{ item.unitMeasure.toUpperCase() }}</div>
                                <div v-if="toggleEditList" class="divTableCell fifthCol center-text"><a @click="deletePackedItem(item.packedItemId)"><span class="material-icons">clear</span></a></div>
                            </div>
                        </div>
                    </div>

                    <div v-if="toggleEditList">
                        <div class="" v-for="vendor in delivery.purchaseOrders" :key="vendor.orderId" >
                            <PackedItemsAdd :id="id" :vId="vendor.vendorId" @item-added-event="handleRefreshTable"/>
                        </div>
                        
                    </div>
                </div>
            </div>

            <div v-if="!isEditing" class="item-f">
                <h4>PRZWOŹNIK</h4> 
                <p>{{delivery.forwarderName}}</p>
            </div>

            <div v-if="!isEditing" class="item-prio">
                <h4>PRIORYTET</h4> 
                <p v-if="!delivery.isPriority">STANDARDOWA DOSTAWA</p>
                <p v-else class="text-yellow">PIERWSZEŃSTWO ROZŁADUNKU</p>
            </div>

            <div v-if="!isEditing"  class="item-c">
                <h4>KOMENTARZ</h4> 
                <p>{{delivery.comment}}</p>
            </div>

            <div v-if="!isEditing" class="item-btn">
                <button @click="handleBack"> <span class="material-icons">keyboard_backspace</span> Powrót</button>
                <button v-if="user.isSpecialist || user.isWarehouseman" @click="handleEditOrder" class="edit-btn">Edytuj</button>
                <button id="subscription" class="sub-btn" :class="{suboff:isSubscribed}" @click="handleSubscription">Subskrybuj</button>
                <router-link v-if="user.isSpecialist || user.isWarehouseman" id="docs" class="btn" :to="{name:'DeliveryDocs', params: {id:id}}">Dokumenty</router-link>
                <button v-if="(delivery.statusId != 3 && user.isAdmin) || (delivery.statusId != 3 && user.isSpecialist)" class="delete-btn" @click="handleDelete">Usuń</button>
            </div>


        </div>         
       
        <!--end of delivery-details-container -->
        <div>
            <DeliveryDelete :id="id" :orders="delivery.purchaseOrders"/>
        </div>

        <div v-if="(isEditing && user.isAdmin) || (isEditing && user.isSpecialist) || (isEditing && user.isWarehouseman)" >

            <form @submit.prevent="handleSubmitChanges" class="delivery-form">
                <div class="triple">
                    <div>
                        <label>Numer zamówienia</label>
                        <div >
                            <h3 id="order-number" v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                <span>{{order.orderNumber}}</span>
                            </h3>
                        </div>
                    </div>

                    <div class="double-items">
                        <label>Ustaw priorytet</label>
                        <div class="flipswitch">
                            <input type="checkbox" name="flipswitch" class="flipswitch-cb" id="fs" checked v-model="formPrio">
                            <label class="flipswitch-label" for="fs">
                                <div class="flipswitch-inner"></div>
                                <div class="flipswitch-switch"></div>
                            </label>
                        </div>
                    </div>
                    <div class="double-items">
                        <label>Ocena dostawy:</label>
                        <div class="double-rating">
                            <span id="rating-value">{{formRating}}</span>
                            <input type="range" max="100" min="0" step="1" v-model="formRating" :class="{redback:formRating<40, orangeback:formRating<70&&rating>39 , greenback:formRating>69}">
                        </div>
                    </div>
                </div>
                <div class="double" >
                    <div class="delivery-item-time">
                        <label>Data i czas dostawy</label>
                        <input type="datetime-local" v-model="formDate">
                    </div>
                    <div class="delivery-item-status">
                        <label>Zmień status</label>
                        <select v-model="formStatusId">
                            <option v-for="status in statuses" :key="status.statusId" :value="status.statusId">{{status.name}}</option>
                            
                        </select>
                    </div>
                    <div v-if="!forError" class="delivery-item-forwarder">
                        <label>Zmień przewoźnika</label>
                        <select  v-model="formForwarderId">
                            <option v-for="forwarder in forwarders" :key="forwarder.forwarderId" :value="forwarder.forwarderId">{{forwarder.name}}</option>
                        </select>
                    </div>
                    <div class="delivery-item-comment">
                        <label>Komentarz</label>
                        <textarea cols="30" rows="5" v-model="formComment"></textarea>
                    </div>
                </div>
                
                <div class="delivery-buttons-container">
                    <button @click="isEditing=false" class="btn">Anuluj</button>
                    <button id="save" class="btn">Zapisz</button>
                </div>



            </form>

        </div>
    </div>
    <div v-else>{{error}}</div>
    <CreatedModal/>
</template>


<script>
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import checkSubscription from '../composables/checkSubscription.js'
import addSubscription from '../composables/addSubscription.js'
import delSubscription from '../composables/delSubscription.js'
import urlHolder from '../composables/urlHolder.js'
import { computed, onMounted, onUpdated, ref, watch, watchEffect } from '@vue/runtime-core'
import {useRouter} from 'vue-router'
import DeliveryDelete from '../components/DeliveryDelete.vue'
import getForwarders from '../composables/getForwarders.js'
import getStatuses from '../composables/getStatuses.js'
import editDeliveryById from '../composables/editDeliveryById.js'
import {useStore} from 'vuex'
import getCurrentUser from '../composables/getCurrentUser.js'
import PackedItemsEdit from '../components/PackedItemsEdit.vue'
import PackedItemsAdd from '../components/PackedItemsAdd.vue'
import axios from 'axios'
import CreatedModal from '../components/CreatedModal.vue'

export default {
    props: ['id'],
    components: {DeliveryDelete, PackedItemsEdit, PackedItemsAdd, CreatedModal},
    setup(props){
        let counter = 1
        const isEditing = ref(false)
        const rating = ref (100)
        const mainUrl = urlHolder
        const store = useStore()
        const user = computed(()=> store.getters.getUser)
        const { getUser, error:getUserError } = getCurrentUser(mainUrl)
        const userToken = computed(()=> store.getters.getUserToken)

        const router = useRouter()
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, userToken.value)
        const { checkSubs, error:subError, isSubscribed } = checkSubscription(mainUrl, userToken.value)
        const { addSubs, error:addSubError, response } = addSubscription(mainUrl, userToken.value)
        const { delSubs, error:delSubError} = delSubscription(mainUrl, userToken.value)
        const subscriptionData = ref()
        const formPrio = ref('')
        const formRating = ref(100)
        const formDate = ref(null)
        const formStatusId = ref('')
        const formForwarderId = ref('')
        const formComment = ref('')
        
        const toggleEditList = ref(false)
        const {loadForwarders, error:forError, forwarders} = getForwarders(mainUrl, userToken.value)
        const {loadStatuses, error:staError, statuses} = getStatuses(mainUrl, userToken.value)
        const {editDelivery,error:ediError} = editDeliveryById(mainUrl, userToken.value)

        onMounted (()=>{
            counter = 1
            
            loadDetails(props.id)
                .then(function(){
                    delivery.value.packedItems.forEach(item =>{
                        item['counter'] = counter++
                    })
                    console.log(delivery.value)
                })
            loadForwarders(1,50)
            loadStatuses(1,50)
            // user = computed(()=> store.getters.getUser)
            getUser(userToken.value)
                .then(function()
                {
                    let currentUser = computed(()=> store.getters.getUser)
                    // console.log(currentUser.value)
                    checkSubs(currentUser.value.userId, props.id)
                    subscriptionData.value =
                    {
                        deliveryId: props.id,
                        userId: currentUser.value.userId
                    }
                })
            
        })
        // onUpdated(()=>
        // {
        //     getUser(userToken.value)
        // })
        const handleSubscription = () =>
        {   
            if(isSubscribed.value == false)
            {   
                addSubs(subscriptionData.value)
                    .then(function()
                    {
                        checkSubs(user.value.userId, props.id)
                    })
            }
            else
            {   
                delSubs(subscriptionData.value)
                    .then(function()
                    {
                        checkSubs(user.value.userId, props.id)
                    })
            }
        }

        watch(isSubscribed,()=>{
            if(isSubscribed.value == false)
            {
                document.getElementById('subscription').innerHTML = "Subskrybuj"
            }
            else
            {
                document.getElementById('subscription').innerHTML = "Wyłącz subskrypcję"
            }
        })

        const handleGoToVendor = (vendorId)=>{
            router.push({name:'VendorDetails', params: {vId:vendorId}})
        }

        const handleEditOrder = () =>{
            isEditing.value = true
            
            formPrio.value = delivery.value.isPriority
            formRating.value = delivery.value.rating
            formDate.value = delivery.value.etaDate
            formStatusId.value = delivery.value.statusId
            formForwarderId.value = delivery.value.forwarderId
            formComment.value = delivery.value.comment
        }

        const handleBack = ()=>{
            router.back()
        }
        const handleSubmitChanges = async ()=>{
            let newEtaDate = new Date(formDate.value)
            let newDeliveryDate = new Date(delivery.value.etaDate)
            
            if(formStatusId.value == 3){
                newDeliveryDate = new Date()
                newEtaDate = delivery.value.etaDate
            }else{
                newEtaDate = formDate.value
                newDeliveryDate = formDate.value
            }
                       
            let deliveryData = {
            createdAt: delivery.value.createdAtshort,
            etaDate: newEtaDate,
            deliveryDate: newDeliveryDate,
            rating: formRating.value,
            comment: formComment.value,
            forwarderId: formForwarderId.value,
            statusId: formStatusId.value,
            isPriority: formPrio.value
            }
            await editDelivery(deliveryData, delivery.value.deliveryId)
                    .then(function(){
                        isEditing.value = false
                        loadDetails(props.id)
                            .then(function(){
                            counter = 1   
                            delivery.value.packedItems.forEach(item =>{
                            item['counter'] = counter++
                            })
                        })
                        document.getElementById("mycreated-modal").style.display="block"
                    })
            //console.log(deliveryData)
        }
        const handleDelete = ()=>
        {
            // deleteFlag.value = true
            var modal = document.getElementById("myModal");
            modal.style.display = "block" 
            
            window.onclick = function(event) 
            {
                if (event.target == modal) 
                {
                    modal.style.display = "none";
                    // deleteFlag.value = false
                }
            }
        }

        const handleEditList = ()=>{
            toggleEditList.value = !toggleEditList.value
        }

        watch(toggleEditList,()=>{
            if(toggleEditList.value == false)
            {
                document.getElementById('editlist').innerHTML = "Edytuj listę"
            }
            else
            {
                document.getElementById('editlist').innerHTML = "Wyłącz edycję"
            }
        })
        const handleRefreshTable = async ()=>{
            counter = 1
            
            await loadDetails(props.id)
                .then(function(){
                    delivery.value.packedItems.forEach(item =>{
                        item['counter'] = counter++
                    })
                })
        }

        const deletePackedItem = async (packedItemId)=>{
            var requestOptions = 
            {
                method: 'DELETE',
                headers: {
                    "Accept":"*/*",
                    'Content-Type':'application/json',
                    "Access-Control-Allow-Origin": "*",
                    "Authorization":"Bearer " + userToken.value
                },
                mode:'cors',
            };
           
                await axios.delete(urlHolder + 'packed/' + packedItemId, requestOptions)
                        .then(handleRefreshTable)
                        .catch(err => console.log(err))
        }

        return{ 
                user,
                deletePackedItem,
                handleRefreshTable,
                toggleEditList,
                handleEditList,
                delivery, 
                error,
                forError,
                staError,
                statuses,
                forwarders,
                counter,
                handleDelete, 
                handleBack,
                handleSubmitChanges,
                handleEditOrder,
                handleGoToVendor, 
                isEditing,
                rating,
                formPrio,
                formRating,
                formDate,
                formStatusId,
                formForwarderId,
                formComment,
                isSubscribed,
                subError,
                addSubError,
                handleSubscription
                }
    }

}
</script>

<style>

.delivery-details-container {
    margin: 25px auto;
    max-width: 880px;
    display: grid;
    grid-template-columns: 220px 220px 220px 220px;
    grid-template-rows: auto;
    grid-template-areas: 
    "vendor vendor vendor status"
    "order po-date eta deliver"
    "plist plist plist plist"
    "counter items items qty"
    "forwarder forwarder prio prio"
    "comment comment comment comment"
    "buttons buttons buttons buttons";
    padding: 0px 0px;
    
}
.delivery-details-container .divTable .divTableCell span{
    color: #afafaf;
    transition: 100ms ease-in-out;
    padding: 2px 1px;
    border-radius: 30px;
    border:solid 2px #afafaf;
    
}
.delivery-details-container .divTable .divTableCell span:hover{
    cursor: pointer;
    color: #ff3232;
    background-color: #252525;
    transform: scale(1.2);
    transition: 100ms ease-in-out;
}
.delivery-details-container .vendor-header{
    display: flex;
    align-items: center;
    cursor: pointer;
}
.delivery-details-container .vendor-header h1{
    padding-left: 24px;
}
.delivery-details-container .vendor-header img{
    padding-left: 6px;
}
.delivery-details-container .item-pl1 {
  grid-area: counter;
  padding: 10px 0px 1px 0px;
  background-color: var(--back-dark);
  font-size: 12px;
}
.delivery-details-container .item-pl2 {
    
    grid-area: items;
    background-color: var(--back-dark);
    padding: 10px 0px 1px 0px;
    font-size: 12px;
}
.delivery-details-container .item-pl3 {
    grid-area: qty;
    background-color: var(--back-dark);
    padding: 10px 0px 1px 2px;
    text-align: right;
    font-size: 12px;
}
.delivery-details-container .item-dd {
    grid-area: deliver;
    margin: 0  20px 0 20px;
}
.delivery-details-container .item-po {
    grid-area: po-date;
    margin: 0  20px 0 20px;
}
.delivery-details-container .item-e {
    grid-area: eta;
    margin: 0  20px 0 20px;
}
.delivery-details-container .item-o {
    grid-area: order;
    margin: 0  0 0 30px;
}
.delivery-details-container .item-p {
    border-top: 3px solid var(--back-grey2) ;
    padding: 20px 0 ;
    grid-area: plist;
    margin: 25px 0 10px 30px;
}
.delivery-details-container .item-p-head {
    display: flex;
    flex-flow: row;
    justify-content: space-between;

}
.delivery-details-container .item-c {
    border-top: 3px solid var(--back-grey2);
    border-bottom: 3px solid var(--back-grey2) ;
    padding: 20px 0px;
    grid-area: comment;
    margin: 0px 0px 25px 30px;
    
}
.delivery-details-container .item-f {
    border-top: 3px solid var(--back-grey2);
    padding: 20px 0px;
    grid-area: forwarder;
    margin: 0px 0px 0px 30px;
}
.delivery-details-container .item-prio {
    border-top: 3px solid var(--back-grey2);
    padding: 20px 0px;
    grid-area: prio;
    align-self: flex-end;
    text-align: left;
}
.delivery-details-container .item-v {
    grid-area: vendor;
    
    margin: 0 0 25px 30px;
    
    /* background-color: rgb(81, 82, 80); */
    display: flex;
    align-items: center;
    justify-content: flex-start;
    
    border-top: 3px solid var(--back-grey2);
    border-bottom: 3px solid var(--back-grey2);
}
.delivery-details-container .item-s {
  grid-area: status;
  display: flex;
  align-self: flex-start;
  align-items: center;
  justify-content: center;
  background-color: var(--dark-blue);
  height: 60px;
  margin-bottom: 25px;
    border-top: 3px solid var(--back-grey2);
    border-bottom: 3px solid var(--back-grey2);
}
.delivery-details-container .item-btn {
  grid-area: buttons;
  display: flex;
  align-self: flex-start;
  align-items: center;
  justify-content: space-between;
  /* background-color:rgb(74, 90, 63); */
  padding: 10px 0 ;
  gap: 10px;
  margin-left: 30px;
  margin-bottom: 25px;
  
}
.delivery-details-container .item-btn span{
    padding-right: 10px;
}
.delivery-details-container .item-btn button {
    box-shadow: 2px 2px 3px rgba(20,20,20,0.5);
    width: 180px;
    min-height: 40px;

}
.delivery-details-container .item-btn #docs{
    background-color: #792dcf;
     box-shadow: 2px 2px 3px rgba(20,20,20,0.5);
    width: 140px;
    min-height: 20px;
}
    .delivery-details-container .item-btn #docs:hover{
background-color: var(--orange);
    }
.delivery-details-container .item-btn .sub-btn.suboff{
    background-color: rgb(93, 93, 93);
}
.delivery-details-container .item-btn .sub-btn.suboff:hover{
    background-color: var(--orange)
}

.delivery-details-container #opis-tabeli{
    font-size: 0.8em;
    margin-bottom: 8px;    
    padding: 0px 30px 0px 30px;
    
}
.delivery-details-container .item-pl1 div, .item-pl2 div, .item-pl3 div{
 background-color: rgb(56, 56, 56);
 margin: 2px 0px;
 padding: 5px 30px 5px 30px;
}

.delivery-details-container h4{
    font-size: 12px;
    font-weight: 500;
    padding-bottom: 6px ;
}
.delivery-details-container .head{
    font-size: 1.1em;
    font-weight: 600;
}
.delivery-details-container .statusDelivered{
    background-color: #5ca101;
}
.delivery-details-container .statusCreated{
    background-color: #5f5f5f;
}
.delivery-details-container .statusInTransit{
    background-color: var(--dark-blue);
}



.delivery-form{
    background: none;
    box-shadow: none;
    margin: 25px auto;
    padding: 0;
}

.delivery-form #order-number{
    margin: 15px 0;
}

.delivery-form .delivery-buttons-container{
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin: 0;
    padding: 0;
}
.delivery-form .delivery-buttons-container .btn{
    width: 160px;
    margin: 20px 0;
}
.delivery-form .delivery-buttons-container .btn#save{
    background: var(--dark-blue);
}
.delivery-form .delivery-buttons-container .btn#save:hover{
    background: var(--orange);
}
.delivery-form #rating-value{
    margin: auto 0;
    padding: 6px 12px;
    border-radius: 20px;
    max-width: 50px;
    display: block;
    text-align: center;
    background: #3a3a3a;
   
}
.delivery-form .double{
    display: grid;
    grid-template-columns: 1fr 1fr;
    column-gap: 40px;
    grid-template-areas: 
    "status comm"
    "time comm"
    "forwarder comm";
    align-items: space-between;
}
.delivery-form .double .delivery-item-status {
    grid-area: status;
    
}
.delivery-form .double .delivery-item-time {
    grid-area: time;
    
    align-self: center;
}
.delivery-form .double .delivery-item-forwarder {
    grid-area: forwarder;
    align-self: end;
}
.delivery-form .double .delivery-item-comment {
    grid-area: comm;
    
}
.delivery-form .double .delivery-item-comment textarea{
    height: 280px;
}
.delivery-form .triple{
    display: grid;
    grid-template-columns: 1fr 1fr 2fr;
    column-gap: 20px;
}
.delivery-form .double-items{ 
    margin-bottom: 25px;
}
.delivery-form .double-rating{
    display: grid;
    grid-template-columns: 1fr 4fr;
    column-gap: 20px;
    justify-self: start;
}



/* INPUT RANGE */
input[type=range] {
  height: 35px;
  -webkit-appearance: none;
  margin: 10px 0;
  width: 100%;
  background: none;
  
}
input[type=range]:focus {
  outline: none;
}
input[type=range]::-webkit-slider-runnable-track {
  width: 100%;
  height: 20px;
  cursor: pointer;
  animate: 0.2s;
  background: #636363;
  border-radius: 24px;
  border: 0px solid #000000;
  box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}

input[type=range]::-webkit-slider-thumb {
    box-shadow: 0px 0px 0px #000000;
  border: 0px solid #000000;
  height: 29px;
  width: 29px;
  border-radius: 50px;
  background: #F5B727;
  cursor: pointer;
  -webkit-appearance: none;
  margin-top: -4.5px;
  box-shadow: 1px 1px 3px rgba(0,0,0,0.6), -1px -1px 3px rgba(0,0,0,0.6);
  
}
input[type=range]:focus::-webkit-slider-runnable-track {
    background: #636363;
}
    input[type=range].redback:focus::-webkit-slider-runnable-track{
        background: rgb(200, 0, 40);
        transition: all 0.3s ease-in 0s;
    }
    input[type=range].orangeback:focus::-webkit-slider-runnable-track{
        background: rgb(200, 137, 0);
        transition: all 0.3s ease-in 0s;
    }
    input[type=range].greenback:focus::-webkit-slider-runnable-track{
        background: #50990F;
        transition: all 0.3s ease-in 0s;
    }
input[type=range]::-moz-range-track {
  width: 100%;
  height: 20px;
  cursor: pointer;
  animate: 0.2s;
  box-shadow: 0px 0px 0px #50555C;
  background: #636363;
  border-radius: 24px;
  border: 0px solid #000000;
}
input[type=range]::-moz-range-thumb {
  box-shadow: 0px 0px 0px #000000;
  border: 0px solid #000000;
  height: 29px;
  width: 29px;
  border-radius: 50px;
  background: #F5B727;
  cursor: pointer;
}
input[type=range]::-ms-track {
  width: 100%;
  height: 20px;
  cursor: pointer;
  animate: 0.2s;
  background: transparent;
  border-color: transparent;
  color: transparent;
}
input[type=range]::-ms-fill-lower {
  background: #636363;
  border: 0px solid #000000;
  border-radius: 48px;
  box-shadow: 0px 0px 0px #50555C;
}
input[type=range]::-ms-fill-upper {
  background: #636363;
  border: 0px solid #000000;
  border-radius: 48px;
  box-shadow: 0px 0px 0px #50555C;
}
input[type=range]::-ms-thumb {
  margin-top: 1px;
  /* box-shadow: 0px 0px 0px rgba(0,0,0,0.6); */
  border: 0px solid #000000;
  height: 29px;
  width: 29px;
  border-radius: 50px;
  background: #F5B727;
  cursor: pointer;
}
input[type=range]:focus::-ms-fill-lower {
  background: #636363;
}
input[type=range]:focus::-ms-fill-upper {
  background: #636363;
}
</style>