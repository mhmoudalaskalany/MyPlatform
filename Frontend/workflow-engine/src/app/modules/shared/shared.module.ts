import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { DeleteModalComponent } from './components/data-table/components/delete-modal.component';
import { DataTableComponent } from './components/data-table/data-table.component';
import { MaterialModule } from 'third-party/material.module';
import { PrimeNgModule } from 'third-party/primeng.module';
import { CollapseContentComponent } from './components/collapse-content/collapse-content.component';

import { AttachmentComponent } from './components/attachment/components/attachment/attachment.component';
import { LoadingComponent } from './components/loading/loading.component';
import { ArabicNamePatternDirective } from './directives/arabicNameValidator.directive';
import { EnglishNamePatternDirective } from './directives/englishNameValidator.directive';
import { RtlDirective } from './directives/rtl.directive';
import { DropdownComponent } from './components/dropdown/dropdown.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { InputTextComponent } from './components/input-text/input-text.component';
import { DatepickerComponent } from './components/datepicker/datepicker.component';
import { ValidationHandlerPipe } from './pipes/validation-handler/validation-handler.pipe';
import { DynamicFormComponent } from './components/dynamic-form/dynamic-form.component';
import { InputTextAreaComponent } from './components/textarea/textarea.component';
import { DetailsComponent } from './components/services/details/details.component';
import { DataViewerComponent } from './components/services/data-viewer/data-viewer.component';
import { SharedPageHeaderComponent } from './components/shared-page-header/shared-page-header.component';




@NgModule({
    entryComponents: [
    ],
    declarations: [
        DropdownComponent,
        CheckboxComponent,
        InputTextComponent,
        InputTextAreaComponent,
        DatepickerComponent,
        DataTableComponent,
        DeleteModalComponent,
        EnglishNamePatternDirective,
        ArabicNamePatternDirective,
        RtlDirective,
        LoadingComponent,
        CollapseContentComponent,
        AttachmentComponent,
        ValidationHandlerPipe,
        DynamicFormComponent,
        DetailsComponent,
        DataViewerComponent,
        SharedPageHeaderComponent
    ],
    imports: [
        CommonModule,
        MaterialModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        PrimeNgModule,
        TranslateModule,
        SweetAlert2Module

    ],
    exports: [
        DropdownComponent,
        CheckboxComponent,
        InputTextComponent,
        InputTextAreaComponent,
        DatepickerComponent,
        LoadingComponent,
        DataTableComponent,
        EnglishNamePatternDirective,
        ArabicNamePatternDirective,
        RtlDirective,
        MaterialModule,
        PrimeNgModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule,
        CollapseContentComponent,
        AttachmentComponent,
        ValidationHandlerPipe,
        SweetAlert2Module,
        DynamicFormComponent,
        DetailsComponent,
        DataViewerComponent,
        SharedPageHeaderComponent
    ]
})
export class SharedModule { }
