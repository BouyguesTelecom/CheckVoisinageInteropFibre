<template>
  <div class="footer-fixed dx-field-label">
    © FitDev<br/>
    {{ currentYear }}<br/>
  </div>    
</template>

<script lang="ts">

export default {
  name: 'FooterFixed',
  data() {
    return {
      currentYear: new Date().getFullYear()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
/* The navigation bar */
.footer-fixed {
  font-size: 11px;
  overflow: hidden;
  color:#d6d9dc;
  position: fixed; /* Set the navbar to fixed position */
  bottom: 0; /* Position the navbar at the top of the page */
  height: 45px;
  width: 100%; 
  z-index: 1001;
}
</style>
