import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { ActionDto, AddActionDto, UpdateActionDto } from 'shared/interfaces/action/action';

@Injectable({
  providedIn: 'root'
})
export class ActionsService extends HttpService<ActionDto, AddActionDto, UpdateActionDto> {

  protected get baseUrl(): string {
    return 'v1/Actions/';
  }

  getAction(id: string) {
    return this.get<ActionDto>({ apiName: `Get/${id}` });
  }

  get actions() {
    return this.get<ActionDto[]>({ apiName: 'GetAll' });
  }

  add(body: AddActionDto) {
    return this.post<ActionDto>({ apiName: 'add', showAlert: true }, body);
  }

  update(body: UpdateActionDto) {
    return this.put({ apiName: 'update', showAlert: true }, body);
  }

  remove(id: string) {
    return this.delete({ apiName: `deleteSoft/${id}`, showAlert: true }, id);
  }
}
