
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseListComponent } from 'base/components/base-list-component';
import { Shell } from 'base/components/shell';
import { Subject, takeUntil, filter } from 'rxjs';
import { TableOptions } from 'shared/interfaces/table/table';
import { ActionsService } from '../../services/actions.service';

@Component({
  selector: 'app-actions',
  templateUrl: './actions.component.html',
  styleUrls: ['./actions.component.scss']
})
export class ActionsComponent extends BaseListComponent {

  isEnglish = false;
  tableOptions!: TableOptions | undefined;

  get service(): ActionsService { return Shell.Injector.get(ActionsService); }

  constructor(activatedRoute: ActivatedRoute) {
    super(activatedRoute);
  }

  override ngOnInit(): void {
    this.localize.currentLanguage$.pipe(takeUntil(this.destroy$)).subscribe(() => this.initializeTableOptions());
    super.ngOnInit();
  }

  initializeTableOptions() {
    this.tableOptions = undefined;

    setTimeout(() => {
      this.tableOptions = {
        inputUrl: {
          getAll: 'v1/actions/getPaged',
          getAllMethod: 'POST',
          delete: 'v1/actions/deleteSoft',
        },
        inputCols: this.initializeTableColumns(),
        inputActions: this.initializeTableActions(),
        permissions: {
          componentName: 'FINANCIAL-SYSTEM-ACTIONS',
          allowAll: true,
          listOfPermissions: []
        },
        bodyOptions: {
          filter: {}
        },
        responsiveDisplayedProperties: ['nameEn', 'nameAr', 'code', 'createdDate']
      };
    });
  }

  initializeTableColumns(): TableOptions['inputCols'] {
    return [
      {
        field: 'nameEn',
        header: 'Fields.EnglishName',
        filter: false,
        filterMode: 'text',
      },
      {
        field: 'nameAr',
        header: 'Fields.ArabicName',
        filter: false,
        filterMode: 'text',
      },
      {
        field: 'code',
        header: 'Fields.Code',
        filter: false,
        filterMode: 'text',
      },
      {
        field: 'createdDate',
        header: 'Fields.CreateDate',
        filter: false,
        filterMode: 'date',
      }
    ];
  }

  initializeTableActions(): TableOptions['inputActions'] {
    return [
      {
        name: 'View',
        icon: 'bx bxs-detail',
        color: 'text-warning',
        isView: true,
        allowAll: true
      },
      {
        name: 'Edit',
        icon: 'bx bx-edit',
        color: 'text-middle',
        isEdit: true,
        allowAll: true
      },
      {
        name: 'Delete',
        icon: 'bx bx-trash',
        color: 'text-error',
        allowAll: true,
        isDelete: true
      }
    ];
  }

  /* when leaving the component */
  override ngOnDestroy() {
    //Called once, before the instance is destroyed.
    //Add 'implements OnDestroy' to the class.
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }
}
