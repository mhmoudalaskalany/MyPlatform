import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { AddCategoryDto, CategoryDto, UpdateCategoryDto } from 'shared/interfaces/category/category';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService extends HttpService{

  protected get baseUrl(): string {
    return 'Categories/';
  }

  getCategory(id: string) {
    return this.get<CategoryDto>({ apiName: `Get/${id}` });
  }

  get categories() {
    return this.get<CategoryDto[]>({ apiName: 'GetAll' });
  }

  add(body: AddCategoryDto) {
    return this.post<AddCategoryDto , CategoryDto>({ apiName: 'Add', showAlert: true }, body);
  }

  update(body: UpdateCategoryDto) {
    return this.put({ apiName: 'Update', showAlert: true }, body);
  }

  remove(id: string) {
    return this.delete({ apiName: `DeleteSoft/${id}`, showAlert: true }, id);
  }
}
