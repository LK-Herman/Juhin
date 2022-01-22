import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './assets/main.css'
import './assets/tables.css'
import urlHolder from'./composables/urlHolder.js'
import store from './store/index'

createApp(App)
    .use(router)
    .use(store)
    .mount('#app')
