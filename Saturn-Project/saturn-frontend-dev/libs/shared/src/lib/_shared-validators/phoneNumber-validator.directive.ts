import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validator, ValidationErrors, ValidatorFn } from '@angular/forms';

export function phoneNumberValidator(): ValidatorFn {

    const PhoneNumber_RegExp = new RegExp(/^\+?[0-9]{2}[0-9]{10}$/);

    return (control: AbstractControl): ValidationErrors | null => {
        const isValid = PhoneNumber_RegExp.test(control.value);

        if (isValid) {
            return null;
        } else {
            return {
                phoneNumberValidator: {
                    valid: false,
                },
            };
        }
    };

}

@Directive({
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: PhoneNumberValidatorDirective,
        multi: true,
    }],
})
export class PhoneNumberValidatorDirective implements Validator {

    public validate(control: AbstractControl): ValidationErrors | null {
        return phoneNumberValidator()(control);
    }
}
