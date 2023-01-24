import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EServicesComponent } from './pages/e-services/e-services.component';
import { RequestDynamicServiceComponent } from './pages/request-dynamic-service/request-dynamic-service.component';
import { RequestServiceComponent } from './pages/request-service/request-service.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: EServicesComponent
      },
      {
        path: 'service/:code',
        component: RequestServiceComponent
      },
      {
        path: 'dynamic-service/:code',
        component: RequestDynamicServiceComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EServicesRoutingModule { }
