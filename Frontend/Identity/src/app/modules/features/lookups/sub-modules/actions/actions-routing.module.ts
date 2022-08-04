import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DialogComponent } from 'shared/components/dialog/dialog.components';
import { AddEditActionComponent } from './components/add-edit-category/add-edit-action.component';
import { ActionsComponent } from './pages/actions/actions.component';

const routes: Routes = [
  {
    path: '',
    data: { title: 'Pages.Lookups.Actions.Title', pageType: 'list' },
    component: ActionsComponent,
    children: [
      {
        path: 'add',
        data: { component: AddEditActionComponent, pageTitle: 'Actions.Add', pageType: 'add' },
        component: DialogComponent
      },
      {
        path: 'edit',
        children: [
          { path: '', redirectTo: '', pathMatch: 'full' },
          {
            path: ':id',
            data: { component: AddEditActionComponent, redirectTo: '/lookups/actions', pageTitle: 'Actions.Edit', pageType: 'edit' },
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
export class ActionsRoutingModule { }
