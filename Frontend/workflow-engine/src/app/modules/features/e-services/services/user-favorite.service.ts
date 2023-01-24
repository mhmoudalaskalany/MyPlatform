import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';


@Injectable({
  providedIn: 'root'
})
export class UserFavoriteService extends HttpService {

  get baseUrl(): string {
    return 'UserFavoriteServices/';
  }



  /**
   * Get All
   * @returns 
   */
  getAll() {
    return this.getReq('GetAll');
  }

 

}
