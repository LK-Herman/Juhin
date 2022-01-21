import { ref } from '@vue/reactivity'
import VueCookies from 'vue-cookies'

const loginUser = (url) =>{
    
    const loginData = ref([])
    const error = ref('')
    // const token = ref('')
    
    const login = async (userEmail, password) => {
        const userData = {emailAddress:userEmail, password:password}
        var requestOptions = {
            body:'raw',
            method: 'POST',
            mode: 'cors',               
            headers: {'Accept':'*/*',
            'Content-Type':'application/json',
            'Accept':'*/*',
            'Accept-Encoding':'gzip, deflate, br',
            'Connection':'keep-alive'
        },
        body: JSON.stringify(userData)
    }
    
    try {
        let data = await fetch(url + 'accounts/Login/', requestOptions)
        if (!data.ok){
            if(data.status === 400){
                throw Error('Niepoprawne dane logowania (400)')}
                throw Error('Dane niedostępne')
            }
            loginData.value = await data.json()
            localStorage.expiration = loginData.value.expiration
            // token.value = loginData.value.token
            localStorage.token = loginData.value.token
            // console.log(loginData.value)
            error.value = ''
        } catch (er) {
            error.value = er.message
            //console.log(error.value)
        }
    }
    
    return {login, error, loginData}
}
export default loginUser