import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'shared/shared.module';
import { PaymentTypesComponent } from './pages/paymentTypes/paymentTypes.component';
import { AddEditPaymentTypeComponent } from './components/add-edit-paymentType/add-edit-paymentType.component';
import { PaymentTypesRoutingModule } from './paymentTypes-routing.module';
import { BaseSharedModule } from 'shared/sub-modules/base-shared/base-shared.module';


@NgModule({
  declarations: [
    PaymentTypesComponent,
    AddEditPaymentTypeComponent
  ],
  imports: [
    CommonModule,
    PaymentTypesRoutingModule,
    SharedModule,
    BaseSharedModule
  ]
})
export class PaymentTypesModule { }
