import { ref } from '@vue/reactivity'
import axios from 'axios'

const getWarehousById = (url, token) =>{

    const warehouse = ref([])
    const error = ref(null)
    
    const loadWarehouseById = async (id) => {

        try {
                let resp = await axios.get(url + 'warehouses/' + id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                warehouse.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
         console.log(warehouse.value)

      }

      return {loadWarehouseById, error, warehouse}
}

export default getWarehousById