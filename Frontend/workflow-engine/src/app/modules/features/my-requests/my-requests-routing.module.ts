import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyRequestsComponent } from './pages/my-requests/my-requests.component';
import { RequestDetailsComponent } from './pages/request-details/request-details.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: MyRequestsComponent
      },
      {
        path: 'request/:id',
        component: RequestDetailsComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MyRequestsRoutingModule { }
