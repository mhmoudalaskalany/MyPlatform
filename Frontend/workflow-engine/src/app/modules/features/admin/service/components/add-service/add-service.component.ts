import { Component, OnInit } from '@angular/core';
import { BaseEditComponent } from 'base/components/base-edit-component';

import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ServiceService } from '../../services/service.service';
import { LookupsService } from 'shared/services/lookups/lookups.service';
@Component({
    selector: 'app-add-service',
    templateUrl: './add-service.component.html',
    styleUrls: ['./add-service.component.scss'],
})
export class AddServiceComponent extends BaseEditComponent implements OnInit {

    categories: any[] = [];
    templates: any[] = [];
    override form: FormGroup;
    get Service(): ServiceService { return Shell.Injector.get(ServiceService); }
    get LookupService(): LookupsService { return Shell.Injector.get(LookupsService); }
    constructor(public override route: ActivatedRoute) {
        super(route);
    }

    override ngOnInit(): void {
        this.initForm();
        this.getlookups();
        super.ngOnInit();
    }

    initForm(): void {
        this.form = this.Fb.group({
            nameEn: ['', Validators.required],
            nameAr: [null, Validators.required],
            descriptionEn: [null, Validators.required],
            descriptionAr: [null, Validators.required],
            categoryId: [null, Validators.required],
            serviceTypeId: [1],
            templateId: [null, Validators.required],
            prefix: [null, Validators.required],
            imageUrl: [null],
            code: [null],
            isActive: [null],
            isDynamic: [null],
            isMultipleRequest: [null]
        });
    }

    getlookups(): void {
        this.LookupService.getCategories().subscribe((res: any) => {
            this.categories = res;
        });

        this.LookupService.getTemplates().subscribe((res: any) => {
            this.templates = res;
        })
    }
}
