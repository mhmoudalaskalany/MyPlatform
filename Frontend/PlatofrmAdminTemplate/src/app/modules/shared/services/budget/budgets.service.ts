import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs/internal/Observable';
import { BudgetDto, AddBudgetDto, UpdateBudgetDto } from 'shared/interfaces/budget/budget';
import { TableOptions } from 'shared/interfaces/table/table';

@Injectable({
  providedIn: 'root'
})
export class BudgetsService extends HttpService<BudgetDto, AddBudgetDto, UpdateBudgetDto> {

  protected get baseUrl(): string {
    return 'budgets/';
  }

  getCategory(id: string) {
    return this.get<BudgetDto>({ apiName: `Get/${id}` });
  }

  get categories() {
    return this.get<BudgetDto[]>({ apiName: 'GetAll' });
  }

  add(body: AddBudgetDto) {
    return this.post<BudgetDto>({ apiName: 'Add', showAlert: true }, body);
  }

  update(body: UpdateBudgetDto) {
    return this.put({ apiName: 'Update', showAlert: true }, body);
  }

  remove(id: string) {
    return this.delete({ apiName: `DeleteSoft/${id}`, showAlert: true }, id);
  }
  getAllBudgets() {
    return this.get<BudgetDto[]>({ apiName: 'GetAll' });
  }
  
}
