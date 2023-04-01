import { Component, ElementRef, ViewChild } from '@angular/core';
import { OnboardingService } from '../../../_services/onboarding.service';
import { OnboardingDto } from '../../../_models/onboarding.model';
import { Router } from '@angular/router';
import { EngagementService } from './_services/engagement.service';
import jsPdf from 'jspdf';
import { AlertService, LoaderService } from '@saturn-frontend/shared';

@Component({
  selector: 'saturn-frontend-engagement',
  templateUrl: './engagement.component.html',
  styleUrls: ['./engagement.component.scss'],
})
export class EngagementComponent {
  @ViewChild('engagementNdaPDF', { static: false}) pdfElem!: ElementRef;

  isAcceptNextDisable: boolean;   // Accept and Visble button (default disabled)
  isEngagementView: boolean;      // Engagement View Area
  isEngagementSign: boolean;      // Engagement Sign Area
  isEngagementSubmitted: boolean;
  ndaUserName: string | undefined;      // Logged in user name
  ndaCompanyName: string | undefined    // Logged in user company name
  ndaSignDate: Date | undefined;        // Current date

  constructor(private onboardingServie: OnboardingService, private router: Router, private engagementService: EngagementService, private alertService: AlertService, private loaderService: LoaderService){
    this.isAcceptNextDisable = false;
    this.isEngagementView = false;
    this.isEngagementSign = false;
    this.isEngagementSubmitted = false;
    this.getEngagementStatus();     // Get Engagement Status
    this.getEngagementDetails();    // Get logged-in user details
  }

  /**
   * Method to show/hide next step (Engagement Sign NDA)
   */
  submitEngagementView(): void{
    if(this.isAcceptNextDisable === true){
      this.isEngagementSign = true;
      this.isEngagementView = false;
      window.scrollTo({
        top: 0
      })
    }
  }

  /**
   * Method finish engagement & NDA and set engagement step status true.
   * Redirect user to personal info screen
   */
  finishEngagementNda(): void {
    this.loaderService.showLoader();    // Show Loader
    this.engagementService.signEngagementNda().subscribe(() => {
      const data = new OnboardingDto();
      data.engagementStatus = true;
      this.onboardingServie.setEngagementStaus(data);
      this.loaderService.hideLoader();    // Hide Loader
      this.router.navigate(['/app/onboarding/personal-info'], { replaceUrl: true});
      //this.router.navigate(['/app/dashboard'], { replaceUrl: true});
    },(error)=> {
      this.loaderService.hideLoader();    // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    });
  }

  /**
   * Method to create PDF from HTML and Downlaod PDF
   */
  downloadPdf(): void {
    const pdf = new jsPdf({ orientation: 'p', unit: 'pt', format: [595, 842], });
    pdf.html(this.pdfElem.nativeElement,{
      margin: [0, 0, 0, 0],
      
      callback: (pdf) => {
        pdf.save("NDA.pdf");
      }
    })
  }

  /**
   * Method to get logged in user name and company name for engagement letter
   */
  getEngagementDetails(){
    this.loaderService.showLoader();  //Show Loader
    this.engagementService.getUserDetails().subscribe((response) => {
      this.ndaUserName = response.name;
      this.ndaCompanyName = response.companyName;
      if(response.ndaSignDate !== '' && response.ndaSignDate !== null) {
        this.ndaSignDate =  new Date(response.ndaSignDate);
      }
      else {
        this.ndaSignDate =  new Date();
      }
      
      this.loaderService.hideLoader();  // Hide Loader
    }, (error) => {
      this.loaderService.hideLoader();  // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    });
  }

  /**
   * Method to get Engagement Step Status
   */
  getEngagementStatus(){    
    this.loaderService.showLoader();  //Show Loader
    this.onboardingServie.getOnboardingStatus().subscribe((response) => {
      if(response.engagementStatus) {
        this.isEngagementView = false;
        this.isEngagementSubmitted = response.engagementStatus;
        this.isEngagementSign = response.engagementStatus;
      }
      else {
        this.isEngagementView = true;
      }
      this.loaderService.hideLoader();  // Hide Loader
    }, (error) => {
      this.loaderService.hideLoader();  // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    });
  }
}
