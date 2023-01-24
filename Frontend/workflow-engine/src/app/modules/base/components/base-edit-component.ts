import { OnInit, Directive } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable, Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'core/services/alert/alert.service';
import { Shell } from './shell';
import { RoleData } from 'core/services/guards/models';
import { SessionManager } from 'core/services/guards/session-manager';
import { TranslationService } from 'core/services/localization/translation.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ConfigService } from 'core/services/config/config.service';
import { SweetAlertService } from 'shared/services/alert/sweet-alert.service';

@Directive()
export abstract class BaseEditComponent implements OnInit {
  constructor(protected route: ActivatedRoute) { }
  model: any = {};
  form: FormGroup;
  isNew = true;
  id: string;
  role: RoleData = {};
  manager: SessionManager = SessionManager.Current();
  abstract get Service(): HttpService;
  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get SweetAlert(): SweetAlertService { return Shell.Injector.get(SweetAlertService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Route(): Router { return Shell.Injector.get(Router); }
  get Config(): ConfigService { return Shell.Injector.get(ConfigService); }
  get Fb(): FormBuilder { return Shell.Injector.get(FormBuilder); }

  protected SubmitNew(): Observable<any> {
    return this.Service.postReq('Add', this.model);
  }
  protected SubmitUpdate(): Observable<any> {
    return this.Service.putReq('Update', this.model);
  }
  protected submitReactiveNew(endpoint?: string): Observable<any> {
    return this.Service.postReq(endpoint != null ? endpoint : 'Add', this.form.value);
  }
  protected submitReactiveUpdate(endpoint?: string): Observable<any> {
    return this.Service.putReq(endpoint != null ? endpoint : 'Update', this.form.value);
  }
  protected GetModelFromServer(id: any): Observable<any> {
    return this.Service.getHeaderReq('Get', id);
  }
  protected getUserRole(): void {
    this.role = this.manager.GetRole();
  }

  Submit(): void {
    console.log(this.model);

    if (this.isNew) {
      this.SubmitNew().subscribe(
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
    } else {
      this.SubmitUpdate().subscribe(
        (data: any) => {
          if (data.status !== 202) {
            this.Alert.showError(data.message);
            return false;
          }
          this.Alert.showSuccess(
            this.Localize.translate.instant('Validation.UpdateSuccess')
          );
          this.Redirect();
        },
        (error) => {
          this.Alert.showError(
            this.Localize.translate.instant('Validation.UpdateError')
          );
          console.log('error at submitting', error);
        }
      );
    }
  }

  submitReactive(endpoint?: string): void {
    if (this.isNew) {
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
    } else {
      this.submitReactiveUpdate(endpoint).subscribe(
        (data: any) => {
          if (data.status !== 202) {
            this.Alert.showError(data.message);
            return false;
          }
          this.Alert.showSuccess(
            this.Localize.translate.instant('Validation.UpdateSuccess')
          );
          this.Redirect();
        },
        (error) => {
          this.Alert.showError(
            this.Localize.translate.instant('Validation.UpdateError')
          );
          console.log('error at submitting', error);
        }
      );
    }

  }

  getRouteParams() {
    this.isNew = false;
    this.id = this.route.snapshot.paramMap.get('id');
    this.Get(this.id);
  }

  // getRouteParams() {
  //   this.route.params.subscribe((p: any) => {
  //     if (p.id != null && p.id !== undefined) {
  //       this.isNew = false;
  //       this.id = p.id;
  //       this.Get(this.id);
  //     }
  //   });
  // }

  Redirect(url?: string) {
    const currentRoute = this.Route.url;
    const index = currentRoute.lastIndexOf(url ? url : '/');
    const str = currentRoute.substring(0, index);
    this.Route.navigate([str]);
  }
  Get(id: any): void {
    this.GetModelFromServer(id).subscribe(
      (data: any) => {
        this.model = data.data;
        console.log(this.model);
        this.patchForm();
      },
      (error) => {
        this.Alert.showError(this.Localize.translate.instant('Validation.GetError'));
        console.log(error);

      }
    );
  }

  ngOnInit(): void {
    if (this.route.snapshot.paramMap.get('id')) {
      this.getRouteParams();
    }
  }

  preventDefault(event) {
    event.preventDefault();
  }

  patchForm(): void {
    this.form.patchValue(this.model);
  }
}
