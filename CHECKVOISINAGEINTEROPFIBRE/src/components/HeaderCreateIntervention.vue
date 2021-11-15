<template>
  <div class="container rounded" v-bind:style="{ height:heigth }">
    <div class="row" style="height:100%">
      <div class="col text-left my-auto">
        Creér une intervention en remplissant les champs ci dessous
      </div>
    </div>
  </div>
</template>

<script lang="ts">

export default {
  name: 'HeaderCreateIntervention',
  props: [
    'heigth'
  ],
  data() {
    return {
    }
  },
  mounted(){
    return {
    }
  }
}
</script>

<style>
</style>
