<template>
  <div class="container rounded" v-bind:style="{ height:heigth }">
    <div class="row mt-2">
      <div class="col">
        <div class="row">
          <div class="col text-left dx-field-item-label-text">ID Intervention</div>
          <div class="col">{{ selectedIntervention.name }}</div>
        </div>
        <div class="row">
          <div class="col text-left dx-field-item-label-text">Opérateur</div>
          <div class="col">{{ selectedIntervention.operator ? selectedIntervention.operator.name : ''}}</div>
        </div>
        <div class="row">
          <div class="col text-left dx-field-item-label-text">Date/heure d'arrivée</div>
          <div class="col" v-if="selectedIntervention.beginDate">{{ selectedIntervention.beginDate | moment("DD/MM/YYYY à HH:mm") }}</div>
        </div>
      </div>
      <div class="col border-left">
        <div class="row">
          <div class="col text-left dx-field-item-label-text">Type d'ntervention</div>
          <div class="col">{{ selectedIntervention.type ? selectedIntervention.type.name : ''}}</div>
        </div>
        <div class="row">
          <div class="col text-left dx-field-item-label-text">Nom du PM d'intervention</div>
          <div class="col">{{ selectedIntervention.mutualisationPoint ? selectedIntervention.mutualisationPoint.pmName : ''}}</div>
        </div>
        <div class="row">
          <div class="col text-left dx-field-item-label-text">PTO</div>
          <div class="col" :class="{'text-right': selectedIntervention.unloadedCode}">{{ selectedIntervention.interventionPto }}</div>
          <div v-if="selectedIntervention.unloadedCode" class="col text-right">
            <font-awesome-icon icon="copy" :title="selectedIntervention.unloadedCode" @click="popupVisible = true" />
          </div>
        </div>
      </div>
    </div>    
    <DxPopup
      :visible="popupVisible"
      v-model="popupVisible"
      :drag-enabled="false"
      :close-on-outside-click="true"
      :show-title="true"
      :width="300"
      :height="250"
      @hiding="popupVisible = false"
      title="Code Décharge">
      <h3 style="text-align:center">
        {{selectedIntervention.unloadedCode}}
      </h3>
    </DxPopup>
  </div>
</template>

<script lang="ts">
import DxPopup from 'devextreme-vue/popup';
import { Intervention } from '@/types/DTO/intervention';
import { Getters } from '@/store';

export default {
  name: 'HeaderCreateIntervention',
  props: [
    'heigth'
  ],
  data() {
    let select = {}
    this.$store.watch(
      (_: void, getter: Getters) => select = getter.getCurrentIntervention,
      (newVal: Intervention) => select = newVal
    )
    return {
      selectedIntervention: select as Intervention,
      popupVisible: false
    }
  },
  mounted(){
    this.$store.watch(
      (_: void, getter: Getters) => getter.getCurrentIntervention,
      (newVal: Intervention) => this.selectedIntervention = newVal
    )
  },
  components:{
    DxPopup
  }
}
</script>

<style>
</style>
