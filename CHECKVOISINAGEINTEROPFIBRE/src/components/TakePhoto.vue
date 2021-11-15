<template>
  <div class="container">
    <div class="row">
      <div class="col m-4">
        <div class="dx-field">
          
    <div class="row">
      <div class="dx-field-label">Description</div>
    </div>
    <div class="row">
          <div class="dx-field-value">
            <DxTextBox
              placeholder="Description"
              v-model="description"           
            />
          </div>
          </div>
      </div>
    </div>
      <div class="col">
        <div class="row justify-content-center mt-5">
          <button class="btn btn-outline" @click="photoTrigger">
            <font-awesome-icon icon="camera" :style="{ color: 'white', width: '100px', height: '100px' }" />
          </button>
        </div>
      </div>
      <div class="col">
      </div>
    </div>
  </div>
</template>

<script lang="ts">

import { DxTextBox } from 'devextreme-vue/text-box';

import { confirm, alert } from 'devextreme/ui/dialog';
import { UserMixin } from '@/mixins/userMixin';

import HomeService from '@/services/HomeService';
import { Intervention } from '@/types/DTO/intervention';
import { TakePhotoRequest } from '@/types/request/takePhotoRequest';
import notify from 'devextreme/ui/notify';
import { Getters } from '@/store';

const $HomeService = new HomeService();


export default {
  mixins: [UserMixin],
  name: 'TakePhoto',
  props: {
    intervention: Object
  },
  data() {    
    return {
      description: '',
      selectedIntervention: this.$store.state.currentIntervention,
      commentsKO: []
    }
  },
  mounted(){
    this.initDatas();  
    this.$store.watch(
      (_: void, getter: Getters) => getter.getCurrentIntervention,
      (newVal: Intervention) => {
        this.selectedIntervention = newVal;
        this.initDatas();
      }
    );
    this.$store.watch(
      (_: void, getter: Getters) => getter.getCommentsClientKO,
      (newVal: string) => this.commentsKO = newVal
    );
    
  },
  methods:{
    initDatas(){
      if (this.selectedIntervention && this.selectedIntervention.interventionId != undefined){
        $HomeService.GetPhotosByIntervention(this.selectedIntervention.interventionId).then((resp) => {
          this.$emit('photosGot', resp.object);
        });
        
        $HomeService.GetClientsKO(this.selectedIntervention.interventionId).then((resp) => {
          this.$emit('clientsReceived', resp.object);
        });
      }
      else{
        notify({ message: "Pas d'intervention selectionnée. Merci d'en choisir une.", width: 350 }, 'info', 5000);
      }
    },
    photoTrigger(){
      if(this.commentsKO.every((comment: any)=> !comment.statusFirstCall.isCommentMandatory || ( comment.firstCallComment != null && comment.firstCallComment?.trim().length > 1))) {
        const result = confirm("<i> Êtes-vous sûr de vouloir prendre une photo ?</i>", "Confirmer");
        result.then(async (dialogResult) => {
            if(dialogResult){
              const data: TakePhotoRequest = {
                Description: this.description,
                InterventionId: this.selectedIntervention.interventionId
              };
              const loader = this.$loading.show();
              const response =  await $HomeService.TakePhoto(data);
              loader.hide();

              if(response.serviceMessage.operationSuccess == false)
              {
                notify({ message: response.serviceMessage.errorMessage, width: 350 }, 'error', 3000);
              }
              else{
                this.$emit('photoTaked', response.object);
                
                if (this.selectedIntervention && this.selectedIntervention.interventionId != undefined){
                  $HomeService.GetClientsKO(this.selectedIntervention.interventionId).then((resp) => {
                    if(response.serviceMessage.operationSuccess)
                    {
                      this.$emit('clientsReceived', resp.object);
                    }
                    else {
                      notify({ message: response.serviceMessage.errorMessage, width: 350 }, 'error', 3000);
                    }
                  });
                }
                
                notify({ message: 'operation réussie', width: 350 }, 'success', 3000);
              }

              
              
              return result;
            }
        });
      }
      else {
        alert("Veuillez renseigner les commentaires obligatoires avant de passer à l'action suivante  ", "Alert")
      }
    }
  },
  components: {
    DxTextBox
  }
}
</script>

<style>

</style>
