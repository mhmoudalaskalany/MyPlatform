import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'review-type',
    loadChildren: () => import('./review-type/review-type.module').then((m) => m.ReviewTypeModule),
  },
  {
    path: 'category',
    loadChildren: () => import('./category/category.module').then((m) => m.CategoryModule),
  },
  {
    path: 'group',
    loadChildren: () => import('./group/group.module').then((m) => m.GroupModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LookupsRoutingModule { }
