import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { createSlider } from '@typeform/embed';
import { EmbedPopup } from '@typeform/embed/types/base';
import { OnboardingDto } from '../../../_models/onboarding.model';
import { OnboardingService } from '../../../_services/onboarding.service';
import { CompanyInformtionDto, BusinessActivityDto, PrimaryCustomer, PrimaryIndustry, SecondaryIndustry } from './_model/company-info.model';
import { CompanyInfoService } from './_services/company-info.service';

@Component({
  selector: 'saturn-frontend-company-info',
  templateUrl: './company-info.component.html',
  styleUrls: ['./company-info.component.scss', '../_styles/common-style.scss'],
})
export class CompanyInfoComponent {

  previewImage!: string | ArrayBuffer | null;
  formCompanyInfo!: FormGroup;
  businessBriefModel = '';
  businessActivitySelected: string | undefined;
  primaryIndustrySelected: string | undefined;
  secondaryIndustrySelected: string | undefined;
  primaryCustomerSelected: string | undefined;
  companyInformation = new CompanyInformtionDto();
  businessActivityList = new Array<BusinessActivityDto>();
  primaryIndustryList = new Array<PrimaryIndustry>();
  secondaryIndustryList = new Array<SecondaryIndustry>();
  primaryCustomerList = new Array<PrimaryCustomer>();

  embedSlider!: EmbedPopup | null;
  formId = '';
  isFormIdFetched = false;

  constructor(private fb: FormBuilder,
    private alertService: AlertService,
    private loaderService: LoaderService,
    private companyInfoService: CompanyInfoService,
    private onboardingServie: OnboardingService,
    private router: Router,
    private sanitizer: DomSanitizer) {
    this.businessActivitySelected = 'Select';
    this.primaryIndustrySelected = 'Select';
    this.secondaryIndustrySelected = 'Select';
    this.primaryCustomerSelected = 'Select';

    this.businessActivityList = [
      { id: 1, name: 'Manufacturing' },
      { id: 2, name: 'Trading (Coming Soon)', disabled: true },
      { id: 3, name: 'Services (Coming Soon)', disabled: true },
    ];

    this.primaryIndustryList = [
      { id: 1, name: 'Agriculture' },
      { id: 2, name: 'Industrial products' }
    ];

    this.secondaryIndustryList = [
      { id: 1, name: 'Fertilizer' },
      { id: 2, name: 'Vegetables' }
    ];

    this.primaryCustomerList = [
      { id: 1, name: 'Businesses' },
      { id: 2, name: 'Consumers (Coming Soon)', disabled: true },
      { id: 2, name: 'Government Institutes (Coming Soon)', disabled: true }
    ];

    this.declareCompanyInfoForm();      // Declare company info form
    this.showCompanyInfoForm();
  }

  /**
   * Method to declare Company Info Form Controls
   */
  declareCompanyInfoForm(): void {
    this.formCompanyInfo = this.fb.group({
      businessBrief: new FormControl('', Validators.required),
      companyWebsite: new FormControl('', Validators.pattern('(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?')),
      businessActivity: new FormControl('', Validators.required),
      primaryIndustry: new FormControl('', Validators.required),
      secondaryIndustry: new FormControl('', Validators.required),
      primaryCustomer: new FormControl('', Validators.required),
    });
  }

  /**
   * Method to get all formcontrols validations
   */
  get formError() {
    return this.formCompanyInfo.controls;
  }


  /**
   * Method to select image upload
   * @param event get uploaded file value
   */
  selectCompanyLogo(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    if (files) {
      const currentFile = files.item(0)
      if (currentFile) {
        const reader = new FileReader();
        reader.readAsDataURL(currentFile);
        reader.onload = (event: Event) => {
          this.previewImage = (event.target as FileReader).result;
        }
      }
    }
  }

  /**
   * Method to get and set Business Activity value
   * Set value to ngModel and formcontrol for validation
   */
  setBusinessActivityValue(value: string): void {
    this.companyInformation.businessActivity = value;
    this.formCompanyInfo.controls['businessActivity'].patchValue(value);
  }

  /**
   * Method to get and set Business Activity value
   * Set value to ngModel and formcontrol for validation
   */
  setPrimaryIndustryValue(value: string): void {
    this.companyInformation.primaryIndustry = value;
    this.formCompanyInfo.controls['primaryIndustry'].patchValue(value);
  }

  /**
   * Method to get and set Business Activity value
   * Set value to ngModel and formcontrol for validation
   */
  setSecondaryIndustryValue(value: string): void {
    this.companyInformation.secondaryIndustry = value;
    this.formCompanyInfo.controls['secondaryIndustry'].patchValue(value);
  }

  /**
   * Method to get and set Business Activity value
   * Set value to ngModel and formcontrol for validation
   */
  setPrimaryCustomerValue(value: string): void {
    this.companyInformation.primaryCustomer = value;
    this.formCompanyInfo.controls['primaryCustomer'].patchValue(value);
  }

  /**
   * Method to submit Company Information Form
   */
  submitCompanyProfile(): void {
    if (this.formCompanyInfo.valid) {
      alert();
    }
    else {
      return;
    }
  }

  /**
   * Method to handle form submit event
   */
  handleTypeformSubmit(): void {
    this.loaderService.showLoader();
    this.alertService.success('Details submitted successfully', false);
    this.companyInfoService.setCompanyInfoFormLink().subscribe(
      () => {
        this.loaderService.hideLoader();
        this.navigateToDashboard();
      }
    );
    if(this.embedSlider != null)
    {
      this.embedSlider.close();
    }
  }

  /**
   * Method to show company info form
   */
  showCompanyInfoForm(): void {
    this.loaderService.showLoader(); // Show Loader
    this.companyInfoService.getCompanyInfoFormLink().subscribe(
      (res) => {
        if (res.link != null && res.link != '') {
          this.loaderService.hideLoader();  //Hide Loader
          this.formId = res.link;
          this.isFormIdFetched = true;
          // this.embedSlider = createSlider(res.link,
          //   {
          //     onSubmit: ({ responseId }) => {
          //       this.handleTypeformSubmit();
          //     }
          //   });
          // this.embedSlider.open();
        }
        else {
          this.loaderService.hideLoader(); // Hide Loader
          this.alertService.error('Oops! something went wrong.', false);
        }
      },
      () => {
        this.loaderService.hideLoader(); // Hide Loader
        this.alertService.error('Oops! something went wrong.', false);
      }
    );
  }

  /**
   * Method to navigate to dashboard page
   */
  navigateToDashboard(): void {
    const engagementStatus = new OnboardingDto();
    engagementStatus.companyInfoStatus = true;
    engagementStatus.personalInfoStatus = true;
    engagementStatus.engagementStatus = true;
    this.onboardingServie.setEngagementStaus(engagementStatus);
    this.router.navigate(['/app'], {
      replaceUrl: true,
    }).then(() => {
      //window.location.reload();
    });
  }

  /**
   * Method to return safe resource url for form
   */
  getFormSrc() : SafeResourceUrl
  {
    return this.sanitizer.bypassSecurityTrustResourceUrl(`https://forms.office.com/Pages/ResponsePage.aspx?id=${this.formId}&embed=true`);
  }
}
