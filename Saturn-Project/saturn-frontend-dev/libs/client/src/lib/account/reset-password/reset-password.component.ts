import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordDto } from '../_models/resetPassword.model';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'saturn-frontend-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss', '../_styles/reset-password.scss'],
})
export class ResetPasswordComponent {
  passwordType: boolean;
  confPasswordType: boolean;
  formResetePassword!: FormGroup;
  token: string | undefined;
  userId: string | undefined;
  newPassword: string | undefined;
  

  constructor(private fb: FormBuilder, private accountService: AccountService, private activeRoute: ActivatedRoute, private router: Router, private alertService: AlertService, private oAuthService: OAuthService, private loaderService: LoaderService){
    // Check if user is logged in then navigate to user landing page.
    if(this.hasUserLoggedIn()) {
      this.router.navigate(['/app']);
    }

    this.passwordType = true;
    this.confPasswordType = true;
    this.declareResetPasswordForm();    //Declare form controls
    this.token = this.activeRoute.snapshot.queryParams['token'];
    this.userId = this.activeRoute.snapshot.queryParams['userId'];
  }

  /**
   * Method to declare reset password form controls
   */
  declareResetPasswordForm(): void{
    this.formResetePassword = this.fb.group({
      password: new FormControl('', [ Validators.required, Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[#?!@$%^&*-]).{8,15}$')]),
      confirmPassword: new FormControl('', Validators.required)
    },{
      validators: this.matchPassword.bind(this)
    });
  } 

  /**
   * Method to match password and confirm password
   * @param form get current formgroup as parameter
   */
  matchPassword(form: FormGroup): void | null {
    const password = form.controls['password'];
    const confirmPassword = form.controls['confirmPassword'];
    return password.value === confirmPassword.value ? null : confirmPassword.setErrors({ passwordNotMatch: true })
  }

  /**
   * Method to get all formcontrols for validations
  */
  get formError(){
    return this.formResetePassword.controls;
  }

  /**
   * Method to submit reset password form
   */
  submitResetPassword(): void{
    this.loaderService.showLoader();    // Show Loader
    if(this.formResetePassword.valid){
      const resetModel = new ResetPasswordDto();
      resetModel.userId = this.userId;
      resetModel.password = this.newPassword;
      resetModel.resetToken = this.token;

      this.accountService.resetPassword(resetModel).subscribe((response)=> {
        this.router.navigate(['/login']);
        this.alertService.success('You password has been reset. You can log in with new password.', false);
        this.loaderService.hideLoader();    // Hide Loader
      },(error)=>{
        this.alertService.error('Oops! something went wrong.', false);
        this.loaderService.hideLoader();    // Hide Loader
      })
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
