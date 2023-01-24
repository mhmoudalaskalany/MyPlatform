import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EServicesService extends HttpService {

  get baseUrl(): string {
    return 'services/';
  }

  getServiceForm(serviceCode?: string): Observable<any> {
    return this.http.get('/assets/data/form.json');
  }


  /**
   * Get All
   * @returns 
   */
  getAll() {
    return this.getReq('GetAll');
  }
  /**
   * Get Reqport
   * @param body 
   * @returns 
   */

  getReport(body) {
    return this.postReq(this.serverUrl + 'Reports/GetRequests', body);
  }

  getServiceByCode(code) {
    return this.getReq(`GetServiceByCode/${code}`).pipe(map(response => response));
  }

  getDynamicServiceByCode(code) {
    return this.getReq(`GetDynamicServiceByCode/${code}`).pipe(map((response: any) => response.data));
  }

  getFavoriteServices(): Observable<any> {
    return this.getReq('GetAll');
  }
}
