import { Component, OnInit } from '@angular/core';
import { BaseEditComponent } from 'base/components/base-edit-component';

import { ActivatedRoute } from '@angular/router';
import { Shell } from 'base/components/shell';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { WorkflowBuilderService } from '../../services/workflow-builder.service';
import { Group } from 'features/admin/lookups/group/models/group';
import { GroupService } from 'features/admin/lookups/group/services/group.service';
import { Lookup } from 'shared/models/lookup';
import { LookupsService } from 'shared/services/lookups/lookups.service';

@Component({
  selector: 'app-add-workflow',
  templateUrl: './add-workflow.component.html',
  styleUrls: ['./add-workflow.component.scss'],
})
export class AddWorkflowComponent extends BaseEditComponent implements OnInit {

  groups: Group[] = [];
  actions: Lookup[] = [];
  workFlowOrderTypes: any[] = [{
    id: 1,
    nameEn: 'Direct Manager',
    nameAr: 'مدير مباشر'
  },
  {
    id: 2,
    nameEn: 'Group',
    nameAr: 'فريق'
  }]

  escalationTypes: Lookup[] = [];
  get Service(): WorkflowBuilderService { return Shell.Injector.get(WorkflowBuilderService); }
  get GroupService(): GroupService { return Shell.Injector.get(GroupService); }
  get LookupService(): LookupsService { return Shell.Injector.get(LookupsService); }
  constructor(public override route: ActivatedRoute) {
    super(route);
  }

  get templateOrders(): FormArray {
    return this.form.get('templateOrders') as FormArray;
  }

  getTemplateOrderActions(tempOrderIndex: number): FormArray {
    return this.templateOrders.at(tempOrderIndex).get('templateOrderActions') as FormArray;
  }

  override ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.getLookups();
    this.initForm();
    super.ngOnInit();
  }

  initForm(): void {
    this.form = this.Fb.group({
      id: [this.isNew ? null : this.id],
      nameEn: [null, Validators.required],
      nameAr: [null, Validators.required],
      managersLevels: [null, Validators.required],
      includeEmployeeFirst: [0, Validators.required],
      templateOrders: this.Fb.array(this.id ? [] : [this.initTemplateOrderForm()], Validators.required)
    });
  }

  initTemplateOrderForm(): FormGroup {
    return this.Fb.group({
      id: [null],
      nameEn: [null, Validators.required],
      nameAr: [null, Validators.required],
      order: [null, Validators.required],
      type: [null, Validators.required],
      hasReminder: [0],
      reminderHours: [0],
      escalationType: [1],
      escalationHours: [0],
      groupId: [null],
      templateId: [null],
      templateOrderActions: this.Fb.array(this.id ? [] : [this.initTemplateOrderActionForm()], Validators.required)
    });
  }

  initTemplateOrderActionForm(): FormGroup {
    return this.Fb.group({
      actionId: [null, Validators.required],
      templateOrderId: [null],
    });
  }

  override patchForm() {
    this.model.templateOrders.map((each, index) => {
      this.templateOrders.push(this.initTemplateOrderForm());

      each.templateOrderActions.map(() => {
        this.getTemplateOrderActions(index).push(this.initTemplateOrderActionForm());
      });
    });
    this.form.patchValue(this.model);
  }

  addTemplateOrderAction(tempOrderIndex: number): void {
    this.getTemplateOrderActions(tempOrderIndex).push(this.initTemplateOrderActionForm());
  }

  removeTemplateOrderAction(tempOrderIndex: number, tempOrderActionIndex: number) {
    this.getTemplateOrderActions(tempOrderIndex).removeAt(tempOrderActionIndex);
  }

  disableSelectedOption(selected, templateorderActionIndex: number, templateOrderIndex: number) {
    if (selected) {
      const typesFiltered = this.getTemplateOrderActions(templateOrderIndex).value.filter((action, index) => action.actionId === selected.id && index !== templateorderActionIndex);

      if (typesFiltered.length) {
        this.getTemplateOrderActions(templateOrderIndex).controls[templateorderActionIndex].get('actionId').reset();
      }
    }
  }
  getLookups(): void {
    this.GroupService.getAll().subscribe((res: any) => {
      this.groups = res.data;
    });

    this.LookupService.getEscalationTypes().subscribe(res => {
      this.escalationTypes = res;
    });

    this.LookupService.getActions().subscribe(res => {
      this.actions = res;
    });
  }

  showHideParamters(event, index): void {
    if (event && event.id == 1) {
      this.templateOrders.at(index).get('groupId').removeValidators(Validators.required);
    }

    if (event && event.id == 2) {
      this.templateOrders.at(index).get('groupId').addValidators(Validators.required);
    }
  }

}
