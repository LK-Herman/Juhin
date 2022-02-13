import { ref } from '@vue/reactivity'
import axios from 'axios'

const getDeliveryDocs = (url, token) =>{

    const docs = ref([])
    const error = ref(null)
    
    const loadDocs = async (id) => {
        
            try 
            {
                let resp = await axios.get(url + 'documents/delivery/'+id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                docs.value = resp.data
                // console.log(vendors.value)
            } 
            catch (er) 
            {
                error.value = er.message
                console.log(error.value)
            }
      
        
        
    }
      return {loadDocs, error, docs}
}

export default getDeliveryDocs