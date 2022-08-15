
import { OnInit, Directive, ViewChild } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { AlertService } from 'core/services/alert/alert.service';
import { Shell } from './shell';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionManager } from 'core/guards/session-manager';
import { TranslationService } from 'core/services/translation/translation.service';
import { NewDataTableService } from 'shared/services/table/datatable.service';
import { TableOptions } from 'shared/interfaces/table/table';
import { LazyLoadEvent } from 'primeng/api';
import { debounceTime, distinctUntilChanged, Subject, takeUntil } from 'rxjs';
import { ExportExcelService } from 'shared/services/export-excel/export-excel.service';




@Directive()
export abstract class BaseComponent implements OnInit {

  title = '';
  pageType = '';
  manager: SessionManager = SessionManager.Current();
  data: any[];
  totalCount: number = 0;
  abstract tableOptions: TableOptions
  /* load data at first time */
  private firstInit: boolean;
  protected destroy$: Subject<boolean> = new Subject<boolean>();
  abstract get service(): HttpService<any, any, any>;
  get dataTableService(): NewDataTableService { return Shell.Injector.get(NewDataTableService); }
  get alert(): AlertService { return Shell.Injector.get(AlertService); }
  get route(): Router { return Shell.Injector.get(Router); }
  get excel(): ExportExcelService { return Shell.Injector.get(ExportExcelService); }
  get localize(): TranslationService { return Shell.Injector.get(TranslationService); }

  constructor(private activatedRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.title = this.activatedRoute.snapshot.data['title'];
    this.pageType = this.activatedRoute.snapshot.data['pageType'];
  }


  /**
   * Handle Data Table Event (Sort , Pagination , Filter , Delete , Print)
   * @param dataTableEvent 
   */
  handleEvent(dataTableEvent: any): void {
    console.log('event', dataTableEvent);
    if (dataTableEvent.eventType == 'lazyLoad') {
      this.loadLazyLoadedData(dataTableEvent.data);
    }
    if (dataTableEvent.eventType == 'reset') {
      this.resetOpt();
    }

    if (dataTableEvent.eventType == 'filter') {
      this.resetOpt();
    }

    if (dataTableEvent.eventType == 'delete') {
      this.resetOpt();
    }
    if (dataTableEvent.eventType == 'export') {
      this.export(dataTableEvent.data.columnNames, dataTableEvent.data.reportName);
    }
  }

  columnSearchInput(): void {
    this.dataTableService.searchNew$
      .pipe(
        debounceTime(1000),
        distinctUntilChanged(),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.firstInit ? this.loadDataFromServer() : (this.firstInit = true);
      });
  };


  // load data from server
  loadDataFromServer(): void {
    this.dataTableService.loadData(this.tableOptions.inputUrl.getAll).subscribe((res: any) => {
      console.log('data at base component', res.data.data);
      this.data = res.data.data;
      this.totalCount = res.data.totalCount;
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
    this.dataTableService.opt.orderByValue = [];
    this.dataTableService.opt.orderByValue.push({
      colId: event.sortField,
      sort: event.sortOrder === 1 ? 'asc' : 'desc',
    });
  }
  /* set paging parameters*/
  setPaging(event?: LazyLoadEvent): void {
    this.dataTableService.opt.pageSize = event.rows;
    this.dataTableService.opt.pageNumber = event.first / event.rows + 1;
  }


  // Filter
  filter(value?: any, column?: any, filterColumnName?: string, dataType?: string): void {
    this.resetOpt();
    value = this.checkDataType(value, dataType);
    if (
      filterColumnName !== undefined &&
      filterColumnName !== '' &&
      filterColumnName !== null
    ) {
      this.dataTableService.searchNew$.next(
        (this.dataTableService.opt.filter[filterColumnName] = value)
      );
    } else {
      this.dataTableService.searchNew$.next((this.dataTableService.opt.filter[column] = value));
    }
  }


  checkDataType(value: any, dataType?: string): any {
    if (dataType === 'number') {
      value = +value;
    }
    return value;
  }




  /* reset server opt */
  resetOpt(): void {
    this.dataTableService.opt = {
      pageNumber: 1,
      pageSize: 10,
      orderByValue: [{ colId: 'id', sort: 'asc' }],
      filter: {},
    };
    this.dataTableService.opt.filter =
      this.tableOptions.bodyOptions.filter !== null &&
        this.tableOptions.bodyOptions.filter !== undefined
        ? this.tableOptions.bodyOptions.filter
        : this.dataTableService.opt.filter;
    this.dataTableService.opt.filter.appId =
      this.tableOptions.appId !== 0 ? this.tableOptions.appId : 0;
  }


  export(sheetDetails: { [k: string]: string; }, fileName: string) {
    const sheetColumnsValues = Object.keys(sheetDetails);

    const newArray = this.data.map((eachData, index) => {
      let eachRow = {};

      sheetColumnsValues.map(eachColumnValue => {
        eachRow = {
          ...eachRow,
          ...{ '#': index + 1 },
          [sheetDetails[eachColumnValue]]: eachData[eachColumnValue]
        };
      });

      return eachRow;
    });

    this.excel.exportAsExcelFile(newArray, fileName);
  }



  /* when leaving the component */
  ngOnDestroy() {
    this.dataTableService.searchNew$.next(null);
  }



}
