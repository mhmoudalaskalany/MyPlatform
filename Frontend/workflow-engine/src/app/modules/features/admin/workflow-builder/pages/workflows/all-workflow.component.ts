import { Component, OnInit } from '@angular/core';
import { SessionManager } from 'core/services/guards/session-manager';
import { Shell } from 'base/components/shell';
import { BaseListComponent } from 'base/components/base-list-component';
import { TableOptions } from 'shared/components/data-table/models/tableOptions';
import { PageHeader } from 'shared/models/page-header';
import { WorkflowBuilderService } from '../../services/workflow-builder.service';

@Component({
  selector: 'app-all-workflow',
  templateUrl: './all-workflow.component.html',
  styleUrls: ['./all-workflow.component.scss'],
})
export class AllWorkflowComponent extends BaseListComponent implements OnInit {
  // private fields
  permissions: string[];
  tableOptions: TableOptions = {
    inputUrl: {
      getAll: 'GET-WORKFLOW',
      delete: 'DELETE-WORKFLOW',
    },
  };
  pageHeaderButtons: PageHeader = {
    buttons: [
      {
        name: 'Add',
        route: '/main/admin/workflow-builder/add'
      }
    ]
  }
  override manager: SessionManager = SessionManager.Current();
  // Services
  get Service(): WorkflowBuilderService { return Shell.Injector.get(WorkflowBuilderService); }
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
    this.tableOptions.inputName = 'SELF-SERVICES-WORKFLOWS';
  }

  getPermission(): void {
    const permissions = this.manager.GetPagePermission('SELF-SERVICES-WORKFLOWS');
    this.permissions = permissions;
  }
}
