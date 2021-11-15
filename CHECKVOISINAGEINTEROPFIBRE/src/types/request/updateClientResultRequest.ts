

export interface updateClientResultRequest {
    clientResultId: number,
    srvID: number,
    ontPath: string,
    statusFirstCallId?: number,
    firstCallComment?: string,
    conclusionId?: number,
    localizationId?: number,
    subCauseId?: number,
    additionalInfo?: string,
}