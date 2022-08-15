import { Lookup, SharedProperties } from "../shared/shared";

export interface PermissionDto extends Lookup, Partial<SharedProperties> {
    code: string;
}