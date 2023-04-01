import { Component } from '@angular/core';
import { DiagnoseService } from './_services/diagnose.service';
import { DiagnoseProductsDto } from './_models/diagnose.model';
import { LoaderService, AlertService } from '@saturn-frontend/shared';

@Component({
  selector: 'saturn-frontend-diagnose',
  templateUrl: './diagnose.component.html',
  styleUrls: ['./diagnose.component.scss'],
})
export class DiagnoseComponent {

  productListing = new Array<DiagnoseProductsDto>();
  constructor(private diagnoseService: DiagnoseService, private loaderService: LoaderService, private alertService: AlertService){ 
    this.getProductList();    // Get product list
  }
  

  /**
   * Method to get dignose products list
   */
  getProductList(): void{
    this.loaderService.showLoader();    // Show loader
    this.diagnoseService.getDiagnoseProducts().subscribe((response) => {
      this.productListing = response;
      this.loaderService.hideLoader();    // Hide loader
    }, (error) =>{
      this.alertService.error('Oops! something went wrong.', false);
      this.loaderService.hideLoader();    // Hide Loader
    });
  }
}
