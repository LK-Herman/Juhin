import { ref } from '@vue/reactivity'
import axios from 'axios'

const getCurrencyById = (url, token) =>{

    const currency = ref()
    const error = ref(null)
    
    const loadCurrencyById = async (id) => {

        try {
                let resp = await axios.get(url + 'currencies/'+id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                currency.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        //console.log(currency.value)

      }

      return {loadCurrencyById, error, currency}
}

export default getCurrencyById
