import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { AddSupplierDto, SupplierDto, UpdateSupplierDto } from 'shared/interfaces/supplier/supplier';

@Injectable({
  providedIn: 'root'
})
export class SuppliersService extends HttpService<SupplierDto, AddSupplierDto, UpdateSupplierDto> {

  protected get baseUrl(): string {
    return 'Suppliers/';
  }

  getSupplier(id: string) {
    return this.get<SupplierDto>({ apiName: `Get/${id}` });
  }

  get suppliers() {
    return this.get<SupplierDto[]>({ apiName: 'GetAll' });
  }

  add(body: AddSupplierDto) {
    return this.post<SupplierDto>({ apiName: 'Add', showAlert: true }, body);
  }

  update(body: UpdateSupplierDto) {
    return this.put({ apiName: 'Update', showAlert: true }, body);
  }

  remove(id: string) {
    return this.delete({ apiName: `DeleteSoft/`, showAlert: true }, id);
  }
}
