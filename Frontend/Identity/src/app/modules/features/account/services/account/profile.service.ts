
import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';
import { User } from 'shared/interfaces/lookups/lookups';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends HttpService {
  get baseUrl(): string {
    return 'Users/';
  }
  getUserProfile(id: any): Observable<any> {
    return this.get<User>({ APIName: `GetProfileAsync/${id}` });
  }
}
