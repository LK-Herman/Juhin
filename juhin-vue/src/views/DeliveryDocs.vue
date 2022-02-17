<template>
<button id="backButton" @click="handleBack"><span class="material-icons">arrow_back</span> Powrót do dostawy</button>
  <h3>Lista dokumentów</h3>
    <div class="docs-container">
        <div class="documents-list" v-if="!error && docs.length != 0">
            <div class="doc" v-for="document in docs" :key="document.documentId">
                <div id="docicon">
                    <span class="material-icons">description</span>
                </div>
                <div>
                    <p id="header">NUMER DOKUMENTU</p>
                    <p>{{document.number}}</p>
                </div>
                <div>
                    <p id="header">TYP DOKUMENTU</p>
                    <p>{{document.type}}</p>
                </div>

                <a class="btn" :href="document.documentFile" target="_blank" :download="document.number">Pobierz dokument</a>
                
            </div>
        </div>
        <div class="documents-list" v-else>
            <div class="doc">
                <h3 id="nodocs">Brak dokumentów</h3>
            </div>
        </div>


        <div class="docs-form">
            <form @submit.prevent="handleSubmit">
                <h3>Dodaj dokument</h3>
                <div class="tripleform">

                    <div>
                        <label>Numer dokumentu</label>
                        <input type="text" v-model="formName">

                    </div>
                    <div>
                        <label>Rodzaj dokumentu</label>
                        <input type="text" v-model="formType">
                    </div>
                    
                        <div>
                            <input type="file" id="file"  @change="onFileChange" /> 
                            <label for="file"><span class="material-icons">cloud_upload</span>Wczytaj plik</label>
                        </div>
                        <label>Wybrany plik:</label>
                        <div class="formfile">
                            <p id="fileName" v-if="formFile != null"> <span class="material-icons">description</span> Nazwa: {{formFile.name}}</p>
                            <p id="fileName" v-if="formFile != null"><span class="material-icons">quiz</span> Typ: {{formFile.type}}</p>
                            <p id="fileName" v-if="formFile != null"><span class="material-icons">fitness_center</span> Rozmiar: {{((Math.ceil(formFile.size / 1024))/1024).toFixed(2)}} MB</p>
                            
                        </div>
                    </div>
                    <button>Dodaj dokument</button>
                    <div class="adderror" v-if="addError != null">
                        <div v-for="err in addError" :key="err">
                            <div v-for="value in err" :key="value">
                                <p>
                                    {{value.includes('Number')?'Numer dokumentu jest wymagany.':''}}
                                    {{value.includes('Type')?'Typ dokumentu jest wymagany.':''}}
                                    {{value.includes('Content type')?'Format dokumentu jest nieobsługiwany. Wybierz plik typu PDF, DOC, XLS, JPG, JPEG.':''}}
                                    {{value.includes('File size')?'Rozmiar pliku nie może przekraczać 4 MB.':''}}
                                </p>
                            </div>
                        </div>
                    </div>
            </form>
        </div>
            
    </div>
    <CreatedModal/>
</template>

<script>
import { computed, ref } from '@vue/reactivity'
import getDeliveryDocs from '../composables/getDeliveryDocs.js'
import addDocument from '../composables/addDocument.js'
import { useStore } from 'vuex'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, watch } from '@vue/runtime-core'
import CreatedModal from '../components/CreatedModal.vue'
import {useRouter} from 'vue-router'

export default {
    components: {CreatedModal},
    props:['id'],
    setup(props){
        const router = useRouter()
        const store = useStore()
        const mainUrl = urlHolder
        const userToken = computed(()=> store.getters.getUserToken)
        const {loadDocs, error, docs} = getDeliveryDocs(mainUrl, userToken.value)
        const {addNewDoc, error:addError, response} = addDocument(mainUrl, userToken.value)
        const formFile = ref()
        const formType = ref()
        const formName = ref()

        const handleBack = () =>{
            router.go(-1)
        }

        const onFileChange = (e)=>{
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length) return;
            console.log(e)
            formFile.value = files[0]
            
            // this.createImage(files[0]);
        }

        onMounted(()=>
        {
            loadDocs(props.id)
        })

        const handleSubmit = ()=>
        {
            addError.value = null
            var docData = new FormData();
            if(formName.value) docData.append("number", formName.value)
            if(formType.value) docData.append("type", formType.value)
            if(formFile.value) docData.append("DocumentFile", formFile.value)
            if(props.id) docData.append("deliveryId", props.id )

            console.log(docData)
            addNewDoc(docData)
                .then(()=>{
                    if(!addError.value){
                        loadDocs(props.id)
                        formFile.value = null
                        formType.value = ''
                        formName.value = ''
                        document.getElementById("mycreated-modal").style.display="block"
                    }
                })
              
        }
      

        return {handleBack, docs, error, addError, formFile, formType, formName, onFileChange, handleSubmit}
    }

}
</script>

