import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { AddEditSupplierComponent } from './components/add-edit-supplier/add-edit-supplier.component';
import { SuppliersComponent } from './pages/suppliers/suppliers.component';
import { SuppliersRoutingModule } from './suppliers-routing.module';
import { SharedModule } from 'shared/shared.module';
import { BaseSharedModule } from 'shared/sub-modules/base-shared/base-shared.module';


@NgModule({
  declarations: [
    SuppliersComponent,
    AddEditSupplierComponent
  ],
  imports: [
    CommonModule,
    SuppliersRoutingModule,
    SharedModule,
    BaseSharedModule
  ]
})
export class SuppliersModule { }
