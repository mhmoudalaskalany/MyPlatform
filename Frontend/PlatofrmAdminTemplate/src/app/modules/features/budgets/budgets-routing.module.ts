import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DialogComponent } from 'shared/components/dialog/dialog.components';
import { AddEditBudgetComponent } from './components/add-edit-budget/add-edit-budget.component';
import { BudgetsDashboardComponent } from './pages/budgets-dashboard/budgets-dashboard.component';
import { BudgetsComponent } from './pages/budgets/budgets.component';

const routes: Routes = [
  {
    path: '',
    data: { title: 'Pages.Budgets.Title', pageType: 'list' },
    component: BudgetsDashboardComponent,
    children: [
      {
        path: 'add',
        data: { component: AddEditBudgetComponent, pageTitle: 'Pages.Budgets.Add', pageType: 'add' },
        component: DialogComponent
      },
      {
        path: 'edit',
        children: [
          { path: '', redirectTo: '', pathMatch: 'full' },
          {
            path: ':id',
            data: { component: AddEditBudgetComponent, redirectTo: '/budgets', pageTitle: 'Pages.Budgets.Edit', pageType: 'edit' },
            component: DialogComponent
          }
        ]
      },
    ],
    
    
  } ,
  {
    path: 'budget-dashboard',
    data: { title: 'Pages.Budgets.Title', pageType: 'list' },
    component: BudgetsDashboardComponent,
  } 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
exports: [RouterModule]
})
export class BudgetsRoutingModule { }
