
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'shared/shared.module';
import { AllServiceComponent } from './components/all-service/all-service.component';
import { AddServiceComponent } from './components/add-service/add-service.component';
import { ServiceRoutingModule } from './service-routing.module';


@NgModule({
    declarations: [
        AllServiceComponent,
        AddServiceComponent

    ],
    imports: [
        CommonModule,
        SharedModule,
        ServiceRoutingModule
    ]
})
export class ServiceModule { }
