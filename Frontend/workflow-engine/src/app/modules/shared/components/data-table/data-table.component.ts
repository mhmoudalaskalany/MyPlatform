import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { DataTableService } from './services/datatable.service';
import { Shell } from 'base/components/shell';
import { ColumnsInterface } from './models/columns.interface';
import { URL } from './models/url';
import {
  take,
  debounceTime,
  distinctUntilChanged,
  takeUntil,
} from 'rxjs/operators';
import { AlertService } from 'core/services/alert/alert.service';
import { ConfigService } from 'core/services/config/config.service';
import { ActionsInterface } from './models/actions.interface';
import { DeleteModalComponent } from './components/delete-modal.component';
import { Router } from '@angular/router';
import { TableOptions } from './models/tableOptions';
import { Subject } from 'rxjs';
import { LazyLoadEvent } from 'primeng/api';
import { TranslationService } from 'core/services/localization/translation.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss'],
})
export class DataTableComponent implements OnInit, OnDestroy {
  // Inputs
  @Input() tableOptions: TableOptions = {};
  // private fields
  /* hold Columns Definitions */
  cols: ColumnsInterface[] = [];
  /* hold actions */
  actions: ActionsInterface[] = [];
  /* hold the urls fro get and delete */
  url: URL = {};
  /* hold the data */
  data: any[] = [];
  /* total count of records */
  totalCount = 0;
  /* hold the component name */
  componentName: string;
  /* hold the permissions */
  permissions: string[] = [];
  /* hold the current route */
  currentRoute;
  /* get reference to bootstrap modal */
  bsModalRef: any;
  /* load data at first time */
  private firstInit: boolean;
  /* subscriber to unsubscribe when leaving the component */
  private destroy$: Subject<boolean> = new Subject<boolean>();
  // services
  get Service(): DataTableService {
    return Shell.Injector.get(DataTableService);
  }
  get Alert(): AlertService {
    return Shell.Injector.get(AlertService);
  }
  get Config(): ConfigService {
    return Shell.Injector.get(ConfigService);
  }
  get Dialog(): MatDialog { return Shell.Injector.get(MatDialog); }
  get ModalConfig(): void { return; }
  get Translate(): TranslationService {
    return Shell.Injector.get(TranslationService);
  }
  get Router(): Router {
    return Shell.Injector.get(Router);
  }
  constructor() {
    this.currentRoute = this.Router.url.substring(
      0,
      this.Router.url.length - 3
    );
  }

  // initialization
  ngOnInit(): void {
    this.initializeInputs();
    // this.loadDataFromServer();
    this.columnSearchInput();
  }
  initializeInputs(): void {
    // initialize url from config service
    this.url = {
      getAll: this.Config.getAppUrl('API')[this.tableOptions.inputUrl.getAll],
      delete: this.Config.getAppUrl('API')[this.tableOptions.inputUrl.delete],
    };
    // initialize columns
    this.cols = this.tableOptions.inputCols;
    this.actions = this.tableOptions.inputActions;
    this.permissions = this.tableOptions.inputPermissions;
    this.componentName = this.tableOptions.inputName;
    this.Service.opt.filter =
      this.tableOptions.filter !== null &&
        this.tableOptions.filter !== undefined
        ? this.tableOptions.filter
        : this.Service.opt.filter;
    this.Service.opt.filter.appId =
      this.tableOptions.appId !== 0 ? this.tableOptions.appId : 0;
  }

  // get cell data
  getCellData(row: any, col: any): any {
    const nestedProperties: string[] = col.field.split('.');
    let value: any = row;
    for (const prop of nestedProperties) {
      if (value[prop] == null) {
        return '';
      }
      value = value[prop];
    }
    return value;
  }
  // load data from server
  loadDataFromServer(): void {
    this.Service.loadData(this.url.getAll).subscribe((res: any) => {
      console.log(res.data.data)
      if (res.status !== 200) {
        this.Alert.showError(this.Translate.translate.instant('Data.' + res.message));
      }
      this.data = res.data.data;
      this.totalCount = res.totalCount;
    });
  }
  /* lazy load table data */
  /* note:  gets called on entering component */
  loadLazyLoadedData(event?: LazyLoadEvent): void {
    this.resetOpt();
    this.setSortColumn(event);
    this.setPaging(event);
    this.loadDataFromServer();
  }

