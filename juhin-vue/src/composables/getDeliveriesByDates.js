import { ref } from '@vue/reactivity'
import axios from 'axios'
import moment from 'moment'

const getDeliveriesByDates = (url, token) =>{

    const deliveries = ref()
    const error = ref(null)
    const delByday = ref()
    const datesWithDeliveries = ref()
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
            
            deliveries.value.forEach(delivery => 
                {
                    delivery['etaHour'] = moment(delivery.etaDate).format("H")
                });
                
                // console.log(deliveries.value)


                datesWithDeliveries.value = []
                let startD = moment(startDate)
                let endD = moment(endDate)
                let daysInRange =  endD.diff(startD,'days')
                
                for (let index = 0; index <= daysInRange; index++) 
                {
                    let actualIndexDate = moment(startD).add(index,'days').format("YYYY-MM-DD")
                    datesWithDeliveries.value[index] = 
                    {
                        date: actualIndexDate,
                        dateShort: moment(startD).add(index,'days').locale("PL").format("DD-MMM").toUpperCase(),
                        dayOfWeek: moment(startD).add(index,'days').locale("PL").format("ddd").toUpperCase(),
                        deliveries: deliveries.value.filter(del => moment(del.etaDate).format("YYYY-MM-DD") == moment(actualIndexDate).format("YYYY-MM-DD") )
                    }
                }
                // console.log(datesWithDeliveries.value)

        } catch (er) {
            error.value = er.message
        }


    }
    
    return {loadDeliveriesByDates, error, datesWithDeliveries }
}

export default getDeliveriesByDates