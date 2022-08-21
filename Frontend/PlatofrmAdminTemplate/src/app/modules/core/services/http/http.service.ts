import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Shell } from 'base/components/shell';
import { Observable, map } from 'rxjs';
import { ApResponse } from 'shared/interfaces/response/response';
import { AlertService } from '../alert/alert.service';
import { ConfigService } from '../config/config.service';
import { TranslationService } from '../translation/translation.service';
import { HttpServiceBaseService } from './HttpServiceBaseService';
import { HttpStatus } from './HttpStatus';
import { UrlConfig } from './UrlConfig';

@Injectable({
  providedIn: 'root'
})
export abstract class HttpService<TRead, TCreate, TUpdate> extends HttpServiceBaseService {

  private domainName: string;
  get alertService(): AlertService { return Shell.Injector.get(AlertService); }
  get configService(): ConfigService { return Shell.Injector.get(ConfigService); }
  get localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  constructor(private http: HttpClient) {
    super();
    this.domainName = this.configService.getAppUrl('HOST_API');
  }

  get<T>(URL_Config: UrlConfig): Observable<TRead> {
    return this.http.get<ApResponse<TRead>>(`${this.domainName}${this.baseUrl}${URL_Config.apiName}`, { params: URL_Config.params }).pipe(map(event => {
      return event.data;
    }));
  }

  getAll<T>(URL_Config: UrlConfig): Observable<TRead[]> {
    return this.http.get<ApResponse<TRead[]>>(`${this.domainName}${this.baseUrl}${URL_Config.apiName}`, { params: URL_Config.params }).pipe(map(event => {
      return event.data;
    }));
  }

  post<T>(URL_Config: UrlConfig, body: TCreate): Observable<TRead> {
    return this.http.post<ApResponse<TRead>>(`${this.domainName}${this.baseUrl}${URL_Config.apiName}`, body, { params: URL_Config.params }).pipe(map(event => {
      URL_Config.showAlert ? this.alertHandling(event) : '';
      return event.data;
    }));
  }

  put(URL_Config: UrlConfig, body: TUpdate): Observable<TRead> {
    return this.http.put<ApResponse<TRead>>(`${this.domainName}${this.baseUrl}${URL_Config.apiName}`, body, { params: URL_Config.params }).pipe(map((event: any) => {
      this.alertHandling(event);
      return event.data;
    }));
  }

  delete(URL_Config: UrlConfig, id): Observable<boolean> {
    return this.http.delete<ApResponse<boolean>>(`${this.domainName}${this.baseUrl}${URL_Config.apiName}`, { body: id, params: URL_Config.params }).pipe(map((event: any) => {
      this.alertHandling(event);
      return event.data;
    }));
  }

  private alertHandling(event: ApResponse<any>) {
    if (event.status) {
      debugger;
      if (Number.isNaN(Number(event.status))) {
        debugger;
        if (event.status.toString().startsWith('2')) {
          this.alertService.success(event.message ? this.localize.translate.instant('Validation.' + event.message) : 'Successfully Done...');
        } else {
          this.alertService.error(event.message ? this.localize.translate.instant('Validation.' + event.message) : '!NOT HANDLED ERROR!');
        }
      } else {
        switch (event.status.toString()) {
          case HttpStatus.CREATED: {
            this.alertService.success(event.message ? this.localize.translate.instant('Validation.' + event.message) : 'Successfully Done...');
            break;
          }
          case HttpStatus.BADREQUEST: {
            this.alertService.error(event.message ? this.localize.translate.instant('Validation.' + event.message) : '!NOT HANDLED ERROR!');
            break;
          }
          default: {
            this.alertService.error(event.message ? this.localize.translate.instant('Validation.' + event.message) : '!NOT HANDLED ERROR!');
          }
        }

      }
    }
  }

}
