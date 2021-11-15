<template>
  <div id="form-demo" class="card cardBloc">
    <div class="">
        <DxForm
          v-model="newExport"
          :read-only="false"
          :show-colon-after-label="true"
          :show-validation-summary="true"
          validation-group="customerData"
          col-count="2"
        >

          <DxSimpleItem :is-required="true">
              <DxLabel text="Date début" />
              <template #default>
                  <DxDateBox
                    v-model="newExport.fromDate"
                    placeholder="Date début"
                    @value-changed="beginDateChangeEvent"                    
                    type="date"
                    display-format="dd/MM/yyyy">             
                    <DxValidator
                        validation-group="customerData">
                        <DxRequiredRule message="Date de début est obligatoire"/>
                    </DxValidator>
                  </DxDateBox>
              </template>
          </DxSimpleItem>     

          <DxSimpleItem :is-required="true">
              <DxLabel text="Date fin" />
              <template #default>
                  <DxDateBox
                    v-model="newExport.toDate"
                    placeholder="Date fin"                    
                    type="date"
                    :min=minDate
                    display-format="dd/MM/yyyy">             
                    <DxValidator
                        validation-group="customerData">
                        <DxRequiredRule message="Date fin est obligatoire"/>
                    </DxValidator>
                  </DxDateBox>
              </template>
          </DxSimpleItem>    

          <DxSimpleItem>
            <template #default>   
              <xlsx-workbook> 
                <xlsx-sheet :collection="sheet.data" v-for="sheet in sheets" :key="sheet.name" :sheet-name="sheet.name"/>
                <xlsx-download :filename="fileName" style="display: none" >
                  <button ref="downloadClick"></button>
                </xlsx-download>
              </xlsx-workbook>             
            </template>
          </DxSimpleItem>

          <DxButtonItem :button-options="buttonOptions" horizontal-alignment="right"/>
          
        </DxForm>
    </div>
  </div>
</template>

<script>

import DxForm, {
  DxGroupItem,
  DxSimpleItem,
  DxButtonItem,
  DxLabel,
  DxRequiredRule,
  DxCompareRule,
  DxRangeRule,
  DxStringLengthRule,
  DxPatternRule,
  DxEmailRule,
  DxAsyncRule
} from 'devextreme-vue/form';
import DxValidator from 'devextreme-vue/validator';
import DxAutocomplete from 'devextreme-vue/autocomplete';
import { XlsxWorkbook, XlsxSheet, XlsxDownload } from "vue-xlsx";

import DxTextBox from 'devextreme-vue/text-box';
import DxSelectBox from 'devextreme-vue/select-box';
import { DxLoadIndicator } from 'devextreme-vue/load-indicator';
import DxDateBox from 'devextreme-vue/date-box';
import DxCheckBox from 'devextreme-vue/check-box';
import DxButton from 'devextreme-vue/button';


import HomeService from '@/services/HomeService';
import notify from 'devextreme/ui/notify';
import moment from 'moment';


const $HomeService = new HomeService();

