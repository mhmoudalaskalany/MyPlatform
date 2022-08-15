import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BudgetsRoutingModule } from './budgets-routing.module';
import { AddEditBudgetComponent } from './components/add-edit-budget/add-edit-budget.component';
import { BaseSharedModule } from 'shared/sub-modules/base-shared/base-shared.module';
import { SharedModule } from 'shared/shared.module';
import { ViewModalComponent } from './pages/view-modal/view-modal.component';
import { BudgetsComponent } from './pages/budgets/budgets.component';
import { BudgetsDashboardComponent } from './pages/budgets-dashboard/budgets-dashboard.component';


@NgModule({
  declarations: [
    BudgetsComponent,
    BudgetsDashboardComponent,
    AddEditBudgetComponent,
    ViewModalComponent
  ],
  imports: [
  CommonModule,
    BudgetsRoutingModule,
    SharedModule,
    BaseSharedModule,
  ]
})
export class BudgetsModule { }
