import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyRequestsRoutingModule } from './my-requests-routing.module';
import { SharedModule } from 'shared/shared.module';
import { MyRequestsComponent } from './pages/my-requests/my-requests.component';
import { RequestDetailsComponent } from './pages/request-details/request-details.component';


@NgModule({
  declarations: [
    MyRequestsComponent,
    RequestDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MyRequestsRoutingModule
  ]
})
export class MyRequestsModule { }
