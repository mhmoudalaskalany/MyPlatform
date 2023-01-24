import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { RequestService } from 'features/my-requests/services/my-requests.service';
import { SweetAlertService } from 'shared/services/alert/sweet-alert.service';
import { ConstructRequestSubmittionService } from 'shared/services/construct-request/construct-request.service';
import { EServicesService } from '../../services/e-services.service';

@Component({
  selector: 'app-request-dynamic-service',
  templateUrl: './request-dynamic-service.component.html',
  styleUrls: ['./request-dynamic-service.component.scss']
})
export class RequestDynamicServiceComponent implements OnInit {

  code: string;
  serviceData: any;
  pageCode = 'SELF-SERVICES-E-SERVICE';
  requestAttachments = [];
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Service(): EServicesService { return Shell.Injector.get(EServicesService); }
  get RequestService(): RequestService { return Shell.Injector.get(RequestService); }
  get ConstructService(): ConstructRequestSubmittionService { return Shell.Injector.get(ConstructRequestSubmittionService); }
  get SweetAlert(): SweetAlertService { return Shell.Injector.get(SweetAlertService); }
  get Router(): Router { return Shell.Injector.get(Router); }
  constructor(public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.code = this.route.snapshot.paramMap.get('code'); // get id parameter
    this.Service.getDynamicServiceByCode(this.code).subscribe((response: any) => {
      this.serviceData = response;
    });
  }


  /**
   * Submit Form Emitted From Dynamic Form Component
   * @param event 
   */
  submitRequest(event) {
    console.log(event);
    const subscription = this.ConstructService.requestAttachments$.subscribe(requestAttachments => this.requestAttachments = requestAttachments);
    const submittedBody = this.ConstructService.constructSubmition(event, `SERVICES`);
    const data = {
      serviceCode: this.serviceData.code,
      requestBody: JSON.stringify(submittedBody),
      requestAttachments: this.requestAttachments
    };

    console.log(submittedBody);
    this.RequestService.submitRequest(data).subscribe((res: any) => {
      if (res.status !== 201) {
        this.SweetAlert.showError(this.Localize.translate.instant('Validation.' + res.message), this.Localize.translate.instant('Validation.AddError'));
        return;
      }
      this.SweetAlert.showSuccess(this.Localize.translate.instant('Validation.AddSuccess'), this.Localize.translate.instant('Common.RequestNumber') + ':' + res.data);
      subscription.unsubscribe();
      this.Router.navigate(['/main/my-requests']);
    });
  }
}
