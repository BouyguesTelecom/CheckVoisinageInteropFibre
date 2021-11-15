import { Client } from "./client";
import { Photo } from "./photo";

export interface Alarm {
    NotificationId: String,

    AlarmType: String,

    Address: String,

    ONTNr: String,

    StartTime: String,

    ClearTime: String,

    Photo: Photo,

    Client : Client,

}