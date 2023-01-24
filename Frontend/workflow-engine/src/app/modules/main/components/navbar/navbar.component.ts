import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { AuthService } from 'core/services/auth/auth.service';
import { ConfigService } from 'core/services/config/config.service';
import { TranslationService } from 'core/services/localization/translation.service';
import { SidebarToggleService } from 'core/services/sidebar-toggle/sidebar-toggle.service';
import { StorageService } from 'core/services/storage/storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  lang = '';
  get Storage(): StorageService { return Shell.Injector.get(StorageService); }
  get Config(): ConfigService { return Shell.Injector.get(ConfigService); }
  get AuthService(): AuthService { return Shell.Injector.get(AuthService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get SideBarService(): SidebarToggleService { return Shell.Injector.get(SidebarToggleService); }
  get Router(): Router { return Shell.Injector.get(Router) }
  constructor() { }

  ngOnInit(): void {
    this.Localize.currentLanguage$.subscribe(lang => this.lang = lang);
  }

  sidebarToggle = () => {
    this.SideBarService.toggle();
  }

  changeLanguage = (lang) => {
    this.Localize.setLanguage(lang);
  }

  goToProfile = () => {
    window.location.href = this.Config.getAppUrl('MY-PROFILE');
  }
  navigateToPortal = () => {
    // this to fix the issue of replacing the permission of the current app to the redirected app
    sessionStorage.removeItem(this.Config.getAppUrl('OidcTicket'));
    location.assign(this.Config.getAppUrl('PORTAL'));;
  }

  logout = async () => {
    await this.AuthService.signout();
  }
}
