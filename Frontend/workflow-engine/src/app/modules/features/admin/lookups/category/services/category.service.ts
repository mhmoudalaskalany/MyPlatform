import { Observable } from 'rxjs';
import { HttpService } from 'core/services/http/http.service';
import { Injectable } from "@angular/core";
import { Result } from 'shared/models/result';
import { Category } from '../models/category';

@Injectable({
    providedIn: 'root'
})

export class CategoryService extends HttpService {
    get baseUrl(): string {
        return 'Categories/'
    }

    getAll(): Observable<Result<Category>> {
        return this.getTypedReq<Category>('GetAll');
    }

}