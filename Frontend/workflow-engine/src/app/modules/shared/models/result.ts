import { HttpStatus } from "shared/enum/httpStatus";

export interface Result<T> {
    data: T;
    status: HttpStatus
    message: string;
}