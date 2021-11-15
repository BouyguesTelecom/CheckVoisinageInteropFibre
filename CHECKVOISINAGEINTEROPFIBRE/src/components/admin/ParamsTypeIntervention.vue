  <template>
    <div class="card">
      <div class="card-header">
        <div class="row">
          <div class="col">
            {{Param.HeaderName}}
          </div>
          <div class="col">
            <div class="row mt-1 float-right">
              <div v-if="!newParam && !EditedItem" class="col">
                <font-awesome-icon icon="plus" title="Ajouter" class="simple-icon" @click="addNewParam()" />
              </div>
              <div v-if="newParam" class="col">
                <font-awesome-icon icon="times" title="Annuler" class="simple-icon" @click="removeNewParam()" />
              </div>
              <div v-if="newParam" class="col">
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
        <div class="row mb-3" v-if="newParam">
          <div class="col">
            <DxTextBox v-model="newParam.Name" placeholder="Nouveau type d'intervention"/>
          </div>
          <div class="col text-center">
            <DxNumberBox
              v-model="newParam.Duration"
              :value="4"
              :min="1"
              :max="24"
              :step="1"
              format="#"
              :show-spin-buttons="true"
              hint="Durée en heure"
              placeholder="Durée en heure"/>
          </div>
          <div class="col text-right">
            <DxSwitch v-model="newParam.isVisible"/>
          </div>
        </div>
        <div class="row header-params mb-2">
          <div class="col">Nom</div>
          <div class="col text-center">Durée (en heure)</div>
          <div class="col text-right">Visible</div>
        </div>
        <div class="row mt-3" v-for="(item, index) in Param.items" :key="index">      
          <div class="col dx-field dx-field-label ml-3">
            <template v-if="EditedItem && EditedItem.Index === index">
                <DxTextBox v-model="EditedItem.Name" />
            </template>
            <template v-else>
              <font-awesome-icon title="Modification" icon="pen" @click="editItem(item, index)" />
              {{item.name}}
            </template>
          </div>        
          <div class="col">
            <template v-if="EditedItem && EditedItem.Index === index">
              <DxNumberBox
                v-model="item.duration"
                :value="4"
                :min="1"
                :max="24"
                :step="1"
                format="#"
                :show-spin-buttons="true"
                hint="Durée en heure"
                placeholder="Durée en heure"/>
            </template>
            <template v-else>
              <div class="p-1 text-center">{{item.duration}}</div>
            </template>
          </div>
          <div class="col text-right py-1">
            <DxSwitch hint="Visible" v-model="item.isVisible" @value-changed="saveChange(index)"/>
          </div>
        </div>
      </div>
  </div>  
</template>
<script lang="ts">
import { DxTextBox } from 'devextreme-vue/text-box';
import { DxNumberBox } from 'devextreme-vue/number-box';
import { DxSwitch } from 'devextreme-vue/switch';


import AdminService from '@/services/AdminService';
import HomeService from '@/services/HomeService';
import { InterventionTypeViewModel } from '@/types/ViewModels/InterventionTypeViewModel';
import notify from 'devextreme/ui/notify';
const $AdminService = new AdminService();
const $HomeService = new HomeService();


export default {
  name: 'ParamsTypeIntervention',
  components: {
    DxTextBox,
    DxSwitch,
    DxNumberBox
  },
  data() {
    return {
      Param: /* --SERVICE-- get paramstype intervetion*/{
                    ID: 5,
                    HeaderName: 'Type Intervention',
                    items: []
                },
      newParam: null,
      EditedItem: null
    };
  },
  mounted(){

    this.initDatas();
  },
  methods:{
    initDatas(){
  

      $HomeService.GetInterventionTypes().then((resp) => {
        resp.forEach((element: any) => {
          element.isVisible = !element.isDeleted
        });
        this.Param.items = resp;
      });
    },
    addNewParam(){
  
      this.newParam = { IDParam: this.Param.ID, Name: '', isVisible: true, Duration: 4 };
      const container = this.$el.querySelector("#container");
      container.scrollTop = 0;
    },
    removeNewParam(){
  
      this.newParam = null;
    },
    async saveParam(){
      /* --SERVICE-- post param*/
  
      
      if(this.newParam.Name) {
        const param: InterventionTypeViewModel = {
          id : 0,
          interventionTypeName : this.newParam.Name,
          isDeleted: !this.newParam.isVisible,
          duration: this.newParam.Duration
        } 
        const response = await $AdminService.CreateInterventionType(param); 
        if (response.serviceMessage.operationSuccess){
          notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
          this.Param.items.push({
          id: response.object.id,
          name: response.object.name,
          duration: response.object.duration,
          isVisible: !response.object.isDeleted
        });    
        }
      }  
      this.newParam = null;
    },
    editItem(item: any, index: number) {
  
      this.EditedItem = {ID: item.id, Name: item.name, Index: index};
    },
    cancelEdit() {
  
      this.EditedItem = null;
    },
    async saveEdit() {
      /* --SERVICE-- post param*/
  
      
      const item = this.Param.items[this.EditedItem.Index];
      this.Param.items[this.EditedItem.Index].Name = this.EditedItem.Name;

      if (this.EditedItem) {
        const param: InterventionTypeViewModel = {
          id : this.EditedItem.ID,
          interventionTypeName : this.EditedItem.Name,
          isDeleted: !item.isVisible,
          duration: item.duration
        } 
        const response = await $AdminService.UpdateInterventionType(param);
        if (response.serviceMessage.operationSuccess){
          notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
          this.Param.items[this.EditedItem.Index] = item;
        }
        
      }
      this.EditedItem = null;
    },
    async saveChange(index: number) {
  
      
      const item = this.Param.items[index];
      if (item) {
        const param: InterventionTypeViewModel = {
          id : item.id,
          interventionTypeName :item.name,
          isDeleted: !item.isVisible,
          duration: item.duration
        } 
        const response = await $AdminService.UpdateInterventionType(param);
        if (response.serviceMessage.operationSuccess){
          notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
        }
        
      }
    }
  }
};
</script>

<style scoped>
.card-body { height: 260px; }
</style>