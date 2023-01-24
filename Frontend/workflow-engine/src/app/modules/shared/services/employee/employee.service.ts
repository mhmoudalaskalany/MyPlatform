import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Shell } from 'base/components/shell';
import { HttpService } from 'core/services/http/http.service';
import { LoadOptions } from 'shared/components/data-table/models/LoadOpts';
import { ConfigService } from 'core/services/config/config.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class EmployeeService extends HttpService {
    get baseUrl(): string {
        return 'ExternalNewEmployees/';
    }
    /**
     * Override Base Server Url
     * @param http 
     * @param config 
     */
    constructor(public override http: HttpClient, public config: ConfigService) {
        super(http, config);
        this.serverUrl = config.getAppUrl('UserManagementApi');
    }

    getPagedEmployees(opts: LoadOptions): Observable<any> {
        return this.postReq('GetDropDown', opts);
    }


}
