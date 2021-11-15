import { ClientStatusViewModel } from '@/types/ViewModels/ClientStatusViewModel';
import { ConclusionViewModel } from '@/types/ViewModels/ConclusionViewModel';
import { InterventionTypeViewModel } from '@/types/ViewModels/InterventionTypeViewModel';
import { LocalizationViewModel } from '@/types/ViewModels/LocalizationViewModel';
import { SubCauseViewModel } from '@/types/ViewModels/SubCauseViewModel';
import HttpClient from './HttpClient';

export default class AdminService {

    URL_BASE = `${process.env.VUE_APP_BASE_URL}/Admin`;
    $HttpClient = new HttpClient();

    //#region "Conclusion"

    async CreateConclusion(viewModel: ConclusionViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/CreateConclusion`, viewModel));
        return response.data;
    }

    async UpdateConclusion(viewModel: ConclusionViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/UpdateConclusion` , viewModel));
        return response.data;
    }

    async GetConclusions(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetConclusions`, null));
        return response.data;
    }

    //#endregion

    //#region "Status"

    async CreateStatus(viewModel: ClientStatusViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/CreateClientStatus` , viewModel));
        return response.data;
    }

    async UpdateStatus(viewModel: ClientStatusViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/UpdateClientStatus` , viewModel));
        return response.data;
    }

    async GetStatus(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetClientStatus`, null));
        return response.data;
    }

    //#endregion

    //#region "SubCause"

    async CreateSubCause(viewModel: SubCauseViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/CreateSubCause` , viewModel));
        return response.data;
    }

    async UpdateSubCause(viewModel: SubCauseViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/UpdateSubCause` , viewModel));
        return response.data;
    }

    async GetSubCauses(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetSubCauses`, null));
        return response.data;
    }

    //#endregion

    //#region "Localization"

    async CreateLocalization(viewModel: LocalizationViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/CreateLocalization` , viewModel));
        return response.data;
    }

    async UpdateLocalization(viewModel: LocalizationViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/UpdateLocalization` , viewModel));
        return response.data;
    }

    async GetLocalizations(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetLocalizations`, null));
        return response.data;
    }

    //#endregion

    //#region "InterventionType"

    async CreateInterventionType(viewModel: InterventionTypeViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/CreateInterventionType` , viewModel));
        return response.data;
    }

    async UpdateInterventionType(viewModel: InterventionTypeViewModel){
        const response = (await this.$HttpClient.Post(`${this.URL_BASE}/UpdateInterventionType` , viewModel));
        return response.data;
    }

    //#endregion
}