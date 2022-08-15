import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DialogComponent } from 'shared/components/dialog/dialog.components';
import { AddEditPaymentTypeComponent } from './components/add-edit-paymentType/add-edit-paymentType.component';
import { PaymentTypesComponent } from './pages/paymentTypes/paymentTypes.component';

const routes: Routes = [
  {
    path: '',
    data: { title: 'Pages.Lookups.PaymentTypes.Title', pageType: 'list' },
    component: PaymentTypesComponent,
    children: [
      {
        path: 'add',
        data: { component: AddEditPaymentTypeComponent, pageTitle: 'Actions.Add', pageType: 'add' },
        component: DialogComponent
      },
      {
        path: 'edit',
        children: [
          { path: '', redirectTo: '', pathMatch: 'full' },
          {
            path: ':id',
            data: { component: AddEditPaymentTypeComponent, redirectTo: '/lookups/paymentTypes', pageTitle: 'Actions.Edit', pageType: 'edit' },
            component: DialogComponent
          }
        ]
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaymentTypesRoutingModule { }
