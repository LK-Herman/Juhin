<template>
<div>
    <form class="add-delivery" @submit.prevent="handleNewDeliverySubmit" v-if="!packedItemsFlag">
      <div class="add-form-header">
        <img src="../assets/images/vendorIcon.png" alt="" />
        <div>
          <h2>{{ vendorName }}</h2>
          <p>Numer zamówienia: {{ " " + orderNo }}</p>
        </div>
        <div></div>
      </div>
        <div v-if="error" class="error">
            <p>{{ error }}</p>
        </div>
        <h3 id="add-delivery-h3">Wprowadź dane dostawy</h3>
        <div class="double">
            <div>
                <label>Wprowadź datę dostawy</label>
                <input type="datetime-local" v-model="eta" :class="{ disabled: packedItemsFlag }" required />
            </div>
            <div>
                <label>Wybierz przewoźnika</label>
                <select v-model="selectedForwarderId" :class="{ disabled: packedItemsFlag }" required>
                    <option v-for="forwarder in forwarders" :key="forwarder.forwarderId" :value="forwarder.forwarderId">
                    {{ forwarder.name }}
                    </option>
                </select>
            </div>

        </div> 
            <label>Dodaj komentarz (opcjonalnie)</label>
            <textarea class="comment" v-model="comment" :class="{ disabled: packedItemsFlag }"></textarea>
        <div class="submit">
            <button>Dalej</button>
      </div>
    </form>

    <div v-if="packedItemsFlag">
        <div>
        <h2 class="center-text">Dostawa została utworzona</h2>
        <p class="center-text">Dodaj pozycje towarowe do zamówiena</p>
        </div>
        <div>
            <PackedItemsAdd :userToken="userToken" :vId="vId" :id="createdId" @item-added-event="handleRefreshTable" />
        </div>
        <div v-if="!loadedDeliveryError">
            <div v-if="delivery.packedItems != 0" class="divTable blueTable">
                <div class="divTableHeading">
                    <div class="divTableRow">
                        <div class="divTableHead firstCol center-text">POZYCJA</div>
                        <div class="divTableHead secondCol">NR TOWARU</div>
                        <div class="divTableHead thirdCol">OPIS TOWARU</div>
                        <div class="divTableHead fourthCol">ILOŚĆ</div>
                        <div class="divTableHead fifthCol center-text">JEDN.</div>
                    </div>
                </div>
                <div v-if="!loadedDeliveryError" class="divTableBody">
                    <div class="divTableRow" v-for="item in delivery.packedItems" :key="item.itemId">
                        <div class="divTableCell firstCol center-text">{{item.counter}}</div>
                        <div class="divTableCell secondCol">{{ item.partNumber.toUpperCase() }}</div>
                        <div class="divTableCell thirdCol">{{ item.description.toUpperCase() }}</div>
                        <div class="divTableCell fourthCol">{{ item.quantity }}</div>
                        <div class="divTableCell fifthCol center-text">{{ item.unitMeasure.toUpperCase() }}</div>
                    </div>
                </div>
            </div>
        </div>
        <div v-else>
        </div>
    </div>
    <button id="cancel" class="btn" @click="handleBack">Anuluj</button>


</div> 
<CreatedModal/>
</template>

<script>
import moment from "moment";
import { computed, ref } from "@vue/reactivity";
import getForwarders from "../composables/getForwarders.js";
import addDelivery from "../composables/addDelivery.js";
import { onMounted, watch, watchEffect } from "@vue/runtime-core";
import urlHolder from "../composables/urlHolder.js";
import PackedItemsAdd from "../components/PackedItemsAdd.vue";
import getDeliveryDetails from '../composables/getDeliveryDetails.js';
import { useStore } from 'vuex'
import CreatedModal from '../components/CreatedModal.vue'
import { useRouter } from 'vue-router'

