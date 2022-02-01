import { ref } from '@vue/reactivity'
import axios from 'axios'
import { useStore } from 'vuex'

const getCurrentUser = (url) =>{


    const error = ref(null)
    const store = useStore()

    const getUser = async (token) => {

        try {
            let data = await axios.get(url + "accounts/userInfo/", {
                method: 'GET',
                headers: {
                    "Authorization":"Bearer " + token,
                    "Accept": "*/*",
                    "Access-Control-Allow-Origin":"*",
                    },
                mode:'cors'
            })
             if (data.status !== 200){
              throw Error('No data available')
              }
              else{
                  store.commit('setUser', data.data)
              }
              
            //   console.log(user.value)
            //   localStorage.token = user.value.role
      } catch (er) {
        error.value = er.message
        console.log(error.value)
      }
    }

      return {getUser, error}
}

export default getCurrentUser