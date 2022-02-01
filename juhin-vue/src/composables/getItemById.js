import { ref } from '@vue/reactivity'
import axios from 'axios'

const getItemById = (url, token) =>{

    const item = ref([])
    const error = ref(null)
    
    const loadItem = async (id) => {

        try {
                let resp = await axios.get(url + 'items/'+ id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                item.value = await resp.data
                // console.log(resp.data)
                
            } catch (er) {
        error.value = er.message
        console.log(error.value)
        }
        //console.log(vendors.value)
        //vendors.value.sort(function(a, b){return a.vendorCode - b.vendorCode})

      }

      return {loadItem, error, item}
}

export default getItemById