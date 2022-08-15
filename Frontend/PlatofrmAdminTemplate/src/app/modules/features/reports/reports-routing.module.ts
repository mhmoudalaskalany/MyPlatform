import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportType, SortType } from './enums/report-type';
import { ReportsComponent } from './pages/reports/reports.component';

const routes: Routes = [
  { path: '', redirectTo: 'highest-earning', pathMatch: 'full' },
  {
    path: 'highest-earning',
    data: {
      title: 'REPORTS.HIGHEST_EARNING', filter: { reportType: ReportType.Earning, sortType: SortType.Desc, count: 20 },
      table: {
        getAllAPI: 'Products/GetProductsReport',
      }
    },
    component: ReportsComponent
  },
  {
    path: 'lowest-earning',
    data: {
      title: 'REPORTS.LOWEST_EARNING', filter: { reportType: ReportType.Earning, sortType: SortType.Asc, count: 20 },
      table: {
        getAllAPI: 'Products/GetProductsReport',
      }
    },
    component: ReportsComponent
  },
  {
    path: 'high-in-demand',
    data: {
      title: 'REPORTS.HIGH_IN_DEMAND', filter: { reportType: ReportType.Demand, sortType: SortType.Desc, count: 20 },
      table: {
        getAllAPI: 'Products/GetProductsReport',
      }
    },
    component: ReportsComponent
  },
  {
    path: 'low-in-demand',
    data: {
      title: 'REPORTS.LOW_IN_DEMAND', filter: { reportType: ReportType.Demand, sortType: SortType.Asc, count: 20 },
      table: {
        getAllAPI: 'Products/GetProductsReport',
      }
    },
    component: ReportsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
