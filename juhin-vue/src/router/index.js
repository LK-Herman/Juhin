import { createRouter, createWebHistory } from 'vue-router'
import Main from '../views/Main.vue'
import Login from '../views/Login.vue'
import Error404 from '../views/Error404.vue'

import Upcoming from '../views/Upcoming.vue'
import Deliveries from '../views/Deliveries.vue'
import DeliverySearch from '../views/DeliverySearch.vue'
import DeliverySchedule from '../views/DeliverySchedule.vue'
import DeliveryDetails from '../views/DeliveryDetails.vue'
import DeliveryAdd from '../views/DeliveryAdd.vue'
import Orders from '../views/Orders.vue'
import OrderAdd from '../views/OrderAdd.vue'
import Items from '../views/Items.vue'
import ItemDetails from '../views/ItemDetails.vue'
import ItemAdd from '../views/ItemAdd.vue'
import Vendors from '../views/Vendors.vue'
import VendorAdd from '../views/VendorAdd.vue'
import Forwarders from '../views/Forwarders.vue'
import ForwarderAdd from '../views/ForwarderAdd.vue'
import ForwarderRanking from '../views/ForwarderRanking.vue'
import VendorDetails from '../views/VendorDetails.vue' 
import OrderDetails from '../views/OrderDetails.vue' 
import Created from '../views/Created.vue'
import store from '../store/index.js';
import getCurrentUser from '../composables/getCurrentUser.js'
import urlHolder from '../composables/urlHolder.js'
// import getCurrentUser from '../composables/getCurrentUser.js'

// const requireAuth = (to, from, next)=>{
    
// }

const requiredAuth = (to, from, next) => {
    let expireTime = new Date(localStorage.expiration)
    let actualTime = new Date()
    // const mainUrl = urlHolder
    // const {getUser} = getCurrentUser(mainUrl)
    // console.log(store)
    // console.log(expireTime)
    // console.log(actualTime)
    if (!expireTime || expireTime < actualTime){
        store.commit('setIsLogged', false)
        next({name: 'Login'})
    } else {
        next()
    }
  }

const routes = [
  {
    path: '/',
    name: 'Main',
    component: Main,
    props: true,
    beforeEnter: requiredAuth
  },
//   {
//     path: '/*',
//     name: 'Error404',
//     component: Error404,
//     beforeEnter: requiredAuth
//   },
//   {
//     path: '/hangfire',
//     beforeEnter: requiredAuth
//   },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    props: true,
    
  },
  {
      path: '/deliveries/upcomming',
      name: 'Upcoming',
      component: Upcoming,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/deliveries',
      name: 'Deliveries',
      component: Deliveries,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/delivery/:id',
      name: 'DeliveryDetails',
      component: DeliveryDetails,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/deliveries/search',
      name: 'DeliverySearch',
      component: DeliverySearch,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/deliveries/add/:vId',
      name: 'DeliveryAdd',
      component: DeliveryAdd,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/deliveries/schedule',
      name: 'DeliverySchedule',
      component: DeliverySchedule,
      props: true,
      beforeEnter: requiredAuth
  },
   {
      path: '/orders',
      name: 'Orders',
      component: Orders,
      props: true,
      beforeEnter: requiredAuth
  },
//   {
//       path: '/orders/:orderId',
//       name: 'OrderDetails',
//       component: OrderDetails,
//       props: true
//   },
  {
      path: '/orders/add',
      name: 'OrderAdd',
      component: OrderAdd,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/items',
      name: 'Items',
      component: Items,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/items/details/:id',
      name: 'ItemDetails',
      component: ItemDetails,
      props: true,
      beforeEnter: requiredAuth
  },
  {
      path: '/items/add',
      name: 'ItemAdd',
      component: ItemAdd,
      props: true,
      beforeEnter: requiredAuth
  },
  {
    path: '/vendors',
    name: 'Vendors',
    component: Vendors,
    props: true,
    beforeEnter: requiredAuth
  },
  {
    path: '/vendors/:vId',
    name: 'VendorDetails',
    component: VendorDetails,
    props: true,
    beforeEnter: requiredAuth
  },
  {
    path: '/vendors/add',
    name: 'VendorAdd',
    component: VendorAdd,
    props: true,
    beforeEnter: requiredAuth
  },
  {
    path: '/forwarders',
    name: 'Forwarders',
    component: Forwarders,
    props: true,
    beforeEnter: requiredAuth
  },
  {
    path: '/forwarders/add',
    name: 'ForwarderAdd',
    component: ForwarderAdd,
    props: true,
    beforeEnter: requiredAuth
  },
  {
    path: '/forwarders/ranking',
    name: 'ForwarderRanking',
    component: ForwarderRanking,
    props: true,
    beforeEnter: requiredAuth
  },
  {
    path: '/ok',
    name: 'Created',
    component: Created,
    props: true,
    beforeEnter: requiredAuth
  }
 
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
