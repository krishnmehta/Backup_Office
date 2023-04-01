import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { Observable } from 'rxjs';
import { FormlinkDto } from '../_models/formlink.model';
import {
  CompetencyList,
  PersonalInfo,
} from './../_models/personal-info.models';
@Injectable({
  providedIn: 'root',
})
export class PersonalInfoService {
  apiUrl = '/api/app/business-user/';
  constructor(private httpService: HttpService) {}

  /**
   * Method to get list of competencies
   */
  getCompetencyList(): Observable<CompetencyList[]> {
    return this.httpService.get(`${this.apiUrl}competency`);
  }

  updatePersonalInfo(personalDetails: PersonalInfo): Observable<PersonalInfo> {
    return this.httpService.put(`${this.apiUrl}personal-info`, personalDetails);
  }

  getPersonalInfo(): Observable<PersonalInfo> {
    return this.httpService.get(`${this.apiUrl}personal-info`);
  }

  public updloadProfilePhoto(formData: FormData) {
    return this.httpService.postFormData(
      `${this.apiUrl}upload-professional-photo`,
      formData
    );
  }

  /**
   * Method to get personal info form link
   */
  getPersonalInfoFormLink() : Observable<FormlinkDto>
  {
    return this.httpService.get(`${this.apiUrl}personal-info-form-link`);
  }

  /**
   * Method to set personal info status
   */
  setPersonalInfoStatus() : Observable<any>
  {
    return this.httpService.post(`${this.apiUrl}set-personal-info-status-submitted`, {});
  }
}
