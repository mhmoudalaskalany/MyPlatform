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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LookupsRoutingModule { }
