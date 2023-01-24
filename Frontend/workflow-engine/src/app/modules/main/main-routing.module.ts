import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UnAuthorizedComponent } from 'features/account/components/un-authorized/un-authorized.component';
import { LayoutComponent } from './components/layout/layout.component';


const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: '',
                redirectTo: 'home',
                pathMatch: 'full'
            },
            {
                path: '403',
                component: UnAuthorizedComponent
            },
            {
                path: 'home',
                loadChildren: () => import('../features/home/home.module').then((m) => m.HomeModule),
            },
            {
                path: 'e-service',
                loadChildren: () => import('../features/e-services/e-services.module').then((m) => m.EServicesModule),
            },
            {
                path: 'my-requests',
                loadChildren: () => import('../features/my-requests/my-requests.module').then((m) => m.MyRequestsModule),
            },
            {
                path: 'my-tasks',
                loadChildren: () => import('../features/my-tasks/my-tasks.module').then((m) => m.MyTasksModule),
            },
            {
                path: 'admin/lookup',
                loadChildren: () => import('../features/admin/lookups/lookups.module').then((m) => m.LookupsModule),
            },
            {
                path: 'admin/service',
                loadChildren: () => import('../features/admin/service/service.module').then((m) => m.ServiceModule),
            },
            {
                path: 'admin/form-builder',
                loadChildren: () => import('../features/admin/form-builder/form-builder.module').then((m) => m.FormBuilderModule),
            },
            {
                path: 'admin/workflow-builder',
                loadChildren: () => import('../features/admin/workflow-builder/workflow-builder.module').then((m) => m.WorkflowBuilderModule),
            }

        ]
    }


];

@NgModule({
    imports: [RouterModule.forChild(routes)],


    exports: [RouterModule]
})
export class MainRoutingModule { }
