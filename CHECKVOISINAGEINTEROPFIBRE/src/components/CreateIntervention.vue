<template>
  <div id="form-demo">
    <div class="widget-container">
        <DxForm
          v-model="newIntervention"
          :read-only="false"
          :show-colon-after-label="true"
          :show-validation-summary="true"
          validation-group="customerData"
          col-count="3"
        >
          <DxSimpleItem :is-required="true">
              <DxLabel text="ID Intervention"/>
              <template #default>
                  <DxTextBox
                    placeholder="ID Intervention"
                    v-model="newIntervention.name"
                    hint="ID Intervention">
                    <DxValidator
                        validation-group="customerData">
                        <DxRequiredRule message="ID Intervention est obligatoire"/>
                        <DxStringLengthRule :min="2" message="ID Intervention doit comporter au moins 2 caractères"/>
                        <DxAsyncRule
                              :validation-callback="interventionAsyncValidation"
                              message="ID Intervention existe"
                            />
                    </DxValidator>
                  </DxTextBox>
              </template>
          </DxSimpleItem>  
          <DxSimpleItem :is-required="true">
              <DxLabel text="Type d'intervention"/>
              <template #default>
                <DxSelectBox
                  :search-enabled="true"
                  :data-source="interventionsTypes"
                  v-model="newIntervention.type"
                  @value-changed="typeChangeEvent($event)"
                  Search-expr="{ 'name' }"
                  display-expr="name"
                  placeholder="Type d'intervention"
                  no-dataText="Pas de résultat disponible"
                  hint="Tapez les premières lettres pour rechercher un type d'intervention"
                >                
                  <DxValidator
                      validation-group="customerData">
                      <DxRequiredRule message="Type d'intervention est obligatoire"/>
                  </DxValidator>
                </DxSelectBox>
              </template>            
          </DxSimpleItem>
          <DxSimpleItem :is-required="true">
              <DxLabel text="Date d'arrivée sur ITV" />
              <template #default>
                  <DxDateBox
                    v-model="newIntervention.beginDate"
                    placeholder="Date d'arrivée"
                    @value-changed="arrivalDateChangeEvent"                    
                    type="datetime"
                    display-format="dd/MM/yyyy HH:mm:ss">             
                    <DxValidator
                        validation-group="customerData">
                        <DxRequiredRule message="Date d'arrivée est obligatoire"/>
                    </DxValidator>
                  </DxDateBox>
              </template>
          </DxSimpleItem>          
          <DxSimpleItem :is-required="true">
            <DxLabel text="Opérateur" />
            <template #default>
             <DxSelectBox
                :data-source="operators"
                v-model="newIntervention.operator"
                :search-enabled="true"
                SearchExpr="{ 'name' }"
                display-expr="name"
                placeholder="Opérateur"
                no-dataText="Pas de résultat disponible"
                hint="Tapez les premières lettres pour rechercher un opérateur"
                >  
                <DxValidator validation-group="customerData">
                    <DxRequiredRule message="Opérateur est obligatoire"/> 
                </DxValidator>
              </DxSelectBox>
            </template>            
          </DxSimpleItem>
          <DxSimpleItem :is-required="true">
            <DxLabel text="Nom du PM"/>
            <template #default>
             <DxSelectBox v-if="!loadingPms"
                :data-source="pms"
                v-model="newIntervention.mutualisationPoint"
                :search-enabled="true"
                SearchExpr="{ 'pmName' }"
                display-expr="pmName"
                placeholder="Nom du PM"
                no-dataText="Pas de résultat disponible"
                hint="Tapez les premières lettres pour rechercher un PM"                  
                >
                <DxValidator validation-group="customerData">
                    <DxRequiredRule message="Nom du PM est obligatoire"/> 
                </DxValidator>
              </DxSelectBox>
              <div class="text-center" v-else>
                <DxLoadIndicator
                  :height="25"
                  :width="25"
                />
              </div>
            </template>           
          </DxSimpleItem>
          <DxSimpleItem>
            <DxLabel text="Heure d'arrivée connue ?" />
            <template #default>
                <DxCheckBox
                v-model="knownHour"
                @value-changed="knownHourChangeEvent"
                />
            </template>
          </DxSimpleItem>
          <DxSimpleItem>
            <DxLabel text="PTO" />
            <template #default>
             <DxTextBox
                v-model="newIntervention.interventionPto"
                placeholder="PTO"
                mask="FI-AAAA-AAAA"
                hint="PTO">
             </DxTextBox>
            </template>            
          </DxSimpleItem>
          <DxSimpleItem>
            <template #default>                
            </template>
          </DxSimpleItem>
          <DxSimpleItem>
            <template #default>                
            </template>
          </DxSimpleItem>
          <DxSimpleItem>
            <template #default>                
            </template>
          </DxSimpleItem>
          <DxButtonItem
            :button-options="buttonOptions"
            horizontal-alignment="center"
          />
        </DxForm>
    </div>
  </div>
</template>

<script lang="ts">
import DxForm, {
  DxSimpleItem,
  DxButtonItem,
  DxLabel,
  DxRequiredRule,
  DxStringLengthRule,
  DxAsyncRule
} from 'devextreme-vue/form';
import DxValidator from 'devextreme-vue/validator';

import DxTextBox from 'devextreme-vue/text-box';
import DxSelectBox from 'devextreme-vue/select-box';
import { DxLoadIndicator } from 'devextreme-vue/load-indicator';
import DxDateBox from 'devextreme-vue/date-box';
import DxCheckBox from 'devextreme-vue/check-box';

