import { Component, OnInit } from '@angular/core';
import { TableOptions } from 'shared/components/data-table/models/tableOptions';
import { Shell } from 'base/components/shell';
import { BaseListComponent } from 'base/components/base-list-component';
import { SessionManager } from 'core/services/guards/session-manager';
import { RequestService } from 'features/my-requests/services/my-requests.service';
import { PageHeader } from 'shared/models/page-header';

@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.scss']
})
export class MyRequestsComponent extends BaseListComponent implements OnInit {
  // private fields
  permissions: string[];
  tableOptions: TableOptions = {
    inputUrl: {
      getAll: 'GET-REQUEST',
      delete: 'DELETE-REQUEST'
    },
  };
  pageHeaderButtons :PageHeader = {
    buttons: [
      {
        name: 'Add',
        route : '/main/e-service'
      }
    ]
  }
  override manager: SessionManager = SessionManager.Current();
  // Services
  get Service(): RequestService { return Shell.Injector.get(RequestService); }
  constructor() {
    super();
  }

  override ngOnInit(): void {
    this.getPermission();
    this.initializeTableColumns();
    this.initializeTableActions();
    this.initializeTablePermissions();
  }

  initializeTableColumns(): void {
    this.tableOptions.inputCols = [
      {
        field: 'requestOrder',
        header: 'EService.RequestId',
        sortCol: 'requestOrder',
        sort: true,
        filterMode: 'text'
      },
      {
        field: this.Localize.isEnglish() ? 'service.nameEn' : 'service.nameAr',
        header: 'EService.ServiceName',
        sortCol: 'serviceName',
        sort: true,
        filterMode: 'text'
      },
      {
        field: this.Localize.isEnglish() ? 'statusNameEn' : 'statusNameAr',
        header: 'Common.Status',
        sortCol: this.Localize.isEnglish() ? 'status.nameEn' : 'status.nameAr',
        sort: true,
        filterMode: 'text'
      },
      {
        field: this.Localize.isEnglish() ? 'pendingOnEn' : 'pendingOnAr',
        header: 'EService.PendingOn',
        sortCol: 'pendingOnName',
        sort: false,
        filterMode: 'text'
      }
    ];
  }
  initializeTableActions(): void {
    this.tableOptions.inputActions = [
      {
        isView: true,
        name: 'View',
        icon: 'bx bx-show',
        route: '/main/my-requests/request/',
        color: 'text-primary'
      }
    ];
  }
  initializeTablePermissions(): void {
    this.tableOptions.inputPermissions = this.permissions;
    this.tableOptions.inputName = 'SELF-SERVICES-MY-REQUESTS';
  }

  getPermission(): void {
    const permission = this.manager.GetPagePermission('SELF-SERVICES-MY-REQUESTS');
    this.permissions = permission;
  }


}
