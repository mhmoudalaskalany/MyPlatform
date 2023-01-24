import { Component, OnInit } from '@angular/core';
import { BaseEditComponent } from 'base/components/base-edit-component';

import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { ReviewTypeService } from '../../services/review-type.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
    selector: 'app-add-review-type',
    templateUrl: './add-review-type.component.html',
    styleUrls: ['./add-review-type.component.scss'],
})
export class AddReviewTypeComponent extends BaseEditComponent implements OnInit {

    override form: FormGroup;
    get Service(): ReviewTypeService { return Shell.Injector.get(ReviewTypeService); }
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
        });
    }
}
