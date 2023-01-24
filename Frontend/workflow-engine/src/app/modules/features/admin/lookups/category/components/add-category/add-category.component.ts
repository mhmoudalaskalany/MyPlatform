import { Component, OnInit } from '@angular/core';
import { BaseEditComponent } from 'base/components/base-edit-component';

import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../services/category.service';
@Component({
    selector: 'app-add-category',
    templateUrl: './add-category.component.html',
    styleUrls: ['./add-category.component.scss'],
})
export class AddCategoryComponent extends BaseEditComponent implements OnInit {

    override form: FormGroup;
    get Service(): CategoryService { return Shell.Injector.get(CategoryService); }
    constructor(public override route: ActivatedRoute) {
        super(route);
    }

    override ngOnInit(): void {
        this.initForm();
    }

    initForm(): void {
        this.form = this.Fb.group({
            nameEn: ['', Validators.required],
            nameAr: [null, Validators.required],
            descriptionEn: [null, Validators.required],
            descriptionAr: [null, Validators.required],
            code: [null]
        });
    }
}
