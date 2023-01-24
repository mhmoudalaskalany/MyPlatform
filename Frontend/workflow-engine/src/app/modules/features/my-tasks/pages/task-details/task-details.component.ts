import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import moment from 'moment';
import { AlertService } from 'shared/services/alert/alert.service';
import { MyTasksService } from '../../services/my-tasks.service';
import { StorageService } from 'core/services/storage/storage.service';
import { FileService } from 'shared/services/file-manager/file/file.service';

interface KeyValue { [key: string]: { labelKey: string; value: string | KeyValue[]; }; };
interface ControlValue { [key: string]: { labelKey: string; value: string | KeyValue[]; }; }
@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.scss']
})
export class TaskDetailsComponent implements OnInit {

  taskId: string;
  taskForm: FormGroup;
  taskData;
  requestData;
  requestDetails;
  requestAttachments = [];
  YES_NO = [
    {
      id: 1,
      nameEn: "Yes",
      nameAr: "نعم"
    },
    {
      id: 2,
      nameEn: "No",
      nameAr: "لا"
    }
  ];
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
  today = moment().format();
  taskBody: string;




  get Storage(): StorageService { return Shell.Injector.get(StorageService); }
  get Router(): Router { return Shell.Injector.get(Router); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  get Alert(): AlertService { return Shell.Injector.get(AlertService); }
  get Fb(): FormBuilder { return Shell.Injector.get(FormBuilder); }
  get Task(): MyTasksService { return Shell.Injector.get(MyTasksService); }
  get Modal(): MatDialog { return Shell.Injector.get(MatDialog); }
  get FileService(): FileService { return Shell.Injector.get(FileService); }
  constructor(private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.taskId = this.activeRoute.snapshot.paramMap.get('id');
    this.initForm();

    this.Task.getTaskDetailsById(this.taskId).subscribe(taskDetails => {
      if (taskDetails) {
        this.taskData = taskDetails;
        this.requestData = this.taskData.request;
      } else {
        this.Router.navigate(['/main/my-tasks']);
      }
    });
  }

  stringifyData(data) {
    return JSON.stringify(data);
  }

  /**
   * Init Form
   */
  initForm() {
    this.taskForm = this.Fb.group({
      comments: [''],
      attachments: [[]]
    });
  }


  doCondition(event, conditionId, formGroup: FormGroup, controls: string[]) { // conditionId === 1 ? Yes : No
    if (event.id === conditionId) {
      controls.map(control => {
        formGroup.get(control).disabled ? formGroup.get(control).enable() : '';
        formGroup.get(control).reset();
      });
    } else {
      controls.map(control => {
        formGroup.get(control).reset();
        formGroup.get(control).enabled ? formGroup.get(control).disable() : '';
      });
    }
  }



  /**
   * Return Background Color For Action
   * @param color 
   * @returns 
   */
  setBackgroundColor(color): string {
    switch (color) {
      case 'Red': {
        return 'bg-error text-white';
      }
      case 'Orange': {
        return 'bg-orange text-white';
      }
      case 'Green': {
        return 'bg-success text-white';
      }
      case 'Dark': {
        return 'bg-purple';
      }
      case 'Blue': {
        return 'bg-bluelight';
      }

      default: {
        return '';
      }
    }
  }

  /**
   * Execute Action
   * @param action 
   */
  executeAction(action) {
    let taskData = {
      id: this.taskData.id,
      comment: this.taskForm.get('comments').value,
      actionId: action.id,
      taskAttachments: [],
      taskBody: this.taskBody
    };

    const value = JSON.parse(JSON.stringify(this.taskForm.getRawValue()));
    value.attachments = value.attachments.map(attachment => {
      attachment.taskId = this.taskData.id;
    });
    if (action.isCommentRequired) {
      if (!this.taskForm.get('comments').value) {
        this.taskForm.get('comments').setErrors({ required: true });
      } else {
        this.Task.submitTask(taskData).subscribe(data => {
          if (data) {
            console.log('data', data);
            this.Alert.showSuccess(this.Localize.translate.instant('Validation.' + data.message));
            this.Router.navigate(['/main/my-tasks']);
          }
        });
      }
    } else {
      this.taskForm.get('comments').setErrors(null);

      this.Task.submitTask(taskData).subscribe(data => {
        if (data) {
          this.Alert.showSuccess(this.Localize.translate.instant('Validation.' + data.message));
          this.Router.navigate(['/main/my-tasks']);
        }
      });
    }
  }

  download(link, name) {
    this.FileService.download(link, name);
  }

}
