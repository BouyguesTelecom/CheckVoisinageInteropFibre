<template>
  <div id="accordion" class="mt-4">
    <DxAccordion
      :data-source="clientKOData"
      :collapsible="collapsible"
      :multiple="multiple"
      :animation-duration="animationDuration"
      :selected-items="clientKOData[0].Rows && clientKOData[0].Rows.length ? clientKOData : []"
    >
      <template #title="{ data }">
        <div id="back-ground-color">
          <div class="row borderss">
          <div class="col">
            {{data.Title}}
          </div>
        </div>
        </div>
      </template>
      <template #item="{ data }">
        <ManyClientKOContent :data="data"/>
      </template>
    </DxAccordion>
  </div>
</template>
<script lang="ts">
import DxAccordion from 'devextreme-vue/accordion';
import ManyClientKOContent from '@/components/main-component/ManyClientKOContent.vue'
import HomeService from '@/services/HomeService';

export default {
  name: 'ManyClientKO',
  props:{
    photoID: Number,
    clientsKO: Array
  },
  data() {


    return {
      clientKOData: [
        {
          Title: "Clients KO",
          Header: [ {value: "SrvID", colspan:1}, {value: "Statut 1er appel", colspan:1}, {value: "Commentaire", colspan:3}],
          Rows: this.clientsKO
        }
      ],
      collapsible: true,
      multiple: true,
      animationDuration: 400
    }
  },
  watch: { 
    clientsKO: function(newVal: any) { // watch it
  
      if (newVal){
          this.clientKOData[0].Rows = newVal;
      }
    }
  },
  components: {
    DxAccordion,
    ManyClientKOContent
  }
};
</script>

<style scoped>

#back-ground-color {  
  text-decoration-color: #FFF;
  background-color:#212f41;
}
div .borderss{
  border-color: red;
}
</style>