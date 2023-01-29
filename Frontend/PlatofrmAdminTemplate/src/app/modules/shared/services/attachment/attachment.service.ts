import { Injectable } from '@angular/core';
import { Shell } from 'base/components/shell';
import { ConfigService } from 'core/services/config/config.service';
import { HttpService } from 'core/services/http/http.service';
import { AttachmentDto } from 'shared/interfaces/attachment/attachment';

@Injectable({
  providedIn: 'root'
})
export class AttachmentService extends HttpService {

  get baseUrl(): string {
    return 'Files/';
  }
  get Config(): ConfigService { return Shell.Injector.get(ConfigService); }

  upload(body: FormData, isPublic: boolean) {
    const serverUrl = this.Config.getAppUrl('FileManagementApi');
    const permissions = JSON.parse(sessionStorage.getItem('Permissions'));
    const appCode = permissions.appCode;
    const categoryCode = 'TEMPLATE';
    const endPoint = serverUrl + `UploadToSanStorage?storageType=1&isPublic=${isPublic}&appCode=${appCode}&categoryCode=${categoryCode}`;

    return this.post<FormData , AttachmentDto[]>({ apiName: endPoint }, body);
  }

  deleteAttachment(fileId: string) {
    const serverUrl = this.Config.getAppUrl('FileManagementApi');
    return this.get<boolean>({ apiName: serverUrl + `Delete/${fileId}` });
  }
}
