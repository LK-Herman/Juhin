import { ref } from '@vue/reactivity'
import axios from 'axios'

const addDocument = (url, token) =>{

    const error = ref(null)
    const response = ref(null)
    const addNewDoc =  async (docData) => {

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
      
        
            await axios.post( url + 'documents', docData, requestOptions)
                .then(value =>{
                    response.value = value
                    // console.log(resp)
                }
                 )
                .catch(err => 
                    {
                        error.value = err.response.data.errors
                        console.log(error.value)
                    }
                )
            
                
    }
      return {addNewDoc, error, response}
}
export default addDocument