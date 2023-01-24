import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { RequestService } from 'features/my-requests/services/my-requests.service';
import { first } from 'rxjs/operators';
import { ConstructRequestSubmittionService } from 'shared/services/construct-request/construct-request.service';
import { LookupsService } from 'shared/services/lookups/lookups.service';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.scss']
})
export class SupportComponent implements OnInit {

  donateForm: FormGroup;
  serviceCode: string;
  donationType = '';
  governorates = [];
  requestAttachments = [];
  fileDetails: any[] = [
    {
      "type": "jpg",
      "maxSize": 5
    },
    {
      "type": "png",
      "maxSize": 5
    },
    {
      "type": "jpeg",
      "maxSize": 5
    },
    {
      "type": "pdf",
      "maxSize": 25
    }
  ];

  get RequestService(): RequestService { return Shell.Injector.get(RequestService); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get LookupService(): LookupsService { return Shell.Injector.get(LookupsService); }
  get ConstructService(): ConstructRequestSubmittionService { return Shell.Injector.get(ConstructRequestSubmittionService); }
  get Fb(): FormBuilder { return Shell.Injector.get(FormBuilder); }
  constructor(public activatedRoute: ActivatedRoute, private location: Location) { }

  ngOnInit(): void {
    this.serviceCode = this.activatedRoute.snapshot.paramMap.get('code'); // get id parameter
    this.initForm();
    this.getLookups();
  }

  initForm() {
    this.donateForm = this.Fb.group({
      donationType: ['', Validators.required],
      governorate: [null, Validators.required],
      state: [null, Validators.required],
      mosque: [null, Validators.required],
      purposeName: [null, Validators.required],
      attachments: [[]]
    });
  }

  enableInputs = (type) => {
    if (type === 'direct-donation') {
      this.donateForm.get('donationType').setValue(this.Localize.translate.instant('SERVICES.TB.DIRECT_DONATION'));
    } else {
      this.donateForm.get('donationType').setValue(this.Localize.translate.instant('SERVICES.TB.DONATE_THUABI'));
    }
  }

  getLookups = () => {
    this.LookupService.getGovernorates().subscribe(governorates => this.governorates = governorates);
  }


  getCitiesByGovernorateId = (event): void => {

  }


  submit = () => {
    this.ConstructService.requestAttachments$.pipe(first()).subscribe(requestAttachments => this.requestAttachments = requestAttachments);
    const submittedBody = this.ConstructService.constructSubmition(this.donateForm.value, `SERVICES`);
    const data = {
      serviceCode: this.serviceCode,
      requestBody: JSON.stringify(submittedBody),
      requestAttachments: this.requestAttachments
    };

    console.log(submittedBody);
    this.RequestService.submitRequest(data).subscribe(() => {
      this.location.back();
    });
  }
}
