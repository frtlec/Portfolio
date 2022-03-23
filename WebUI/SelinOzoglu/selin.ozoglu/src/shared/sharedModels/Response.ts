export interface IResponse<T> {
    data: T;
    errors: string[];
}