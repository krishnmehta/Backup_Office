export class OtpVerifyRequest {
    public phoneNumber: string;
    public otp: string;
    
    constructor() {
        this.phoneNumber = '';
        this.otp = '';
    }
}