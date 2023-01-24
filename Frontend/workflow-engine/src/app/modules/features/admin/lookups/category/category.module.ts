
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'shared/shared.module';
import { AllCategoryComponent } from './pages/all-category/all-category.component';
import { AddCategoryComponent } from './components/add-category/add-category.component';
import { CategoryRoutingModule } from './category-routing.module';


@NgModule({
    declarations: [
        AllCategoryComponent,
        AddCategoryComponent

    ],
    imports: [
        CommonModule,
        SharedModule,
        CategoryRoutingModule
    ]
})
export class CategoryModule { }
