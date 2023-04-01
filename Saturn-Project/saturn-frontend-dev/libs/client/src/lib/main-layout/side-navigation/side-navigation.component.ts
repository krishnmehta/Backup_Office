import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from '@abp/ng.core';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { OnboardingService } from '../../_services/onboarding.service';
import { MsalService } from '@azure/msal-angular';
@Component({
  selector: 'saturn-frontend-side-navigation',
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss'],
})
export class SideNavigationComponent {

  @Output() sideNavigationCollapseEvent = new EventEmitter<boolean>();

  isSideNavCollapse: boolean;       // Side navigation 
  isEngagementComplete: boolean;    // Engagement Step 
  isPersonalInfoComplete: boolean;  // Personal Info Step
  isCompanyInfoComplete: boolean;   // Company Info step
  isOnboardingStepVisible: boolean | undefined;   // Onboaring links or dashboard links

  constructor(private authService: AuthService, private alertService: AlertService, private onboardingService: OnboardingService, private loaderService: LoaderService,private msalService:MsalService){
    this.isSideNavCollapse = false;    
    this.isEngagementComplete = false;
    this.isPersonalInfoComplete = false;
    this.isCompanyInfoComplete = false;

    this.setOnboardingStepStatus();   // Get Onboard step status
  }

  /**
   * Method to collapse and expand side navigation
   */
  collapseSideNavigation(): void{
    this.isSideNavCollapse = !this.isSideNavCollapse;
    this.sideNavigationCollapseEvent.emit(this.isSideNavCollapse);
  }

  /**
   * Method to logout user from the application
   */
  logout(): void {
    if(this.msalService.instance.getActiveAccount()!=null)
    {
      this.msalService.logout();
    }
    else{
      this.authService.logout().subscribe((response) => {
        this.loaderService.hideLoader();  // Hide loader
      },() =>{
        this.loaderService.hideLoader();  // Hide loader
        this.alertService.error('Oops! something went wrong.', false);
      });
    }
    this.loaderService.showLoader();    // Show loader
   
  }

  /**
   * Method to get onboarding steps status
   */
  setOnboardingStepStatus(): void{
    this.onboardingService.getStatus().subscribe((data) => {
      if(data.engagementStatus){
        this.isEngagementComplete = data.engagementStatus;
      }
      if(data.personalInfoStatus){
        this.isPersonalInfoComplete = data.personalInfoStatus;
      }
      if(data.companyInfoStatus){
        this.isCompanyInfoComplete = data.companyInfoStatus;
      }
      if(!data.engagementStatus || !data.personalInfoStatus || !data.companyInfoStatus){
      //if(!data.engagementStatus){
        this.isOnboardingStepVisible = true;
      }
      else {
        this.isOnboardingStepVisible = false;
      }
    });
  }
}
