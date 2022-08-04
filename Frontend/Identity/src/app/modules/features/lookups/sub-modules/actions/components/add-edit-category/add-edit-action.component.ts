import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { ActionsService } from '../../services/actions.service';

@Component({
  selector: 'app-add-edit-action',
  templateUrl: './add-edit-action.component.html',
  styleUrls: ['./add-edit-action.component.scss']
})
export class AddEditActionComponent  implements OnInit {

  pageTitle = '';
  pageType = '';
  lookupId = '';
  form!: FormGroup;
  get service(): ActionsService { return Shell.Injector.get(ActionsService); }
  constructor(private dialogRef: MatDialogRef<AddEditActionComponent>, @Optional() @Inject(MAT_DIALOG_DATA) public data: { activatedRoute: ActivatedRoute; },
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.pageTitle = this.data.activatedRoute.snapshot.firstChild?.data['pageTitle'];
    this.pageType = this.data.activatedRoute.snapshot.firstChild?.data['pageType'];
    this.lookupId = this.data.activatedRoute.snapshot.firstChild?.paramMap.get('id') as string;

    this.initFormGroup();
  }

  initFormGroup() {
    this.form = this.fb.group({
      nameEn: ['', Validators.required],
      nameAr: ['', Validators.required],
      code: ['', Validators.required]
    });

    if (this.pageType === 'edit') {
      this.getLookup();
    }
  }

  getLookup() {
    this.service.getAction(this.lookupId).subscribe(action => this.form.patchValue(action));
  }

  submit() {
    if (this.pageType === 'add') this.service.add(this.form.value).subscribe(() => this.dialogRef.close(true));
    if (this.pageType === 'edit') this.service.update({ id: this.lookupId, ...this.form.value }).subscribe(() => this.dialogRef.close(true));
  }
}
