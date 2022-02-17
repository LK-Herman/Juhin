import { ref } from '@vue/reactivity'
import axios from 'axios'
import { useStore } from 'vuex'

const getUserById = (url,token) =>{

    const error = ref(null)
    const userData = ref()
    const email = ref()
    const getUser = async (id) => {

        try {
            let data = await axios.get(url + "accounts/User/"+id, {
                method: 'GET',
                headers: {
                    "Authorization":"Bearer " + token,
                    "Accept": "*/*",
                    "Access-Control-Allow-Origin":"*",
                    },
                mode:'cors'
            })
            if (data.status !== 200)
            {
                throw Error('No data available')
            }
            else
            {
                userData.value = data.data
                email.value = data.data.emailAddress
            }
            //   console.log(user.value)
            
      } catch (er) {
        error.value = er.message
        console.log(error.value)
      }
    }

      return {getUser, email, userData, error}
}

export default getUserById