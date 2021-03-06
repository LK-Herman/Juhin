import { ref } from '@vue/reactivity'
import axios from 'axios'

const addDelivery = (url, token) =>{

    const error = ref(null)
    const createdId = ref('')

    const addNewDelivery =  async (deliveryData, poId) => {

        var requestOptions = {
        method: 'POST',
        headers: {
            "Accept":"*/*",
            'Content-Type':'application/json',
            "Access-Control-Allow-Origin": "*",
            "Authorization":"Bearer " + token
        },
        mode:'cors',
        };
      
        try {
            let resp = await axios.post( url + 'deliveries/'+ poId, deliveryData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data.deliveryId
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {addNewDelivery, error, createdId}
}
export default addDelivery