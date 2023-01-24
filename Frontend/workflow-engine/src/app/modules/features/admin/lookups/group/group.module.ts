
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'shared/shared.module';
import { AddGroupComponent } from './components/add-group/add-group.component';
import { GroupRoutingModule } from './group-routing.module';
import { AllGroupComponent } from './pages/all-group/all-group.component';


@NgModule({
    declarations: [
        AllGroupComponent,
        AddGroupComponent

    ],
    imports: [
        CommonModule,
        SharedModule,
        GroupRoutingModule
    ]
})
export class GroupModule { }
