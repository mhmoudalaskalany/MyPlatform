import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddWorkflowComponent } from './components/add-workflow/add-workflow.component';
import { AllWorkflowComponent } from './pages/workflows/all-workflow.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'all',
        pathMatch: 'full'
    },
    {
        path: 'all',
        component: AllWorkflowComponent
    },
    {
        path: 'add',
        component: AddWorkflowComponent
    },
    {
        path: ':id',
        component: AddWorkflowComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WorkflowBuilderRoutingModule { }
