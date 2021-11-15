<template>
    <div class="container rounded">
      <div class="row mt-2">
        <div class="col">
          <div class="row" style="margin-bottom: 20px;">
            <div class="col text-left dx-field-item-label-text">Casse client:</div>
            <div class="col">
              <div v-if="globalInfo.isClientKo != undefined">
                <font-awesome-icon v-if="globalInfo.isClientKo" icon="times" class="NokIcon" title="Au moins un client KO durant l'intervention"  />
                <font-awesome-icon v-if="!globalInfo.isClientKo" icon="check" class="OkIcon" title="Pas de clients KO durant l'intervention" />  
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-8 text-left dx-field-item-label-text">Durée moyenne de coupure en(s):</div>
            <div class="col-4">{{ globalInfo.timingCutAvg ? parseFloat(globalInfo.timingCutAvg).toFixed(2) : null }}</div>
          </div>
          <div class="row">
            <div class="col-8 text-left dx-field-item-label-text">Nb de clients impactés:</div>
            <div class="col-4">{{ globalInfo.impactedClientCount}}</div>
          </div>
        </div>
        <div class="col border-left">
          <div class="row text-center" style="margin-bottom: 20px;"><br></div>
          <div class="row text-center">
            <span style="margin: auto;"><span class="dx-field-item-label-text">Nb de coupures inférieures à 20 sec:</span> {{ globalInfo.inferior20CutCount}}</span>
          </div>
          <div class="row text-center">
            <span style="margin: auto;"><span class="dx-field-item-label-text">Nb de coupures supérieures à 20 sec:</span> {{ globalInfo.superior20CutCount}}</span>
          </div>
        </div>
        <div class="col border-left">
          <div class="row" style="margin-bottom: 20px;">
            <div class="col text-left dx-field-item-label-text">Statut opération:</div>
            <div class="col">
              <div v-if="globalInfo.interventionStatus != undefined">
                <font-awesome-icon v-if="globalInfo.interventionStatus" icon="check" class="OkIcon" title="Pas de clients KO à la fin de l'intervention" />
                <font-awesome-icon v-if="!globalInfo.interventionStatus" icon="times" class="NokIcon" title="Un ou plusieurs clients sont toujours KO à la fin de l'intervention" />
              </div>
            </div>
          </div>
          <div class="row text-left">
            <div class="col dx-field-item-label-text">Nb de client au PM:</div>
            <div class="col">{{ globalInfo.pmCount}}</div>
          </div>
        </div>
      </div>
    </div>
</template>

<script lang="ts">
import { InterventionGlobalInfo } from '@/types/DTO/interventionGlobalInfo';
import { Getters } from '@/store';

export default {
  name: 'HistoryInfo',
  props: [],
  data() {
    return {
        globalInfo: {} as InterventionGlobalInfo
    }
  },
  mounted() {
    this.$store.watch((_: void, getter: Getters) =>
      this.globalInfo = getter.getHistoryInfo ? getter.getHistoryInfo: {}
    );
  }
}
</script>

<style scoped>
    .NokIcon{
        color: red;
        border: 1px solid #ff0000;
        font-size: 20px;
        padding: 2px 2px;
        border-radius: 50%;
        width: 20px;
        height: 20px;
    }
    .OkIcon{
        color: green;
        border: 1px solid green;
        font-size: 20px;
        padding: 2px 2px;
        border-radius: 50%;
        width: 20px;
        height: 20px;
    }
</style>