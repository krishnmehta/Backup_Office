import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { createSlider } from '@typeform/embed';
import { EmbedPopup } from '@typeform/embed/types/base';
import { OnboardingDto } from '../../../_models/onboarding.model';
import { OnboardingService } from '../../../_services/onboarding.service';
import { EngagementService } from '../engagement/_services/engagement.service';
import {
  companyCompetencyMappings,
  CompetencyList,
  PersonalInfo,
} from './_models/personal-info.models';
import { PersonalInfoService } from './_services/personal-info.service';
@Component({
  selector: 'saturn-frontend-personal-info',
  templateUrl: './personal-info.component.html',
  styleUrls: ['./personal-info.component.scss', '../_styles/common-style.scss'],
})
export class PersonalInfoComponent {
  formPersonalInfo!: FormGroup;
  files!: FileList | null;
  currentFile!: File | null;
  previewImage: string | ArrayBuffer | null | undefined;

  isPersonalInfoSubmitted: boolean;
  isPersonalInfoView: boolean;
  isPersonalInfoSign: boolean;
  competencyList: Array<CompetencyList> = [];
  selectedComptency: Array<CompetencyList> = [];
  selectedCompetenciesId: number[] = [];
  personalDetails: PersonalInfo;
  isSaveClicked: boolean;
  uploadedImageNameResponse: string;
  uploadedPhotoName: string;

  embedSlider!: EmbedPopup | null;
  formId = '';
  isFormIdFetched = false;

  constructor(
    private fb: FormBuilder,
    private alertService: AlertService,
    private loaderService: LoaderService,
    private personalInfoService: PersonalInfoService,
    private onboardingServie: OnboardingService,
    private router: Router,
    private sanitizer: DomSanitizer
  ) {
    this.personalDetails = new PersonalInfo();
    this.isPersonalInfoSubmitted = false;
    this.isPersonalInfoView = true;
    this.isPersonalInfoSign = false;
    this.isSaveClicked = false;
    this.uploadedImageNameResponse = '';
    this.uploadedPhotoName = '';

    this.getCompetencyList(); // get list of competencies

    //this.declarePersonalInfoForm();
    this.showPersonalInfoForm();
  }

  /**
   * Get list of competencies
   */
  getCompetencyList(): void {
    this.loaderService.showLoader(); // Show Loader
    // call method to get list of competencies
    this.personalInfoService.getCompetencyList().subscribe(
      (response: CompetencyList[]) => {
        this.competencyList = response;
        this.loaderService.hideLoader();
      },
      (error) => {
        this.loaderService.hideLoader(); // Hide Loader
        this.alertService.error('Oops! something went wrong.', false);
      }
    );
  }

  /**
   * get saved personal details info
   */
  getSavedPersonalInfoDetails(): void {
    this.previewImage = '';
    this.personalInfoService.getPersonalInfo().subscribe(
      (response) => {
        // pending photo
        this.uploadedPhotoName = response.professionalPhotoUrl;
        this.previewImage = response.professionalPhotoUrl;

        // set company user type
        let selectedCompanyUserType;
        if (response.companyUserType === 0) {
          selectedCompanyUserType = 'Owner';
        } else {
          selectedCompanyUserType = 'Employee';
        }

        // fetch competency id from the response array of 'companyCompetencyMappings'
        this.selectedCompetenciesId = [];
        response.companyCompetencyMappings?.forEach(
          (element: companyCompetencyMappings) => {
            if (element.competencyId !== undefined) {
              this.selectedCompetenciesId.push(element.competencyId);
            }
          }
        );

        // get competency list by id from the list of competencies to bind with selected competency
        this.selectedComptency = [];
        for (let i = 0; i < this.selectedCompetenciesId.length; i++) {
          this.competencyList.forEach((competency: CompetencyList) => {
            if (this.selectedCompetenciesId[i] === competency.id) {
              this.selectedComptency.push({
                id: competency.id,
                title: competency.title,
              });
            }
          });
        }

        // set form values if user has added and saved the same
        this.formPersonalInfo.patchValue({
          firstName: response.firstName,
          lastName: response.lastName,
          email: response.email,
          contactNumber: response.phoneNumber,
          location: {
            address: response.streetLine,
            city: response.city,
            state: response.state,
          },
          employeeOwner: selectedCompanyUserType,
          summary: response.professionalSummary,
        });
        this.loaderService.hideLoader(); // Hide Loader
      },
      (error) => {
        this.loaderService.hideLoader(); // Hide Loader
        this.alertService.error('Oops! something went wrong.', false);
      }
    );
  }

  /**
   * get personal details info status to check if user hase saved the personal info previously or not
   */
  getPersonalInfoStatus(): void {
    this.loaderService.showLoader(); //Show Loader
    this.onboardingServie.getOnboardingStatus().subscribe(
      (response) => {
        if (response.personalInfoStatus) {
          this.isPersonalInfoView = false;
          this.isPersonalInfoSubmitted = response.engagementStatus;
          this.isPersonalInfoSign = response.engagementStatus;
        } else {
          this.isPersonalInfoView = true;
        }
        this.loaderService.hideLoader(); // Hide Loader
      },
      (error) => {
        this.loaderService.hideLoader(); // Hide Loader
        this.alertService.error('Oops! something went wrong.', false);
      }
    );
  }

