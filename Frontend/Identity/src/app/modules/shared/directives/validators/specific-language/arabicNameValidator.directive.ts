import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl } from '@angular/forms';
import { CustomValidationService } from 'shared/services/validation/custom-validation.service';
@Directive({
  selector: '[appArabicNamePattern]',
  providers: [{ provide: NG_VALIDATORS, useExisting: ArabicNamePatternDirective, multi: true }]
})
export class ArabicNamePatternDirective implements Validator {

  constructor(private customValidator: CustomValidationService) {
  }

  validate(control: AbstractControl): { [key: string]: any } | null {
    return this.customValidator.arabicNamePatternValidator()(control);
  }
}
