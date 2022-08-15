import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Shell } from 'base/components/shell';
import { SessionManager } from 'core/guards/session-manager';
import { TranslationService } from 'core/services/translation/translation.service';
import { take } from 'rxjs';
import { DeleteModalComponent } from 'shared/components/delete-modal/delete-modal.component';
import { TableOptions } from 'shared/interfaces/table/table';
import { BudgetsService } from 'shared/services/budget/budgets.service';
import { NewDataTableService } from 'shared/services/table/datatable.service';
import { ViewModalComponent } from './../view-modal/view-modal.component';

@Component({
  selector: 'app-budgets-dashboard',
  templateUrl: './budgets-dashboard.component.html',
  styleUrls: ['./budgets-dashboard.component.scss']
})
export class BudgetsDashboardComponent implements OnInit {
  budgets: any[] = [];
  permissions: string[];

  opt: TableOptions['bodyOptions'] = {
    pageNumber: 1,
    pageSize: 10,
    orderByValue: [{ colId: 'id', sort: 'asc' }],
    filter: {}
  };
  
  get Dialog(): MatDialog { return Shell.Injector.get(MatDialog); }
  get Service(): BudgetsService { return Shell.Injector.get(BudgetsService) };
  get dataTableService(): NewDataTableService { return Shell.Injector.get(NewDataTableService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  manager: SessionManager = SessionManager.Current();
  constructor() { }

  ngOnInit(): void {
    //this.getPermission();
    this.getAllBudgets();
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




}
