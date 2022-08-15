import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseListComponent } from 'base/components/base-list-component';
import { Shell } from 'base/components/shell';
import { takeUntil } from 'rxjs';
import { TableOptions } from 'shared/interfaces/table/table';
import { BudgetsService } from 'shared/services/budget/budgets.service';

@Component({
  selector: 'app-budgets',
  templateUrl: './budgets.component.html',
  styleUrls: ['./budgets.component.scss']
})
export class BudgetsComponent extends BaseListComponent {

  isEnglish = false;
  tableOptions!: TableOptions | undefined;
  get service(): BudgetsService { return Shell.Injector.get(BudgetsService); }
  constructor(activatedRoute: ActivatedRoute) {
    super(activatedRoute)
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.localize.currentLanguage$.pipe(takeUntil(this.destroy$)).subscribe(() => this.initializeTableOptions());
  }

  initializeTableOptions() {
    this.tableOptions = undefined;

    setTimeout(() => {
      this.tableOptions = {
        inputUrl: {
          getAll: 'v1/budgets/getPaged',
          getAllMethod: 'POST',
          delete: 'v1/budgets/deleteSoft',
        },
        permissions: {
          componentName: 'FINANCIAL-SYSTEM-CATEGORIES',
          listOfPermissions: []
        },
        bodyOptions: {
          filter: {}
        },
        inputCols: this.initializeTableColumns(),
        inputActions: this.initializeTableActions(),
        responsiveDisplayedProperties: ['nameEn', 'nameAr', 'createdDate']
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
        field: 'createdDate',
        header: 'Fields.CreatedDate',
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
