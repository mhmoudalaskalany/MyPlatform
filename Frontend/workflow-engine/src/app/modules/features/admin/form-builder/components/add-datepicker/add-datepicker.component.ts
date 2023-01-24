import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseFormBuilderComponent } from 'base/components/base-form-builder-component';
import { Shell } from 'base/components/shell';
import { FormBuilderService } from '../../services/form-builder.service';
import * as moment from 'moment';
@Component({
    selector: 'app-add-datepicker',
    templateUrl: './add-datepicker.component.html',
    styleUrls: ['./add-datepicker.component.scss']
})
export class AddDatePickerComponent extends BaseFormBuilderComponent implements OnInit {

    code: string;
    control: any;
    today = moment().format();

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
            value: [null],
            required: [null]
        });

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
        formValue.type = 'datepicker';
        formValue.validators.required = value.required ? value.required : false;


        console.log(this.form.valid);
        this.model = {
            control: JSON.stringify(formValue),
            serviceId: this.serviceId
        }
        console.log(this.model);
        super.submitReactive();
    }
}
