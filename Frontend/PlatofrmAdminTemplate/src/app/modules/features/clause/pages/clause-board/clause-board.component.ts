import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Shell } from 'base/components/shell';
import { SessionManager } from 'core/guards/session-manager';
import { TranslationService } from 'core/services/translation/translation.service';
import { DeleteModalComponent } from 'shared/components/delete-modal/delete-modal.component';
import { TableOptions } from 'shared/interfaces/table/table';
import { NewDataTableService } from 'shared/services/table/datatable.service';
import { ViewModalComponent } from './../view-modal/view-modal.component';
import { ClauseService } from './../../../../shared/services/clause/clause.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { Moment } from 'moment';
import { MatDatepicker } from '@angular/material/datepicker';

export const MY_FORMATS = {
  parse: {
    dateInput: 'YYYY',
  },
  display: {
    dateInput: 'YYYY',
    monthYearLabel: 'YYYY',
    monthYearA11yLabel: 'YYYY',
  },
};
@Component({
  selector: 'app-clause-board',
  templateUrl: './clause-board.component.html',
  styleUrls: ['./clause-board.component.scss'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },
    { 
     provide: MAT_DATE_FORMATS, useValue: MY_FORMATS
    },
   ]
})
export class ClauseBoardComponent implements OnInit {
  budgets: any[] = [];
  permissions: string[];
  form!: FormGroup;
  model: any;
  opt: TableOptions['bodyOptions'] = {
    pageNumber: 1,
    pageSize: 10,
    orderByValue: [{ colId: 'id', sort: 'asc' }],
    filter: {}
  };

  
  
  get Dialog(): MatDialog { return Shell.Injector.get(MatDialog); }
  get Service(): ClauseService { return Shell.Injector.get(ClauseService) };
  get dataTableService(): NewDataTableService { return Shell.Injector.get(NewDataTableService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Fb(): FormBuilder { return Shell.Injector.get(FormBuilder); }
  manager: SessionManager = SessionManager.Current();
  constructor() { }

  ngOnInit(): void {
    //this.getPermission();
    this.getAllBudgets();
    this.initForm();
  }
  initForm() {
    this.form = this.Fb.group({
      
      budgetId: ['', Validators.required],
      year: ['', Validators.required],
      
    });
  }
  setMonthAndYear(normalizedMonthAndYear: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.form.controls['year'].value!;
    ctrlValue.month(normalizedMonthAndYear.month());
    ctrlValue.year(normalizedMonthAndYear.year());
    this.form.controls['year'].setValue(ctrlValue);
    datepicker.close();
  }
  getAllBudgets(): void {
    this.Service.getAllBudgets().subscribe((res: any) => {

      this.budgets = (res);
      console.log( this.budgets);
      
    });
  }
  getPermission(): void {
    const permission = this.manager.GetPagePermission('FINANCIAL-SYSTEM-BUDGET');
    this.permissions = permission;
  }
  openDeleteModal(id?: string): void {
    const modal = this.Dialog.open(DeleteModalComponent, {
      disableClose: true,
      width: "500px",
    });
    modal.afterClosed().subscribe((dialogResult: any) => {
      if (dialogResult == false) {
        return;
      } else {
        this.Service.remove(id).subscribe(res => {
          if (res === true) {
            this.budgets = this.budgets.filter(x => x.id != id);
          }
        });
      }
    });
  }
  openViewModal(row?: any): void {
    this.Dialog.open(ViewModalComponent, {
      disableClose: true,
      panelClass: ['default', 'p-0'],
      data: { pageValue: row }
    });
  }

  submit() {
    this.model = this.form.value;
    console.log(this.model);
    
  }



}
