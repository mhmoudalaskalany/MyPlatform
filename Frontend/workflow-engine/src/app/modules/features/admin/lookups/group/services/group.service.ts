import { Observable } from 'rxjs';
import { HttpService } from 'core/services/http/http.service';
import { Injectable } from "@angular/core";
import { Action } from 'core/services/guards/models';
import { Result } from 'shared/models/result';
import { Group } from '../models/group';

@Injectable({
    providedIn: 'root'
})

export class GroupService extends HttpService {
    get baseUrl(): string {
        return 'groups/'
    }

    getAll(): Observable<Result<Group>> {
        return this.getTypedReq<Group>('GetAll');
    }
    

}