import store from '@/store';
import { Intervention } from '@/types/DTO/intervention';
import { Operator } from '@/types/DTO/operator';
import { confirm } from 'devextreme/ui/dialog';

import HomeService from '@/services/HomeService';
import { createInterventionRequest } from '@/types/request/createInterventionRequest';
const $HomeService = new HomeService();
import moment from 'moment';
import { InterventionType } from '@/types/DTO/interventionType';
import DataSource from 'devextreme/data/data_source';
import notify from 'devextreme/ui/notify';
import { Getters } from '@/store';

export default {
  name: 'CreateIntervention',
  data() {
    return {
      newDate:{},
      newIntervention: {
        name: '',
        interventionId: 0,
        type: undefined,
        beginDate: new Date(), 
        mutualisationPoint: undefined,
        operator: {} as Operator,
        interventionPto: '',
      } as Intervention,
      knownHour: false,
      beginDate: new Date(),
      customer:{},      
      interventionsTypes: [],
      buttonOptions: {},
      interventions: [] as Array<Intervention>,
      selectedInterventionType: {},
      operators: [],
      pms: new DataSource(store.getters.getMutualisationPoints),
      selectedOperator: {},
      heure: {},
      loadingPms: store.getters.getMutualisationPoints.length === 0
    }
  },
  mounted(){    
    this.initDatas();
    this.buttonOptions = {
      text: 'Créer',
      onClick: this.handleSubmit,
      useSubmitBehavior: true
    }
    this.$store.watch(
      (_: void, getter: Getters) => this.newIntervention.name = getter.getNewInterventionName,
      (newVal: string) => newVal
    );
    this.$store.watch(
      (_: void, getter: Getters) => getter.getNewIntervention,
      (newVal: string) => this.interventions.push(newVal)
    );
  },
  methods:{
    async initDatas() { 
      $HomeService.GetInterventionTypes().then((resp) => {
        this.interventionsTypes = resp.object;
      });

      $HomeService.GetOperators().then((resp) => {
        this.operators = resp.object;
        this.newIntervention.operator =  this.operators.find((x: Operator) => x.name?.toUpperCase() == 'ORANGE');
      });
      
      this.$store.watch(
        (_: void, getter: Getters) => getter.getMutualisationPoints,
        (newVal: any) => {           
          this.pms = new DataSource(newVal);
          this.loadingPms = false;
        }
      );

      const response =  await $HomeService.GetAllInterventions();
      this.interventions = response;
    
    },
    async handleSubmit(e: any) {
  

      /* --SERVICE-- create intrevention*/
      if(e.validationGroup.validate().isValid) { 
        const data: createInterventionRequest = {
          name: this.newIntervention.name,
          pmId: this.newIntervention.mutualisationPoint.id,
          beginDate: this.newIntervention.beginDate,
          interventionPto: this.newIntervention.interventionPto,
          typeId: this.newIntervention.type.id,
          operatorId : this.newIntervention.operator.id
        };
               
        const response = await $HomeService.CreateIntervention(data);
        if(response.serviceMessage.operationSuccess){
          notify({ message: 'operation réussie', width: 350 }, 'success', 3000);
          this.$store.commit('CURRENT_MAIN_TAB', 'camera');
          this.$store.commit('CURRENT_INTERVENTION', response.object);
          this.$store.commit('NEW_INTERVENTION', response.object);
        }
        else {
          notify({ message: response.serviceMessage.errorMessage, width: 350 }, 'error', 3000);
        }
      }  
    },
    knownHourChangeEvent() {
  
      if(!this.knownHour) {
        this.newIntervention.beginDate = this.beginDate;
      }
    },
    arrivalDateChangeEvent(e: any) {
  
      if(this.newIntervention.beginDate.getDate() != this.beginDate.getDate()) {
        const result = confirm("<i>La date de début d'intervention choisie est différente de la date du jour.<br>Êtes-vous sûr de votre choix ?</i>", "Confirmer");
        result.then((dialogResult) => {
          if(dialogResult) {
            if(this.newIntervention.beginDate && this.newIntervention.beginDate.getTime()  != this.beginDate.getTime()) {
              this.knownHour = true;
            }
          }
          else {
            this.newIntervention.beginDate = e.previousValue;
          }
        });
      }
      else {
        if(this.newIntervention.beginDate && this.newIntervention.beginDate.getTime()  != this.beginDate.getTime()) {
          this.knownHour = true;
        }
      }
    },
    typeChangeEvent(e: any) {
      console.log(e);
      console.log(typeof(e));
      
      if(e.value) {
        const newdate = moment().subtract((e.value as InterventionType).duration, 'hour');
        // this.newIntervention.beginDate = null;     
        this.newIntervention.beginDate = newdate.toDate();
        this.beginDate = newdate.toDate();
        this.knownHour = false;
      }
    },
    interventionAsyncValidation(interventionId: any) {
  
      return new Promise((resolve) => {        
        resolve(this.interventions.filter((e: any) => e.name?.trim().toUpperCase() === interventionId.value?.trim().toUpperCase()).length === 0);
      });
    }
  },
  components: {
    DxSimpleItem,
    DxButtonItem,
    DxLabel,
    DxRequiredRule,
    DxStringLengthRule,
    DxForm,
    DxAsyncRule,
    DxTextBox,
    DxSelectBox,
    DxDateBox,
    DxCheckBox,
    DxValidator,
    DxLoadIndicator
  }
}
</script>

<style>

</style>
