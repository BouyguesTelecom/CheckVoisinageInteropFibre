<template>
    <div id="back-ground-color">
      <div id="Accordion-border" class="row border">
        <div class="col"><br/></div>
      </div>
        <table v-if="data.Rows.length" class="table table-striped">
          <caption>Liste des clients KO pour une seule photo</caption>
            <thead>
                <tr class="th-color text-center">
                <th v-for="(title, index) in data.Header" :key="index" scope="col">{{title}}</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(row, index) in data.Rows" :key="index" :class="{'tr-color': index%2, 'tr-color2': index%2 == 0}">
                    <td class="text-center" width="20%">{{row.srvID}}</td>
                    <td width="20%">
                      <DxSelectBox name="StatutFirstCallID"
                            :data-source="StatutFirstCallList"
                            v-model="row.statusFirstCall"
                            :search-enabled="true"
                            Search-expr="{ 'name' }"
                            display-expr="name"
                            placeholder="Statut 1er appel"
                            no-data-text="Pas de résultat"
                            @value-changed="updateClientResult(row, $event)"
                          >                              
                        <DxValidator
                            ref="Validation"
                            validation-group="customerData">
                            <DxAsyncRule
                              :validation-callback="CommentValidation(row)"
                              message="Commentaire obligatoire pour ce statut"
                            />
                        </DxValidator>
                      </DxSelectBox>
                    </td>
                    <td>
                    <div class="input-group mb-3">
                      <DxTextArea
                      :height="35"
                      :width="96+'%'"
                      :max-length="250"
                      :placeholder="commentairePlaceHolder(row)"
                      v-model="row.firstCallComment"
                      @value-changed="updateClientResult(row, $event)"
                      >
                        <DxValidator
                            ref="Validation"
                            validation-group="customerData">
                            <DxAsyncRule
                              :validation-callback="CommentValidation(row)"
                              message="Commentaire est obligatoire"
                            />
                        </DxValidator>
                      </DxTextArea>
                      <div class="input-group-append">
                          <font-awesome-icon v-if="row.firstCallComment && row.firstCallComment.length > 127" class="mt-2 ml-1" icon="expand-alt" title="Augmenter" style="cursor:pointer;width:18px;height:18px" @click="expandArea(row)" />
                        </div>
                      </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <DxPopup
          :visible="popupExpandArea"
          v-model="popupExpandArea"
          :drag-enabled="false"
          :close-on-outside-click="true"
          :show-title="true"
          :width="500"
          :height="220"
          @hiding="popupExpandArea = false"
          title="Commentaire">
          <DxTextArea
            :width="456"
            :height="130"
            :max-length="250"
            :placeholder="commentairePlaceHolder(selectedRow)"
            v-model="selectedRow.firstCallComment"
            @value-changed="updateClientResult(selectedRow, $event)"
            >
              <DxValidator
                  ref="Validation"
                  validation-group="customerData">
                  <DxAsyncRule
                    :validation-callback="CommentValidation(selectedRow)"
                    message="Commentaire est obligatoire"
                  />
              </DxValidator>
            </DxTextArea>
        </DxPopup>
    </div>
</template>
<script lang="ts">
import DxSelectBox from 'devextreme-vue/select-box';
import DxTextArea from 'devextreme-vue/text-area';
import DxValidator, { DxAsyncRule } from 'devextreme-vue/validator';
import AdminService from '@/services/AdminService';
import { ClientStatusViewModel } from '@/types/ViewModels/ClientStatusViewModel';
import HomeService from '@/services/HomeService';
import { updateClientResultRequest } from '@/types/request/updateClientResultRequest';
import DxPopup from 'devextreme-vue/popup';

import notify from 'devextreme/ui/notify';
import { clientKORow } from '@/types/models';

const $HomeService = new HomeService();
const $AdminService = new AdminService();

export default {
  props: {
    initStatusFirstCall: Boolean,
    data: Object
  },
  data() {
    return {
        StatutFirstCallList: [] as Array<ClientStatusViewModel>,
        selectedRow: {} as clientKORow,
        popupExpandArea: false
    }
  },
  watch:{
    initStatusFirstCall(){  
      this.initiateStatusFirstCall();
    }
  },
  mounted() {

    this.initDatas();
  },
  methods: {
    initDatas (){
  
      $AdminService.GetStatus().then((resp) => {
        this.StatutFirstCallList = resp.filter((e: ClientStatusViewModel) => !e.isDeleted);

        this.initiateStatusFirstCall();
        this.validation();
      });
    },
    initiateStatusFirstCall(){
  
      this.data.Rows.forEach((el: clientKORow) => {
        el.statusFirstCall = this.StatutFirstCallList.find((elm: ClientStatusViewModel)=> elm.id == el.statusFirstCallId);
      });
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
    updateClientResult(row: clientKORow, event: any){
      if (event?.event){
        if(!row.statusFirstCall) return false;
        if(row.statusFirstCall.isCommentMandatory && (row.firstCallComment == null || row.firstCallComment?.trim().length < 2)) {
          return false;
        }
        const param: updateClientResultRequest = {
          clientResultId: row.id,
          srvID : row.srvID,
          ontPath: row.ontPath,
          firstCallComment: row.firstCallComment,
          statusFirstCallId: row.statusFirstCall?.id,
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
      
    },
    commentairePlaceHolder(comment: clientKORow) {  
      return comment.statusFirstCall?.isCommentMandatory ? "Commentaire obligatoire" : "Commentaire";
    },
    CommentValidation(comment: clientKORow) {        
      return ()=> new Promise((resolve) => {
        if(comment.statusFirstCall?.isCommentMandatory && (comment.firstCallComment == null || comment.firstCallComment?.trim().length < 2)) {
          this.$store.commit('COMMENTS_CLIENT_KO', this.data.Rows);
        }
        resolve(!comment.statusFirstCall.isCommentMandatory || (comment.firstCallComment?.trim().length > 1)); 
        
      });
    },
    expandArea(row: clientKORow) {  
      this.selectedRow = row;
      this.popupExpandArea = true;
    }   

  },
  components:{
    DxSelectBox,
    DxTextArea,
    DxValidator,
    DxAsyncRule,
    DxPopup
  }
};
</script>

<style scoped>

#back-ground-color {  
  text-decoration-color: #FFF;
  background-color:#212f41;
}

.th-color {
  color: #e6e7f3f5;
}

.tr-color {
  color: #e6e7f3f5;
  background-color:#04548a41;
}

.tr-color2 {
  color:#e6e7f3f5;
  background-color:#b8b4b72c;
}
</style>