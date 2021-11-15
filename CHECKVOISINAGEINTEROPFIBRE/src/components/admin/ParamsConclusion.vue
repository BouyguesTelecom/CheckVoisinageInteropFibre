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
                <font-awesome-icon icon="plus" title="Ajouter" style="color:'white';width: '25px'; height: '20px'" @click="addNewParam()" />
              </div>
              <div v-if="NewParam" class="col">
                <font-awesome-icon icon="times" title="Annuler" style="color:'white';width: '25px'; height: '20px'" @click="removeNewParam()" />
              </div>
              <div v-if="NewParam" class="col">
                <font-awesome-icon icon="check" title="Sauvegarder" style="color:'white';width: '25px'; height: '20px'" @click="saveParam()" />
              </div>
              <div v-if="EditedItem" class="col">
                <font-awesome-icon icon="times" title="Annuler" style="color:'white';width: '25px'; height: '20px'" @click="cancelEdit()" />
              </div>
              <div v-if="EditedItem" class="col">
                <font-awesome-icon icon="check" title="Sauvegarder" style="color:'white';width: '25px'; height: '20px'" @click="saveEdit()" />
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="container" class="card-body overflow-auto">
        <div class="row mb-3" v-if="NewParam">
          <div class="col">
            <DxTextBox v-model="NewParam.Name" placeholder="Nouveau paramètre"/>
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
          <div class="dx-field-label p-0" style="width:80%">
            <template v-if="EditedItem && EditedItem.Index === index">
              <DxTextBox v-model="EditedItem.Name" />
            </template>
            <template v-else>
              <font-awesome-icon v-if="item.name != 'Nouveau voisin KO'" title="Modification" icon="pen" @click="editItem(item, index)" />
              &nbsp;<label class="mt-2">{{item.name}}</label>              
            </template>
          </div>
          <div class="dx-field-value" style="width:20%">
            <DxSwitch :disabled="item.name == 'Nouveau voisin KO'" hint="Visible" v-model="item.isVisible" @value-changed="saveChange(index)"/>
          </div>
        </div>
      </div>
  </div>  
</template>
<script lang="ts">
import { ConclusionViewModel } from '@/types/ViewModels/ConclusionViewModel';
import { DxTextBox } from 'devextreme-vue/text-box';
import { DxSwitch } from 'devextreme-vue/switch';
import AdminService from '@/services/AdminService';
import notify from 'devextreme/ui/notify';

const $AdminService = new AdminService();

export default {
  name: 'ParamsConclusion',
  components: {
    DxTextBox,
    DxSwitch
  },
  data() {
    return {
      Param: {   
        ID: 2,
        HeaderName: 'Conclusion',
        Items: []
      },
      NewParam: null,
      EditedItem: null
    };
  },
  mounted(){

    this.initDatas();
  },
  methods:{

    initDatas(){
  
      $AdminService.GetConclusions().then((resp) => {
        resp.forEach((element: any) => {
          element.isVisible = !element.isDeleted
        });
        this.Param.Items = resp;
      });
    },
    addNewParam(){
  
      this.NewParam = { ID: 0, Name: '', isVisible: true };
      const container = this.$el.querySelector("#container");
      container.scrollTop = 0;
    },
    removeNewParam(){
  
      this.NewParam = null;
    },
    async saveParam(){
  
      const data: ConclusionViewModel = {
        ID: this.NewParam.ID,
        Name: this.NewParam.Name,
        IsDeleted: !this.NewParam.isVisible
      };
      const response = await $AdminService.CreateConclusion(data);

      if(response.serviceMessage.operationSuccess == true){
        this.Param.Items.push({
          id: response.object.id,
          name: response.object.name,
          isVisible: !response.object.isDeleted
        });
        
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
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
        isVisible: item.isVisible,
        Index: index
      };
    },
    cancelEdit() {
  
      this.EditedItem = null;
    },
    async saveEdit() {
  
      const item = this.Param.Items[this.EditedItem.Index];

      const data: ConclusionViewModel = {
        ID: this.EditedItem.ID,
        Name: this.EditedItem.Name,
        IsDeleted: !item.isVisible
      };
      const response = await $AdminService.UpdateConclusion(data);

      if(response.serviceMessage.operationSuccess == true){

        this.Param.Items[this.EditedItem.Index] = {
          id: response.object.id,
          name: response.object.name,
          isVisible: !response.object.isDeleted
        };
        notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000);
        this.EditedItem = null;
      }
      else{
        notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 1000);
      }
    },
    async saveChange(index: number) {
  
      const item = this.Param.Items[index];

      const data: ConclusionViewModel = {
        ID: item.id,
        Name: item.name,
        IsDeleted: !item.isVisible
      };
      const response = await $AdminService.UpdateConclusion(data);

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
</style>