export default {
  name: 'ExportForm',
  data() {
    return {
      fileName: "Reporting_CVI_" + moment(String(new Date())).format('YYYYMMDD') + "_" + moment(String(new Date())).format('HHmm') + ".xlsx",
      minDate: new Date(),
      newExport: {
        fromDate: new Date(), 
        toDate: new Date()
      },
      buttonOptions: {
        text: 'Exporter',
        onClick: this.handleSubmit,
        useSubmitBehavior: true
      },
      sheets: [
        { name: "Suivi Interventions", data: [{}]},
        { name: "Clients Impactés", data: [{}]},
        { name: "Clients KO", data: [{}]}
      ]
    }
  },
  methods:{
    beginDateChangeEvent(){

      if(this.newExport.toDate < this.newExport.fromDate) {
        this.newExport.toDate = this.newExport.fromDate;
      }
      this.minDate = this.newExport.fromDate;

    },
    async handleSubmit(e) {
      const fromDate = moment(String(this.newExport.fromDate)).format('YYYY-MM-DD');
      const toDate  = moment(String(this.newExport.toDate)).format('YYYY-MM-DD');
      const delay = ms => new Promise(res => setTimeout(res, ms));

      /* --SERVICE-- EXPORT INTERVENTIONS*/
      if(e.validationGroup.validate().isValid) { 

        const loader = this.$loading.show();

        const response = await $HomeService.ExportInterventions(
          fromDate, 
          toDate
        );

        if(response == null){
          notify({ message: 'Erreur de serveur.', width: 350 }, 'error', 3000);
        }
        else if(response.serviceMessage.operationSuccess){
          const intrs = response.object.interventions;
          const impClints = response.object.impactedClients;
          const koClients = response.object.koClients;

          this.sheets[0].data = [];
          this.sheets[1].data = [];
          this.sheets[2].data = [];

          intrs.forEach(element => {
            this.sheets[0].data.push({
              "Numéro intervention": element.interventionNum,
              "PM": element.interventionPm,
              "Type intervention": element.interventionType,
              "PTO": element.interventionPto,
              "Créé par" :element.createdBy, 
              "Date début intervention": moment(String(element.startDateIntervention)).format('DD/MM/yyyy HH:mm'),
              "Date fin intervention": element.endDateIntervention ? moment(String(element.endDateIntervention)).format('DD/MM/yyyy HH:mm') : "",
              "Liste des identifiants": element.clients,
              "Nb clients coupés et rétablis à la 1ere photo": element.restoredClientsCountOfFirstPhoto,
              "Nb clients coupés et rétablis après 1ere photo": element.restoredClientsCountOfOthersPhotos,
              "nb clients avec coupures courtes <20 sec. et >=5 bagots": element.restoredClientsCountWithShortCutWith5Alarms,
              "nb clients avec coupures courtes <20sec. et <5 bagots": element.restoredClientsCountWithShortCutLessThen5Alarms,
              "nb clients KO rétablis": element.restoredKOClients,
              "nb de clients coupés non rétablis": element.notRestoredCliensCount,
              "nb clients PM": element.totalCliensCount,
              "nb client en DG/Off pendant l'inter": element.dgClientCount,
              "Liste SrvID DG/OFF" : element.listDGOFF,
              "Statut 1er Appel": element.firstCallStatus,
              "Conclusion + localisation (PM, PBO, Autre )au 2ème appel": element.conclusionAndLocalisation
            });
          });

          impClints.forEach(element => {
            this.sheets[1].data.push({
              "Numéro intervention": element.interventionNum,
              "PM": element.interventionPm,
              "Date Photo": moment(element.photoDate).format('DD/MM/yyyy HH:mm'),
              "Prise par": element.photoUser,
              "Description": element.photoDescription,
              "SrvID": element.srvID,
              "Durée cumulée de l'interruption": element.sumDurationOfInterruption,
              "ID Alarme": element.alarmId,
              "Type Alarme": element.alarmType,
              "PON": element.alarmAdress,
              "Numéro ONT": element.alarmOnt,
              "StartTime": moment(element.alarmStartTime).format('DD/MM/yyyy HH:mm'),
              "Clear Time": moment(element.alarmEntTime).format('DD/MM/yyyy HH:mm')
            });
          });

          koClients.forEach(element => {
            this.sheets[2].data.push({
              "Numéro intervention": element.interventionNum,
              "PM": element.interventionPm,
              "SrvID": element.srvID,
              "Date KO": moment(element.startTimeAlarm).format('DD/MM/yyyy HH:mm'),
              "Statut Final": element.finalStatus == true? 'OK': 'KO',
              "Statut 1er appel": element.firstCallStatus,
              "Commentaire": element.firstCallStatusComment,
              "Conclusion": element.conclusion,
              "Localisation": element.location,
              "Sous-Cause": element.subCause,
              "Infos Complémentaires": element.additionalInfo
            });
          });


          //DELAY  C'EST IMPORTANT POUR GENERATION DES DONNEES DANS SHEET.
          await delay(2000);

          const validation = this.$refs["downloadClick"];
          validation.click();
          loader.hide();
        }
        else {
          notify({ message: response.serviceMessage.errorMessage, width: 350 }, 'error', 3000);
        }
      }
    }
  },
  components: {
    DxGroupItem,
    DxSimpleItem,
    DxButtonItem,
    DxLabel,
    DxRequiredRule,
    DxCompareRule,
    DxPatternRule,
    DxRangeRule,
    DxEmailRule,
    DxStringLengthRule,
    DxForm,
    DxAutocomplete,
    DxAsyncRule,
    DxTextBox,
    DxSelectBox,
    DxDateBox,
    DxCheckBox,
    DxButton,
    DxValidator,
    DxLoadIndicator,
    XlsxWorkbook,
    XlsxDownload,
    XlsxSheet
  }
}
</script>

<style>
  .cardBloc{
    width: 100%;
    margin: 10px 0.5rem 0.5rem !important;
    padding: 20px;
  }
</style>
