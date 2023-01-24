import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseFormBuilderComponent } from 'base/components/base-form-builder-component';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { FormBuilderService } from '../../services/form-builder.service';

@Component({
    selector: 'app-add-dropdown',
    templateUrl: './add-dropdown.component.html',
    styleUrls: ['./add-dropdown.component.scss']
})
export class AddDropDownComponent extends BaseFormBuilderComponent implements OnInit {

    code: string;
    control: any;

    @Input() serviceId: any;
    get Service(): FormBuilderService { return Shell.Injector.get(FormBuilderService); }
    get Fb(): FormBuilder { return Shell.Injector.get(FormBuilder); }
    get Router(): Router { return Shell.Injector.get(Router); }
    constructor() {
        super();
    }

    get dropDownDataOptions() {
        return this.form.get('dropDownDataOptions') as FormArray;
    }

    override ngOnInit(): void {
        this.initForm();
    }

    initForm(): void {
        this.form = this.Fb.group({
            nameEn: [null, Validators.required],
            nameAr: [null, Validators.required],
            isServerSide: [false],
            url: [null],
            required: [null],
            dropDownDataOptions: this.Fb.array([this.initOption()]),
        });

    }

    initOption() {
        return this.Fb.group({
            id: [null],
            nameEn: [null, Validators.required],
            nameAr: [null, Validators.required]
        });
    }


    showorHideUrl(checked): void {
        if (checked) {
            this.form.get('url').addValidators(Validators.required);
            this.dropDownDataOptions.clear();
        } else {
            this.form.get('url').removeValidators(Validators.required);
            this.dropDownDataOptions.controls.push(this.initOption());
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
        formValue.dropDownOptions = {};
        formValue.name = this.form.controls['nameEn'].value;
        formValue.labelEn = this.form.controls['nameEn'].value;
        formValue.labelAr = this.form.controls['nameAr'].value;
        formValue.dropDownDataOptions = value.dropDownDataOptions;
        formValue.dropDownOptions.url = value.url;
        formValue.dropDownOptions.bindLabelAr = 'nameAr';
        formValue.dropDownOptions.bindLabelEn = 'nameEn';
        formValue.type = 'dropdown';
        formValue.validators.required = value.required ? value.required : false;
        console.log(this.form.valid);
        debugger;
        this.model = {
            control: JSON.stringify(formValue),
            serviceId: this.serviceId
        }
        console.log(this.model);
        super.submitReactive();
    }
}
