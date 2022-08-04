import { Lookup, SharedProperties } from "../shared/shared";

export interface PaymentTypeDto extends Lookup, Partial<SharedProperties> {
    description: string;
    code: string;
}

export interface AddPaymentTypeDto extends Lookup, Partial<SharedProperties> {
    description: string;
    code: string;
}

export interface UpdatePaymentTypeDto extends Lookup, Partial<SharedProperties> {
    description: string;
    code: string;
}