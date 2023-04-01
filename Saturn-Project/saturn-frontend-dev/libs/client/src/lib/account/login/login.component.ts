import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountLoginService } from '../_services/account-login.service';
import { LoginModel } from '../_models/login.model';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthService } from '@abp/ng.core';
import { Router } from '@angular/router';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Component({
  selector: 'saturn-frontend-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss', '../_styles/common-style.scss'],
})
export class LoginComponent {
  
  isEmailTabVisible: boolean;
  isPhoneTabVisible: boolean;
  formEmailLogin!: FormGroup;
  formPhoneLogin!: FormGroup;
  formOtpLogin!: FormGroup;
  isPhoneStep1: boolean;
  isPhoneStep2: boolean;
  otp1: string | undefined;
  otp2: string | undefined;
  otp3: string | undefined;
  otp4: string | undefined;
  loginModelData: LoginModel = new LoginModel();
  UserName : any;
  isName: any = false;
  loginLinkText:any="Logout";

  constructor(private fb: FormBuilder, private loginService: AccountLoginService, private oAuthService: OAuthService, private authService: AuthService, private router: Router, private alertService: AlertService, private loaderService: LoaderService,private msalService:MsalService) {
    if(!this.isUserLoggedIn())
    {
      this.loginLinkText="Login";
    }
    // Check if user is logged in then navigate to user landing page.
    if (this.hasUserLoggedIn()) {
      this.router.navigate(['/app']);
    }
    
    this.isEmailTabVisible = true;
    this.isPhoneTabVisible = false;
    this.isPhoneStep1 = true;
    this.isPhoneStep2 = false;

    this.declareLoginFormControls();  //Declare form controls
  }

  login()
  {
    this.msalService.loginPopup().subscribe((response:AuthenticationResult)=>{
      sessionStorage.setItem('msal.accessToken', response.accessToken);
      console.log("ok",response.account?.idTokenClaims?.name);
      this.UserName = response.account?.idTokenClaims?.name;
        this.msalService.instance.setActiveAccount(response.account);
        this.isName= true;
        this.router.navigate(['/app']);
    })
  }
  logout()
  {
    this.isName= false;
    this.msalService.logout();
    window.location.reload();
  }
  isUserLoggedIn():boolean
  
  {
    if(this.msalService.instance.getActiveAccount()!=null)
    {
      return true;
    }
    return false;
  }
  /**
   * Method to declare Login form controls
  */
  declareLoginFormControls(): void {
    this.formEmailLogin = this.fb.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });

    this.formPhoneLogin = this.fb.group({
      phone: new FormControl('', [Validators.required, Validators.pattern('^[+]{1}[0-9]{2}[0-9]{10}$')])
    });

    this.formOtpLogin = this.fb.group({
      otp1: new FormControl('', Validators.required),
      otp2: new FormControl('', Validators.required),
      otp3: new FormControl('', Validators.required),
      otp4: new FormControl('', Validators.required),
    })
  }

  /**
   * Method to get all formControls for validations
  */
  get emailFormError() {
    return this.formEmailLogin.controls;
  }

  get phoneFormError() {
    return this.formPhoneLogin.controls;
  }

  /**
   * Method to select tab Email or Phone
   * @param tab Get name of tab email or phone and manage tab view
  */
  selectTab(tab: string): void {
    switch (tab) {
      case 'email':
        this.isEmailTabVisible = true;
        this.isPhoneTabVisible = false;
        this.isPhoneStep1 = true;
        this.isPhoneStep2 = false;
        break;
      case 'phone':
        this.isPhoneTabVisible = true;
        this.isEmailTabVisible = false;
        break;
      default:
        break;
    }
    this.formEmailLogin.reset();  //Reset Email form
    this.formPhoneLogin.reset();  //Reset Phone form
    this.formOtpLogin.reset();    //Reset OTP form
  }

  /**
   * Method to submit login form using email address
  */
  submitEmailLogin(): void {
    if (this.formEmailLogin.valid) {
      this.loaderService.showLoader();  //Show Loader
      this.authService.login({
        username: this.loginModelData.userNameOrEmailAddress as string,
        password: this.loginModelData.password as string,
        rememberMe: true,
        redirectUrl: ''
      }).subscribe(
        {
          next: () => {
            this.loaderService.hideLoader(); //Hide Loader
            this.router.navigate(['/app']);
          },
          error: (err) => {
            this.loaderService.hideLoader(); //Hide Loader
            if (err.status !== undefined && err.status === 400 && err.error !== undefined && err.error.error === 'invalid_grant') {
              this.alertService.error('Invalid username or password!', false);
            }
            else {
              this.alertService.error('Oops! something went wrong.', false);
            }
          }
        });
    }
    else {
      return
    }
  }

  /**
   * Method to submit login form using phone number
  */
  submitPhoneLogin(): void {
    if (this.formPhoneLogin.valid) {
      this.isPhoneStep1 = false;
      this.isPhoneStep2 = true;
    }
  }

  /**
   * Method to submit login form using OTP
  */
  submitOtpLogin(): void {
    /** */
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
