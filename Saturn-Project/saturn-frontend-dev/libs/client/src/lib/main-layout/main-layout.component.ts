import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import { OnboardingService } from '../_services/onboarding.service';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { MsalService } from '@azure/msal-angular';
@Component({
  selector: 'saturn-frontend-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss'],
})
export class MainLayoutComponent {
  
  isContentCollapse: boolean;     // Content collapse or expand boolean

  constructor(private oAuthService: OAuthService, private router: Router, private onboardingService: OnboardingService, private alertService: AlertService, private loaderService: LoaderService,private msalService:MsalService){
    // if(!this.isUserLoggedIn())
    // {
    //   this.loginLinkText="Login";
    // }
    this.isContentCollapse = false;

    // Check if user is not logged in then navigate to login page.
    if (!this.hasUserLoggedIn() && !this.isUserLoggedIn() ) {
      this.router.navigate(['/login']);
    }
    
    this.navigateUserOnBoardingStatus();    // Navigate user on component load
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
   * Method to check if the user logged in
   * @returns Returns true if the user is logged in else false
   */
  hasUserLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  /**
   * Method to navigate user based on boarding process status on component load
   * Get onboarding steps staus: If all steps true then navigate to dashboard otherwise on onboarding component
   */
  navigateUserOnBoardingStatus(): void {
    this.loaderService.showLoader();    // Show Loader
    this.onboardingService.getOnboardingStatus().subscribe((response)=> {
      if(!response.engagementStatus || !response.personalInfoStatus || !response.companyInfoStatus) {
      //if(!response.engagementStatus) {
        this.loaderService.hideLoader();    // Hide Loader
        this.router.navigate(['/app/onboarding']);
      }
      this.loaderService.hideLoader();    // Hide Loader
    }, () => {
      this.loaderService.hideLoader();    // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    });
  }
}