export default {
  props: ["vId", "vendorName", "orderNo", "orderId"],
  components: { PackedItemsAdd, CreatedModal },
  setup(props) {
    const mainUrl = urlHolder;
    const store = useStore()
    const user = computed(()=> store.getters.getUser)
    const userToken = computed(()=> store.getters.getUserToken)
    const router = useRouter()

    const eta = ref(moment().format("YYYY-MM-DDThh:mm"));
    const comment = ref("");
    const selectedForwarderId = ref("");
    const packedItemsFlag = ref(false);
    let counter = 1
    const { loadForwarders, error, forwarders } = getForwarders( mainUrl, userToken.value);
    const { addNewDelivery, error: deliveryError, createdId } = addDelivery(mainUrl, userToken.value );
    const { loadDetails, error:loadedDeliveryError, delivery } = getDeliveryDetails ( mainUrl, userToken.value )

    const handleBack = ()=>{
        router.back()
    }

    onMounted(() => {
      loadForwarders(1, 100);
    });

    const handleNewDeliverySubmit = async () => {
        let createdAt = moment().toISOString();
        error.value = null;

        const deliveryData = {
            createdAt: createdAt,
            etaDate: eta.value,
            deliveryDate: eta.value,
            rating: 0,
            comment: comment.value,
            forwarderId: selectedForwarderId.value,
            statusId: 1,
            isPriority: false
        }
        
        await addNewDelivery(deliveryData, props.orderId);
        if (!deliveryError.value) {
            document.getElementById("mycreated-modal").style.display="block";
            packedItemsFlag.value = true;
            await loadDetails(createdId.value)
        }
    }

    const handleRefreshTable = async() =>{
        await loadDetails(createdId.value, false)
            .then( function(){
                counter = 1
                delivery.value.packedItems.forEach(item =>{
                            item['counter'] = counter++
                        })
                    })
    }

    return {
        user,
        userToken,
        handleNewDeliverySubmit,
        handleBack,
        eta,
        comment,
        forwarders,
        error,
        deliveryError,
        selectedForwarderId,
        packedItemsFlag,
        createdId,
        loadedDeliveryError,
        delivery,
        handleRefreshTable
    };
  },
};
</script>

<style>
#cancel{
    margin-top:40px;
}
form.add-delivery {
  background-color: transparent;
  box-shadow: none;
  min-width: 700px;
}
form.add-delivery h2{
    color: var(--yellow);
}
form.add-delivery #add-delivery-h3{
    margin: 25px 0;
}
form.add-delivery .add-form-header{
    border-bottom: solid 2px #666666;
    padding-bottom: 10px;
    margin-bottom: 40px;
}

form.add-delivery .double{
    display: grid;
    grid-template-columns: 2fr 3fr;
    column-gap: 30px;
    align-self: end;
    margin-bottom: 25px;
}

form .comment {
  height: 140px;
  resize: none;
}

div.blueTable {
  margin: 20px auto;
  background-color: #363636;
  width: 100%;
  max-width: 880px;
  text-align: left;
}
.divTable.blueTable .divTableCell{
  padding: 6px 10px;
}
.divTable.blueTable .divTableHead{
    padding:15px 10px;
}

.divTable.blueTable .firstCol {
  width: 50px;
}
.divTable.blueTable .secondCol {
  width: 160px;
}
.divTable.blueTable .thirdCol {
  width: 200px;
}
.divTable.blueTable .fourthCol {
  width: 100px;
}
.divTable.blueTable .fifthCol {
  width: 50px;
}

.divTable.blueTable .divTableBody .divTableCell {
  font-size: 0.9em;
  color: #ffffff;
  height: 30px;
  vertical-align: middle;
}
.divTable.blueTable .divTableRow:nth-child(even) {
  background: #424242;
}
.divTable.blueTable .divTableHeading {
  background: #686868;
}
.divTable.blueTable .divTableHeading .divTableHead {
  font-size: 0.8em;
  font-weight: 500;
  color: #ffffff;
}
.blueTable .tableFootStyle {
  font-size: 14px;
  font-weight: bold;
  color: #ffffff;
  border-top: 2px solid #444444;
}
.blueTable .tableFootStyle {
  font-size: 14px;
}
.blueTable .tableFootStyle .links {
  text-align: right;
}
.blueTable .tableFootStyle .links a {
  display: inline-block;
  background: #3b3b3b;
  color: #ffffff;
  padding: 4px 10px;
  border-radius: 0px;
  margin: 5px 0px;
}

.divTable {
  display: table;
}
.divTableRow {
  display: table-row;
}
.divTableHeading {
  display: table-header-group;
}
.divTableCell,
.divTableHead {
  display: table-cell;
}
.divTableHeading {
  display: table-header-group;
}

.divTableBody {
  display: table-row-group;
}
</style>