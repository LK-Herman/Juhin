import { ref } from '@vue/reactivity'
import axios from 'axios'

const getPalletById = (url, token) =>{

    const pallet = ref([])
    const error = ref(null)
    
    const loadPalletById = async (id) => {

        try {
                let resp = await axios.get(url + 'pallets/'+id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                pallet.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        // console.log(pallet.value)

      }

      return {loadPalletById, error, pallet}
}

export default getPalletById
