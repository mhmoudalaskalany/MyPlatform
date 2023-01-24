import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'core/services/guards/authguard.guard';
import { AddServiceComponent } from './components/add-service/add-service.component';
import { AllServiceComponent } from './components/all-service/all-service.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'all',
        pathMatch: 'full'
    },
    {
        path: 'all',
        component: AllServiceComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: 'add',
        component: AddServiceComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: ':id',
        component: AddServiceComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ServiceRoutingModule { }
