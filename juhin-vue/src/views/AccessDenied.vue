<template>
<div class="main-deniedcontainer">

    <div class="deniedcontainer">
    <h2 class="denieditem">BRAK DOSTĘPU</h2>
    <h3 class="denieditem"><span class="material-icons">no_accounts</span></h3>
    <h3 class="denieditem">Skontaktuj się z administratorem w celu przyznania dostępu do tej strony.</h3>
    </div>
  
</div>
</template>

<script>
export default {

}
</script>

<style>
.main-deniedcontainer{
    min-width: 1000px;
    
}
.deniedcontainer{
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
.deniedcontainer .denieditem{
    margin: 15px 0;
}
.deniedcontainer h3.denieditem{
    text-align: center;
    color: #999;
}
.deniedcontainer .denieditem span{
    font-size: 80px;
    /* text-shadow: 4px 4px 6px rgba(15,15,15,0.5); */
    color: #dd0025;
}

</style>