  <template>
    <div class="card">
      <div class="card-header">
        <div class="row">
          <div class="col">
            {{Param.HeaderName}}
          </div>
          <div class="col">
            <div class="row mt-1 float-right">
              <div v-if="!NewParam && !EditedItem" class="col">
                <font-awesome-icon icon="plus" title="Ajouter" class="simple-icon" @click="addNewParam()" />
              </div>
              <div v-if="NewParam" class="col">
                <font-awesome-icon icon="times" title="Annuler" class="simple-icon" @click="removeNewParam()" />
              </div>
              <div v-if="NewParam" class="col">
                <font-awesome-icon icon="check" title="Sauvegarder" class="simple-icon" @click="saveParam()" />
              </div>
              <div v-if="EditedItem" class="col">
                <font-awesome-icon icon="times" title="Annuler" class="simple-icon" @click="cancelEdit()" />
              </div>
              <div v-if="EditedItem" class="col">
                <font-awesome-icon icon="check" title="Sauvegarder" class="simple-icon" @click="saveEdit()" />
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="container" class="card-body overflow-auto">
        <div class="row mb-3" v-if="NewParam">
          <div class="col">
            <DxTextBox v-model="NewParam.name" placeholder="Nouveau paramètre"/>
          </div>
          <div class="col text-right">
            <DxSwitch v-model="NewParam.isVisible"/>
          </div>
        </div>
        <div class="row header-params mb-2">
          <div class="col">Nom</div>
          <div class="col text-right">Visible</div>
        </div>
        <div class="dx-field mt-3" v-for="(item, index) in Param.Items" :key="index">
          <div class="dx-field-label" style="width:80%">
            <template v-if="EditedItem && EditedItem.Index === index">
              <DxTextBox v-model="EditedItem.name" />
            </template>
            <template v-else>
              <font-awesome-icon title="Modification" icon="pen" class="mt-2" @click="editItem(item, index)" />
              {{item.name}}
            </template>
          </div>
          <div class="dx-field-value" style="width:20%">
            <DxSwitch hint="Visible" v-model="item.isVisible" @value-changed="saveChange(index)"/>
          </div>
        </div>
      </div>
  </div>  
</template>
<script lang="ts">

import { SubCauseViewModel } from '@/types/ViewModels/SubCauseViewModel';
import { DxTextBox } from 'devextreme-vue/text-box';
import { DxSwitch } from 'devextreme-vue/switch';
import AdminService from '@/services/AdminService';
import notify from 'devextreme/ui/notify';
import { confirm } from 'devextreme/ui/dialog';
import { Getters } from '@/store';

const $AdminService = new AdminService();

export default {
  name: 'ParamsSubCause',
  components: {
    DxTextBox,
    DxSwitch
  },
  data() {
    return {
      Param: {
        ID: 3,
        HeaderName: 'Sous-Cause',
        Items: []
      },
      NewParam: null,
      EditedItem: null
    };
  },
  mounted(){
    this.initDatas();
    this.$store.watch(
      (_: void, getter: Getters) => getter.getToUpdateAdminBlocks.indexOf('SubCause')> -1 ? this.initDatas() : false ,
      (newVal: string) => newVal
    )
  },
  methods:{
    initDatas(){
  

      $AdminService.GetSubCauses().then((resp) => {
        resp.forEach((element: any) => {
          element.isVisible = !element.isDeleted
        });
        this.Param.Items = resp;        
      });
    },
    addNewParam(){
  
      this.NewParam = { ID: 0, name: '', isVisible: true };
      const container = this.$el.querySelector("#container");
      container.scrollTop = 0;
    },
    removeNewParam(){
  
      this.NewParam = null;
    },
    async saveParam(){
  
      const data: SubCauseViewModel = {
        ID: this.NewParam.ID,
        Name: this.NewParam.name,
        IsDeleted: !this.NewParam.isVisible
      };
      const response = await $AdminService.CreateSubCause(data);

      if(response.serviceMessage.operationSuccess == true){
        this.Param.Items.push({
          id: response.object.id,
          name: response.object.name,
          isVisible: !response.object.isDeleted
        });
        
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1500); 
        this.NewParam = null;
      }
      else{
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1500);
      }
    },
    editItem(item: any, index: number) {
  
      
      this.EditedItem = {
        ID: item.id, 
        name: item.name,
        isVisible: item.isVisible,
        Index: index
      };
    },
    cancelEdit() {
  
      this.EditedItem = null;
    },
    async saveEdit() {
  

      const item = this.Param.Items[this.EditedItem.Index];

      const data: SubCauseViewModel = {
        ID: this.EditedItem.ID,
        Name: this.EditedItem.name,
        IsDeleted: !item.isVisible
      };
      const response = await $AdminService.UpdateSubCause(data);

      if(response.serviceMessage.operationSuccess == true){

        this.Param.Items[this.EditedItem.Index] = {
          id: response.object.id,
          name: response.object.name,
          isVisible: !response.object.isDeleted
        };
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1500);
        this.EditedItem = null;
      }
      else{
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1500);
      }
    },
    async saveChange(index: number) {
  

      const item = this.Param.Items[index];
      const data: SubCauseViewModel = {
        ID: item.id,
        Name: item.name,
        IsDeleted: !item.isVisible
      };
      let response: any = {};
      if(item.isVisible) {
        if(item.isDeleted)
        response =  await $AdminService.UpdateSubCause(data);
      }
      else {
        if(item.localizations.length !== 0) {
          const result = confirm("<i style='text-align:center'> Cette suppression entrainera le retrais de cette valeur dans tous les localisations où celle-ci est présente.<p>Voulez-vous continuer ?</p></i>", "Confirmer");
          result.then(async (dialogResult) => {
              if(dialogResult){
                response =  await $AdminService.UpdateSubCause(data);
                this.initDatas();
                this.$store.commit('SET_TO_UPDATE_ADMIN_BLOCK', ['Localisation']);
                notify({ message: 'Opération réussie.', width: 350 }, 'info', 1500);
              }
              else {
                item.isVisible = !item.isVisible;
              }
          });
        }
        else {
          response =  await $AdminService.UpdateSubCause(data);
        }
      }
      if(response?.serviceMessage?.operationSuccess == true){
        this.initDatas();
        this.$store.commit('SET_TO_UPDATE_ADMIN_BLOCK', ['Localisation']);
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1500);
      }
      else if(response?.serviceMessage?.operationSuccess == false){
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1500);
      }
    }
  }
};
</script>

<style scoped>
  .card-body { height: 260px; }
</style>