
import { OnInit, Directive } from '@angular/core';
import { AlertService } from 'core/services/alert/alert.service';
import { Shell } from './shell';
import { Router } from '@angular/router';
import { SessionManager } from 'core/services/guards/session-manager';
import { RoleData } from 'core/services/guards/models';
import { TableOptions } from 'shared/components/data-table/models/tableOptions';
import { AuthHttpService } from 'core/services/http/authHttp.service';




@Directive()
export abstract class BaseAuthListComponent implements OnInit {

  role: RoleData = {};
  tableOptions: TableOptions = {};
  abstract get Service(): AuthHttpService;
  manager: SessionManager = SessionManager.Current();
  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get Route(): Router { return Shell.Injector.get(Router); }
  constructor() {
    this.getUserRole();
  }

  ngOnInit(): void {
  }

  Redirect() {
    const currentRoute = this.Route.url;
    const index = currentRoute.lastIndexOf('/');
    const str = currentRoute.substring(0, index);
    this.Route.navigate([str]);
  }
  protected getUserRole(): void {
    this.role = this.manager.GetRole();
  }


}
