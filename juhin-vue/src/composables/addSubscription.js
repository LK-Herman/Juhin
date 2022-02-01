import { ref } from '@vue/reactivity'
import axios from 'axios'

const addSubscription = (url, token) =>{

    const error = ref(null)
    const response = ref(null)
    const addSubs =  async (subscriptionData) => {
        
        var requestOptions = {
            method: 'POST',
            headers: {
                "Accept":"*/*",
                "Access-Control-Allow-Origin": "*",
                'Content-Type':'application/json',
                "Authorization":"Bearer " + token
            },
            mode:'cors',
            };
      
        try {
            let resp = await axios.post( url + 'subscriptions/', subscriptionData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            response.value = resp.data
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
         console.log(response.value)
    }
      return {addSubs, error, response}
}
export default addSubscription