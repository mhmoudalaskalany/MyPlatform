import { Injectable } from '@angular/core';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MyTasksService extends HttpService {

  protected get baseUrl(): string {
    return 'Tasks/';
  }

  submitTask(Model) {
    return this.postReq('Update', Model).pipe(map((request: any) => request));
  }

  getAllTasksCount(): Observable<any> {
    return this.getReq(this.serverUrl + 'Tasks/GetTasksCount');
  }

  getAllRequestsCount(): Observable<any> {
    return this.getReq(this.serverUrl + 'Requests/GetRequestsCoun');
  }

  getTasksCount() {
    return this.getReq('GetTasksCount');
  }

  getLatestTask(opt: any) {
    return this.postReq('GetTasks', opt);
  }

  getTaskDetailsById(id) {
    return this.getReq('Get/' + id).pipe(map((request: any) => request.data));
  }

  sendOtp(data) {
    return this.getReq(this.serverUrl + 'Accounts/GenerateOtp/' + data);
  }

  validateOtp(data) {
    return this.getReq(this.serverUrl + 'Accounts/ValidateOtp/' + data);
  }
}
