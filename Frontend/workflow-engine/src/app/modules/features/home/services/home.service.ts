import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService extends HttpService {
  get baseUrl(): string {
    return 'Reports/';
  }

  
}