  /* set SortColumn */
  setSortColumn(event?: LazyLoadEvent): void {
    this.Service.opt.orderByValue = [];
    this.Service.opt.orderByValue.push({
      colId: event.sortField,
      sort: event.sortOrder === 1 ? 'asc' : 'desc',
    });
  }
  /* set paging parameters*/
  setPaging(event?: LazyLoadEvent): void {
    this.Service.opt.pageSize = event.rows;
    this.Service.opt.pageNumber = event.first / event.rows + 1;
  }
  // Delete
  openDeleteModal(id?: string): void {
    const modal = this.Dialog.open(DeleteModalComponent, {
      disableClose: true,
      width: "500px"
    });
    modal.afterClosed().subscribe((dialogResult: any) => {
      if (dialogResult == false) {
        return;
      } else {
        this.delete(id)
      }
    });
  }
  delete(id: string): void {
    if (this.tableOptions.includeAppId) {
      this.deleteByAppId(id, this.tableOptions.appId);
    } else {
      this.deleteById(id);
    }
  }

  deleteByAppId(id?: any, appId?: any): void {
    this.Service.delete(this.url.delete, id, appId)
      .pipe(take(1))
      .subscribe(
        (x) => {
          this.Alert.showError(
            this.Translate.translate.instant('Data.DeleteSuccess')
          );
          this.loadDataFromServer();
        },
        (error) => {
          this.Alert.showError(
            this.Translate.translate.instant('Data.DeleteError')
          );
        }
      );
  }

  deleteById(id?: any): void {
    this.Service.delete(this.url.delete, id)
      .pipe(take(1))
      .subscribe(
        (x) => {
          this.Alert.showError(
            this.Translate.translate.instant('Data.DeleteSuccess')
          );
          this.loadDataFromServer();
        },
        (error) => {
          this.Alert.showError(
            this.Translate.translate.instant('Data.DeleteError')
          );
        }
      );
  }

  // Filter
  filter(
    value?: any,
    column?: any,
    filterColumnName?: string,
    dataType?: string
  ): void {
    this.resetOpt();
    value = this.checkDataType(value, dataType);
    if (
      filterColumnName !== undefined &&
      filterColumnName !== '' &&
      filterColumnName !== null
    ) {
      this.Service.searchNew$.next(
        (this.Service.opt.filter[filterColumnName] = value)
      );
    } else {
      this.Service.searchNew$.next((this.Service.opt.filter[column] = value));
    }
  }
  checkDataType(value: any, dataType?: string): any {
    if (dataType === 'number') {
      value = +value;
    }

    return value;
  }
  /* get search value when typing on column search box */
  columnSearchInput(): void {
    this.Service.searchNew$
      .pipe(
        debounceTime(1000),
        distinctUntilChanged(),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.firstInit ? this.loadDataFromServer() : (this.firstInit = true);
      });
  }
  /* when leaving the component */
  ngOnDestroy() {
    this.Service.searchNew$.next({});
    this.resetOpt();
    this.firstInit = false;
    this.destroy$.next(true);
    this.destroy$.unsubscribe();
  }

  /* reset server opt */
  resetOpt(): void {
    this.Service.opt = {
      pageNumber: 1,
      pageSize: 10,
      orderByValue: [{ colId: 'id', sort: 'asc' }],
      filter: {},
    };
    this.Service.opt.filter =
      this.tableOptions.filter !== null &&
        this.tableOptions.filter !== undefined
        ? this.tableOptions.filter
        : this.Service.opt.filter;
    this.Service.opt.filter.appId =
      this.tableOptions.appId !== 0 ? this.tableOptions.appId : 0;
  }
}
