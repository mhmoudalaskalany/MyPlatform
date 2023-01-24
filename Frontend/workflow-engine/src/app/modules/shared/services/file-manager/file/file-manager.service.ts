import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from 'core/services/config/config.service';
import { HttpService } from 'core/services/http/http.service';
import { Observable } from 'rxjs';
import { LoadOptions } from 'shared/components/data-table/models/LoadOpts';

@Injectable({
    providedIn: 'root'
})
export class FileManagerService extends HttpService {

    /**
     * Override Base Server Url
     * @param http 
     * @param config 
     */
    constructor(public override http: HttpClient, public config: ConfigService) {
        super(http, config);
        this.serverUrl = config.getAppUrl('FileManagementApi');
    }
    /**
     * Controller Name
     */
    get baseUrl(): string {
        return 'Files/';
    }

    /**
     * Print Certificate By Id
     * @param certificateId 
     * @returns 
     */
    printCertificate(certificateId) {
        return this.getReq('DownloadWithId/' + certificateId + '?appCode=COVID', { responseType: 'arraybuffer' as 'json' });
    }
    /**
     * Print Certificate With Token
     * @param certificateId 
     * @param token 
     * @returns 
     */
    printCertificateWithToken(certificateId, token) {
        return this.getReq('DownloadWithAppCode/' + certificateId + '?token=' + token, { responseType: 'arraybuffer' as 'json' });
    }
    /**
     * Get Paged Files
     * @param loadOpt 
     * @returns 
     */
    getPagedFiles(loadOpt: LoadOptions): Observable<any> {
        return this.postReq('GetPaged', loadOpt);
    }

    /**
     * Delete File By Id
     * @param fileId 
     * @returns 
     */
    deleteFile(fileId: string) {
        this.getReq('Delete', fileId).subscribe((res: any) => {
            console.log(res);
        });
    }




}
