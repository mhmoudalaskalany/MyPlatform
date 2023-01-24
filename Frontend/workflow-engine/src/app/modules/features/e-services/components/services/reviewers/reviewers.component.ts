import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BaseEditComponent } from 'base/components/base-edit-component';
import { Shell } from 'base/components/shell';
import { RequestService } from 'features/my-requests/services/my-requests.service';
import { ConstructRequestSubmittionService } from 'shared/services/construct-request/construct-request.service';
import { LookupsService } from 'shared/services/lookups/lookups.service';

@Component({
  selector: 'app-reviewers',
  templateUrl: './reviewers.component.html',
  styleUrls: ['./reviewers.component.scss']
})
export class ReviewersComponent extends BaseEditComponent implements OnInit {

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
  constructor(public activatedRoute: ActivatedRoute) {
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
    const subscription = this.ConstructService.requestAttachments$.subscribe(requestAttachments => this.requestAttachments = requestAttachments);
    const submittedBody = this.ConstructService.constructSubmition(this.form.value, `SERVICES`);
    const data = {
      serviceCode: this.serviceCode,
      requestBody: JSON.stringify(submittedBody),
      requestAttachments: this.requestAttachments
    };

    console.log(submittedBody);
    this.Service.submitRequest(data).subscribe((res: any) => {
      if (res.status !== 201) {
        this.SweetAlert.showError(this.Localize.translate.instant('Validation.' + res.message), this.Localize.translate.instant('Validation.AddError'));
        return;
      }
      this.SweetAlert.showSuccess(this.Localize.translate.instant('Validation.AddSuccess'), this.Localize.translate.instant('Common.RequestNumber') + ':' + res.data);
      subscription.unsubscribe();
      this.Route.navigate(['/main/my-requests']);
    });

  }

}
