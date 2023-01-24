import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'core/services/guards/authguard.guard';
import { AddGroupComponent } from './components/add-group/add-group.component';
import { AllGroupComponent } from './pages/all-group/all-group.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'all',
        pathMatch: 'full'
    },
    {
        path: 'all',
        component: AllGroupComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: 'add',
        component: AddGroupComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: ':id',
        component: AddGroupComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class GroupRoutingModule { }
