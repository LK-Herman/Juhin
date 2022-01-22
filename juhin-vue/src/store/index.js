import { createStore } from "vuex";

const store =createStore({
    state:{
        userToken: '',
        expiration: null,
        user: {},
        isLogged: false

    },
    getters:{
        getUserToken(state){
            return state.userToken
        },
        getUser(state){
            return state.user
        },
        getIsLogged(state){
            return state.isLogged
        }
    },
    mutations:{
        setIsLogged(state, loginState){
            state.isLogged = loginState
        },
        setUserToken(state, token){
            state.userToken = token
        },
        setExpiration(state, date){
            state.expiration = date
        },
        setUser(state, user){
            state.user = user
        },
        clearUserToken(state){
            state.userToken = ''
        },
        clearExpiration(state){
            state.expiration = null
        },
        clearUser(state){
            state.user = null
        }
    },
    actions:{}
})

export default store