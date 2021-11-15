<template>
<div class="container text-light rounded custom-container">
    <div class="row">
      <template>
        <HeaderParams heigth="100px" />
      </template>
    </div>
    <div class="row mt-2">
        <div class="col mx-n2">
            <ParamsFirstCall/>
        </div>
        <div class="col mx-n3">
            <ParamsConclusion/>
        </div>
        <div class="col mx-n2">
            <ParamsSubCause/>
        </div>
    </div>
    <div class="row mt-2">
        <div class="col-6 ml-n2">
            <ParamsLocalisation/>
        </div>
        <div class="col-6 p-0">
            <ParamsTypeIntervention/>
        </div>
    </div>

    <DxLoadPanel :show-indicator="true"
                :show-pane="true"
                :shading="true"
                :close-on-outside-click="true"
                shading-color="rgba(0,0,0,0.4)" />
</div>
</template>
<script lang="ts">
    import HeaderParams from '@/components/admin/HeaderParams.vue'
    import ParamsFirstCall from '@/components/admin/ParamsFirstCall.vue'
    import ParamsLocalisation from '@/components/admin/ParamsLocalisation.vue'
    import ParamsTypeIntervention from '@/components/admin/ParamsTypeIntervention.vue'
    import ParamsSubCause from '@/components/admin/ParamsSubCause.vue'
    import ParamsConclusion from '@/components/admin/ParamsConclusion.vue'
  
    import { DxLoadPanel } from 'devextreme-vue/load-panel';

    export default {
        name: "ParamsDataGrid",
        components: {
            DxLoadPanel, HeaderParams, ParamsFirstCall, ParamsLocalisation, ParamsTypeIntervention, ParamsConclusion, ParamsSubCause
        },
        data() {
            return {
            };
        },
        methods: {
        }
    }
</script>
<style scoped>
</style>