import { Injectable } from '@angular/core';
import { HttpService } from '@saturn-frontend/shared';
import { Observable } from 'rxjs';
import { LoginModel, LoginResponse } from '../_models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AccountLoginService {

  accountLoginServiceUrl= '/api/account/';
  
  constructor(private httpService: HttpService) { }

  /**
   * Method to login user into application
   * @param loginData pass login data as parameter
   */
  loginAccount(loginData: LoginModel): Observable<LoginResponse>{
    return this.httpService.post(`${this.accountLoginServiceUrl}login`, loginData);
  }
}
