import { Directive, forwardRef, Input } from '@angular/core';
import { AbstractControl, NG_ASYNC_VALIDATORS, AsyncValidator, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { CustomValidationService } from 'shared/services/validation/custom-validation.service';

@Directive({
  selector: '[appValidateEmail]',
  providers: [{ provide: NG_ASYNC_VALIDATORS, useExisting: forwardRef(() => ValidateEmailDirective), multi: true }]

})
export class ValidateEmailDirective implements AsyncValidator {
  @Input('appValidateEmail') userId: string;
  constructor(private customValidator: CustomValidationService) { }

  validate(control: AbstractControl): Observable<ValidationErrors | null> {
    const obs = this.customValidator.emailValidator(control , this.userId);
    return obs;
  }
}
