import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { EServicesService } from '../../services/e-services.service';

@Component({
  selector: 'app-request-service',
  templateUrl: './request-service.component.html',
  styleUrls: ['./request-service.component.scss']
})
export class RequestServiceComponent implements OnInit {

  code: string;
  serviceData: any;
  pageCode = 'SELF-SERVICES-E-SERVICE'
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Service(): EServicesService { return Shell.Injector.get(EServicesService); }
  constructor(public route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.code = this.route.snapshot.paramMap.get('code'); // get id parameter
    this.Service.getServiceByCode(this.code).subscribe((response: any) => this.serviceData = response.data);
  }
}
