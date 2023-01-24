import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseFormBuilderComponent } from 'base/components/base-form-builder-component';
import { Shell } from 'base/components/shell';
import { FormBuilderService } from '../../services/form-builder.service';
import * as moment from 'moment';
@Component({
    selector: 'app-add-checkbox',
    templateUrl: './add-checkbox.component.html',
    styleUrls: ['./add-checkbox.component.scss']
})
export class AddCheckBoxComponent extends BaseFormBuilderComponent implements OnInit {

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
        formValue.type = 'checkbox';
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
