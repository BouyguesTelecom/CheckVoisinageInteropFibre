import { createInterventionRequest } from '@/types/request/createInterventionRequest';
import { TakePhotoRequest } from '@/types/request/takePhotoRequest';
import { updateClientResultRequest } from '@/types/request/updateClientResultRequest';
import HttpClient from './HttpClient';

export default class HomeService {

    $HttpClient = new HttpClient();

    URL_BASE = `${process.env.VUE_APP_BASE_URL}/Home`;

    //#region "Take Photo"

    async TakePhoto(param: TakePhotoRequest){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/TakePhoto` , param));
        return response.data;
    }

    //#endregion

    //#region "Interventions"

    async GetAllInterventions(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetAllInterventions` , null));
        return response.data;
    }

    async GetInterventionTypes(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetInterventionTypes` , null));
        return response.data;
    }

    async GetOperators(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetOperators` , null));
        return response.data;
    }

    async GetMutualisationPoints(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetMutualisationPoints` , null));        
        return response.data;
    }

    async CreateIntervention(param: createInterventionRequest){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/CreateIntervention` , param));
        return response.data;
    }

    async GetPhotosByIntervention(param: number){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetPhotosByIntervention/${param}`, null));
        return response.data;
    }
    
    async GetCodeDecharge(param: number){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetCodeDechargeByInterventionId?interventionId=${param}`, null));
        return response.data;
    }

    //#endregion

    //#region "Client KO"

    async GetClientsKO(param: number){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetClientsKO/${param}`, null));
        return response.data;
    }

    async UpdateClientResult(param: updateClientResultRequest){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/UpdateClientResult`, param));
        return response.data;
    }

    //#endregion

    //#region "Historic"

    async GetInterventionHistoric(param: number)
    {
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetInterventionHistoric/${param}`, null));
        return response.data;
    }

    async GetHistoricAlarms(param: number)
    {
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetHistoricAlarms/${param}`, null));
        return response.data;
    }

    //#endregion

    //#region "Export"

    async ExportInterventions(fromDate: string, toDate: string)
    {
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/ExportInterventions/${fromDate}/${toDate}`, null));
        return response.data;
    }

    //#endregion
}