import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { Observable } from 'rxjs';
import { FormlinkDto } from '../_model/formlink.model';
@Injectable({
    providedIn: 'root',
})
export class CompanyInfoService {
    apiUrl = '/api/app/business-user/';
    constructor(private httpService: HttpService) { }

    /**
     * Method to get company info form link
     */
    getCompanyInfoFormLink(): Observable<FormlinkDto> {
        return this.httpService.get(`${this.apiUrl}company-info-form-link`);
    }

    /**
     * Method to set company info status
     */
    setCompanyInfoFormLink(): Observable<any> {
        return this.httpService.post(`${this.apiUrl}set-company-info-status-submitted`, {});
    }
}
