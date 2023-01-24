export interface JsonFormValidators {
    min?: number;
    max?: number;
    required?: boolean;
    requiredTrue?: boolean;
    email?: boolean;
    minLength?: boolean;
    maxLength?: boolean;
    pattern?: string;
    nullValidator?: boolean;
}

export interface JsonFormControlOptions {
    min?: string;
    max?: string;
    step?: string;
    icon?: string;
}

export interface DropDownDataOptions {
    id?: any;
    nameEn?: string;
    nameAr?: string;
    code?: string;
}

export interface DropDownOptions {
    bindValue?: string;
    bindLabelEn?: string,
    bindLabelAr?: string;
}

export interface JsonFormControls {
    name: string;
    label: string;
    value: string;
    type: string;
    dataType: string;
    options?: JsonFormControlOptions;
    dropDownDataOptions?: DropDownDataOptions[];
    dropDownOptions?: DropDownOptions;
    required: boolean;
    validators: JsonFormValidators;
}

export interface JsonFormData {
    controls: JsonFormControls[];
}