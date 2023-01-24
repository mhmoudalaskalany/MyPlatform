import { Observable } from 'rxjs';
import { HttpService } from 'core/services/http/http.service';
import { Injectable } from "@angular/core";
import { FileDto } from '../models/file';
import { ConfigService } from 'core/services/config/config.service';
import { Shell } from 'base/components/shell';
import { Result } from 'shared/models/result';

@Injectable({
    providedIn: 'root'
})

export class AttachmentService extends HttpService {
    get baseUrl(): string {
        return 'Files/'
    }
    get Config(): ConfigService { return Shell.Injector.get(ConfigService); }

    upload(formData: FormData, isPublic: boolean, categoryCode: string, classificationCode: string): Observable<Result<FileDto>> {
        const appCode = 'SELF-SERVICES';
        const endPoint = `UploadToSanStorage?storageType=1&isPublic=${isPublic}&appCode=${appCode}&categoryCode=${categoryCode}&classificationCode=${classificationCode}`;
        const serverUrl = this.Config.getAppUrl('FileManagementApi');
        return this.postExternalTypedRequest<FileDto>(serverUrl, endPoint, formData);
    }

    /**
    * Update File
    * @param formData 
    * @param fileId 
    * @returns 
    */
    update(formData: FormData, fileId: string): Observable<Result<FileDto>> {
        const serverUrl = this.Config.getAppUrl('FileManagementApi');
        const endPoint = `Update?id=${fileId}`;
        return this.postExternalTypedRequest<FileDto>(serverUrl, endPoint, formData)

    }
    delete(fileId): Observable<any> {
        const serverUrl = this.Config.getAppUrl('FileManagementApi');
        return this.http.get(serverUrl + 'Files/Delete/' + fileId);
    }
}