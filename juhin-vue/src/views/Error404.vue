<template>
<div class="main-error-container">

    <div class="error-container">
    <h2 class="error-item">NIE ZNALEZIONO STRONY</h2>
    <h3 class="error-item"><span class="material-icons">error</span></h3>
    <h3 class="error-item">..coś poszło nie tak.. </h3>
    </div>
  
</div>
</template>

<script>
export default {

}
</script>

<style>
.main-error-container{
    min-width: 1000px;
    
}
.error-container{
    max-width: 450px;
    min-width: 400px;
    display: flex;
    flex-flow: column;
    margin:40px auto;
    align-items: center;
    padding: 40px;
    border: solid 8px #5c5c5c;
    border-radius: 50px;
    box-shadow: 20px 20px 30px rgba(15,15,15,0.5);
}
.error-container .error-item{
    margin: 15px 0;
}
.error-container h3.error-item{
    color: #999;
}
.error-container .error-item span{
    font-size: 80px;
    /* text-shadow: 4px 4px 6px rgba(15,15,15,0.5); */
    color: #dd0025;
}

</style>