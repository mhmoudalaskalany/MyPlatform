import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseListComponent } from 'base/components/base-list-component';
import { Shell } from 'base/components/shell';
import { takeUntil } from 'rxjs';
import { TableOptions } from 'shared/interfaces/table/table';
import { PaymentTypesService } from 'shared/services/paymentTypes/paymentTypes.service';

@Component({
  selector: 'app-paymentTypes',
  templateUrl: './paymentTypes.component.html',
  styleUrls: ['./paymentTypes.component.scss']
})
export class PaymentTypesComponent extends BaseListComponent {

  isEnglish = false;
  tableOptions!: TableOptions | undefined;

  get service(): PaymentTypesService { return Shell.Injector.get(PaymentTypesService); }
  constructor(activatedRoute: ActivatedRoute) {
    super(activatedRoute);
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
          getAll: 'v1/PaymentTypes/GetPaged',
          getAllMethod: 'POST',
          delete: 'v1/PaymentTypes/DeleteSoft',
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
        header: 'Fields.code',
        filter: false,
        filterMode: 'textF',
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
        name: 'EDIT',
        icon: 'bx bx-edit',
        color: 'text-middle',
        isEdit: true,
        route: 'edit/'
      },
      {
        name: 'DELETE',
        icon: 'bx bx-trash',
        color: 'text-error',
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
