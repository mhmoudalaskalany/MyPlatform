import { Lookup, SharedProperties } from "../shared/shared";


export interface SupplierDto extends Lookup, Partial<SharedProperties> {
    contactPerson: string;
    email: string;
    phone: string;
    address: string;
}

export interface AddSupplierDto extends Lookup, Partial<SharedProperties> {

}


export interface UpdateSupplierDto extends Lookup, Partial<SharedProperties> {

}

