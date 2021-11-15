<template> 
  <div>
    <div class="card">

      <div class="card-header">
        <div class="row">
          <div class="col text-left">
            <template v-if="selectedTab=='plus'">Création d'une intervention</template>
            <template v-if="selectedTab=='camera'">Prise de photo</template>
            <template v-if="selectedTab=='history'">Historique de l'intervention</template>
          </div>
          <div class="col">
            <div class="row float-right">
              <font-awesome-icon icon="plus" class="mt-1 py-1" title="Ajouter" :style="{ backgroundColor:  selectedTab == 'plus' ? '#24364b':'', width: '30px', height: '25px', borderTop: selectedTab == 'plus' ? '3px solid dodgerblue' : '3px solid #212f41' }" @click="selectTab('plus')" />
              <font-awesome-icon icon="camera" class="mt-1 py-1" :style="{ backgroundColor: selectedTab == 'camera' ? '#24364b':'', width: '30px', height: '25px', borderTop: selectedTab == 'camera' ? '3px solid dodgerblue' : '3px solid #212f41' }" @click="selectTab('camera')" />
              <font-awesome-icon v-if="currentUser.profil !== 'Hotline'" icon="history" class="mt-1 py-1" :style="{ backgroundColor:  selectedTab == 'history' ? '#24364b':'', width: '30px', height: '25px', borderTop: selectedTab == 'history' ? '3px solid dodgerblue' : '3px solid #212f41' }" @click="selectTab('history')" />
            </div>
          </div>
        </div>
        
      </div>

      <div class="card-body p-2">
        <template v-if="selectedTab=='plus'">
          <CreateIntervention />
        </template>
        <template v-if="selectedTab=='camera'">
          <TakePhoto @photoTaked="onTakedPhoto" @photosGot="onPhotoGot" @clientsReceived="onClientsReceived" />
        </template>

        <template v-if="selectedTab=='history'">
          <HistoryInfo />
        </template>

      </div>
    </div>
    <template v-if="selectedTab == 'camera' && dataPhoto[0].dataGrid.dataSource.length > 0">
      <ComponentModel :dataAccordion="dataPhoto" />
    </template>
    <template v-if="selectedTab == 'camera' && dataPhoto[0].dataGrid.dataSource.length == 1">
      <FirstClientKO :photoID="selectedPhotoID"
        :clientsKO="clientsKO"
        :initStatusFirstCall="initStatusFirstCall"/>
    </template>
    <template v-if="selectedTab == 'camera' && dataPhoto[0].dataGrid.dataSource.length > 1">
      <ManyClientKO :photoID="selectedPhotoID"
        :clientsKO="clientsKO"/>
    </template>
    <template v-if="selectedTab == 'history'">
      <HistoryAccordion />
    </template>
  </div>
</template>

<script lang="ts">

import store from '@/store';
import { UserMixin } from '@/mixins/userMixin';
import TakePhoto from '@/components/TakePhoto.vue';
import HistoryInfo from '@/components/main-component/HistoryInfo.vue';
import CreateIntervention from '@/components/CreateIntervention.vue';
import ComponentModel from '@/components/main-component/ComponentModel.vue';
import FirstClientKO from '@/components/main-component/FirstClientKO.vue';
import ManyClientKO from '@/components/main-component/ManyClientKO.vue';
import HistoryAccordion from '@/components/main-component/History.vue';
import { Getters } from '@/store';

export default {
  mixins: [UserMixin],
  name: 'CentreComponent',
  data() {
    const dataPhoto= [{
      'ID': 1,
      'Title': 'Résultat prise de photo',
      'Type': 'DataGrid',
      'dataGrid': {
        'disabled': false,
        'dataSource': [],
        'columns': [{
            'DataField': "creationDate",
            'Caption': "Date Consultation",
            'Type': "date",
            'Format': 'shortDateShortTime',
            'Sort': 'desc',
            'Visible': true
          },{
            'DataField': "user.login",
            'Caption': "Login",
            'Type': "string",
            'Visible': true
          }, {
            'DataField': "description",
            'Caption': "Description",
            'Type': "string",
            'Visible': true
          }, {
            'DataField': "photoResult.clientNumber",
            'Caption': "Nb Clients PM",
            'Type': "number",
            'Visible': true
          }, {
            'DataField': "photoResult.downClientNumber",
            'Caption': "NB Clients Impactés",
            'Type': "number",
            'Visible': true
          }, {
            'DataField': "photoResult.nokClientNumber",
            'Caption': "NB Clients non rétablis",
            'Type': "number",
            'Visible': true
          }
        ]
      }
    }
    ];   
    return {
      dataPhoto,
      selectedTab: 'plus',
      currentUser: {},
      selectedPhotoID: 0,
      clientsKO: [],
      initStatusFirstCall: true
    }
  },
  mounted(){
    this.initDatas();
    this.$store.watch(
      (_: void, getter: Getters) => getter.getCurrentTab ? this.selectTab(getter.getCurrentTab) : false,
      (newVal: string) => newVal
    )
  },
  methods:{
    initDatas(){
      this.selectedTab = store.state.currentTab ? store.state.currentTab : 'plus';
    },
    selectTab(e: string){
      this.selectedTab = e;
    },
    onTakedPhoto (photo: any) {

      photo.creationDate = new Date(photo.creationDate);
      this.dataPhoto[0].dataGrid.dataSource.push(photo);
    },
    onPhotoGot (photos: Array<any>) {

      this.dataPhoto[0].dataGrid.dataSource = [];
      photos.forEach((photo) => {
        photo.creationDate = new Date(photo.creationDate);
        this.dataPhoto[0].dataGrid.dataSource.push(photo);
      });
      
      const length = this.dataPhoto[0].dataGrid.dataSource.length;
      this.selectedPhotoID = this.dataPhoto[0].dataGrid.dataSource[length-1]?.id;
    },
    onClientsReceived (data: any){
      this.clientsKO = data;
      this.initStatusFirstCall = !this.initStatusFirstCall;
    }
  },
  watch:{
    selectedTab(val: string){
      this.$store.commit('CURRENT_MAIN_TAB', val);
    }
  },
  computed:{
    
  },
  components: {
    TakePhoto,
    CreateIntervention,
    ComponentModel,
    FirstClientKO,
    ManyClientKO,
    HistoryAccordion,
    HistoryInfo
  }
}
</script>

<style>

</style>
