import { ref } from '@vue/reactivity'
import moment from 'moment'
import axios from 'axios'

const getSearchedDeliveries = (url, token) =>{

    const deliveries = ref([])
    const error = ref(null)
    const totalRecords = ref('')
    const lastPage = ref('')
    let requestOptions = 
    {       
        headers: {'Authorization':'Bearer ' + token,
        'Accept':'*/*',
        'Access-Control-Allow-Origin':'*' },
    }
    
    const loadSearchedDeliveries = async (searchData) => {

        try 
        {
                let resp = await axios.post(url + 'deliveries/search', searchData, requestOptions)
                
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                console.log(resp)
                deliveries.value = resp.data
                totalRecords.value= resp.headers['all-records']
                console.log(totalRecords.value)
        } 
        catch (er) 
        {
            error.value = er.message
            //console.log(error.value)
            }

        deliveries.value.forEach(delivery => {
            delivery.etaDate = moment(delivery.etaDate).format('YYYY-MM-DD')
            delivery.createdAt = moment(delivery.createdAt).format('YYYY-MM-DD')
            delivery.purchaseOrders.forEach(order => 
                {
                axios.get(url + 'vendors/' + order.vendorId, requestOptions)
                    .then(res => order['vendorData'] = res.data)
                    .catch(err => console.log(err.message))
                });
            axios.get(url+'forwarders/'+delivery.forwarderId, requestOptions)
                .then(res => delivery['forwarderName'] = res.data.name)
                .catch(err => console.log(err.message))
            axios.get(url+'statuses/'+delivery.statusId, requestOptions)
                .then(res => delivery['statusName'] = res.data.name)
                .catch(err => console.log(err.message))
        });
      }
    
      return {loadSearchedDeliveries, error, deliveries, totalRecords}
}
export default getSearchedDeliveries