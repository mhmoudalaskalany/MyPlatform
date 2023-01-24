import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BaseEditComponent } from 'base/components/base-edit-component';
import { Shell } from 'base/components/shell';
import { RequestService } from 'features/my-requests/services/my-requests.service';
import { first } from 'rxjs/operators';
import { ConstructRequestSubmittionService } from 'shared/services/construct-request/construct-request.service';
import { LookupsService } from 'shared/services/lookups/lookups.service';

@Component({
  selector: 'app-software-support',
  templateUrl: './software-support.component.html',
  styleUrls: ['./software-support.component.scss']
})
export class SoftwareSupportComponent extends BaseEditComponent implements OnInit {

  serviceCode: string;
  governorates = [];
  reviewTypes = [];
  states = [];
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

  get Service(): RequestService { return Shell.Injector.get(RequestService); }
  get LookupService(): LookupsService { return Shell.Injector.get(LookupsService); }
  get ConstructService(): ConstructRequestSubmittionService { return Shell.Injector.get(ConstructRequestSubmittionService); }
  constructor(public activatedRoute: ActivatedRoute, private location: Location) {
    super(activatedRoute);
  }

  override ngOnInit(): void {
    this.serviceCode = this.activatedRoute.snapshot.paramMap.get('code'); // get id parameter
    this.initForm();
    this.getLookups();
  }

  initForm() {
    this.form = this.Fb.group({
      reviewSubject: ['', Validators.required],
      governorate: [null, Validators.required],
      state: [null, Validators.required],
      reviewType: [null, Validators.required],
      attachments: [[]]
    });
  }


  getLookups = () => {
    this.LookupService.getGovernorates().subscribe(governorates => this.governorates = governorates);
    this.LookupService.getReviewTypes().subscribe(reviewTypes => this.reviewTypes = reviewTypes);
  }


  getCitiesByGovernorateId(governorateId) {
    this.LookupService.getCitiesByGovernorateId(governorateId).subscribe(states => this.states = states);
  }


  submit = () => {
    this.ConstructService.requestAttachments$.pipe(first()).subscribe(requestAttachments => this.requestAttachments = requestAttachments);
    const submittedBody = this.ConstructService.constructSubmition(this.form.value, `SERVICES`);
    const data = {
      serviceCode: this.serviceCode,
      requestBody: JSON.stringify(submittedBody),
      requestAttachments: this.requestAttachments
    };

    console.log(submittedBody);
    this.Service.submitRequest(data).subscribe(() => {
      this.location.back();
    });
  }
}
