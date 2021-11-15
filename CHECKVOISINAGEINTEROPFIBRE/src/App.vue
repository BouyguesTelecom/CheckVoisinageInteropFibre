<template>
  <div id="app" class="app-container">
    <HeaderFixed />
    <Menu />
    <main id="page-wrap">
      <router-view/>
    </main>
    <FooterFixed />
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import Menu from '@/components/shared/Menu.vue'
import HeaderFixed from '@/components/shared/HeaderFixed.vue'
import FooterFixed from '@/components/shared/FooterFixed.vue'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';

Vue.use(Loading, {
  color: '#FFFFFF',
  loader: 'dots',  
  width: 150,
  height: 150,
});
export default Vue.extend({
  components: {
        Menu,
        HeaderFixed,
        FooterFixed
    }
})
</script>
<style scoped>
    @import "./assets/styles/app.css";
</style>
