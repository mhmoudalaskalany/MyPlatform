import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseFormBuilderComponent } from 'base/components/base-form-builder-component';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { FormBuilderService } from '../../services/form-builder.service';

@Component({
    selector: 'app-add-text',
    templateUrl: './add-text.component.html',
    styleUrls: ['./add-text.component.scss']
})
export class AddTextComponent extends BaseFormBuilderComponent implements OnInit {

    code: string;
    control: any;
    dateTypes: any[] = [
        {
            id: 1,
            nameEn: 'Text',
            nameAr: 'نص',
            code: 'text'
        },
        {
            id: 2,
            nameEn: 'Numbers',
            nameAr: 'أرقام',
            code: 'number'
        },
        {
            id: 3,
            nameEn: 'Email',
            nameAr: 'بريد الكترونى',
            code: 'email'
        },
        {
            id: 4,
            nameEn: 'Password',
            nameAr: 'كلمة مرور',
            code: 'password'
        },
    ];

    @Input() serviceId: any;
    get Service(): FormBuilderService { return Shell.Injector.get(FormBuilderService); }
    get Fb(): FormBuilder { return Shell.Injector.get(FormBuilder); }
    get Router(): Router { return Shell.Injector.get(Router); }
    constructor() {
        super();
    }

    override ngOnInit(): void {
        this.initForm();
    }

    initForm(): void {
        this.form = this.Fb.group({
            nameEn: [null, Validators.required],
            nameAr: [null, Validators.required],
            dataType: ['text', Validators.required],
            type: ['text'],
            value: [null],
            minLength: [null],
            maxLength: [null],
            min: [null],
            max: [null],
            required: [null]
        });

        this.form.get('min').disable();
        this.form.get('max').disable();
    }

    setControlDataType(event): void {
        switch (event.code) {
            case 'number': {
                this.form.get('minLength').disable();
                this.form.get('maxLength').disable();
                this.form.get('min').enable();
                this.form.get('max').enable();
                break;
            }
            default: {
                this.form.get('minLength').enable();
                this.form.get('maxLength').disable();
                this.form.get('min').disable();
                this.form.get('max').disable();
                break;
            }
        }
    }

    submit(): void {
        if (!this.serviceId) {
            this.Alert.showError(this.Localize.translate.instant('Validation.SelectService'));
            return;
        }
        const value = this.form.value;
        let formValue = {} as any;
        formValue.validators = {};
        formValue.name = this.form.controls['nameEn'].value;
        formValue.labelEn = this.form.controls['nameEn'].value;
        formValue.labelAr = this.form.controls['nameAr'].value;
        formValue.value = this.form.controls['value'].value;
        formValue.dataType = this.form.controls['dataType'].value;
        formValue.type = 'text';
        formValue.validators.required = value.required ? value.required : false;
        if (formValue.dataType == 'number') {
            formValue.validators.min = value.min ? value.min : "";
            formValue.validators.max = value.max ? value.max : "";

        } else {
            formValue.validators.minLength = value.minLength ? value.minLength : "";
            formValue.validators.maxLength = value.maxLength ? value.maxLength : "";
        }
        console.log(this.form.valid);
        this.model = {
            control: JSON.stringify(formValue),
            serviceId: this.serviceId
        }
        console.log(this.model);
        super.submitReactive();
    }
}
