import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EServicesRoutingModule } from './e-services-routing.module';
import { SharedModule } from 'shared/shared.module';
import { EServicesComponent } from './pages/e-services/e-services.component';
import { RequestServiceComponent } from './pages/request-service/request-service.component';
import { ServiceCardComponent } from './components/service-card/service-card.component';
import { NewsComponent } from './components/services/news/news.component';
import { ReviewersComponent } from './components/services/reviewers/reviewers.component';
import { SoftwareSupportComponent } from './components/services/software-support/software-support.component';
import { SupportComponent } from './components/services/support/support.component';
import { InkComponent } from './components/services/ink/ink.component';
import { WorkCardComponent } from './components/services/work-card/work-card.component';
import { NewEmailComponent } from './components/services/new-email/new-email.component';
import { VpnComponent } from './components/services/vpn/vpn.component';
import { LeaveComponent } from './components/services/leave/leave.component';
import { NewDeviceComponent } from './components/services/new-device/new-device.component';
import { RequestDynamicServiceComponent } from './pages/request-dynamic-service/request-dynamic-service.component';


@NgModule({
  declarations: [
    EServicesComponent,
    RequestServiceComponent,
    RequestDynamicServiceComponent,
    ServiceCardComponent,
    ReviewersComponent,
    NewsComponent,
    SoftwareSupportComponent,
    SupportComponent,
    InkComponent,
    WorkCardComponent,
    NewEmailComponent,
    VpnComponent,
    LeaveComponent,
    NewDeviceComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    EServicesRoutingModule
  ]
})
export class EServicesModule { }
