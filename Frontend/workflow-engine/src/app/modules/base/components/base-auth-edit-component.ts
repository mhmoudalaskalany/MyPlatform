import { OnInit, Directive } from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'core/services/alert/alert.service';
import { Shell } from './shell';
import { AuthHttpService } from 'core/services/http/authHttp.service';
import { RoleData } from 'core/services/guards/models';
import { SessionManager } from 'core/services/guards/session-manager';
import { TranslationService } from 'core/services/localization/translation.service';


@Directive()
export abstract class BaseAuthEditComponent implements OnInit {

  constructor(protected route: ActivatedRoute) {
    this.getUserRole();
  }
  model: any = {};
  isNew = true;
  id: string;
  role: RoleData = {};
  manager: SessionManager = SessionManager.Current();
  abstract get Service(): AuthHttpService;
  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Route(): Router { return Shell.Injector.get(Router); }

  protected SubmitNew(): Observable<any> {
    return this.Service.postReq('Add', this.model);
  }
  protected SubmitUpdate(): Observable<any> {
    return this.Service.putReq('Update', this.model);
  }
  protected GetModelFromServer(id: any): Observable<any> {
    return this.Service.getHeaderReq('Get', id);
  }
  protected getUserRole(): void {
    this.role = this.manager.GetRole();
  }

  Submit(): void {
    if (this.isNew) {
      this.SubmitNew().subscribe((data: any) => {
        if (data.status !== 201) {
          this.Alert.showError(data.message);
          return false;
        }
        this.Alert.showSuccess(this.Localize.translate.instant('Data.AddSuccess'));
        this.Redirect();
      }, error => {
        this.Alert.showError(this.Localize.translate.instant('Data.AddError'));
      });
    }
    else {
      this.SubmitUpdate().subscribe((data: any) => {
        this.Alert.showSuccess(this.Localize.translate.instant('Data.UpdateSuccess'));
        this.Redirect();
      }, error => {
        this.Alert.showError(this.Localize.translate.instant('Data.UpdateError'));
      });
    }
  }

  getRouteParams() {
    this.route.params.subscribe((p: any) => {
      if (p.id != null && p.id !== undefined) {
        this.isNew = false;
        this.id = p.id;
        this.Get(this.id);
      }
    });
  }

  Redirect() {
    const currentRoute = this.Route.url;
    const index = currentRoute.lastIndexOf('/');
    const str = currentRoute.substring(0, index);
    this.Route.navigate([str]);
  }
  Get(id: any): void {
    this.GetModelFromServer(id).subscribe((data: any) => {
      this.model = data.data;
    }, error => {
      this.Alert.showError(this.Localize.translate.instant('Data.GetError'));
      console.log(error);
    });
  }

  ngOnInit(): void {
    this.getRouteParams();
  }

}
