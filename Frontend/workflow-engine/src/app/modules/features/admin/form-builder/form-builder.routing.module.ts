import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddControlComponent } from './pages/add-control/add-control.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'add',
        pathMatch: 'full'
    },
    {
        path: 'add',
        component: AddControlComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FormBuilderRoutingModule { }
