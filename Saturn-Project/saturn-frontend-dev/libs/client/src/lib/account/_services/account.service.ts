import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { Observable } from 'rxjs';
import { CreateCompanyResponse } from '../_models/createCompanyResponse.model';
import { OtpSendRequest } from '../_models/otpSendRequest.model';
import { OtpVerifyRequest } from '../_models/otpSendVerify.model';
import { CreateUserDto } from '../_models/register.model';
import { ResetPasswordDto } from '../_models/resetPassword.model';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  accountServiceUrl= '/api/app/custom-account/';

  constructor(private httpService: HttpService) {

  }

  /**
   * Method to send an OTP on register mobile
   * @param phoneNumber pass registered phone number 
   */
  sendOtp(phoneNumber: string): Observable<any> {
    const otpSendRequest = new OtpSendRequest();
    otpSendRequest.phoneNumber = phoneNumber;
    return this.httpService.post(`${this.accountServiceUrl}send-otp`, otpSendRequest);
  }

  /**
   * Method to verify an OTP
   * @param phoneNumber pass registered phone number
   * @param otp pass entered OTP
   */
  verifyOtp(phoneNumber: string, otp: string): Observable<boolean> {
    const otpVerifyRequest = new OtpVerifyRequest();
    otpVerifyRequest.phoneNumber = phoneNumber;
    otpVerifyRequest.otp = otp;
    return this.httpService.post(`${this.accountServiceUrl}verify-otp`, otpVerifyRequest);
  }

  /**
   * Method to create a new with signup
   * @param userData pass form data values
   */
  createAccount(userData: CreateUserDto): Observable<CreateCompanyResponse> {
    return this.httpService.post(`${this.accountServiceUrl}company`, userData);
  }

  /**
   * Method to send forgot password request
   * @param email pass email address as query parameter
   */
  forgotPassword(email: string):Observable<void> {
    return this.httpService.post(`${this.accountServiceUrl}send-password-reset-link?email=${email}`, email);
  }

  /**
   * Method to send 
   * @param resetPasswordData pass token, userID and new password as parameter
   */
  resetPassword(resetPasswordData: ResetPasswordDto): Observable<void> {
    return this.httpService.post(`${this.accountServiceUrl}reset-password-by-token`,resetPasswordData);
  }
  
}