  /**
   * Method to declare Personal Info Form controls
   */
  declarePersonalInfoForm(): void {
    this.formPersonalInfo = this.fb.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
        Validators.pattern('^[a-zA-Z]{1,20}$'),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
        Validators.pattern('^[a-zA-Z]{1,20}$'),
      ]),
      email: new FormControl('', Validators.required),
      location: this.fb.group({
        address: [''],
        city: [''],
        state: [''],
      }),
      contactNumber: new FormControl('', [
        Validators.required,
        Validators.pattern('^[+]{1}[0-9]{2}[0-9]{10}$'),
      ]),
      employeeOwner: new FormControl('Owner'),
      summary: new FormControl('', Validators.maxLength(300)),
      competency: new FormControl(''),
    });

    this.getSavedPersonalInfoDetails();
  }

  /**
   * Method to get all formcontrols validations
   */
  get formError() {
    return this.formPersonalInfo.controls;
  }

  /**
   * Method to submit Personal Information form
   */
  submitPersonalInfo(): void {
    this.loaderService.showLoader(); // Show Loader
    this.setPersonalInfoDetailsToObject();

    if (this.currentFile) {
      const formData: FormData = new FormData();
      formData.append('professionalPhoto', this.currentFile);
      this.personalInfoService.updloadProfilePhoto(formData).subscribe(
        (result: any) => {
          alert(result.imageName);
          // set the name of the image received in response to update object.
          this.uploadedImageNameResponse = result.imageName;
          this.personalDetails.ProfessionalPhotoName =
            this.uploadedImageNameResponse;

          this.updateProfileInfo();
          this.loaderService.hideLoader(); // Hide Loader
        },
        (error) => {
          this.loaderService.hideLoader(); // Hide Loader
          this.alertService.error('Oops! something went wrong.', false);
        }
      );
    } else {
      this.uploadedImageNameResponse = '';
      this.personalDetails.ProfessionalPhotoName = '';
      this.updateProfileInfo();
      this.loaderService.hideLoader(); // Hide Loader
    }
  }

  /**
   * Set personal details to object to send in backend to update the details
   */
  setPersonalInfoDetailsToObject(): void {
    this.personalDetails = new PersonalInfo();

    // First Name
    this.personalDetails.firstName =
      this.formPersonalInfo.get('firstName')?.value;
    // Last Name
    this.personalDetails.lastName =
      this.formPersonalInfo.get('lastName')?.value;
    // Email
    this.personalDetails.email = this.formPersonalInfo.get('email')?.value;
    // Address
    this.personalDetails.streetLine = this.formPersonalInfo.get([
      'location',
      'address',
    ])?.value;
    // City
    this.personalDetails.city = this.formPersonalInfo.get([
      'location',
      'city',
    ])?.value;
    // State
    this.personalDetails.state = this.formPersonalInfo.get([
      'location',
      'state',
    ])?.value;
    // Contact Number
    this.personalDetails.phoneNumber =
      this.formPersonalInfo.get('contactNumber')?.value;
    // Summary
    this.personalDetails.professionalSummary =
      this.formPersonalInfo.get('summary')?.value;

    // user type
    if (this.formPersonalInfo.get('employeeOwner')?.value === 'Owner') {
      this.personalDetails.companyUserType = 0;
    } else {
      this.personalDetails.companyUserType = 1;
    }

    // competency list by id
    const selectedCompetencyIds: number[] = [];
    this.formPersonalInfo
      .get('competency')
      ?.value.forEach((competency: CompetencyList) => {
        if (competency.id !== undefined) {
          selectedCompetencyIds.push(competency.id);
        }
      });

    this.personalDetails.competencyIds = selectedCompetencyIds;
  }

  /**
   * Update the profile info added by the user
   */
  updateProfileInfo(): void {
    this.personalInfoService.updatePersonalInfo(this.personalDetails).subscribe(
      (response) => {
        this.loaderService.hideLoader(); // Hide Loader
      },
      (error) => {
        this.loaderService.hideLoader(); // Hide Loader
        this.alertService.error('Oops! something went wrong.', false);
      }
    );

    // navigate if save & continue is clicked
    if (!this.isSaveClicked) {
      this.loaderService.hideLoader(); // Hide Loader
      this.router.navigate(['/app/onboarding/company-info'], {
        replaceUrl: true,
      });
    }
  }

  /**
   * Method to detect image upload
   * @param event get uploaded file value
   */
  selectFile(event: Event) {
    this.files = (event.target as HTMLInputElement).files;

    if (this.files) {
      this.currentFile = this.files.item(0);

      if (this.currentFile) {
        const reader = new FileReader();
        reader.readAsDataURL(this.currentFile);
        reader.onload = (event: Event) => {
          console.log((event.target as FileReader).result);
          this.previewImage = (event.target as FileReader).result;
        };

        this.formPersonalInfo.patchValue({
          profilePhoto: this.currentFile,
        });
      }
    }
  }

  /**
   * Method to handle form submit
   */
  handleTypeformSubmit(): void {
    this.loaderService.showLoader();
    this.alertService.success('Details submitted successfully', false);
    this.personalInfoService.setPersonalInfoStatus().subscribe(
      () => {
        this.loaderService.hideLoader();
        this.navigateToComapnyInfo();
      }
    );
    if(this.embedSlider != null)
    {
      this.embedSlider.close();
    }
  }

  /**
   * Method to show personal info form
   */
  showPersonalInfoForm(): void {
    this.loaderService.showLoader(); // Show Loader
    this.personalInfoService.getPersonalInfoFormLink().subscribe(
      (res) => {
        if (res.link != null && res.link != '') {
          this.loaderService.hideLoader();
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
   * Navigate to company info page
   */
  navigateToComapnyInfo(): void{
    this.router.navigate(['/app/onboarding/company-info'], {
      replaceUrl: true,
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
