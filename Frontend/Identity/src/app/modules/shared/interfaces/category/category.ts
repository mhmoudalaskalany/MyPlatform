import { Lookup, SharedProperties } from "../shared/shared";


export interface CategoryDto extends Lookup, Partial<SharedProperties> {
    code: string;
}

export interface AddCategoryDto extends Lookup, Partial<SharedProperties> {
    code: string;
}


export interface UpdateCategoryDto extends Lookup, Partial<SharedProperties> {
    code: string;
}

