import { ref } from '@vue/reactivity'
import axios from 'axios'

const checkSubscription = (url, token) =>{

    const error = ref(null)
    const isSubscribed = ref()    
    const checkSubs = async (userId, deliveryId) => {

        try 
        {
            let resp = await axios.get(url + 'subscriptions/isActive?userId=' + userId + '&deliveryId=' + deliveryId , {
                headers: {'Authorization':'Bearer ' + token,
                        'Accept':'*/*'
                }
            })
            //console.log(resp)
            if (resp.status != 200)
            {
                throw Error()
            }
            isSubscribed.value = resp.data
            // console.log(resp.data)
        } 
        catch (er) 
        {
            error.value = er.message
        }
      }

      return { checkSubs, error, isSubscribed }
}

export default checkSubscription