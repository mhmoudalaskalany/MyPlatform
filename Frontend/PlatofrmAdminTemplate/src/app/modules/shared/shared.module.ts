import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialModule } from './sub-modules/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { MomentModule } from 'ngx-moment';
import { NgApexchartsModule } from "ng-apexcharts";
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { TableModule } from 'primeng/table';

// -------- Directives --------
import { RtlDirective } from './directives/rtl/rtl.directive';
import { IsEnglishDirective } from './directives/is-english/is-english.directive';

// Pipes
import { ValidationHandlerPipe } from './pipes/validation-handler.pipe';

// Components
import { InputTextComponent } from './components/input-text/input-text.component';
import { DropdownComponent } from './components/dropdown/dropdown.component';
import { DatepickerComponent } from './components/datepicker/datepicker.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { AttachmentComponent } from './components/attachment/attachment.component';
import { DeleteModalComponent } from './components/delete-modal/delete-modal.component';
import { PaginatorComponent } from './components/paginator/paginator.component';
import { SpecificLanguageDirective } from './directives/validators/specific-language/specific-language.directive';
import { BaseSharedModule } from './sub-modules/base-shared/base-shared.module';
import { SecureDirective } from './directives/secure/secure.directive';
import { ArabicNamePatternDirective } from './directives/validators/specific-language/arabicNameValidator.directive';
import { EnglishNamePatternDirective } from './directives/validators/specific-language/englishNameValidator.directive';
import { MatchPasswordDirective } from './directives/validators/specific-language/matchPassword.directive';
import { PasswordPatternDirective } from './directives/validators/specific-language/passwordPattern.directive';
import { ValidateEmailDirective } from './directives/validators/specific-language/validateEmail.directive';
import { ValidateNationalIdDirective } from './directives/validators/specific-language/validateNationalId.directive';
import { KeysPipe } from './pipes/tableHeader.pipe';
import { ChartsModule } from './sub-modules/charts/charts.module';
import { DataTableComponent } from './components/datatable/data-table.component';
import { PdfViewerComponent } from './components/pdf-viewer/pdf-viewer.component';
import { PdfJsViewerModule } from 'ng2-pdfjs-viewer';
import { ImageViewerComponent } from './components/image-viewer/image-viewer.component';


@NgModule({
  declarations: [
    // Directives
    RtlDirective,
    IsEnglishDirective,
    SpecificLanguageDirective,
    SecureDirective,
    ArabicNamePatternDirective,
    EnglishNamePatternDirective,
    MatchPasswordDirective,
    PasswordPatternDirective,
    ValidateEmailDirective,
    ValidateNationalIdDirective,
    // Components
    InputTextComponent,
    DropdownComponent,
    DatepickerComponent,
    CheckboxComponent,
    AttachmentComponent,
    DeleteModalComponent,
    DataTableComponent,
    PaginatorComponent, // unused...
    PdfViewerComponent,
    ImageViewerComponent,
    // Pipes
    ValidationHandlerPipe,
    KeysPipe
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MomentModule,
    MaterialModule,
    BaseSharedModule,
    ChartsModule,
    PdfJsViewerModule,
    NgSelectModule,
    NgbPaginationModule,
    NgApexchartsModule,
    TableModule,
  ],
  exports: [
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MomentModule,
    ChartsModule,
    // Directives
    RtlDirective,
    IsEnglishDirective,
    SecureDirective,
    ArabicNamePatternDirective,
    EnglishNamePatternDirective,
    MatchPasswordDirective,
    PasswordPatternDirective,
    ValidateEmailDirective,
    ValidateNationalIdDirective,
    // Components
    InputTextComponent,
    DropdownComponent,
    DatepickerComponent,
    CheckboxComponent,
    AttachmentComponent,
    DeleteModalComponent,
    DataTableComponent,
    PaginatorComponent, // unused...
    PdfViewerComponent,
    ImageViewerComponent
  ]
})
export class SharedModule { }
