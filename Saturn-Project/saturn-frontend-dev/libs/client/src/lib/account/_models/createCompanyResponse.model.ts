export class CreateCompanyResponse {
    public isError: boolean;

    public isEmailExist: boolean;

    public isPhoneNumberExist: boolean;

    constructor() {
        this.isError = false;
        this.isEmailExist = false;
        this.isPhoneNumberExist = false;
    }
}