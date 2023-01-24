import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Lookup } from 'shared/models/lookup';


@Injectable({
  providedIn: 'root'
})
export class LookupsService extends HttpService {

  get baseUrl(): string {
    return 'lookups/';
  }

  /**
  * Get Statuses
  * @returns 
  */
  getStatuses(): Observable<Lookup[]> {
    return this.getReq('GetStatuses').pipe(map((request: any) => request.data));
  }

  /**
   * Get Actions
   * @returns 
   */
  getActions(): Observable<Lookup[]> {
    return this.getReq('GetActions').pipe(map((request: any) => request.data));
  }

  /**
 * Get Escalation Types
 * @returns 
 */
  getEscalationTypes(): Observable<Lookup[]> {
    return this.getReq('GetEscalationTypes').pipe(map((request: any) => request.data));
  }

  /**
 * Get Categories
 * @returns 
 */
  getCategories(): Observable<Lookup[]> {
    return this.getReq('GetCategories').pipe(map((request: any) => request.data));
  }

  /**
* Get Templates
* @returns 
*/
  getTemplates(): Observable<Lookup[]> {
    return this.http.get(this.serverUrl + 'Templates/getAll').pipe(map((request: any) => request.data));
  }

  /**
   * Get Governorates
   * @returns 
   */
  getGovernorates(): Observable<Lookup[]> {
    return this.getReq('GetGovernorates').pipe(map((request: any) => request.data));
  }

  /**
   * Get Cities
   * @returns 
   */
  getCities(): Observable<Lookup[]> {
    return this.getReq('GetCities').pipe(map((request: any) => request.data));
  }

  /**
   * Get Cities By Governorate Id
   * @returns 
   */
  getCitiesByGovernorateId(governorateId): Observable<Lookup[]> {
    return this.getReq('GetCitiesByGovernorateId/' + governorateId).pipe(map((request: any) => request.data));
  }

  /**
   * Get Endowment Deeds
   * @returns 
   */
  getReviewTypes(): Observable<Lookup[]> {
    return this.getReq('getReviewTypes').pipe(map((request: any) => request.data));
  }



  /**
   * Get Nationalities
   * @returns 
   */
  getNationalities(): Observable<Lookup[]> {
    return this.getReq('GetNationalities').pipe(map((request: any) => request.data));
  }




  /**
   * Get Jobs
   * @returns 
   */
  getJobs(): Observable<any> {
    return this.http.get(this.serverUrl + 'i/api/lookups/getJobs').pipe(map((request: any) => request.data));
  }

  /**
   * Get Roles
   * @returns 
   */
  getRoles(): Observable<any> {
    return this.http.get(this.serverUrl + 'i/api/lookups/getRoles').pipe(map((request: any) => request.data));
  }

  getBuildingTypes(): Observable<any> {
    return this.http.get(this.serverUrl + 'c/api/lookups/GetBuildingTypes').pipe(map((request: any) => request.data));
  }

  getMasjedTypes(): Observable<any> {
    return this.http.get(this.serverUrl + 'c/api/lookups/GetMasjedsTypes').pipe(map((request: any) => request.data));
  }

  getManagementTypes(): Observable<any> {
    return this.http.get(this.serverUrl + 'c/api/lookups/GetManagementTypes').pipe(map((request: any) => request.data));
  }

  getAddressCity(): Observable<any> {
    return this.http.get(this.serverUrl + 'c/api/lookups/GetGovernorates').pipe(map((request: any) => request.data));
  }

  getAddressGovernerate(): Observable<any> {
    return this.http.get(this.serverUrl + 'c/api/lookups/GetCities').pipe(map((request: any) => request.data));
  }
}
