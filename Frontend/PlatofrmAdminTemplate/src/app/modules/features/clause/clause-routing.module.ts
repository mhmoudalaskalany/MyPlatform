import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClauseBoardComponent } from './pages/clause-board/clause-board.component';

const routes: Routes = [
  {
    path: '',
    data: { title: 'Pages.Budgets.Title', pageType: 'list' },
    component: ClauseBoardComponent,
    
    
    
  } 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
exports: [RouterModule]
})
export class ClauseRoutingModule { }
