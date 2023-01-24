import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { LoadOptions } from '../models/LoadOpts';

@Injectable({
  providedIn: 'root'
})
export class DataTableService {

  opt: LoadOptions = {
    pageNumber: 1,
    pageSize: 10,
    orderByValue: [{ colId: 'id', sort: 'asc' }],
    filter: {}
  };
  public searchNew$: BehaviorSubject<{}> = new BehaviorSubject({});
  constructor(private http: HttpClient) { }

  loadData(url?: string): Observable<any> {
    return this.http.post(url, this.opt);
  }

  delete(url?: any, id?: string, appId?: any): Observable<any> {
    if (appId) {
      return this.http.delete(url + '/' + id + '/' + appId);
    }
    return this.http.delete(url + '/' + id);
  }
}
