
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'shared/shared.module';
import { FormBuilderRoutingModule } from './form-builder.routing.module';
import { AddControlComponent } from './pages/add-control/add-control.component';
import { AddTextComponent } from './components/add-text/add-text.component';
import { AddDropDownComponent } from './components/add-dropdown/add-dropdown.component';
import { AddDatePickerComponent } from './components/add-datepicker/add-datepicker.component';
import { AddCheckBoxComponent } from './components/add-checkbox/add-checkbox.component';
import { AddAttachmentComponent } from './components/add-attachment/add-attachment.component';
import { AddTextAreaComponent } from './components/add-textarea/add-textarea.component';

@NgModule({
    declarations: [
        AddControlComponent,
        AddTextComponent,
        AddTextAreaComponent,
        AddDropDownComponent,
        AddDatePickerComponent,
        AddCheckBoxComponent,
        AddAttachmentComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        FormBuilderRoutingModule
    ]
})
export class FormBuilderModule { }
