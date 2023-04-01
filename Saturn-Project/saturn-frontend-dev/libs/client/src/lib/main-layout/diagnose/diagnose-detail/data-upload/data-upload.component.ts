import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { createSlider } from '@typeform/embed';
import { EmbedPopup } from '@typeform/embed/types/base';
import { ProductDataPointListDto } from '../../_models/diagnose.model';
import { DiagnoseService } from '../../_services/diagnose.service';

@Component({
  selector: 'saturn-frontend-data-upload',
  templateUrl: './data-upload.component.html',
  styleUrls: ['./data-upload.component.scss', '../_styles/common-style.scss'],
})
export class DataUploadComponent {

  productId: string;
  productDataPointObj!: ProductDataPointListDto;
  embedSlider!: EmbedPopup | null;
  isFormIdFetched = false;

  constructor(private diagnoseService: DiagnoseService, private activatedRoute: ActivatedRoute, private loaderService: LoaderService, private alertService: AlertService,
    private sanitizer: DomSanitizer) {
    this.productId = this.activatedRoute.snapshot.params['id'];
    this.productDataPointObj = new ProductDataPointListDto();
    this.productDataPointObj.productDataUploadPoints = [];
    this.getProductDataPointDetailsWithFormLink();
  }

  /**
   * Method to get product data point details with form link
   */
  getProductDataPointDetailsWithFormLink() {
    this.loaderService.showLoader();   // Show Loader
    this.diagnoseService.getProductDataPointsWithFormLinkById(this.productId).subscribe(
      (res) => {
        this.productDataPointObj = res;
        if (this.productDataPointObj.productDataUploadPoints === undefined || this.productDataPointObj.productDataUploadPoints === null || this.productDataPointObj.productDataUploadPoints.length === 0) {
          this.productDataPointObj.productDataUploadPoints = [];
        }
        this.loaderService.hideLoader();    // Hide Loader
      },
      () => {
        this.alertService.error('Oops! something went wrong.', false);
        this.loaderService.hideLoader();    // Hide Loader
      }
    );
  }

  /**
   * This method is used to show data upload form.
   */
  showDataUploadForm(): void {
    if (this.productDataPointObj.dataUploadTypeformLink != null && this.productDataPointObj.dataUploadTypeformLink != '') {
      this.loaderService.hideLoader();
      this.isFormIdFetched = true;
      // this.embedSlider = createSlider(this.productDataPointObj.dataUploadTypeformLink,
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
  }

  /**
   * This method is used to show success message and close the form.
   */
  handleTypeformSubmit(): void {
    this.alertService.success('Form submitted successfully', false);
    if (this.embedSlider != null) {
      this.embedSlider.close();
    }
  }

  /**
   * Method to download data upload template
   * @param templateLink link of template
   */
  downloadTemplate(templateLink: string): void {
    window.open(`${templateLink}&download=1`, "_blank");
  }

  /**
   * Method to return safe resource url for form
   */
  getFormSrc() : SafeResourceUrl
  {
    return this.sanitizer.bypassSecurityTrustResourceUrl(`https://forms.office.com/Pages/ResponsePage.aspx?id=${this.productDataPointObj.dataUploadTypeformLink}&embed=true`);
  }
}
