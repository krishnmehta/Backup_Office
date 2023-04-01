import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'saturn-frontend-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss', '../_styles/reset-password.scss'],
})
export class ForgotPasswordComponent {
  formForgotPassword!: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, private alertService: AlertService, private router: Router, private oAuthService: OAuthService, private loaderService: LoaderService){
    // Check if user is logged in then navigate to user landing page.
    if(this.hasUserLoggedIn()) {
      this.router.navigate(['/app']);
    }

    this.declareForgotPasswordForm();   //Declare form controls
  }

  /**
   * Method to declare forgot password form controls
   */
  declareForgotPasswordForm(): void{
    this.formForgotPassword = this.fb.group({
      email: new FormControl('', Validators.required)
    });
  }

  /**
   * Method to get all formcontrols for validations
   */
  get formError(){
    return this.formForgotPassword.controls;
  }

  /**
   * Method to submit forgot password form
   */
  submitForgotPassword():void {
    this.loaderService.showLoader();    // Show Loader

    if(this.formForgotPassword.valid){
      const email = this.formForgotPassword.controls['email'].value;
      this.accountService.forgotPassword(email).subscribe((response) => {
        this.formForgotPassword.reset();
        this.router.navigate(['/login']);
        this.alertService.success('The reset password email is sent. Check your email and reset password.', false);
        this.loaderService.hideLoader();    // Hide loader
      },(error)=>{
        this.loaderService.hideLoader();    // Hide loader
        this.alertService.error('Oops! something went wrong.', false);
      });
    }
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
