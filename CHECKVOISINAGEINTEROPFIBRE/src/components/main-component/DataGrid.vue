<template>
  <DxDataGrid :disabled="data.disabled"
              :show-borders="false"
              :data-source="data.dataSource"
              v-model="selectedElement"
              width="100%"
              height="250px"
              :column-auto-width="true"
              :allow-column-resizing="true"
              :allow-column-reordering="true"
              :remote-operations="true"
              @selection-changed="onSelectionChanged">
    <DxSelection mode="single"/>
    <DxColumn v-for="(column, index) in data.columns" :key="index"
            :data-field="column.DataField"
            :data-type="column.Type"
            :caption="column.Caption"
            :visible="column.Visible"
            :format="column.Format"
            :sort-order="column.Sort" />
                          
  </DxDataGrid> 
</template>
<script lang="ts">
import { DxDataGrid, DxColumn, DxSelection} from 'devextreme-vue/data-grid';

export default {
  name: 'DataGrid',
  props: {
    data: {
      type: Object
    }
  },
  components: {
    DxDataGrid,
    DxColumn,
    DxSelection
  },
  data() {
    return {
      selectedElement: {}
    };
  },
  methods: {
    onSelectionChanged({ selectedRowsData } : any) {
  
      const data = selectedRowsData[0];
      this.$emit('selectionChanged', data);
    }
  }
};
</script>
<style scoped>

</style>