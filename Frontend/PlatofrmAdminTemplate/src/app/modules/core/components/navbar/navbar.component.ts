import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { AuthService } from 'core/services/auth/auth.service';
import { ConfigService } from 'core/services/config/config.service';
import { SidebarToggleService } from '../../services/sidebar-toggle/sidebar-toggle.service';
import { TranslationService } from '../../services/translation/translation.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  isEnglish = true;
  get Config(): ConfigService { return Shell.Injector.get(ConfigService); }
  get AuthService(): AuthService { return Shell.Injector.get(AuthService); }
  constructor(public translateService: TranslationService, private sidebarService: SidebarToggleService) { }

  ngOnInit(): void { }

  sidebarToggle() {
    this.sidebarService.toggle();
  }

  changeLanguage() {
    this.translateService.changeLanguage();
  }



  navigateToPortal(): void {
    // this to fix the issue of replacing the permission of the current app to the redirected app
    sessionStorage.removeItem(this.Config.getAppUrl('OidcTicket'));
    window.location.href = this.Config.getAppUrl('PORTAL');
  }
  async logout() {
    await this.AuthService.signout();
  }
}
