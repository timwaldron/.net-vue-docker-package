export type ServiceResult<T> = {
    result: T;
    status: ServiceResultStatus;
    messages: ServiceResultMessage[];
}

export type ServiceResultMessage = {
    // field: string;
    message: string;
}

export enum ServiceResultStatus {
    NA = 'NA',
    Success = 'Success',
    Warning = 'Warning',
    Failure = 'Failure',
}