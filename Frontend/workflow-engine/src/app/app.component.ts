import { Component, Injector } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private router: Router, public inj: Injector, public translateService: TranslationService, public titleService: Title) {
    Shell.Injector = inj;
    this.translateService.setDefaultLanguage();
    this.setTitle();
  }

  setTitle(): void {
    const lang = this.translateService.getCurrentLanguage();
    if (lang === 'ar') {
      this.titleService.setTitle('الطلبات الالكترونية');
    } else {
      this.titleService.setTitle('E-Services');
    }
  }
}
