import { Observable } from 'rxjs';
import { HttpService } from 'core/services/http/http.service';
import { Injectable } from "@angular/core";
import { Result } from 'shared/models/result';
import { GroupMember } from '../models/groupMember';

@Injectable({
    providedIn: 'root'
})

export class GroupMemberService extends HttpService {
    get baseUrl(): string {
        return 'groupmembers/'
    }

    getAll(): Observable<Result<GroupMember>> {
        return this.getTypedReq<GroupMember>('GetAll');
    }

    add(model): Observable<Result<any>> {
        return this.postTypedRequest<any>('add', model);
    }

    getByGroupId(groupId): Observable<Result<GroupMember[]>> {
        return this.getTypedReq<GroupMember[]>('getbygroupid/' + groupId);
    }

    deleteMember(groupId, civilNumber): Observable<Result<any>> {
        return this.deleteTypedReq<any>('deletemember/' + groupId + '/' + civilNumber);
    }



}