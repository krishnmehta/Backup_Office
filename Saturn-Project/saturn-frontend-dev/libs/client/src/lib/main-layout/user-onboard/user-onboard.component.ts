import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OnboardingService } from '../../_services/onboarding.service';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
@Component({
  selector: 'saturn-frontend-user-onboard',
  templateUrl: './user-onboard.component.html',
})
export class UserOnboardComponent {


  constructor(private router: Router, private onboardingService: OnboardingService, private alertService: AlertService, private loaderService: LoaderService) {
    this.navigateUser();    // Navigate user on component load
   }

  /**
   * Method to navigate user to respective onboarding step on component load
   * If step 1 is complete navigate user to step 2 (Personal Information)
   * If step 2 is complete navigate user to step 3 (Company Information)
   * If step 3 is complete navigate user to Dashboard 
   */
  navigateUser(): void {
    this.loaderService.showLoader();    // Show Loader
    
    this.onboardingService.getOnboardingStatus().subscribe((response) => {
      
      // step 1 (Engagement) = true, Navigate to Personal Information
      if(response.engagementStatus && !response.personalInfoStatus && !response.companyInfoStatus) {
        this.loaderService.hideLoader();    // Hide Loader
        this.router.navigate(['/app/onboarding/personal-info']);
      }

      // Below code is commented as implementation is pending after Microsoft Forms Integration
      // // step 2 (Personal Information) = true, Navigate to Company Information
      // else if(response.engagementStatus && response.personalInfoStatus && !response.companyInfoStatus) {
      //   this.loaderService.hideLoader();    // Hide Loader
      //   this.router.navigate(['/app/onboarding/company-info']);
      // }

      // // step 3 (Company Information) = true, Navigate to Dashboard
      // else if(response.engagementStatus && response.personalInfoStatus && response.companyInfoStatus){
      //   this.loaderService.hideLoader();    // Hide Loader
      //   this.router.navigate(['/app/dashboard']);
      // }

      //Navigate to Engagement if all steps are in-complete (pass return only)
      else {
        this.loaderService.hideLoader();    // Hide Loader
        this.router.navigate(['/app/onboarding/engagement']);
      }

    }, () => {
      this.loaderService.hideLoader();    // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    })
  }
}
