<template>
   <div id="mycreated-modal" class="created-modal">
        <div class="created-modal-content">
                <div class="created-modal-question">
                    <p>Czy na pewno chcesz usunąć?</p>
                </div>
        </div>

    </div>
</template>

<script>
export default {

}
</script>

<style scoped>

.created-modal {
  display: none; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 99; /* Sit on top */
  padding-top: 300px; /* Location of the box */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0,0,0); /* Fallback color */
  background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* created-modal Content */
.created-modal-content {
  position: relative;
  background-color: #313131;
  color: #fff;
  border-radius: 20px;
  margin: auto;
  padding: 20px;
  border: 4px solid rgb(144, 144, 144);
  width: 460px;
  box-shadow: 8px 8px 12px 0 rgba(0, 0, 0, 0.5),-8px -8px 12px 0 rgba(0,0,0,0.5);
  -webkit-animation-name: animatetop;
  -webkit-animation-duration: 0.4s;
  animation-name: animatetop;
  animation-duration: 0.4s
}
.created-modal-content .created-modal-question {
    text-align: center;
    padding: 20px 20px 40px 20px;
}

/* Add Animation */
@-webkit-keyframes animatetop {
  from {top:-300px; opacity:0} 
  to {top:0; opacity:1}
}

@keyframes animatetop {
  from {top:-300px; opacity:0}
  to {top:0; opacity:1}
}

/* The Close Button */
.close {
  color: white;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: #000;
  text-decoration: none;
  cursor: pointer;
}

.created-modal-header {
  padding: 2px 16px;
  background-color: #5cb85c;
  color: white;
}

.created-modal-body {padding: 2px 16px;}

.created-modal-footer {
  padding: 2px 16px;
  background-color: #5cb85c;
  color: white;
}
</style>