import { Directive } from '@angular/core';
import { NG_VALIDATORS, AbstractControl, Validator, ValidationErrors, ValidatorFn } from '@angular/forms';

export function emailValidator(): ValidatorFn {

    const Email_RegExp = new RegExp(/^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/);

    return (control: AbstractControl): ValidationErrors | null => {
        const isValid = Email_RegExp.test(control.value);

        if (isValid) {
            return null;
        } else {
            return {
                emailValidator: {
                    valid: false,
                },
            };
        }
    };

}

@Directive({
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: EmailValidatorDirective,
        multi: true,
    }],
})
export class EmailValidatorDirective implements Validator {

    public validate(control: AbstractControl): ValidationErrors | null {
        return emailValidator()(control);
    }
}
