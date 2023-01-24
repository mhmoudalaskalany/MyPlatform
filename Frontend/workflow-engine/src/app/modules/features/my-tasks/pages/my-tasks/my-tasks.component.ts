import { Component, OnInit } from '@angular/core';
import { TableOptions } from 'shared/components/data-table/models/tableOptions';
import { Shell } from 'base/components/shell';
import { MyTasksService } from 'features/my-tasks/services/my-tasks.service';
import { SessionManager } from 'core/services/guards/session-manager';
import { BaseListComponent } from 'base/components/base-list-component';
import { PageHeader } from 'shared/models/page-header';

@Component({
  selector: 'app-my-tasks',
  templateUrl: './my-tasks.component.html',
  styleUrls: ['./my-tasks.component.scss']
})
export class MyTasksComponent extends BaseListComponent implements OnInit {

  // private fields
  permissions: string[];
  tableOptions: TableOptions = {
    inputUrl: {
      getAll: 'GET-TASKS',
      delete: 'DELETE-TASK'
    },
  };
  pageHeaderButtons :PageHeader = {
    buttons: []
  }
  override manager: SessionManager = SessionManager.Current();
  // Services
  get Service(): MyTasksService { return Shell.Injector.get(MyTasksService); }
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
        field: 'request.requestOrder',
        header: 'EService.RequestId',
        sort: true,
        sortCol: 'requestOrder',
        filterMode: 'text'
      },

      {
        field: this.Localize.isEnglish() ? 'service.nameEn' : 'service.nameAr',
        header: 'Service',
        sort: true,
        sortCol: 'serviceId',
        filterMode: 'text'
      },
      {
        field: this.Localize.isEnglish() ? 'request.createdByNameEn' : 'request.createdByNameAr',
        header: 'EService.RequestedBy',
        sort: true,
        sortCol: 'requesterNameEn',
        filterMode: 'text'
      },
      {
        field: this.Localize.isEnglish() ? 'request.statusNameEn' : 'request.statusNameAr',
        header: 'Common.Status',
        sort: true,
        sortCol: 'statusName',
        filterMode: 'text'
      },
      {
        field: 'request.createdDate',
        header: 'Common.CreatedDate',
        sort: true,
        sortCol: 'createdDate',
        filterMode: 'date'
      },

    ];
  }
  initializeTableActions(): void {
    this.tableOptions.inputActions = [
      {
        isView: true,
        name: 'View',
        icon: 'bx bx-show',
        route: '/main/my-tasks/task/',
        color: 'text-primary'
      }
    ];
  }
  initializeTablePermissions(): void {
    this.tableOptions.inputPermissions = this.permissions;
    this.tableOptions.inputName = 'SELF-SERVICES-MY-TASKS';
  }

  getPermission(): void {
    const permission = this.manager.GetPagePermission('SELF-SERVICES-MY-TASKS');
    this.permissions = permission;
  }

  getRow(row) {
    this.Route.navigate([`/main/my-tasks/task/${row.id}`]);
  }

}
