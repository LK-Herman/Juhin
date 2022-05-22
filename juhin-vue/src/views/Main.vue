<template>
    <div>
        <div class="main-page">
            <div class="desc01">
                <div class="header-text">
                    
                    <h2><span class="material-icons">warehouse</span> JUHIN</h2>
                    <h4 >WAREHOUSE MANAGEMENT</h4>
                </div>
                <p>JUHIN jest aplikacją webową do zarządzania i{{'\xa0'}}monitorowania procesami dostaw komponentów w{{'\xa0'}}firmie produkcyjnej.</p>
                <p>Celem wdrożenia aplikacji jest usprawnienie pracy personelu magazynu oraz specjalistów-logistyków odpowiedzialnych za zaopatrzenie linii produkcyjnych.</p>
                <p>Aplikacja  umożliwi  opcję mierzenia terminowości przewoźników/dostawców. Wspomoże zarządzanie magazynem, oraz ułatwi komunikacje z wszystkimi uczestnikami procesu</p>
                
            </div>
            <div class="war01">
                <img id="war01-img" src="@/assets/images/warehouse11.jpg"/>
            </div>                
        </div>
        <div class="main-page second-page">
                <div class="war02">
                    <img id="war02-img" src="@/assets/images/warehouse03.jpg"/>
                </div>                
                <div class="desc02">
                    <h3>ROLE UŻYTKOWNIKÓW</h3>
                    <br>
                    <p>Użytkownicy aplikacji podzieleni będą na cztery grupy, w{{'\xa0'}}zależności od spełnianej w{{'\xa0'}}systemie roli. </p>
                    <p><b>Administrator systemu</b> - posiada pełen pakiet uprawnień do zarządzania użytkownikami systemu.</p>
                    <p><b>Specjalista</b> – posiada uprawnienia do wprowadzania, edycji, usuwania danych oraz dokumentów.</p>
                    <p><b>Magazynier</b> – posiada uprawnienia do wprowadzania, edycji, usuwania danych.</p>
                    <p><b>Gość</b> – posiada uprawnienia do przeglądania danych.</p>
                </div>
        </div>

       
        <div class="main-page third-page">

        

        </div>
        
    </div>
</template>

<script>

export default {
  setup() {

    return {  }
  }
}
</script>
<style>

.main-page{
    display: grid;
    grid-template-columns: 3fr 2fr;
    background-color: var(--back-grey2);
    margin-bottom: 40px;
    /* background-image: url("../assets/images/warehouse02.jpg"); */
    background-image: linear-gradient(to right bottom, #333333,#666);
    
}
.main-page p{
    margin: 12px 0;
}
.main-page .desc01{
    padding: 30px 60px;
    /* max-height: 420px; */
    /* text-align: justify; */
}
.main-page .desc02{
    padding: 30px 60px;
    /* max-height: 420px; */
    /* text-align: justify; */
}
.main-page .desc02 h3{
    margin-top: 40px;
}

.main-page .war01{
    display: flex;
    padding: 0;
    margin: 0;
    justify-content: end;
}
.second-page{
        grid-template-columns: 2fr 3fr;
    }
.main-page .war02{
    display: flex;
    justify-self: start;
    /* grid-column: 1 / 3; */
    max-height: 400px;
    
}
.main-page #war02-img{
    
    height: 100%;
}
.main-page .war01 #war01-img{
    max-width: 600px;
    padding: 0px;
    margin:0;
}
#image1{
    max-width: 440px;
}
.third-page{
        grid-template-columns: 1141px;
    }
.third-page h3{
    margin-top: 40px;
}
#image2{
    width: 90%;
}
#picture2{
    display: flex;
    
    padding: 15px 0;
    align-items: center;
    justify-content: center;
}
/* .main-page .header-text{
    display: flex;
    flex-direction:row;
    justify-items: start;
} */
.main-page .header-text{
    margin:40px 0;
}
.main-page .material-icons, h2{
    font-size: 26px;
    padding:0;
    margin: 0;
}
#picture h2{
    font-family: 'Amaranth', sans-serif;
}
#picture2 img{
    max-width: 800px;
}
#helmet{
    width: 50px;
    padding: 0 10px;
}
</style>
