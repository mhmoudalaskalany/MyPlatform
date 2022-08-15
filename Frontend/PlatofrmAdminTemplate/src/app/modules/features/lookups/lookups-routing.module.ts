import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'categories', pathMatch: 'full' },
  {
    path: 'actions',
    loadChildren: () => import('./sub-modules/actions/actions.module').then(m => m.ActionsModule)
  },
  {
    path: 'categories',
    loadChildren: () => import('./sub-modules/categories/categories.module').then(m => m.CategoriesModule)
  },
  {
    path: 'paymentTypes',
    loadChildren: () => import('./sub-modules/paymentTypes/paymentTypes.module').then(m => m.PaymentTypesModule)
  },
  {
    path: 'suppliers',
    loadChildren: () => import('./sub-modules/suppliers/suppliers.module').then(m => m.SuppliersModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LookupsRoutingModule { }
