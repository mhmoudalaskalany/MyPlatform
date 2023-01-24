import { Observable } from 'rxjs';
import { HttpService } from 'core/services/http/http.service';
import { Injectable } from "@angular/core";
import { Action } from 'core/services/guards/models';
import { Result } from 'shared/models/result';

@Injectable({
    providedIn: 'root'
})

export class ServiceService extends HttpService {
    get baseUrl(): string {
        return 'Services/'
    }

    getAll(): Observable<Result<Action>> {
        return this.getTypedReq<Action>('GetAll');
    }

}