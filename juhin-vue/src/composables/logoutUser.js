import { ref } from '@vue/reactivity'
import axios from 'axios'
import {useStore} from 'vuex'

const logoutUser = (url) =>{
    
    const logoutData = ref([])
    const error = ref('')
    const store = useStore()
    
    const logout = async () => {
        
        try {
              let data = await axios.post(url + 'accounts/Logout/', 
                { method: 'POST',
                  mode: 'cors'
                })
               
               if (data.status != 200 ){
                   if(data.status === 404){
                       throw Error('Nieudana próba wylogowania (404)')}
                throw Error('Nastąpił błąd podczas próby wylogowania')
                }
                logoutData.value = await data.data
                error.value = ''
                localStorage.removeItem('token')
                localStorage.removeItem('user')
                localStorage.removeItem('expiration')
                localStorage.removeItem('emailAddress')
                store.commit('clearUser')
                store.commit('clearUserToken')
                store.commit('clearExpiration')

              console.log('Logged out successfuly')
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {logout, error, logoutData}
}
export default logoutUser