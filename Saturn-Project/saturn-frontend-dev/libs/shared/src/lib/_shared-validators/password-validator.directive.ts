import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validator, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {

    const Password_RegExp = new RegExp(/(?=^.{8,15}$)(?=.*\d)(?=.*[@#%&!$*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/);

    return (control: AbstractControl): ValidationErrors | null => {
        const isValid = Password_RegExp.test(control.value);

        if (isValid) {
            return null;
        } else {
            return {
                passwordValidator: {
                    valid: false,
                },
            };
        }
    };

}

@Directive({
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: PasswordValidatorDirective,
        multi: true,
    }],
})
export class PasswordValidatorDirective implements Validator {

    public validate(control: AbstractControl): ValidationErrors | null {
        return passwordValidator()(control);
    }
}
