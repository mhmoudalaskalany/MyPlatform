import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from 'shared/shared.module';
import { BaseSharedModule } from 'shared/sub-modules/base-shared/base-shared.module';
import { LoginComponent } from './components/login/login.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { UnAuthorizedComponent } from './components/un-authorized/un-authorized.component';


@NgModule({
  declarations: [
    LoginComponent,
    AuthCallbackComponent,
    UnAuthorizedComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    BaseSharedModule
  ]
})
export class AccountModule { }
