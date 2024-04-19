export type OperationResult = {
    outcome: OperationOutcome;
    message: string;
}

export enum OperationOutcome {
    Failure = 'Failure',
    Warning = 'Warning',
    Success = 'Success',
}