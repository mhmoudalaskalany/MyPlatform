import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'core/services/guards/authguard.guard';
import { AddReviewTypeComponent } from './components/add-review-type/add-review-type.component';
import { AllReviewTypeComponent } from './components/all-review-type/all-review-type.component';

const routes: Routes = [

    {
        path: '',
        redirectTo: 'all',
        pathMatch: 'full'
    },
    {
        path: 'all',
        component: AllReviewTypeComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: 'add',
        component: AddReviewTypeComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
    {
        path: ':id',
        component: AddReviewTypeComponent,
        canActivate: [AuthGuard],
        data: { permission: 'allowAll' }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ReviewTypeRoutingModule { }
