<template>
  <div id="back-ground-color">
    <div id="Accordion-border" class="row border text-right">
      <div v-if="data.Rows.length && showUnloadedCode()" class="col">
        <DxButton
          text="Code décharge"
          @click="codedechargeClick"
        />
      </div>
    </div>
    <table v-if="data.Rows.length" class="table table-borderless">
      <caption>Liste des clients KO pour plusieurs photos</caption>
        <thead>
            <tr class="th-color text-center">
            <th v-for="(title, index) in data.Header" :key="index" :colspan="title.colspan" scope="col">{{title.value}}</th>
            </tr>
        </thead>
        <tbody v-for="(row, index) in data.Rows" :key="index" :class="{'tr-color': index%2, 'tr-color2': index%2 == 0}">
            <tr>
              <td class="text-center" width="20%">
                <font-awesome-icon v-if="row.clientStep == ClientSteps.newKO" icon="user-plus" title="Nouveau KO" />
                {{row.srvID}}
                <font-awesome-icon v-if="row.clientStep == ClientSteps.oldOK" style="color:#B493C3" icon="check" title="Ancien OK" />
                <font-awesome-icon v-if="row.clientStep == ClientSteps.oldKO" style="color:red" icon="times" title="Ancien KO" />
              </td>
              <td width="20%">
                  <DxSelectBox name="StatutFirstCallID"
                        :data-source="StatutFirstCallList"
                        v-model="row.statusFirstCallId"
                        :disabled="true"
                        :search-enabled="true"
                        value-expr="id"
                        Search-expr="{ 'name' }"
                        display-expr="name"
                        placeholder="Statut 1er appel"
                        />
              </td>
              <td colspan="3">
                <div class="input-group mb-3">
                  <DxTextArea
                  :height="35"
                  :width="96+'%'"
                  :max-length="250"
                  placeholder="Commentaire"
                  :value="row.firstCallComment"
                  :disabled="true"
                  />
                  <div class="input-group-append">
                    <font-awesome-icon v-if="row.firstCallComment && row.firstCallComment.length > 127" class="mt-2 ml-1" icon="expand-alt" title="Augmenter" style="cursor:pointer;width:18px;height:18px" @click="expandArea('Commentaire', row, 'firstCallComment')"/>
                  </div>
                </div>
              </td>
            </tr>
            <tr>
              <td>
              </td>
              <td>Conclusion
                <span class="dx-field-item-required-mark">&nbsp;*</span>
              </td>
              <td>Localisation
                <span v-if="row.clientStep != ClientSteps.newKO" class="dx-field-item-required-mark">&nbsp;*</span>
              </td>
              <td>Sous-cause
              </td>
              <td>Infos complémentaires
                <span class="dx-field-item-required-mark">&nbsp;*                  
                </span>
                <font-awesome-icon v-if="row.problemOrigin && row.problemOrigin.length > 27" class="mt-2 ml-1" icon="expand-alt" title="Augmenter" style="cursor:pointer;width:18px;height:18px" @click="expandArea('Infos complémentaires', row, 'problemOrigin')"/>
              </td>
            </tr>
            <tr>
              <td>
              </td>
              <td>
                <DxSelectBox name="ConclusionID"
                        :data-source="ConclusionList"
                        v-model="row.conclusionId"
                        value-expr="id"
                        :search-enabled="true"
                        Search-expr="{ 'name' }"
                        display-expr="name"
                        placeholder="Conclusion"
                        @value-changed="autoSave(row, $event)"
                        >
                  <DxValidator
                      ref="Validation"
                      validation-group="customerData"
                      >
                      <DxRequiredRule
                        message="Conclusion est obligatoire"
                      />
                  </DxValidator>
                </DxSelectBox>
              </td>
              <td>
                <DxSelectBox name="LocalisationID"
                        :data-source="LocalisationList"
                        v-model="row.localizationId"
                        value-expr="id"
                        :search-enabled="true"
                        Search-expr="{ 'name' }"
                        display-expr="name"
                        placeholder="Localisation"
                        :onContentReady="localizationChanged(row)"
                        @value-changed="autoSave(row, $event)"
                        >
                  <DxValidator
                      ref="Validation"
                      validation-group="customerData"
                      >
                      <DxAsyncRule
                        :validation-callback="localisationValidation(row)"
                        message="Localisation est obligatoire"
                      />
                  </DxValidator>
                </DxSelectBox>
              </td>
              <td>
                <DxSelectBox name="SousCauseID"
                        :data-source="row.SousCauseList"
                        v-model="row.subCauseId"
                        value-expr="id"
                        :search-enabled="true"
                        Search-expr="{ 'name' }"
                        display-expr="name"
                        placeholder="Sous-cause"
                        @value-changed="autoSave(row, $event)"
                        />
              </td>
              <td>
                <DxTextArea
                :height="35"
                :max-length="250"
                placeholder="Infos complémentaires"
                v-model="row.problemOrigin"
                @value-changed="autoSave(row, $event)"
                >
                  <DxValidator
                      ref="Validation"
                      validation-group="customerData"
                      >
                      <DxAsyncRule
                        :validation-callback="problemOriginValidation(row)"
                        message="Infos complémentaires est obligatoire"
                      />
                  </DxValidator>
                </DxTextArea>
              </td>
            </tr>
        </tbody>
    </table>
    <DxPopup
      :visible="popupVisible"
      v-model="popupVisible"
      :drag-enabled="false"
      :close-on-outside-click="true"
      :show-title="true"
      :width="300"
      :height="250"
      @hiding="onHiding"
      title="Code Décharge"
    >
      <h3 style="text-align:center">
        {{selectedIntervention.unloadedCode}}
      </h3>
    </DxPopup>
    <DxPopup v-if="selectedRow.row"
      :visible="popupExpandArea"
      v-model="popupExpandArea"
      :drag-enabled="false"
      :close-on-outside-click="true"
      :show-title="true"
      :width="500"
      :height="220"
      @hiding="popupExpandArea = false"
      :title="selectedRow.title">
      <DxTextArea
        :max-length="250"
        :width="456"
        :height="130"
        :placeholder="selectedRow.title"
        :disabled="selectedRow.title == 'Commentaire'"
        v-model="selectedRow.row[selectedRow.column]"
        @value-changed="autoSave(selectedRow.row, $event)"
        >
          <DxValidator
              ref="Validation"
              validation-group="customerData"
              >
              <DxAsyncRule
                :validation-callback="problemOriginValidation(selectedRow.row)"
                message="Le champ infos complémentaires est obligatoire"
              />
          </DxValidator>
        </DxTextArea>
    </DxPopup>
  </div>
</template>
<script lang="ts">
import DxSelectBox from 'devextreme-vue/select-box';
import DxTextArea from 'devextreme-vue/text-area';
import DxButton from 'devextreme-vue/button';
import DxPopup from 'devextreme-vue/popup';
import AdminService from '@/services/AdminService';
import HomeService from '@/services/HomeService';
import { ClientStatusViewModel } from '@/types/ViewModels/ClientStatusViewModel';
import { ConclusionViewModel } from '@/types/ViewModels/ConclusionViewModel';
import { LocalizationViewModel } from '@/types/ViewModels/LocalizationViewModel';
import DxValidator from 'devextreme-vue/validator';
import { DxRequiredRule, DxAsyncRule } from 'devextreme-vue/form';
import { Intervention } from '@/types/DTO/intervention';
const $AdminService = new AdminService();
const $HomeService = new HomeService();
import { ClientSteps } from "@/types/enums/ClientSteps";
import { updateClientResultRequest } from '@/types/request/updateClientResultRequest';
import notify from 'devextreme/ui/notify';
import { Getters } from '@/store';


export default {
  props: {
    data: {
      type: Object
    }
  },
  data() {
    
    return {
        StatutFirstCallList: [] as Array<ClientStatusViewModel>,
        ConclusionList:[] as Array<ConclusionViewModel>,
        LocalisationList:[] as Array<LocalizationViewModel>,
        selectedIntervention: {} as Intervention,
        popupVisible: false,
        popupExpandArea: false,
        ClientSteps,        
        selectedRow: {} as any,
    }
  },
  mounted() {
    this.initDatas();
    this.$store.watch(
      (_: void, getter: Getters) => this.selectedIntervention = getter.getCurrentIntervention ? getter.getCurrentIntervention: {},
      (newVal: Intervention) => this.selectedIntervention = newVal
    )
  },
  components:{
    DxSelectBox,
    DxTextArea,
    DxButton,
    DxPopup,
    DxValidator,
    DxAsyncRule,
    DxRequiredRule
  },
  methods: {
    codedechargeClick() {
  
      if(!this.selectedIntervention.unloadedCode) {
        $HomeService.GetCodeDecharge(this.selectedIntervention.interventionId).then((resp) => {
          this.selectedIntervention.unloadedCode = resp;
        });
      }
      this.popupVisible = true;
    },
    onHiding() {
  
      this.popupVisible = false;
    },
    validation(){
  
      const validation = this.$refs["Validation"];
      if (validation){
        for(let i = 0 ; i<validation.length; i++) {
          if(validation[i])
            validation[i].$_instance.validate()
        }
      }
    },
    updateClientResult(row: any, event: any){
      if (event?.event){
    
        const validation = this.$refs["Validation"];
        if(validation){
          for(let i = 0 ; i<validation.length; i++) {
            if(validation[i])
              validation[i].$_instance.validate()
          }
          if(!row.conclusionId || row.conclusionId==0 || !row.problemOrigin || row.problemOrigin.length == 0 || !this.localisationCondition(row)) {
            return;
          }
          const param: updateClientResultRequest = {
            clientResultId: row.id,
            srvID : row.srvID,
            ontPath: row.ontPath,
            firstCallComment: row.firstCallComment,
            statusFirstCallId: row.statusFirstCallId,
            conclusionId: row.conclusionId,
            localizationId: row.localizationId,
            subCauseId: row.subCauseId,
            additionalInfo: row.problemOrigin
          }

          $HomeService.UpdateClientResult(param).then((response) => {
            if(response.serviceMessage.operationSuccess){
              notify({ message: 'operation réussie', width: 350 }, 'success', 1000);
            }
            else {
              notify({ message: response.serviceMessage.errorMessage, width: 350 }, 'error', 1000);
            }
          });      
        }
      }
    },
    initDatas(){
  
      $AdminService.GetStatus().then((resp) => {
        this.StatutFirstCallList = resp;
      });

      $AdminService.GetConclusions().then((resp) => {
        this.ConclusionList = resp.filter( (e: any) => !e.isDeleted);
      });
      
      $AdminService.GetLocalizations().then((resp) => {
        this.LocalisationList = resp.filter( (e: any) => !e.isDeleted);
      });

      this.validation();
    },
    localizationChanged(row: any){
  
      row.subCauseId = null;
      row.SousCauseList = this.LocalisationList.find((e: any) => e.id == row.localizationId)?.subCauses;
      row.subCauseId = null;
    },
    autoSave(row: any, event: any) {
  
      this.updateClientResult(row, event);
      
    },
    showUnloadedCode() {
  
      return this.data.Rows.some((e: any) => e.clientStep == ClientSteps.oldKO);
    },
    problemOriginValidation(row: any) {
  
      return ()=> new Promise((resolve) => {
        resolve(row.problemOrigin?.trim().length > 0);        
      });
    },
    localisationValidation(row: any) {
  
      return ()=> new Promise((resolve) => {
        resolve(this.localisationCondition(row));        
      });
    },
    localisationCondition(row: any) {
      return row.clientStep != ClientSteps.newKO && row.localizationId > 0;
    },
    expandArea(title: string, row: any, column: string) {
  
      this.selectedRow = {
        title: title,
        row: row,
        column: column
      };
      this.popupExpandArea = true;
    }
  }
};
</script>

<style scoped>

.th-color {
  color: #e6e7f3f5;
}

.tr-color {
  color: #e6e7f3f5;
  background-color:#465672;
}

.tr-color2 {
  color:#e6e7f3f5;
  background-color:#04548a41;
}

</style>