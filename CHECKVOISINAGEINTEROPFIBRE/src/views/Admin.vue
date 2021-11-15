<template>
  <div class="about">
    <params-datagrid></params-datagrid>
  </div>
</template>
<script lang="ts">

import Vue from 'vue';
import ParamsDatagrid from '@/components/admin/ParamsDatagrid.vue'

export default Vue.extend({
  components: {
    ParamsDatagrid
  }
})

</script>
