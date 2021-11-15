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
          <div class="row">
          <div class="col">
            {{data.Title}}
          </div>
        </div>
        </div>
      </template>
      <template #item="{ data }">
        <FirstClientKOContent :data="data" :initStatusFirstCall="initStatusFirstCall"/>
      </template>
    </DxAccordion>
  </div>
</template>
<script lang="ts">
import DxAccordion from 'devextreme-vue/accordion';
import FirstClientKOContent from '@/components/main-component/FirstClientKOContent.vue';


export default {
  name: 'FirstClientKO',
  props:{
    photoID: Number,
    clientsKO: Array,
    initStatusFirstCall: Boolean
  },
  data() {

    
    return {
      clientKOData: [
        {
          Title: "Clients KO",
          Header: ["SrvID", "Statut 1er appel", "Commentaire",],
          Rows: this.clientsKO
        }
      ],
      collapsible: true,
      multiple: true,
      animationDuration: 400
    }
  },
  watch: { 
    clientsKO: function(newVal:any) { // watch it
  
      if (newVal){
          this.clientKOData[0].Rows = newVal;
      }
    }
  },
  components: {
    DxAccordion,
    FirstClientKOContent
  }
};
</script>

<style scoped>

#back-ground-color {  
  text-decoration-color: #FFF;
  background-color:#212f41;
}

</style>