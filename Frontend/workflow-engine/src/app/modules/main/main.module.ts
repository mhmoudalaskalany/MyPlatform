import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "shared/shared.module";
import { LayoutComponent } from "./components/layout/layout.component";
import { MainRoutingModule } from "./main-routing.module";
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { UnAuthorizedComponent } from "features/account/components/un-authorized/un-authorized.component";
import { SidebarListItemComponent } from "./components/sidebar-list-item/sidebar-list-item.component";

@NgModule({
    declarations: [
        LayoutComponent,
        SidebarComponent,
        NavbarComponent,
        SidebarListItemComponent,
        UnAuthorizedComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        LayoutModule,
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        MainRoutingModule
    ],
    providers: [
    ]
})
export class MainModule { }