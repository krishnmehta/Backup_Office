import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { interval, Subscription } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { CreateUserDto } from '../_models/register.model';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'saturn-frontend-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss', '../_styles/common-style.scss'],
})

export class RegisterComponent {

  formRegisterStep1!: FormGroup;
  formRegisterStep2!: FormGroup;
  formRegisterStep3!: FormGroup;
  registerModel: CreateUserDto = new CreateUserDto();
  isRegisterStep1: boolean;
  isRegisterStep2: boolean;
  isRegisterStep3: boolean;
  isOtpVerified: boolean;
  isOtpSubmitted: boolean;
  otpSeconds: number;
  timeInterval: number;
  otp1: string;
  otp2: string;
  otp3: string;
  otp4: string;
  otpIntervalListener: Subscription = Subscription.EMPTY;
  

  constructor(private accountService: AccountService, private router: Router, private fb: FormBuilder, private alertService: AlertService, private oAuthService: OAuthService, private loaderService: LoaderService) {
    // Check if user is logged in then navigate to user landing page.
    if(this.hasUserLoggedIn()) {
      this.router.navigate(['/app']);
    }
    
    this.isRegisterStep1 = true;
    this.isRegisterStep2 = false;
    this.isRegisterStep3 = false;
    this.isOtpVerified = false;
    this.isOtpSubmitted = false;
    this.otpSeconds = 0;
    this.timeInterval = 1000;
    this.otp1 = '';
    this.otp2 = '';
    this.otp3 = '';
    this.otp4 = '';
    this.declareRegistrationForm();   //Declare form controls
  }
  
  /**
   * Method to declare Registration Form controls
  */
  declareRegistrationForm(): void {
    this.formRegisterStep1 = this.fb.group({
      firstName: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.pattern('^[a-zA-Z]{1,20}$')]),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.pattern('^[a-zA-Z]{1,20}$')]),
      companyName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
      email: new FormControl('', Validators.required),
    });

    this.formRegisterStep2 = this.fb.group({
      phoneNumber: new FormControl('', [Validators.required, Validators.pattern('^[+]{1}[0-9]{2}[0-9]{10}$')]),
      panNumber: new FormControl('', [Validators.required, Validators.pattern('^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$')]),
      password: new FormControl('', [Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[#?!@$%^&*-]).{8,15}$')])
    });

    this.formRegisterStep3 = this.fb.group({
      otp1: new FormControl(''),
      otp2: new FormControl('', Validators.required),
      otp3: new FormControl('', Validators.required),
      otp4: new FormControl('', Validators.required),
      termsPolicy: new FormControl('', Validators.required),
    })
  }

  /** 
   * Method to get all formcontrols for Validations
  */
  get errorStep1(){
    return this.formRegisterStep1.controls;
  }

  get errorStep2(){
    return this.formRegisterStep2.controls;
  }

  /**
   * Method to submit registration step 1
  */
  submitRegisterFormStep1(): void{
    this.loaderService.showLoader();    // Show Loader
    if(this.formRegisterStep1.valid){
      this.isRegisterStep1 = false;
      this.isRegisterStep2 = true;
      this.loaderService.hideLoader();    // Hide Loader
    }
  }
  
  /**
   * Method to submit registration step 3
  */
  submitRegisterFormStep2(): void{
    this.loaderService.showLoader();    // Show Loader
    if(this.formRegisterStep2.valid){
      this.isRegisterStep2 = false;
      this.isRegisterStep3 = true;
      this.loaderService.hideLoader();    // Hide Loader
      this.sendOtp();
    }
  }

  /**
   * Method to submit registration step 3
  */
  submitRegisterFormStep3(): void{
    this.loaderService.showLoader();    // Show Loader
    if(this.formRegisterStep3.valid) {
        const otp = `${this.otp1}${this.otp2}${this.otp3}${this.otp4}`;

        this.accountService.verifyOtp(this.registerModel.phoneNumber, otp).subscribe((response) => {
          this.isOtpSubmitted = true;
          this.isOtpVerified = response;

          if(this.isOtpVerified) {
            this.accountService.createAccount(this.registerModel).subscribe((response) => {
              
              if(!response.isError && !response.isEmailExist && !response.isPhoneNumberExist){
                this.loaderService.hideLoader();    // Hide Loader
                this.router.navigate(['/login']);
                this.alertService.success('Congratulations, your account has been successfully created. You can login with email address and password.', false);
              }
              else {
                this.alertService.error('The user already registered with the same email address and phone number.', false);
                this.loaderService.hideLoader();    // Hide Loader
              }
            },(error)=>{
              alert(error);
              this.isOtpSubmitted = false;
              this.isOtpVerified = false;
            });
          }
          else {
            this.loaderService.hideLoader();    // Hide Loader
          }

        }, (error)=>{
            this.isOtpSubmitted = false;
            this.loaderService.hideLoader();    // Hide Loader
        });
    }
  }

  /**
   * Method to send & resend OTP
  */
  sendOtp() {
    this.loaderService.showLoader();    // Show Loader
    this.accountService.sendOtp(this.registerModel.phoneNumber).subscribe(() => {
      this.otpSeconds = 30;
      this.otpIntervalListener = this.getOtpIntervalSubscibe();
      this.loaderService.hideLoader();    // Hide Loader
    });
  }

  /**
   * Method to countdown an OTP
  */
  countDownOtp(): void {
    if (this.otpSeconds === 0) {
      this.otpIntervalListener.unsubscribe();
    }
    else {
      this.otpSeconds = this.otpSeconds - 1;
    }
  }

  /**
   * Method to subscribe an OTP interval
  */
  getOtpIntervalSubscibe(): Subscription {
    return interval(this.timeInterval).subscribe(() => { this.countDownOtp(); });
  }

  /**
   * Method to check if the user logged in
   * @returns Returns true if the user is logged in else false
   */
  hasUserLoggedIn(): boolean
  {
    return this.oAuthService.hasValidAccessToken();
  }
}
