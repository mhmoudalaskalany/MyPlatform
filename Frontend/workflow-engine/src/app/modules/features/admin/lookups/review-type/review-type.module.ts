
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'shared/shared.module';
import { AllReviewTypeComponent } from './components/all-review-type/all-review-type.component';
import { AddReviewTypeComponent } from './components/add-review-type/add-review-type.component';
import { ReviewTypeRoutingModule } from './review-type-routing.module';


@NgModule({
    declarations: [
        AllReviewTypeComponent,
        AddReviewTypeComponent,

    ],
    imports: [
        CommonModule,
        SharedModule,
        ReviewTypeRoutingModule
    ]
})
export class ReviewTypeModule { }
