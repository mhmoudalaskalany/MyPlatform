import { SharedProperties } from "../shared/shared";

export interface UserDto extends Partial<SharedProperties> {
    roleIds?: string[];
    name: string;
    userName: string;
    email: string;
    password: string,
    phone: string,
}
