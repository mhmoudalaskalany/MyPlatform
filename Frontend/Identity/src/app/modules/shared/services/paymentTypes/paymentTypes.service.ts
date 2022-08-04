import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { PaymentTypeDto, AddPaymentTypeDto, UpdatePaymentTypeDto } from 'shared/interfaces/paymentType/paymentType';

@Injectable({
  providedIn: 'root'
})
export class PaymentTypesService extends HttpService<PaymentTypeDto, AddPaymentTypeDto, UpdatePaymentTypeDto> {

  protected get baseUrl(): string {
    return 'PaymentTypes/';
  }

  getPaymentType(id: string) {
    return this.get<PaymentTypeDto>({ apiName: `Get/${id}` });
  }

  get paymentTypes() {
    return this.get<PaymentTypeDto[]>({ apiName: 'GetAll' });
  }

  add(body: AddPaymentTypeDto) {
    return this.post<PaymentTypeDto>({ apiName: 'Add', showAlert: true }, body);
  }

  update(body: UpdatePaymentTypeDto) {
    return this.put({ apiName: 'Update', showAlert: true }, body);
  }

  remove(id: string) {
    return this.delete({ apiName: `DeleteSoft/${id}`, showAlert: true }, id);
  }
}
