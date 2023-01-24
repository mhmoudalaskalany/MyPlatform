
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'shared/shared.module';
import { WorkflowBuilderRoutingModule } from './workflow-builder.routing.module';
import { AddWorkflowComponent } from './components/add-workflow/add-workflow.component';
import { AllWorkflowComponent } from './pages/workflows/all-workflow.component';

@NgModule({
    declarations: [
        AddWorkflowComponent,
        AllWorkflowComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        WorkflowBuilderRoutingModule
    ]
})
export class WorkflowBuilderModule { }
