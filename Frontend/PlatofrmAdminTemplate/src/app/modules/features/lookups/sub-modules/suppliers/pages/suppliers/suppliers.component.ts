import { BaseListComponent } from 'base/components/base-list-component';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs';
import { TableOptions } from 'shared/interfaces/table/table';
import { SuppliersService } from 'shared/services/suppliers/suppliers.service';
import { Shell } from 'base/components/shell';

@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  styleUrls: ['./suppliers.component.scss']
})
export class SuppliersComponent extends BaseListComponent {

  isEnglish = false;
  tableOptions!: TableOptions | undefined;

  get service(): SuppliersService { return Shell.Injector.get(SuppliersService); }
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
          getAll: 'v1/Suppliers/GetPaged',
          getAllMethod: 'POST',
          delete: 'v1/Suppliers/DeleteSoft',
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
        header: 'Fields.CREATED_DATE',
        filter: false,
        filterMode: 'date',
      },
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
