import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { DiagnoseProductsDto, ProductDataPointListDto, QuestionnaireDto } from '../_models/diagnose.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DiagnoseService {

  apiUrl = "/api/app/business-insight/"
  questionnaireId: number | undefined;
  constructor(private httpService: HttpService) { }

  /**
   * Method to get Business Insights products listing
   */
  getDiagnoseProducts(): Observable<DiagnoseProductsDto[]> {
    return this.httpService.get(`${this.apiUrl}business-insight-product-details`);
  }

  /**
   * Method to get questionnaire details
   * @param id get id of questionnaire product
   */
  getQuestionnaireById(id: string): Observable<QuestionnaireDto> {
    return this.httpService.get(`${this.apiUrl}${id}/business-insight-product-view-details-by-id`);
  }
  
  /**
   * Method to get product data points with form link
   * @param id Id of product
   */
  getProductDataPointsWithFormLinkById(id: string): Observable<ProductDataPointListDto>
  {
    return this.httpService.get(`${this.apiUrl}${id}/product-data-points-with-form-link-by-id`);
  }
}
