import { OnInit, Directive } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable, Subject } from 'rxjs';
import { AlertService } from 'core/services/alert/alert.service';
import { Shell } from './shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { FormGroup } from '@angular/forms';
import { ConfigService } from 'core/services/config/config.service';
import { SweetAlertService } from 'shared/services/alert/sweet-alert.service';
import { Router } from '@angular/router';

@Directive()
export abstract class BaseFormBuilderComponent implements OnInit {
  constructor() { }
  model: any = {};
  form: FormGroup;
  abstract get Service(): HttpService;
  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get SweetAlert(): SweetAlertService { return Shell.Injector.get(SweetAlertService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Route(): Router { return Shell.Injector.get(Router); }
  get Config(): ConfigService { return Shell.Injector.get(ConfigService); }



  protected submitReactiveNew(endpoint?: string): Observable<any> {
    return this.Service.postReq(endpoint != null ? endpoint : 'Add', this.model);
  }





  submitReactive(endpoint?: string): void {

    this.submitReactiveNew(endpoint).subscribe(
      (data: any) => {
        if (data.status !== 201) {
          this.Alert.showError(data.message);
          return false;
        }
        this.Alert.showSuccess(
          this.Localize.translate.instant('Validation.AddSuccess')
        );
        this.Redirect();
      },
      (error) => {
        this.Alert.showError(
          this.Localize.translate.instant('Validation.AddError')
        );
      }
    );

  }



  Redirect(url?: string) {

    this.Route.navigate(['/main/admin/service']);
  }


  ngOnInit(): void {

  }

  preventDefault(event) {
    event.preventDefault();
  }


}
