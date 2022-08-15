import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BudgetsService } from './../../../../shared/services/budget/budgets.service';

@Component({
  selector: 'app-add-edit-budget',
  templateUrl: './add-edit-budget.component.html',
  styleUrls: ['./add-edit-budget.component.scss']
})
export class AddEditBudgetComponent implements OnInit {

  pageTitle = '';
  pageType = '';
  lookupId = '';
  form!: FormGroup;

  constructor(private dialogRef: MatDialogRef<AddEditBudgetComponent>, @Optional() @Inject(MAT_DIALOG_DATA) public data: { activatedRoute: ActivatedRoute; },
    private fb: FormBuilder, private BudgetsService: BudgetsService) {

  }

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
      budgetNumber: ['', Validators.required],
      budgetYear: ['', Validators.required],
      amount: ['', Validators.required],
      clauses: this.fb.array([this.initClause()]),
    });
    console.log(this.form);

    if (this.pageType === 'edit') {
      this.getLookup();
    }
  }

  initClause(): FormGroup {
    return this.fb.group({
      id: [null],
      nameEn: ['', Validators.required],
      nameAr: ['', Validators.required],
      clauseNumber: ['', Validators.required],
      approvedAmount: ['', Validators.required],
   //   actualAmount: ['', Validators.required]
    });
  }
  get clauses() {
    return this.form.get('clauses') as FormArray;
  }
  resetclauses() {
    this.clauses.controls.map((control, index) => index > 0 ? this.clauses.removeAt(index) : '');
    this.clauses.reset();
  }


  getLookup() {
    this.BudgetsService.getCategory(this.lookupId).subscribe(category => this.form.patchValue(category));
  }

  submit() {
    if (this.pageType === 'add') this.BudgetsService.add(this.form.value).subscribe(() => this.dialogRef.close());
    if (this.pageType === 'edit') this.BudgetsService.update({ id: this.lookupId, ...this.form.value }).subscribe(() => this.dialogRef.close());
  }
}
