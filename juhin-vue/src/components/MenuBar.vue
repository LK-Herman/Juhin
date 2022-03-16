<template>
<div v-if="user" class="menubar">
    <div v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="title-bar">
        <div id="deliveries" class="menu-icon">
             <span class="material-icons">local_shipping</span>
        </div>
        <div>
            <h4 >DOSTAWY</h4>
        </div>
    </div>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" :to="{name:'Upcoming'}" class="deliveries-btn btn">Najbliższe dostawy</router-link>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" :to="{name:'Deliveries'}" class="deliveries-btn btn">Przeglądaj</router-link>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" :to="{name:'DeliverySchedule'}" class="deliveries-btn btn">Grafik dostaw</router-link>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" :to="{name:'DeliverySearch'}" class="deliveries-btn btn">Wyszukaj</router-link>
    <!-- <router-link  class="deliveries-btn btn">Dodaj</router-link> -->
    
    <div v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="title-bar">
        <div id="orders" class="menu-icon">
             <span class="material-icons">shopping_cart</span>
        </div>
        <div>
            <h4>ZAMÓWIENIA</h4>
        </div>
    </div>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" :to="{name:'Orders'}" class="orders-btn btn">Przeglądaj</router-link>
    

     <div v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="title-bar">
        <div id="items" class="menu-icon">
             <span class="material-icons">extension</span>
        </div>
        <div>
            <h4>TOWARY</h4>
        </div>
    </div>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" :to="{name:'Items'}" class="items-btn btn">Przeglądaj</router-link>
    

    <div v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="title-bar">
        <div id="vendors" class="menu-icon">
             <span class="material-icons">factory</span>
        </div>
        <div>
            <h4>DOSTAWCY</h4>
        </div>
    </div>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="vendors-btn btn" :to="{name:'Vendors'}">Przeglądaj</router-link>
    <router-link v-if="user.isSpecialist" class="vendors-btn btn" :to="{name:'VendorAdd'}">Dodaj</router-link>

    <div v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="title-bar">
        <div id="forwarders" class="menu-icon">
             <span class="material-icons">anchor</span>
        </div>
        <div>
            <h4>PRZEWOŹNICY</h4>
        </div>
    </div>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="forwarders-btn btn" :to="{name:'Forwarders'}">Przeglądaj</router-link>
    <router-link v-if="user.isSpecialist" class="forwarders-btn btn" :to="{name:'ForwarderAdd'}">Dodaj</router-link>
    <router-link v-if="user.isGuest || user.isSpecialist || user.isWarehouseman" class="forwarders-btn btn" :to="{name:'ForwarderRanking'}">Ranking</router-link>
    <div v-if="user.isSpecialist" class="title-bar">
        <div id="warehouses" class="menu-icon">
             <span class="material-icons">warehouse</span>
        </div>
        <div>
            <h4>MAGAZYNY</h4>
        </div>
    </div>
    <router-link v-if="user.isSpecialist" class="warehouses-btn btn" :to="{name:'Warehouses'}">Przeglądaj</router-link>
    

    <div v-if="user.isAdmin" class="title-bar">
        <div id="users" class="menu-icon">
             <span class="material-icons">people</span>
        </div>
        <div>
            <h4>UŻYTKOWNICY</h4>
        </div>
    </div>
    <router-link  v-if="user.isAdmin" class="users-btn btn" :to="{name:'Users'}">Przeglądaj</router-link>
</div>

</template>

<script>
import { computed, ref } from '@vue/reactivity'
import { useStore } from 'vuex'
export default {
setup(){
    const store = useStore()
    const user = ref(computed(()=>store.getters.getUser))

    return{ user }
}
}
</script>

<style>
.menubar{
    display: flex;
    flex-flow: column;
    background-color:var(--back-dark);
    padding: 40px 0 0 0;
    margin: 0;
    height: 100%;
    box-shadow: 4px 0px 4px rgba(30,30,30,0.2);
    margin-bottom: 100px;
}
.menubar h4 {
    font-weight: 300;
    margin-left:14px;
}
.menubar .menu-icon{
    /* display: inline-block; */
    display: flex;
    padding: 8px 0;    
    text-align: center;
    width: 60px;
    height: 40px;
    align-items: center;
    justify-content: center;
    margin: 3px 10px 3px 5px;
}
.menubar .menu-icon span{
    font-size: 30px;
}
.menubar #deliveries {
    
    background-color: var(--deliveries);
}
.menubar #orders{
    background-color: var(--orders);
}
.menubar #items{
    background-color: #218FF3;
}
.menubar #vendors{
    background-color: #528322;
}
.menubar #forwarders{
    background-color: #613FC0;
}
.menubar #warehouses{
    background-color: #3322cf;
}
.menubar #users{
    background-color: #175985;
}
.menubar .deliveries-btn:hover{
    background-color: #2A9F98;
}
.menubar .orders-btn:hover{
    background-color: #D445EC;
}
.menubar .items-btn:hover{
    background-color: #218FF3;
}
.menubar .vendors-btn:hover{
    background-color: #528322;
}
.menubar .users-btn:hover{
   background-color: #175985;
}
.menubar .forwarders-btn:hover{
    background-color: #613FC0;
}
.menubar .warehouses-btn:hover{
    background-color: #3322cf;
}

.menubar .title-bar{
    display: grid;
    grid-template-columns: 60px 1fr;
    column-gap: 10px;
} 
.menubar .title-bar div{
    align-self: center;
}
.menubar button, .menubar .btn{
    margin: 3px 0;
    box-shadow: 3px 4px 4px rgba(30,30,30,0.7);
    transform: translateX(5px);
}

</style>