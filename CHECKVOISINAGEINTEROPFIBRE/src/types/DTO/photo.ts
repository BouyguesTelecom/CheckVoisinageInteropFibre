import { Alarm } from "./alarm";
import { Intervention } from "./intervention";
import { PhotoResult } from "./photoResult";
import { User } from "./user";

export interface Photo {
        ID?: number,
        Date?: Date,
        Description?: String,
        Intervention?: Intervention,
        User?: User,
        Alarms?: Array<Alarm>,
        PhotoResult?: PhotoResult
}