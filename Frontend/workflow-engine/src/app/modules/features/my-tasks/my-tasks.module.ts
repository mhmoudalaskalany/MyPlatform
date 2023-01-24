import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyTasksRoutingModule } from './my-tasks-routing.module';
import { MyTasksComponent } from './pages/my-tasks/my-tasks.component';
import { SharedModule } from 'shared/shared.module';
import { TaskDetailsComponent } from './pages/task-details/task-details.component';


@NgModule({
  declarations: [
    MyTasksComponent,
    TaskDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MyTasksRoutingModule
  ]
})
export class MyTasksModule { }
