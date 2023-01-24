import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './components/home/home.component';
import { SharedModule } from 'shared/shared.module';
import { TopCardComponent } from './components/top-card/top-card.component';
import { FavoriteServiceComponent } from './components/favorite-service/favorite-service.component';


@NgModule({
    declarations: [
        TopCardComponent,
        FavoriteServiceComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        SharedModule
    ]
})
export class HomeModule { }
