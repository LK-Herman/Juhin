import { ref } from '@vue/reactivity'
import axios from 'axios'
import {useStore} from 'vuex'

const loginUser = (url) =>{
    
    let loginData = null
    const error = ref('')
    const store = useStore()
    // const token = ref('')
    
    const login = async (userEmail, password) => 
    {
        let userData = {emailAddress:userEmail, password:password}
        var requestOptions = 
        {
            body:'raw',
            method: 'POST',
            mode: 'cors',               
            headers: 
            {
                'Accept':'*/*',
                'Content-Type':'application/json',
                'Accept':'*/*'
            },  
            // body: JSON.stringify(userData)
        }
    
        try 
        {
            let resp = await axios.post(url + 'accounts/Login/',userData, requestOptions)
            if (resp.status !== 200)
            {
                if(resp.status == 400)
                {
                    throw Error('Niepoprawne dane logowania (400)')
                }
                throw Error('Dane niedostępne')
            }
            loginData = resp.data
            
            store.commit('setExpiration', new Date (loginData.expiration))
            store.commit('setUserToken',loginData.token)
            store.commit('setIsLogged',true)
            localStorage.expiration = loginData.expiration
            localStorage.token = loginData.token
            // token.value = loginData.value.token
            // console.log(loginData.value)
            error.value = ''
        } 
        catch (er) 
        {
                error.value = er.message
                //console.log(error.value)
        }
    }
    
    return {login, error}
}
export default loginUser