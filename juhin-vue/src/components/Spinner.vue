<template>
    <div class="spiner-ctnr">
        <div class="spin"></div>
        <div class="spin-label"><p>to potrwa tylko chwilę ...  </p></div>
    </div>
</template>

<script>
export default {

}
</script>

<style>
.spiner-ctnr{
    position: absolute;
    width: 100%;
    height: 100%;
    background: rgba(20,20,20,0.65);
    top: 0;
    border-radius: 0;
    z-index: 1000;
    transition: 300ms ease-in;
}
.spin{
    
    display: block;
    box-shadow: inset 5px 5px #ff8800;
    width: 90px;
    height: 90px;
    margin:40px auto 20px auto;
    border: 4px solid transparent;
    border-radius: 50%;
    /* border-top-color: #ff8800; */
    animation: spin 1s linear infinite;
}
@keyframes spin {
    to { transform: rotateZ(360deg)}
}
.spin-label p{
    margin: 3px auto;
    text-align: center;
}
</style>