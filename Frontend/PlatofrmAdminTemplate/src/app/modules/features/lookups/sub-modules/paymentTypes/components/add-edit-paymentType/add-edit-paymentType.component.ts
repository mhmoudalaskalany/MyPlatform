import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { PaymentTypesService } from 'shared/services/paymentTypes/paymentTypes.service';

@Component({
  selector: 'app-add-edit-paymentType',
  templateUrl: './add-edit-paymentType.component.html',
  styleUrls: ['./add-edit-paymentType.component.scss']
})
export class AddEditPaymentTypeComponent implements OnInit {

  pageTitle = '';
  pageType = '';
  lookupId = '';
  lookupFormGroup!: FormGroup;

  constructor(private dialogRef: MatDialogRef<AddEditPaymentTypeComponent>, @Optional() @Inject(MAT_DIALOG_DATA) public data: { activatedRoute: ActivatedRoute; },
    private fb: FormBuilder, private paymentTypesService: PaymentTypesService) { }

  ngOnInit(): void {
    this.pageTitle = this.data.activatedRoute.snapshot.firstChild?.data['pageTitle'];
    this.pageType = this.data.activatedRoute.snapshot.firstChild?.data['pageType'];
    this.lookupId = this.data.activatedRoute.snapshot.firstChild?.paramMap.get('id') as string;

    this.initFormGroup();
  }

  initFormGroup() {
    this.lookupFormGroup = this.fb.group({
      description: ['', Validators.required],
      nameEn: ['', Validators.required],
      nameAr: ['', Validators.required],
      code: ['', Validators.required]
    });

    if (this.pageType === 'edit') {
      this.getLookup();
    }
  }

  getLookup() {
    this.paymentTypesService.getPaymentType(this.lookupId).subscribe(paymentType => this.lookupFormGroup.patchValue(paymentType));
  }

  submit() {
    if (this.pageType === 'add') this.paymentTypesService.add(this.lookupFormGroup.value).subscribe(() => this.dialogRef.close());
    if (this.pageType === 'edit') this.paymentTypesService.update({ id: this.lookupId, ...this.lookupFormGroup.value }).subscribe(() => this.dialogRef.close());
  }
}
