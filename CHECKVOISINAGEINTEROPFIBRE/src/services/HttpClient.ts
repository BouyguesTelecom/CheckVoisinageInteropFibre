import Axios, { AxiosResponse } from 'axios';
import notify from 'devextreme/ui/notify';

const HttpAxios = Axios.create({
    baseURL: process.env.VUE_APP_BASE_URL,
    headers: {
        "Content-Type": "application/json"
    },
    withCredentials: true
});

export default class HttpClient{

    public Get(url: string, _: any) {
        const xhr: Promise<AxiosResponse<any>> = HttpAxios.get(url);

        xhr.catch(() => {
            notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 3000);
        });
        return xhr;
    }

    public Post(url: string, data: any) {
        let xhr: Promise<AxiosResponse<any>>;

        if (data){
            xhr = HttpAxios.post(url, data, {
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': process.env.VUE_APP_BASE_URL
                }
            });
        }
        else {
            xhr = HttpAxios.post(url);
        }

        xhr.catch(() => {
            notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 3000);
        });
        return xhr;
    }

    public Patch(url: string, data: any) {
        let xhr: Promise<AxiosResponse<any>>;

        if (data){
            xhr = HttpAxios.patch(url, data, {
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': process.env.VUE_APP_BASE_URL
                }
            });
        }
        else {
            xhr = HttpAxios.patch(url);
        }

        xhr.catch(() => {
            notify({ message: 'Une erreur s\'est produite durant l\'opération', width: 350 }, 'error', 3000);
        });
        return xhr;
    }
}