import { ref } from '@vue/reactivity'
import axios from 'axios'

const editItemById = (url, token) =>{

    const error = ref(null)

    const editItem =  async (id, itemData) => {

        var requestOptions = {
        method: 'PUT',
        headers: {
            "Accept":"*/*",
            'Content-Type':'application/json',
            "Access-Control-Allow-Origin": "*",
            "Authorization":"Bearer " + token
        },
        mode:'cors',
        };
      
        try {
            let resp = await axios.put( url + 'items/'+ id, itemData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {editItem, error}
}
export default editItemById