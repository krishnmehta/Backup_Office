import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DiagnoseService } from '../../_services/diagnose.service';
import { QuestionnaireDto } from '../../_models/diagnose.model';
import { createSlider } from '@typeform/embed'
import { LoaderService, AlertService } from '@saturn-frontend/shared';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'saturn-frontend-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss', '../_styles/common-style.scss'],
})
export class QuestionnaireComponent {
  productId: string;
  questionnaire = new QuestionnaireDto();
  typeFormId!: string;
  isFormIdFetched = false;

  constructor(private diagnoseService: DiagnoseService, private activatedRoute: ActivatedRoute, private loaderService: LoaderService, private alertService: AlertService,
    private sanitizer: DomSanitizer) {
    this.productId = this.activatedRoute.snapshot.params['id'];
    this.getQuestionnaireDetail();
  }


  /**
   * Method to get details of product's questionnaire 
   */
  getQuestionnaireDetail(): void{
    this.loaderService.showLoader();   // Show Loader

    this.diagnoseService.getQuestionnaireById(this.productId).subscribe((response) => {
      this.questionnaire = response;
      this.typeFormId = response.typeformLink
      this.loaderService.hideLoader();    // Hide Loader
    }, (error) => {
      this.alertService.error('Oops! something went wrong.', false);
      this.loaderService.hideLoader();
    });
  }


  /**
   * Method to show typeForm as popup
   */
  showTypeForm(): void {
    this.isFormIdFetched = true;
    // createSlider(this.typeFormId).open();
  }

  /**
   * Method to return safe resource url for form
   */
  getFormSrc() : SafeResourceUrl
  {
    return this.sanitizer.bypassSecurityTrustResourceUrl(`https://forms.office.com/Pages/ResponsePage.aspx?id=${this.typeFormId}&embed=true`);
  }
}
