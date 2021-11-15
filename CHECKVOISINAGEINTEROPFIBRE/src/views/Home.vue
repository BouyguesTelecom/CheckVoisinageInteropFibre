<template>
  <div class="container text-light rounded custom-container">
    <div class="row">
      <template v-if="selectedTab  == 'plus'">
        <HeaderCreateIntervention heigth="100px" />
      </template>
      <template v-else>        
        <HeaderInfoIntervention heigth="100px" />
      </template>
    </div>
    <div class="row mt-4 mb-3">
      <div class="col">
        <CentreComponent />
      </div>
    </div>
  </div>
</template>

<script lang="ts">

import store from '@/store';
import HeaderCreateIntervention from '@/components/HeaderCreateIntervention.vue'
import HeaderInfoIntervention from '@/components/HeaderInfoIntervention.vue'
import CentreComponent from '@/components/CentreComponent.vue'
import { Getters } from '@/store';

export default {
  name: 'Home',
  data() {
    return {
      selectedTab: 'plus'
    };
  },
  mounted(){
      this.$store.watch(
        (_: void, getter: Getters) => getter.getCurrentTab,
        (newVal: string) => this.selectedTab = newVal
      )
  },
  methods:{
    gridContentReady(e: any){
  

      const grid = e.component;  
      const selection = grid.getSelectedRowKeys();  
      if(selection.length == 0) {  
          grid.selectRowsByIndexes([0]);  
      }  
    },
    initDatas(){
  

      this.selectedTab = store.state.currentTab ? store.state.currentTab : 'plus';

    },
  },
  components: {
    HeaderCreateIntervention,
    HeaderInfoIntervention,
    CentreComponent    
  }
}
</script>
<style>
</style>