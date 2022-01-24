import { ref } from '@vue/reactivity'
import axios from 'axios'

const getDeliveriesByDates = (url, token) =>{

    const deliveries = ref()
    const error = ref(null)
    
    const loadDeliveriesByDates = async (startDate, endDate) => {
        try {
            let resp = await axios.get(url + 'deliveries/dates?startDate=' + startDate +'&endDate='+endDate, {
                headers: {'Authorization':'Bearer ' + token,
                'Accept':'*/*',
                'Access-Control-Allow-Origin':'*' }})
            if (resp.status != 200)
            {
                if(resp.status != 404)
                    throw Error('Coś poszło nie tak..')
                throw Error()
            }
            deliveries.value = resp.data
            console.log(deliveries.value)
            
        } catch (er) {
            error.value = er.message
        }
    }
    
    return {loadDeliveriesByDates, error, deliveries }
}

export default getDeliveriesByDates