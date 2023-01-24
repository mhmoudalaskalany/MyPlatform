
import {
  Component,
  OnChanges,
  Input,
  ChangeDetectionStrategy,
  SimpleChanges,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Shell } from 'base/components/shell';
import { TranslationService } from 'core/services/localization/translation.service';
import { JsonFormControls, JsonFormData } from 'shared/models/dynamic-form/dynamic-form';



@Component({
  selector: 'app-dynamic-form',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.scss'],
})
export class DynamicFormComponent implements OnInit, OnChanges {
  /** Fields */
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

  /** Inputs */
  @Input() jsonFormData: JsonFormData;


  /**Output */
  @Output() add: EventEmitter<any> = new EventEmitter();
  /** Dependencies */
  get Router(): Router { return Shell.Injector.get(Router); }
  get Localize(): TranslationService { return Shell.Injector.get(TranslationService); }
  public form: FormGroup = this.fb.group({});

  constructor(private fb: FormBuilder) {
  }



  ngOnInit(): void {
    this.createForm(this.jsonFormData.controls);
  }

  ngOnChanges(changes: SimpleChanges) {
    // if (!changes.jsonFormData.firstChange) {
    //   console.log(this.jsonFormData.controls)
    //   this.createForm(this.jsonFormData.controls);
    // }
  }

  createForm(controls: JsonFormControls[]) {
    for (const control of controls) {
      const validatorsToAdd = [];
      if (control.validators) {
        for (const [key, value] of Object.entries(control.validators)) {
          switch (key) {
            case 'min':
              validatorsToAdd.push(Validators.min(value));
              break;
            case 'max':
              validatorsToAdd.push(Validators.max(value));
              break;
            case 'required':
              if (value) {
                validatorsToAdd.push(Validators.required);
              }
              break;
            case 'requiredTrue':
              if (value) {
                validatorsToAdd.push(Validators.requiredTrue);
              }
              break;
            case 'email':
              if (value) {
                validatorsToAdd.push(Validators.email);
              }
              break;
            case 'minLength':
              validatorsToAdd.push(Validators.minLength(value));
              break;
            case 'maxLength':
              validatorsToAdd.push(Validators.maxLength(value));
              break;
            case 'pattern':
              validatorsToAdd.push(Validators.pattern(value));
              break;
            case 'nullValidator':
              if (value) {
                validatorsToAdd.push(Validators.nullValidator);
              }
              break;
            default:
              break;
          }
        }
      }

      this.form.addControl(
        control.name,
        this.fb.control(control.value, validatorsToAdd)
      );
    }
  }



  onSubmit() {
    console.log('Form valid: ', this.form.valid);
    console.log('Form values: ', this.form.value);
    this.add.emit(this.form.value);
  }


  /**
   * will be used to validate all forms at submit (not used NOW)
   */
  submit() {
    if (this.form.valid) {
      this.onSubmit();
    } else {
      this.validateAllFormFields(this.form);
    }
  }

  validateAllFormFields(formGroup: FormGroup) {         //{1}
    Object.keys(formGroup.controls).forEach(field => {  //{2}
      const control = formGroup.get(field);             //{3}
      if (control instanceof FormControl) {             //{4}
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {        //{5}
        this.validateAllFormFields(control);            //{6}
      }
    });
  }

}