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
        <div v-if="NewParam" class="row mb-3 card-add">
          <div class="col">
            <DxTextBox v-model="NewParam.Name" placeholder="Nouvelle localisation"/>
          </div>
          <div class="col text-right">
            <DxSwitch v-model="NewParam.isVisible"/>
          </div>
        </div>
        <div class="row header-params mb-2">
          <div class="col">Nom</div>
          <div class="col text-center">Sous-cause</div>
          <div class="col text-right">Visible</div>
        </div>
        <div class="row mt-3" v-for="(item, index) in Param.Items" :key="index">
            <div class="col-3 dx-field dx-field-label ml-3 py-2">
              <template v-if="EditedItem && EditedItem.Index === index">
                <DxTextBox v-model="EditedItem.Name" />
              </template>
              <template v-else>
                <font-awesome-icon title="Modification" icon="pen" class="mt-2" @click="editItem(item, index)" />
                {{item.name}}
              </template>
            </div>        
            <div class="col-7 dx-field dx-field-label ml-3 py-2">
              <template v-if="EditedItem && EditedItem.Index === index">
                <DxTagBox :items="Param.ParamSousCause" :search-enabled="true" v-model="item.subCauses" display-expr="name" placeholder="Sous-Cause" />
              </template>
              <template v-else>
                <span v-for="(item, index) in item.subCauses" :key="index" class="itemParam"> {{ item.name }} </span>
              </template>
            </div>
            <div class="col-1 text-right py-2">
              <DxSwitch hint="Visible" v-model="item.isVisible" @value-changed="saveChange(index)"/>
            </div>
          </div>
        </div>
    </div>
</template>
<script lang="ts">

import { LocalizationViewModel } from '@/types/ViewModels/LocalizationViewModel';
import { DxTextBox } from 'devextreme-vue/text-box';
import { DxSwitch } from 'devextreme-vue/switch';
import { DxTagBox } from 'devextreme-vue/tag-box';
import AdminService from '@/services/AdminService';
import notify from 'devextreme/ui/notify';
import { Getters } from '@/store';

const $AdminService = new AdminService();

export default {
  name: 'ParamsLocalisation',
  components: {
    DxTextBox,
    DxSwitch,
    DxTagBox
  },
  data() {
    return {
      Param: {
        ID: 4,
        HeaderName: 'Localisation',
        Items: [],
        ParamSousCause:[]
      },
      NewParam: null,
      EditedItem: null
    };
  },
  mounted(){
    this.initDatas();
    this.$store.watch(
      (_: void, getter: Getters) => getter.getToUpdateAdminBlocks.indexOf('Localisation')> -1 ? this.initDatas() : false ,
      (newVal: string) => newVal
    )
  },
  methods:{
    initDatas(){
  
      $AdminService.GetSubCauses().then((resp) => {
        this.Param.ParamSousCause = resp.filter( (e: any) => !e.isDeleted);
      });
      $AdminService.GetLocalizations().then((resp) => {
        resp.forEach((element: any) => {
          element.isVisible = !element.isDeleted
        });
        this.Param.Items = resp;
      });
    },
    addNewParam(){
      this.NewParam = { ID: 0, Name: '', isVisible: true, SubCauses: [] };
      const container = this.$el.querySelector("#container");
      container.scrollTop = 0;
    },
    removeNewParam(){
      this.NewParam = null;
    },
    async saveParam(){
  
      const data: LocalizationViewModel = {
        ID: this.NewParam.ID,
        Name: this.NewParam.Name,
        IsDeleted: !this.NewParam.isVisible,
        SubCauses:[]
      };
      const response = await $AdminService.CreateLocalization(data);

      if(response.serviceMessage.operationSuccess == true){
        this.Param.Items.push({
          id: response.object.id,
          name: response.object.name,
          isVisible: !response.object.isDeleted,
          subCauses: response.object.subCauses
        });
        
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000);
        this.$store.commit('SET_TO_UPDATE_ADMIN_BLOCK', ['SubCause']); 
        this.NewParam = null;
      }
      else{
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1000);
      }
    },
    editItem(item: any, index: number) {
  
      
      this.EditedItem = {
        ID: item.id, 
        Name: item.name,
        IsDeleted: !item.isVisible,
        SubCauses: item.subCauses,
        Index: index
      };
      $AdminService.GetSubCauses().then((resp) => {
        this.Param.ParamSousCause = resp.filter( (e: any) => !e.isDeleted);
      });
    },
    cancelEdit() {
  
      this.EditedItem = null;
    },
    async saveEdit() {
  
      const item = this.Param.Items[this.EditedItem.Index];

      const data: LocalizationViewModel = {
        ID: this.EditedItem.ID,
        Name: this.EditedItem.Name,
        IsDeleted: !item.isVisible,
        SubCauses: item.subCauses
      };
      const response = await $AdminService.UpdateLocalization(data);

      if(response.serviceMessage.operationSuccess == true){

        this.Param.Items[this.EditedItem.Index] = {
          id: response.object.id,
          name: response.object.name,
          isDeleted: response.object.isDeleted,
          subCauses: response.object.subCauses,
        };
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000);
        this.$store.commit('SET_TO_UPDATE_ADMIN_BLOCK', ['SubCause']);
        this.EditedItem = null;
      }
      else{
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1000);
      }
    },
    async saveChange(index: number) {
  
      const item = this.Param.Items[index];

      const data: LocalizationViewModel = {
        ID: item.id,
        Name: item.name,
        IsDeleted: !item.isVisible,
        SubCauses: item.subCauses
      };
      const response = await $AdminService.UpdateLocalization(data);

      if(response.serviceMessage.operationSuccess == true){
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000);
      }
      else{
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1000);
      }
    }
  }
};
</script>

<style scoped>
  .card-body { height: 260px; }
  .card-add{
    border-bottom: 1px solid #2c3e50;
    height: 50px;
    margin-bottom: 10px;
  }
  .itemParam{
    padding: 5px 13px;
    background: #2C3E50;
    margin: 0 2px;
  }
</style>