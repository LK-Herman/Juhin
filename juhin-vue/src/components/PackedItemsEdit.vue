<template>
  
</template>

<script>
import { computed } from '@vue/reactivity'
import { useStore } from 'vuex'
import urlHolder from '../composables/urlHolder.js'
export default {
    setup(){
        const url = urlHolder
        const store = useStore()

        const user = computed(()=> store.getters.getUser)
        const userToken = computed(()=> store.getters.getUserToken) 

        

        return {  }
    }

}
</script>

<style>

</style>