<template>
  <div id="accordion" class="mt-4"> 
    <DxAccordion
      :data-source="dataAccordion"
      :collapsible="collapsible"
      :multiple="multiple"
      :animation-duration="animationDuration"      
    >
      <template #title="{ data }">
        <div id="back-ground-color" class="text-left"> {{ data.Title }}</div>
      </template>
      <template #item="{ data }" class="border border light">
        <div id="back-ground-color">
          <DataGrid  v-if="data.Type == 'DataGrid'" :data="data.dataGrid" @selectionChanged="onSelectionChanged"/>
        </div>
      </template>
    </DxAccordion>
  </div>
</template>
<script lang="ts">
import DxAccordion from 'devextreme-vue/accordion';

import DataGrid from '@/components/main-component/DataGrid.vue'

export default {
  name: 'ComponentModel',
  props: {
    dataAccordion: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      collapsible: true,
      multiple: true,
      animationDuration: 700
    }
  },
  components: {
    DxAccordion,
    DataGrid
  },
  methods: {
    onSelectionChanged(data: any) {
  
      this.$emit('selectionChanged', data);
    }
  }
};
</script>
<style scoped>

#back-ground-color {
  background-color:#212f41;
}

</style>