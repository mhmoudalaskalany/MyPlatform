import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'core/services/guards/authguard.guard';
import { AuthCallbackComponent } from 'features/account/components/auth-callback/auth-callback.component';
import { LoginComponent } from 'features/account/components/login/login.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'auth-callback',
    component: AuthCallbackComponent
  },
  {
    path: 'main',
    loadChildren: () => import('main/main.module').then((m) => m.MainModule),
    canActivate: [AuthGuard],
    data: { permission: 'allowAll' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
