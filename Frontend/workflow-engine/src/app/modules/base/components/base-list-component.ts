
import { OnInit, Directive } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { AlertService } from 'core/services/alert/alert.service';
import { Shell } from './shell';
import { Router } from '@angular/router';
import { SessionManager } from 'core/services/guards/session-manager';
import { TranslationService } from 'core/services/localization/translation.service';




@Directive()
export abstract class BaseListComponent implements OnInit {

  manager: SessionManager = SessionManager.Current();
  abstract get Service(): HttpService;
  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get Route(): Router { return Shell.Injector.get(Router); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  constructor() { }


  ngOnInit(): void {
  }

  Redirect() {
    const currentRoute = this.Route.url;
    const index = currentRoute.lastIndexOf('/');
    const str = currentRoute.substring(0, index);
    this.Route.navigate([str]);
  }


}
