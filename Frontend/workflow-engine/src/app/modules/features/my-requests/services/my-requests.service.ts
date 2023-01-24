import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RequestService extends HttpService {

  protected get baseUrl(): string {
    return 'Requests/';
  }

  /**
   * Add Request
   * @param Model 
   * @returns 
   */
  submitRequest(Model) {
    return this.postReq('Add', Model);
  }
  /**
   * Get All Requests
   * @returns 
   */
  getAllRequests(): Observable<any> {
    return this.getReq('GetAll');
  }

  
  getRequestCards(): Observable<any> {
    return this.getReq('getRequestCard');
  }

  /**
   * Get All Requests paged
   * @returns 
   */
  getPagedRequests(filter:any):Observable<any>{
    return this.getReq('GetPaged',filter);
  }

  /**
   * Get Requests Count
   * @returns 
   */
  getAllRequestsCount(): Observable<any> {
    return this.getReq('GetRequestsCount');
  }

  /**
   * Get Request By Id
   * @param id 
   * @returns 
   */
  getRequestById(id) {
    return this.getReq('Get/' + id).pipe(map((request: any) => request.data));
  }
}
