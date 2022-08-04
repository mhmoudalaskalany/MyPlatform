import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'core/guards/authguard.guard';
import { Action } from 'core/guards/models';
import { LayoutComponent } from './modules/core/components/layout/layout.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'account',
    pathMatch: 'full'
  },
  {
    path: 'account',
    loadChildren: () => import('./modules/features/account/account.module').then(m => m.AccountModule)
  },
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        canActivate: [AuthGuard],
        data: { permission: Action.Anonymous },
        loadChildren: () => import('./modules/features/dashboard/dashboard.module').then(m => m.DashboardModule)
      }
      {
        path: 'lookups',
        loadChildren: () => import('./modules/features/lookups/lookups.module').then(m => m.LookupsModule)
      }
    ]
  },
  { path: 'error', loadChildren: () => import('./modules/features/error/error.module').then(m => m.ErrorModule) },
  { path: '**', redirectTo: '/error/404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
