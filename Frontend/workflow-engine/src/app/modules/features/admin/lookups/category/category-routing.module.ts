import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'core/services/guards/authguard.guard';
import { AddCategoryComponent } from './components/add-category/add-category.component';
import { AllCategoryComponent } from './pages/all-category/all-category.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'all',
        pathMatch: 'full'
    },
    {
        path: 'all',
        component: AllCategoryComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: 'add',
        component: AddCategoryComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: ':id',
        component: AddCategoryComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryRoutingModule { }
