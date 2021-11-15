<template>
    <Push
      :closeOnNavigation="true"
      :disableOutsideClick="false"
      :disableEsc="false"
      :isOpen="false"
      :burgerIcon="true" 
      width="70"
      height="100%"
      noOverlay
      :crossIcon="false">
      <router-link to="/"><font-awesome-icon icon="camera" size="2x" :style="{ color: 'white' }" /></router-link>
      <router-link v-if="currentUser.profil !== 'Hotline'" to="/export"><font-awesome-icon icon="file-export" size="2x" :style="{ color: 'white' }"/></router-link>
      <router-link v-if="currentUser.profil === 'Administrateur'" to="/admin"><font-awesome-icon icon="cog" size="2x" :style="{ color: 'white' }"/></router-link>
    </Push>
</template>

<script>
import { Push } from 'vue-burger-menu';  // import the CSS transitions you wish to use, in this case we are using `Slide`
import { UserMixin } from '@/mixins/userMixin';

export default {
  mixins: [UserMixin],
  data() {
    return {
      currentUser: {}
    }
  },
  components: {
      Push // Register your component
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

.router-link-exact-active{
  background-color: #24364b;
  border-left: 3px solid orangered;
}

.bm-item-list {
  color: #b8b7ad;
  font-size: 20px;
  margin-left: 0px;
}

.bm-menu {
    background-color: #182538 !important;
    height: 100%;
    left: -6px !important;
    overflow-x: hidden;
    padding-top: 60px;
    position: fixed;
    transition: .5s;
    width: 0;
    z-index: 1000;
    top: 50px;
}

.bm-burger-button {
    cursor: pointer;
    color: white !important;
    height: 30px;
    z-index: 2000;
    width: 36px;
    left: 18px !important;
    top: 10px !important;
    position: fixed !important;
}

.bm-burger-bars {
    background-color: #d6d9dc !important;
}
</style>
