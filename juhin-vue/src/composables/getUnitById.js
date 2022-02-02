import { ref } from '@vue/reactivity'
import axios from 'axios'

const getUnitById = (url, token) =>{

    const unit = ref([])
    const error = ref(null)
        
    const loadUnitById = async (id) => {

        try {
                let resp = await axios.get(url + 'units/' + id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                unit.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        // console.log(unit.value)

      }

      return { loadUnitById, error, unit }
}

export default getUnitById
