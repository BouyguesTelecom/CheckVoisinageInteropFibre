  <template>
    <div class="card" style="height: 18rem;">
      <div class="card-header">
        <div class="row">
          <div class="col">
            {{Param.HeaderName}}
          </div>
          <div class="col">
            <div class="row mt-1 float-right">
              <div v-if="!newParam && !EditedItem" class="col">
                <font-awesome-icon icon="plus" title="Ajouter" style="color:'white';width: '25px'; height: '20px'" @click="addNewParam()" />
              </div>
              <div v-if="newParam" class="col">
                <font-awesome-icon icon="times" title="Annuler" style="color:'white';width: '25px'; height: '20px'" @click="removeNewParam()" />
              </div>
              <div v-if="newParam" class="col">
                <font-awesome-icon icon="check" title="Sauvegarder" style="color:'white';width: '25px'; height: '20px'" @click="saveParam()" />
              </div>
              <div v-if="EditedItem" class="col">
                <font-awesome-icon icon="times" title="Annuler" style="color:'red';width: '25px'; height: '20px'" @click="cancelEdit()" />
              </div>
              <div v-if="EditedItem" class="col">
                <font-awesome-icon icon="check" title="Sauvegarder" style="color:'red';width: '25px'; height: '20px'" @click="saveEdit()" />
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="container" class="card-body overflow-auto">
        <div class="row mb-3" v-if="newParam">
          <div class="col-7">
            <DxTextBox v-model="newParam.name" placeholder="Nouveau paramètre"/>
          </div>
          <div class="col-2">
            <DxSwitch v-model="newParam.isCommentMandatory"/>
          </div>
          <div class="col-3 text-right">
            <DxSwitch v-model="newParam.isVisible"/>
          </div>
        </div>
        <div class="row header-params mb-2">
          <div class="col-3 text-center">Nom</div>          
          <div class="col-3 text-right">Défaut</div>
          <div class="col-4 text-center">Commentaire</div>
          <div class="col-2" style="padding: 0">Visible</div>
        </div>
        <div class="dx-field mt-3" v-for="(item, index) in Param.items" :key="index">
          <div class="dx-field-label p-0">
            <template v-if="EditedItem && EditedItem.Index === index">
              <DxTextBox placeholder="Nom" v-model="EditedItem.name" />
            </template>
            <template v-else>
              <font-awesome-icon title="Modification" icon="pen" class="mt-2" @click="editItem(item, index)" />
              {{item.name}}
            </template>
          </div>
          <div class="dx-field-value">
            <div class="row">
              <div class="col">              
                <font-awesome-icon icon="flag" title="Définir comme valeur par défaut" :style="{'color': item.isDefault ? '' : '#2c3e50'}" class="mt-2" @click="makeDefault(item)" />
              </div>
              <div class="col">
                <DxSwitch hint="Commentaire obligatoire" v-model="item.isCommentMandatory" @value-changed="saveChange(index, 'c')"/>
              </div>
              <div class="col">
                <DxSwitch hint="Visible" v-model="item.isVisible" @value-changed="saveChange(index, 'v')"/>
              </div>
            </div>
            
          </div>
        </div>
      </div>
      <DxPopup
      :visible="popupVisible"
      v-model="popupVisible"
      :drag-enabled="false"
      :close-on-outside-click="true"
      :show-title="true"
      :width="300"
      :height="250"
      @hiding="onHiding"
      title="Alert !"
    >
      <p style="text-align:center">
        Le champ doit être visible pour qu'il puisse être choisi par défaut.
      </p>
    </DxPopup>
  </div>  
</template>
<script lang="ts">
import { DxTextBox } from 'devextreme-vue/text-box';
import { DxSwitch } from 'devextreme-vue/switch';
import DxPopup from 'devextreme-vue/popup';

import AdminService from '@/services/AdminService';
import { ClientStatusViewModel } from '@/types/ViewModels/ClientStatusViewModel';
import notify from 'devextreme/ui/notify';
const $AdminService = new AdminService();

