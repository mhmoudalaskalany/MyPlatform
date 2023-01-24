import { Injectable } from '@angular/core';
import moment from 'moment';
import { BehaviorSubject, Observable } from 'rxjs';

interface Value { labelKey: string; value: string | KeyValue[]; };
interface KeyValue { [key: string]: Value; };
interface ControlValue { [key: string]: Value; }

@Injectable({
  providedIn: 'root'
})
export class ConstructRequestSubmittionService {

  private attachments = [];
  private requestAttachments = new BehaviorSubject([]);
  requestAttachments$ = this.requestAttachments as Observable<any>;

  constructor() { }

  constructSubmition(formValue: any, keyPrefix: string, checkboxProperties: string[] = []): ControlValue {
    const formValueCopy = JSON.parse(JSON.stringify(formValue));
    const formKeys = Object.keys(formValueCopy);
    let finalValues: ControlValue = {};

    formKeys.map(formKey => {
      finalValues[formKey] = this.getFinalSubmitObject(formValue, formKey, keyPrefix, checkboxProperties);
    });

    return finalValues;
  }

  private getFinalSubmitObject(formValue, controlName, keyPrefix, checkboxProperties): Value {
    let finalValue;

    // handling form array value
    if (Array.isArray(formValue[controlName])) {
      if (controlName.toLowerCase().includes('attachment')) {
        this.attachments = this.attachments.concat(formValue[controlName]);
        this.requestAttachments.next(this.attachments);
        return;
      } else {
        finalValue = {
          labelKey: `${keyPrefix}.${this.getKeyFromTranslationKey(controlName)}`,
          value: []
        };

        formValue[controlName].map(arrayKeyValue => {
          (finalValue.value as Array<KeyValue>).push(this.getSubObject(arrayKeyValue, keyPrefix, checkboxProperties));
        });
      }
    } else if (typeof formValue[controlName] === 'object') {
      finalValue = {
        labelKey: `${keyPrefix}.${this.getKeyFromTranslationKey(controlName)}`,
        value: []
      };

      const arrayKeyValue = formValue[controlName];
      if (arrayKeyValue) {
        (finalValue.value as Array<KeyValue>).push(this.getSubObject(arrayKeyValue, keyPrefix, checkboxProperties));
      } else {
        finalValue.value = null;
      }
    } else {
      // handling basic form control value
      finalValue = {
        labelKey: `${keyPrefix}.${this.getKeyFromTranslationKey(controlName)}`,
        value: this.generateControlValue(controlName, formValue[controlName], checkboxProperties)
      };
    }

    return finalValue;
  }

  private getSubObject(arrayKeyValue, keyPrefix, checkboxProperties): KeyValue {
    let arrayKeyValueKeys, arrayKeyValueSubmition: KeyValue = {};

    arrayKeyValueKeys = Object.keys(arrayKeyValue);
    arrayKeyValueKeys.map(arrayKeyValueKey => {
      arrayKeyValueSubmition[arrayKeyValueKey] = this.getFinalSubmitObject(arrayKeyValue, arrayKeyValueKey, keyPrefix, checkboxProperties);
    });

    return arrayKeyValueSubmition;
  }

  private generateControlValue(controlName: string, controlValue: string, checkboxProperties: string[]): string {
    if (controlName.includes('date') || controlName.includes('Date')) {
      return moment(controlValue).format('MM/DD/YYYY');
    } else if (checkboxProperties.includes(controlName)) {
      return controlValue ? '✔' : controlValue;
    }

    return controlValue;
  }

  private getKeyFromTranslationKey(controlName: string): string {
    const finalTranslateKey = controlName.replace(/([a-z])([A-Z])/g, '$1_$2').toUpperCase();

    return finalTranslateKey;
  }
}
