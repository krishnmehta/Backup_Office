import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { OnboardingDto } from '../_models/onboarding.model';
import { HttpService } from '@saturn-frontend/shared';
@Injectable({
  providedIn: 'root',
})
export class OnboardingService {
  subjectStepStatus = new Subject<OnboardingDto>();
  apiUrl = '/api/app/business-user/';

  constructor(private httpService: HttpService) {}

  /**
   * Method to set onboard steps status(Sign engagement, Personal info, Company info)
   * @param status get true/false value of individual step
   */
  setEngagementStaus(data: OnboardingDto): void {
    this.subjectStepStatus.next(data);
  }

  /**
   * Method to Observe the Subject subjectStepStatus
   */
  getStatus(): Observable<OnboardingDto> {
    this.getOnboardingStatus().subscribe((response) => {
      this.setEngagementStaus(response);
    });
    return this.subjectStepStatus.asObservable();
  }

  /**
   * Method to get status of onboarding steps status from server
   */
  getOnboardingStatus(): Observable<OnboardingDto> {
    return this.httpService.get(`${this.apiUrl}onboarding-status`);
  }
}