export default {
  name: 'ParamsFirstCall',
  components: {
    DxTextBox,
    DxSwitch,
    DxPopup
  },
  data() {
    return {
      Param: /* --SERVICE-- get params 1st call*/{
        ID: 1,
        HeaderName: 'Statut 1er appel',
        defaultID: 5,
        items: []                    
      },
      newParam: null,
      EditedItem: null,
      popupVisible: false
    };
  },
  mounted() {
  
      this.initDatas();
  },
  methods:{
    initDatas(){
  
      $AdminService.GetStatus().then((resp) => {
        resp.forEach((element: any) => {
          element.isVisible = !element.isDeleted
        });
        this.Param.items = resp;
      });
    },
    async addNewParam(){
  
      this.newParam = { IDParam: this.Param.ID, name: '', isVisible: true, isCommentMandatory: false, isDeleted: false, isDefault: false };
      const container = this.$el.querySelector("#container");
      container.scrollTop = 0;
    },
    removeNewParam(){
  
      this.newParam = null;
    },
    async saveParam(){
  
      if(this.newParam.name) {
        const param: ClientStatusViewModel = {
          id : 0,
          name : this.newParam.name,
          isDeleted: !this.newParam.isVisible,
          isCommentMandatory : this.newParam.isCommentMandatory,
          isDefault : this.newParam.isDefault
        } 
        const response = await $AdminService.CreateStatus(param); 
        
        if (response.serviceMessage.operationSuccess){
          notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
          this.initDatas(); 
        }
      } 
      
      this.newParam = null;
    },
    async makeDefault(item: any) {
  
      if(!item.isVisible) {
        this.popupVisible = true;
        return;
      }
      if(!item.isDefault) {
        const noMore = this.Param.items.find((e: any) => e.isDefault) as any;
        if(noMore) {
          noMore.isDefault = false;
        }
        item.isDefault = true;
        let param: ClientStatusViewModel = {
          id : item.id,
          name : item.name,
          isDeleted: item.isDeleted,
          isCommentMandatory : item.isCommentMandatory,
          isDefault : item.isDefault
        } 
        const response1 = await $AdminService.UpdateStatus(param);
        param = {
          id : noMore.id,
          name : noMore.name,
          isDeleted: noMore.isDeleted,
          isCommentMandatory : noMore.isCommentMandatory,
          isDefault : noMore.isDefault
        }
        const response2 = await $AdminService.UpdateStatus(param);
        if (response2.serviceMessage.operationSuccess && response1.serviceMessage.operationSuccess){
          notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
        }
      }      
    },
    onHiding() {
  
      this.popupVisible = false;
    },
    editItem(item: any, index: number) {
  
      this.EditedItem = {
        ID: item.id, 
        name: item.name, 
        Index: index,
      };
    },
    cancelEdit() {
  
      this.EditedItem = null;
    },
    async saveEdit() {
  
      const item = this.Param.items[this.EditedItem.Index];
      if (this.EditedItem) {
        const param: ClientStatusViewModel = {
          id : this.EditedItem.ID,
          name : this.EditedItem.name,
          isDeleted: !item.isVisible,
          isCommentMandatory : item.isCommentMandatory,
          isDefault : item.isDefault
        } 
        const response = await $AdminService.UpdateStatus(param);
        if (response.serviceMessage.operationSuccess){
          notify({ message: 'Opération réussie.', width: 350 }, 'info', 1000); 
          this.Param.items[this.EditedItem.Index] = param;
        }
        
      }
      this.EditedItem = null;
    },
    async saveChange(index: number, attribute: string) {
  
      const item = this.Param.items[index];
      if(!item.isVisible && item.isDefault) {
        item.isVisible = await !item.isVisible;
        this.popupVisible = true;
        return;
      }
      if ((item && item.isDeleted == item.isVisible)|| (item && attribute == 'c')) {
        item.isDeleted = !item.isVisible;
        const param: ClientStatusViewModel = {
          id : item.id,
          name : item.name,
          isDeleted: !item.isVisible,
          isCommentMandatory : item.isCommentMandatory,
          isDefault : item.isDefault
        } 
        const response = await $AdminService.UpdateStatus(param);
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
  .card-add{
    border-bottom: 1px solid #2c3e50;
    height: 50px;
    margin-bottom: 10px;
  }
</style>