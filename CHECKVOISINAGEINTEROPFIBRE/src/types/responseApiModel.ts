export interface ResponseApiModel {
    Result: any,
    ServiceMessage: ServiceMessage
}

export interface ServiceMessage {
    OperationSuccess: Boolean,
    ErrorMessage: string,
}