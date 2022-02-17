import { ref } from '@vue/reactivity'
import axios from 'axios'

const getUsers = (url, token) =>{

    const usersList = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')
    
    const loadUsers = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'accounts/UsersDetails?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                console.log(resp.headers)
                totalRecords.value = resp.headers["all-records"]
                lastPage.value = resp.headers["totalamountpages"]
                usersList.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }

      }

      return {loadUsers, error, usersList, totalRecords, lastPage}
}

export default getUsers
