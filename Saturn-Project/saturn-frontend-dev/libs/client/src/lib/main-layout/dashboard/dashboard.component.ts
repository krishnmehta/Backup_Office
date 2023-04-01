import { Component } from '@angular/core';
import { ProductListingDto } from './_models/dashboard.model';
import { DashboardService } from './_services/dashboard.service';
import { AlertService, LoaderService } from '@saturn-frontend/shared';
import { BackgroundType, IReportEmbedConfiguration, TokenType } from 'powerbi-models';
@Component({
  selector: 'saturn-frontend-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {

  productListing = new Array<ProductListingDto>();
  currentOptionValue: string;
  embedReportConfig: IReportEmbedConfiguration;
  isReportAvailable = false;

  constructor(private dashboardService: DashboardService, private alertService: AlertService, private loaderService: LoaderService) {
    this.embedReportConfig = {}; // Initialization

    this.currentOptionValue = 'Strategic Insights';    // Default Selected Option for dropdown 

    this.getProductDropdownList();    // Get dropdown list

    this.getEmbedReport();    //Get embed report
  }

  /**
   * Method to get current selection and show/hide product sections
   * @param event get current selected value
   */
  getCurrentSelection(value: string): void {
    this.currentOptionValue = value;
  }

  /**
   * Method to get dropdown listing
   */
  getProductDropdownList(): void {
    this.loaderService.showLoader();    // Show Loader
    this.dashboardService.getProductListing().subscribe((response) => {
      this.productListing = response;
      this.loaderService.hideLoader();    // Hide Loader
    }, (error) => {
      this.loaderService.hideLoader();    // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    });
  }

  /**
   * Method to get embed report
   */
  getEmbedReport(): void {
    this.loaderService.showLoader();    // Show Loader
    this.dashboardService.getEmbedReport().subscribe((response) => {
      if (response != null) {
        this.embedReportConfig = {
          type: "report",
          id: response.reportId,
          embedUrl: response.embedUrl,
          accessToken: response.embedToken,
          tokenType: TokenType.Embed,
          settings: {
            panes: {
              filters: {
                expanded: false,
                visible: true
              }
            },
            background: BackgroundType.Transparent,
          }
        } as IReportEmbedConfiguration;
        this.isReportAvailable = true;
      }
      else
      {
        this.isReportAvailable = false;
        this.alertService.error('Oops! something went wrong.', false);  
      }
      this.loaderService.hideLoader();    // Hide Loader
    }, (error) => {
      this.isReportAvailable = false;
      this.loaderService.hideLoader();    // Hide Loader
      this.alertService.error('Oops! something went wrong.', false);
    });
  }
}
