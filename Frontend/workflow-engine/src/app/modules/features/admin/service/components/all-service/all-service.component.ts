import { Component, OnInit } from '@angular/core';
import { SessionManager } from 'core/services/guards/session-manager';
import { Shell } from 'base/components/shell';
import { BaseListComponent } from 'base/components/base-list-component';
import { TableOptions } from 'shared/components/data-table/models/tableOptions';
import { ServiceService } from '../../services/service.service';
import { PageHeader } from 'shared/models/page-header';

@Component({
  selector: 'app-all-service',
  templateUrl: './all-service.component.html',
  styleUrls: ['./all-service.component.scss'],
})
export class AllServiceComponent extends BaseListComponent implements OnInit {
  // private fields
  permissions: string[];
  tableOptions: TableOptions = {
    inputUrl: {
      getAll: 'GET-SERVICE',
      delete: 'DELETE-SERVICE',
    },
  };

  pageHeaderButtons :PageHeader = {
    buttons: [
      {
        name: 'Add',
        route : '/main/admin/service/add'
      },
      {
        name: 'ManageControls',
        route : '/main/admin/form-builder/add'
      },
    ]
  }
  override manager: SessionManager = SessionManager.Current();
  // Services
  get Service(): ServiceService { return Shell.Injector.get(ServiceService); }
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
        field: 'nameEn',
        header: 'Common.EnglishName',
        sort: true,
        sortCol: 'nameEn',
        filterMode: 'text',
      },
      {
        field: 'nameAr',
        header: 'Common.ArabicName',
        sort: true,
        sortCol: 'nameAr',
        filterMode: 'text',
      },
    ];
  }
  initializeTableActions(): void {
    this.tableOptions.inputActions = [
      {
        isEdit: true,
        name: 'Edit',
        icon: 'bx bxs-edit',
        color: 'text-blue'
      },
      {
        isDelete: true,
        name: 'Delete',
        icon: 'bx bx-trash',
        color: 'text-danger'
      }
    ];
  }
  initializeTablePermissions(): void {
    this.tableOptions.inputPermissions = this.permissions;
    this.tableOptions.inputName = 'SELF-SERVICES-SERVICE';
  }

  getPermission(): void {
    const permission = this.manager.GetPagePermission('SELF-SERVICES-SERVICE');
    this.permissions = permission;
  }
}
