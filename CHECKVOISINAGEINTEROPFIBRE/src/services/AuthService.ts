import HttpClient from './HttpClient';

export default class AuthService {

    $HttpClient = new HttpClient();

    URL_BASE = `${process.env.VUE_APP_BASE_URL}/Authent`;
    
    async GetLoggedUser(){
        const response = (await this.$HttpClient.Get(`${this.URL_BASE}/GetLoggedUser` , null));
        return response.data;
    }

}