import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { Observable } from 'rxjs';
import { EmbedReportDto, ProductListingDto } from '../_models/dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  apiUrl= '/api/app/dashboard/';

  constructor(private httpService: HttpService) { }

  /**
   * Method to get fetch list of products
   */
  getProductListing(): Observable<ProductListingDto[]>{
    return this.httpService.get(`${this.apiUrl}product-dropdown-details`);
  }

  /**
   * Method to get embed report
   */
  getEmbedReport(): Observable<EmbedReportDto>
  {
    return this.httpService.post(`${this.apiUrl}generate-embed-report`, {});
  }
}
