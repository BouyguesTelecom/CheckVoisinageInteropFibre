<template>
<div class="container text-light rounded custom-container">
    <div class="row">
      <template>
        <HeaderParams heigth="100px" />
      </template>
    </div>
    <div class="row mt-2">
      <template>
        <ExportForm />
      </template>
    </div>

    <DxLoadPanel :show-indicator="true"
                :show-pane="true"
                :shading="true"
                :close-on-outside-click="true"
                shading-color="rgba(0,0,0,0.4)" />
</div>

</template>
<script lang="ts">
    import HeaderParams from '@/components/export/HeaderParams.vue';
    import ExportForm from '@/components/export/ExportForm.vue';
    import { DxLoadPanel } from 'devextreme-vue/load-panel';

    export default {
        name: "ParamsDataGrid",
        components: {
            DxLoadPanel, HeaderParams, ExportForm
        },
        data() {
            return {
            };
        },
        mounted() {
        },
        methods: {
        }
    }
</script>
<style scoped>
</style>