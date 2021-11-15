<template>
  <div>
    <ComponentModel :dataAccordion="clientKOHistory" />
    <ComponentModel :dataAccordion="dataPhotoHistory" @selectionChanged="onSelectionChanged" />
    <ComponentModel id="container" v-if="alarmsVisible" :dataAccordion="alarmsHistory" />
  </div>
</template>
<script lang="ts">
import ComponentModel from '@/components/main-component/ComponentModel.vue';
import HomeService from '@/services/HomeService';
import notify from 'devextreme/ui/notify';
import { InterventionGlobalInfo } from '@/types/DTO/interventionGlobalInfo';
import { Getters } from '@/store';

const $HomeService = new HomeService();

export default {
  name: 'History',
  components: {
    ComponentModel
  },
  data() {    
    const clientKOHistory = [{
      'ID': 2,
      'Title': 'Client KO',
      Type: 'DataGrid',
      'dataGrid': {
        'disabled': false,
        'dataSource': [],
        'columns': [{
            DataField: "srvID",
            Caption: "SrvID",
            Type: "number",
            Visible: true
          }, {
            DataField: "clientStatus",
            Caption: "Statut Client",
            Type: "string",
            Visible: true
          }, {
            DataField: "localization",
            Caption: "Localisation défaut",
            Type: "string",
            Visible: true
          },{
            DataField: "subCause",
            Caption: "Sous-cause",
            Type: "string",
            Visible: true
          },{
            DataField: "info",
            Caption: "Info Complémentaires",
            Type: "string",
            Visible: true
          }
        ]
      }
    }];
    const dataPhotoHistory= [{
      'ID': 1,
      'Title': 'Photos',
      Type: 'DataGrid',
      'dataGrid': {
        'disabled': false,
        'dataSource': [],
        'columns': [
          {
          DataField: "creationDate",
          Caption: "Date Consultation",
          Type: "date",
          Format: 'shortDateShortTime',
          Sort: 'desc',
          Visible: true
          },{
            DataField: "user.login",
            Caption: "Login",
            Type: "string",
            Visible: true
          },{
          DataField: "description",
          Caption: "Description",
          Type: "string",
          Visible: true
          },
          {
          DataField: "impactedClient",
          Caption: "NB des clients impactés",
          Type: "number",
          Visible: true
          },
          {
          DataField: "koClient",
          Caption: "NB des clients non rétablis",
          Type: "number",
          Visible: true
          }
        ]
      }
    }
    ];
    const alarmsHistory= [{
      ID: 1,
      Title: 'Alarms',
      Type: 'DataGrid',
      dataGrid: {
        disabled: false,
        dataSource: [],
        columns: [
          {
          DataField: "notificationId",
          Caption: "Identifiant",
          Type: "number",
          Sort: 'desc',
          Visible: true
          },
          {
          DataField: "alarmType",
          Caption: "Type",
          Type: "string",
          Visible: true
          },
          {
          DataField: "startTime",
          Caption: "Start time",
          Type: "date",
          Format: 'shortDateShortTime',
          Visible: true
          },
          {
          DataField: "clearTime",
          Caption: "Clear Time",
          Type: "date",
          Format: 'shortDateShortTime',
          Visible: true
          },
          {
          DataField: "duration",
          Caption: "Durée",
          Type: "string",
          Visible: true
          },
          {
          DataField: "srvID",
          Caption: "SrvID",
          Type: "number",
          Visible: true
          }
        ]
      }
    }];
    
    return {
      dataPhotoHistory,
      clientKOHistory,
      alarmsHistory,
      alarmsVisible: false,
      globalInfo: {} as InterventionGlobalInfo
    };
  },
  mounted() {
      this.unwatch = this.$store.watch(
      (_: void, getter: Getters) => {
          const selectId = getter.getCurrentIntervention ? getter.getCurrentIntervention.interventionId: 0;
          if(selectId) {

            $HomeService.GetInterventionHistoric(selectId).then((resp) => {
              const message = resp.serviceMessage;
              if(message.operationSuccess == false){
                notify({ message: message.errorMessage, width: 350 }, 'error', 3000);
                this.$store.commit('HISTORY_INFO', {});
                this.clientKOHistory[0].dataGrid.dataSource = [];
                this.dataPhotoHistory[0].dataGrid.dataSource = [];
                this.alarmsVisible = false;
              }
              else{
                const object = resp.object;
                if(object){
                  this.$store.commit('HISTORY_INFO', object.globalInfo);
                  this.clientKOHistory[0].dataGrid.dataSource = object.koClients;
                  this.dataPhotoHistory[0].dataGrid.dataSource = object.photos;
                }
                else{
                  this.clientKOHistory[0].dataGrid.dataSource = [];
                  this.dataPhotoHistory[0].dataGrid.dataSource = [];
                  notify({ message: "Object is null.", width: 350 }, 'error', 3000);
                }
              }
            });
          }
          else{
            notify({ message: "Pas d'intervention selectionnée. Merci d'en choisir une.", width: 350 }, 'info', 5000);
          }
      },
      (_: any) => null
    );
  },
  beforeDestroy(){
    this.unwatch();
  },
  methods: {
    async onSelectionChanged(data: any) {
      const delay = (ms: number) => new Promise(res => setTimeout(res, ms));
      if (data){
        this.alarmsVisible = data.photoID > 0;
        $HomeService.GetHistoricAlarms(data.photoID).then((resp) => {
          if (resp.serviceMessage.operationSuccess){
            this.alarmsHistory[0].dataGrid.dataSource = resp.object;
          }
          else {
            notify({ message: "Erreur lors de la récupération des alarmes", width: 350 }, 'info', 5000);
          }
        });
      }
      //DELAY  C'EST IMPORTANT POUR GENERATION DES DONNEES DANS SHEET.
      await delay(100);
      this.$el.querySelector("#container").scrollIntoView({behavior: 'smooth'});
    }
  }
};
</script>
<style scoped>
</style>