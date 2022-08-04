import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BaseSharedModule } from 'shared/sub-modules/base-shared/base-shared.module';
import { SharedModule } from 'shared/shared.module';
import { ActionsComponent } from './pages/actions/actions.component';
import { AddEditActionComponent } from './components/add-edit-category/add-edit-action.component';
import { ActionsRoutingModule } from './actions-routing.module';


@NgModule({
  declarations: [
    ActionsComponent,
    AddEditActionComponent
  ],
  imports: [
    CommonModule,
    ActionsRoutingModule,
    SharedModule,
    BaseSharedModule
  ]
})
export class ActionsModule { }
