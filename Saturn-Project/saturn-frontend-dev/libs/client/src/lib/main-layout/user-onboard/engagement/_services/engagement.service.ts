import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { Observable } from 'rxjs';
import { EngagementDto } from '../_models/engagement.models';

@Injectable({
  providedIn: 'root'
})
export class EngagementService {

  apiUrl = '/api/app/business-user/';
  constructor(private httpService: HttpService) { }

  /**
   * Method to get logged-in user details
   */
  getUserDetails(): Observable<EngagementDto>{
    return this.httpService.get(`${this.apiUrl}nda-details`);
  }

  /**
   * Method to send post request
   */
  signEngagementNda():Observable<boolean> {
    return this.httpService.post(`${this.apiUrl}sign-nda`,{});
  }
}
