<template>
  <div class="container rounded" v-bind:style="{ height:heigth }">
    <div class="row" style="height:100%">
      <div class="col text-left my-auto">Exporter les interventions</div>
    </div>
  </div>
</template>

<script lang="ts">
 

export default {
  name: 'HeaderParams',
  props: [
    'heigth'
  ]
}
</script>

<style>
</style>
