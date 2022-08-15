import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseSharedModule } from 'shared/sub-modules/base-shared/base-shared.module';
import { SharedModule } from 'shared/shared.module';
import { ViewModalComponent } from './pages/view-modal/view-modal.component';
import { ClauseBoardComponent } from './pages/clause-board/clause-board.component';
import { ClauseRoutingModule } from './clause-routing.module';


@NgModule({
  declarations: [
    ClauseBoardComponent,
    ViewModalComponent
  ],
  imports: [
  CommonModule,
    ClauseRoutingModule,
    SharedModule,
    BaseSharedModule,
  ]
})
export class ClauseModule { }