<style>
#backButton{
    margin-bottom: 40px;
}
#backButton span{
    margin-right: 10px;
}
.adderror{
    padding: 10px;
    }
.adderror p{
    color: rgb(255, 0, 0);
    font-size: 12px;
    margin: 10px 0;
    word-wrap: break-word;
}
.docs-container{
    display: grid;
    grid-template-columns: 3fr 2fr;
    gap: 40px;
}
.docs-container .documents-list{
    margin-top: 20px;
    }
.docs-container .documents-list .doc{
    display: grid;
    grid-template-columns: 1fr 3fr 3fr;
    /* background-color: rgb(70, 70, 70); */
    border: solid 3px rgb(70, 70, 70);
    margin-bottom:20px;  
    padding: 10px 16px;
}
.docs-container .documents-list .doc #nodocs{
     grid-column-start: 1;
    grid-column-end: 4;
    grid-row-start: 1;
    grid-row-end: 3;
    align-self: center;
    justify-self: center;
    margin: 30px 0;

}
.docs-container .documents-list .doc #docicon{
    grid-column-start: 1;
    grid-column-end: 1;
    grid-row-start: 1;
    grid-row-end: 3;
    align-self: center;
}
.docs-container .documents-list .doc #docicon span{
    color: #ffffff;
    font-size: 40px;
}
.docs-container .documents-list .doc #header{
    font-size: 12px;
    font-weight: 500;
    margin-bottom: 6px;
    color:#bbb;
}

.docs-container .documents-list .doc .btn{
    grid-column-start: 2;
    grid-column-end: 4;
    margin: 10px 0px;
}
.docs-form form{
    margin-top: 20px;
    min-width: 200px;
    max-width: 300px;
    background-color: transparent;
    box-shadow: none;
    border: solid 3px rgb(70, 70, 70);
}
.docs-form form .tripleform{
    
    max-width: 300px;
    min-width: 200px;
}
.docs-form form h3{
    margin:0 0 20px 0;
}
.docs-form form button{
    display: flex;
    justify-self: center;
    margin: 40px auto 0 auto;
}

.docs-form form .tripleform input{
    padding: 10px 10px;
    margin: 10px 0;
    display: inline-block;
}
.docs-form form .tripleform .formfile{
    display: flex;
    flex-direction: column;
    min-height: 40px;
    padding: 6px 20px;
    margin: 10px 0 20px 0;
    background: #3a3a3a;
    box-shadow: inset 1px 1px 5px rgba(20,20,20,0.5);
    overflow: hidden;
}
.docs-form form .tripleform .formfile #fileName {
    margin-bottom: 6px ;
    word-break: break-all;
    color: #fff;
    font-size: 12px;
}
.docs-form form .tripleform input[type="file"] {
    border: 0;
    clip: rect(0, 0, 0, 0);
    height: 1px;
    overflow: hidden;
    padding: 10px;
    position: absolute !important;
    white-space: nowrap;
    width: 1px;
}
.docs-form form .tripleform label span{
    margin-right: 20px;
}

.docs-form form .tripleform input[type="file"] + label {
  /* File upload button styles */
    display: flex;;
    background-color: #046475;
    cursor: pointer;
    
    padding: 8px 10px;
    text-align: center;
    margin: 16px auto;
    justify-self: center;
    align-items: center;
    width: 140px;
    font-size: 14px;
    
}

.docs-form form .tripleform input[type="file"]:focus + label,
.docs-form form .tripleform input[type="file"] + label:hover {
    background-color: var(--orange);
}

.docs-form form .tripleform input[type="file"]:focus + label {
    /* File upload focus state button styles */
    background-color: #008fa8;
}
</style>