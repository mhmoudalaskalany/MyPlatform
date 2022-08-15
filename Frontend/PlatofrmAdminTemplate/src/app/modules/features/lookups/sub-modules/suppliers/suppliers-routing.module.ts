import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DialogComponent } from 'shared/components/dialog/dialog.components';
import { AddEditSupplierComponent } from './components/add-edit-supplier/add-edit-supplier.component';
import { SuppliersComponent } from './pages/suppliers/suppliers.component';

const routes: Routes = [
  {
    path: '',
    data: { title: 'Pages.Lookups.Suppliers.Title', pageType: 'list' },
    component: SuppliersComponent,
    children: [
      {
        path: 'add',
        data: { component: AddEditSupplierComponent, pageTitle: 'SETTINGS.SUPPLIERS.ADD', pageType: 'add' },
        component: DialogComponent
      },
      {
        path: 'edit',
        children: [
          { path: '', redirectTo: '', pathMatch: 'full' },
          {
            path: ':id',
            data: { component: AddEditSupplierComponent, redirectTo: '/lookups/suppliers', pageTitle: 'SETTINGS.SUPPLIERS.EDIT', pageType: 'edit' },
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
export class SuppliersRoutingModule { }
