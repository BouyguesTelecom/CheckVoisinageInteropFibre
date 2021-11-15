import { Photo } from "./photo";

export interface PhotoResult {
    ClientNumber: number,

    DownClientNumber: number,

    NOKClientNumber: number,

    AverageDownTime: number,

    LowerTo20DownNumber: number,

    GreaterTo20DownNumber: number,

    OperationStatus: string,

    Photo: Photo
}