import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseListComponent } from 'base/components/base-list-component';
import { Shell } from 'base/components/shell';
import { takeUntil } from 'rxjs';
import { TableOptions } from 'shared/interfaces/table/table';
import { CategoriesService } from 'shared/services/categories/categories.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent extends BaseListComponent {

  isEnglish = false;
  tableOptions!: TableOptions | undefined;

  get service(): CategoriesService { return Shell.Injector.get(CategoriesService); }

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
          getAll: 'v1/categories/getPaged',
          getAllMethod: 'POST',
          delete: 'v1/categories/deleteSoft',
        },
        inputCols: this.initializeTableColumns(),
        inputActions: this.initializeTableActions(),
        permissions: {
          componentName: 'FINANCIAL-SYSTEM-CATEGORIES',
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
