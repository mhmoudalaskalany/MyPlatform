import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpServiceBaseService } from 'base/services/http-service-base.service';
import { Observable } from 'rxjs';
import { ConfigService } from '../config/config.service';
import { Result } from 'shared/models/result';
@Injectable({
  providedIn: 'root',
})

/**
 * Manipulate the HTTP requests for the whole app
 * handle the main POST, GET, UPDATE, DELETE methods
 */
export abstract class HttpService extends HttpServiceBaseService {
  public serverUrl = this.configService.getServerUrl();

  constructor(public http: HttpClient, public configService: ConfigService) {
    super();
  }
  /**
   * Get request using angular httpClient module
   * @param {string} url - the end point url
   * @param {?any} [data] - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  getLookup(url: string, data?: any) {
    return this.http.get(this.serverUrl + this.baseUrl + url, data);
  }

  /**
   * Get request using angular httpClient module
   * @param {string} url - the end point url
   * @param {?any} [data] - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  get(url: string, data?: any) {
    return this.http.get(this.serverUrl + url, data);
  }

  /**
   * Post request using angular httpClient module
   * @param {string} url - the end point url
   * @param {any} data - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  postReq(url: string, data: any, options?: any) {
    console.log('at post request');
    return this.http.post(this.serverUrl + this.baseUrl + url, data, options);
  }
  /**
   * post request with typed return type
   * @param url 
   * @param data 
   * @returns 
   */
  postTypedRequest<T>(url: string, data: any): Observable<Result<T>> {
    return this.http.post<Result<T>>(this.serverUrl + this.baseUrl + url, data);
  }

  /**
 * post request with typed return type
 * @param serverUrl external service url 
 * @param data 
 * @returns 
 */
  postExternalTypedRequest<T>(serverUrl: string, endpoint: string, data: any): Observable<Result<T>> {
    return this.http.post<Result<T>>(serverUrl + this.baseUrl + endpoint, data);
  }


  /**
   * Get request using angular httpClient module
   * @param {string} url - the end point url
   * @param {?any} [data] - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  getReq(url: string, data?: any) {
    return this.http.get(this.serverUrl + this.baseUrl + url, data);
  }

  /**
   * Get request using angular httpClient module
   * you can bass a parameter (data) in the url separated by '/'
   * @param {string} url - the end point url
   * @param {string} data - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  getTypedReqWithParameter<T>(url: string, data?: string): Observable<Result<T>> {
    return this.http.get<Result<T>>(this.serverUrl + this.baseUrl + url + '/' + data);
  }

  /**
   * Get request using angular httpClient module
   * you can bass a parameter (data) in the url separated by '/'
   * @param {string} url - the end point url
   *
   * @return {Observable} Observable of response, comes from the end point
   */
  getTypedReq<T>(url: string): Observable<Result<T>> {
    return this.http.get<Result<T>>(this.serverUrl + this.baseUrl + url);
  }

  /**
 * Get request using angular httpClient module
 * you can bass a parameter (data) in the url separated by '/'
 * @param {string} url - the end point url
 *
 * @return {Observable} Observable of response, comes from the end point
 */
  deleteTypedReq<T>(url: string): Observable<Result<T>> {
    return this.http.delete<Result<T>>(this.serverUrl + this.baseUrl + url);
  }

  /**
   * Get request using angular httpClient module
   * @param {string} url - the end point url
   * @param {any} [data] - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  getPaged(url: string, data: any): Observable<any> {
    return this.http.post(this.serverUrl + this.baseUrl + url, data);
  }

  /**
   * Get request using angular httpClient module
   * @param {string} url - the end point url
   * @param {?any} [params] - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  getQueryReq(url: string, params?: any) {
    return this.http.get(this.serverUrl + this.baseUrl + url, { params });
  }
  /**
   * Get request using angular httpClient module
   * you can bass a parameter (data) in the url separated by '/'
   * @param {string} url - the end point url
   * @param {string} data - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  getHeaderReq(url: string, data: string) {
    return this.http.get(this.serverUrl + this.baseUrl + url + '/' + data);
  }

  /**
   * PUT request using angular httpClient module
   * you can bass a parameter (data) in the url separated by '/'
   * @param {string} url - the end point url
   * @param {?any} data - request payload
   * @return {Observable} Observable of response, comes from the end point
   */
  putReq(url: string, data?: any) {
    return this.http.put(this.serverUrl + this.baseUrl + url, data);
  }

  deleteReq(url: string, data?: any) {
    return this.http.delete(this.serverUrl + this.baseUrl + url + '/' + data);
  }
}
