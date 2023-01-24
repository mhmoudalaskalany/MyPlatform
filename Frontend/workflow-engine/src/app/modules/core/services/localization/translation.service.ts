import { Injectable, PLATFORM_ID, Optional, RendererFactory2, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

/**
 * Auth Services
 * the main service for authentications
 */
export class TranslationService {
  langs = ['en', 'fr', 'ar'];
  lang;
  currentLanguage$ = new BehaviorSubject<string>(null);
  private renderer: Renderer2;
  constructor(public translate: TranslateService, private rendererFactory: RendererFactory2) {
    this.renderer = rendererFactory.createRenderer(null, null);
    this.lang = localStorage.getItem('language') != null ? localStorage.getItem('language') : 'en';
  }

  setDefaultLanguage(): void {
    this.translate.addLangs(['en', 'fr', 'ar']);
    this.translate.setDefaultLang('ar');
    this.translate.use(this.lang === undefined ? 'ar' : this.lang);
    if (this.lang === 'ar') {
      this.renderer.addClass(document.body, 'rtl');
    } else {
      this.renderer.removeClass(document.body, 'rtl');
    }
  }

  useLanguage(lang: string): void {
    this.translate.setDefaultLang(lang);
    localStorage.setItem('language', lang);
  }

  getCurrentLanguage(): string {
    return this.translate.currentLang;
  }

  setLanguage(lang: string): void {
    this.currentLanguage$.next(lang);
    localStorage.setItem('language', lang);
    if (lang === 'ar') {
      this.renderer.addClass(document.body, 'rtl');
    } else {
      this.renderer.removeClass(document.body, 'rtl');
    }
    this.translate.use(lang);
  }
  isEnglish(): boolean {
    const currentLang = this.translate.currentLang;
    if (currentLang === 'en') {
      return true;
    } else {
      return false;
    }
  }
}
