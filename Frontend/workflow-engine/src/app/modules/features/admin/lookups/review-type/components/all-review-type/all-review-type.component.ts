import { ReviewTypeService } from '../../services/review-type.service';
import { Component, OnInit } from '@angular/core';
import { SessionManager } from 'core/services/guards/session-manager';
import { Shell } from 'base/components/shell';
import { BaseListComponent } from 'base/components/base-list-component';
import { TableOptions } from 'shared/components/data-table/models/tableOptions';
import { PageHeader } from 'shared/models/page-header';

@Component({
  selector: 'app-all-review-type',
  templateUrl: './all-review-type.component.html',
  styleUrls: ['./all-review-type.component.scss'],
})
export class AllReviewTypeComponent extends BaseListComponent implements OnInit {
  // private fields
  permissions: string[];
  tableOptions: TableOptions = {
    inputUrl: {
      getAll: 'GET-REVIEW-TYPE',
      delete: 'DELETE-REVIEW-TYPE',
    },
  };
  pageHeaderButtons :PageHeader = {
    buttons: [
      {
        name: 'Add',
        route : '/main/admin/lookup/review-type/add'
      }
    ]
  }
  override manager: SessionManager = SessionManager.Current();
  // Services
  get Service(): ReviewTypeService { return Shell.Injector.get(ReviewTypeService); }
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
        color: 'text-lightblue'
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
    this.tableOptions.inputName = 'SELF-SERVICES-REVIEW-TYPE';
  }

  getPermission(): void {
    const permission = this.manager.GetPagePermission('SELF-SERVICES-REVIEW-TYPE');
    this.permissions = permission;
  }
}
