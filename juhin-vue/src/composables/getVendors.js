import { ref } from '@vue/reactivity'
import axios from 'axios'

const getVendors = (url, token) =>{

    const vendors = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadVendors = async (pageNo, recordsPerPage, name) => {
        if(name == null || name == ''){
            try {
                let resp = await axios.get(url + 'vendors?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                totalRecords.value = resp.headers["all-records"]
                lastPage.value = resp.headers["totalamountpages"]
                vendors.value = resp.data
                // console.log(vendors.value)
            } catch (er) {
                error.value = er.message
                console.log(error.value)
            }
        }else{
            try{
                let resp = await axios.get(url + 'vendors/search/'+ name + '?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
                    headers: {'Authorization':'Bearer ' + token,
                    'Accept':'*/*'
                }
            })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                totalRecords.value = resp.headers["all-records"]
                lastPage.value = resp.headers["totalamountpages"]
                vendors.value = resp.data
                // vendors.value.sort(function(a, b){return ('' + a.name).localeCompare(b.name)})
                
            } catch (er) {
                error.value = er.message
                console.log(error.value)
            }
        }
        
    }


      return {loadVendors, error, vendors, totalRecords}
}

export default getVendors