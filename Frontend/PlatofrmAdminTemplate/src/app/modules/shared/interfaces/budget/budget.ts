import { Lookup, SharedProperties } from "../shared/shared";


export interface BudgetDto extends Lookup, Partial<SharedProperties> {
    code: string;
}


export interface AddBudgetDto extends Lookup, Partial<SharedProperties> {
    code: string;
}

export interface UpdateBudgetDto extends Lookup, Partial<SharedProperties> {
    code: string;
}