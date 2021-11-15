import { ClientStatusViewModel } from "./ViewModels/ClientStatusViewModel";
import { Intervention } from './DTO/intervention';

export interface ResponseModel {
    Success: number,
    Result: any,
    Message: string
}

export interface clientKORow {
    id: number,
    statusFirstCallId: number,
    statusFirstCall: ClientStatusViewModel,
    firstCallComment: string,
    srvID: number,
    ontPath: string,
    conclusionId?: number,
    localizationId?: number,
    subCauseId?: number,
    problemOrigin?: string,
}