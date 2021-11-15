<template>
  <div class="header-fixed eyeliner">
    <div class="container" style="max-Width:90% !important">
      <div class="d-flex align-items-center">
        <div class="mr-auto">
              <DxSelectBox name="InterventionSearch"
                              :data-source="interventions"
                              :accept-custom-value="true"
                              v-model="selectedIntervention"
                              :search-enabled="true"
                              :disabled="visibleSearsh"
                              Search-expr="name"
                              display-expr="name"
                              placeholder="Création ou Recherche des interventions"
                              no-data-text="Création d'une intervention"
                              hint="Tapez les premières lettres pour rechercher une intervention"                              
                              @customItemCreating="CreateIntervention($event)"
                              @focusIn="reloadInterventions()"
                              />
        </div>

        <div class="mr-auto"><span class="app-name">Check Voisinage InterOp'fibre</span></div>
        <div class="ml-auto">
          <div class="flex-item mr-2" style="color:#d6d9dc">{{ currentUser.firstName }}</div>
          <div class="flex-item" style="color:#d6d9dc">{{ currentUser.lastName }}</div>
        </div>
      </div>
    </div>
  </div>    
</template>

<script lang="ts">
 
import { DxSelectBox } from 'devextreme-vue/select-box';
import { Intervention } from '@/types/DTO/intervention';
import HomeService from '@/services/HomeService';
import { User } from '@/types/DTO/user';
import { Getters } from '@/store';

const $HomeService = new HomeService();

export default {
  name: 'HeaderFixed',
  data() {    
    return {
      interventions: [] as Array<Intervention>,
      currentUser: {},
      selectedIntervention: {interventionId: 0, name:''} as Intervention,
      selectedInterventionId: 0 
    }
  },
  mounted(){
    this.$store.watch(
      (_: void, getter: Getters) => getter.getNewIntervention,
      (newVal: string) => this.interventions.push(newVal)
    );
    const loader = this.$loading.show();
      this.$store.watch(
      (_: void, getter: Getters) => getter.getCurrentUser,
      (newVal: User) => {
          if (newVal && newVal.profil != undefined && newVal.profil != ''){
            this.currentUser = newVal;
            loader.hide();
          }
          else{
            loader.hide();
          }
      }
    )
  },
  methods:{
    CreateIntervention(data: any) {      
      if(!data.text) {
        return;
      }
  
      this.$store.commit('NEW_INTERVENTION_ID', data.text);
      this.$store.commit('CURRENT_MAIN_TAB', 'plus');
    },
    async reloadInterventions() {
  
      const response =  await $HomeService.GetAllInterventions();
      this.interventions = response;
    }
  },
  watch:{
    selectedIntervention(val: Intervention){
      let currentTab = '';
      this.$store.commit('CURRENT_INTERVENTION', val);
      if(val && val.name) {
        this.$store.watch(
          (_: void, getter: Getters) => currentTab = getter.getCurrentTab,
          (newVal: string) => newVal
        )
        if(currentTab == 'plus' || currentTab == '') this.$store.commit('CURRENT_MAIN_TAB', 'camera');
      }        
    }
  },
  components:{
    DxSelectBox
  },
  computed: {
    visibleSearsh() {
  
      return this.$route.name != "Home";
    }
}

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
/* The navigation bar */
.header-fixed {
  overflow: hidden;
  background-color: #202F45;
  border-bottom: 1px solid aqua;
  position: fixed; /* Set the navbar to fixed position */
  top: 0; /* Position the navbar at the top of the page */
  height: 50px;
  width: 100%; 
  z-index: 1001;
}

.app-name{
  color: #d6d9dc;
  text-align: left;
  font-size: 30px;
  padding-top: 40px;
}

</style>
