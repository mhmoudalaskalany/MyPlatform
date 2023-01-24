import { AuthHttpService } from 'core/services/http/authHttp.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends AuthHttpService {
  get baseUrl(): string {
    return 'Users/';
  }
  getUserProfile(id: any): Observable<any> {
    return this.getHeaderReq('GetProfileAsync', id);
  }
}
