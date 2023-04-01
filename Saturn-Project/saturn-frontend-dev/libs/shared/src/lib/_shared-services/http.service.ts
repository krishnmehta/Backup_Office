import { throwError as observableThrowError, Observable } from 'rxjs';

import { map } from 'rxjs/operators';
import { Inject, Injectable } from '@angular/core';
import {
  HttpClient,
  HttpRequest,
  HttpHeaders,
  HttpResponse,
  HttpEvent,
} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private headers: any;
  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string
  ) {
    //Prevent request caching for internet explorer
  }

  testGet(url: string) {
    alert(`${this.baseUrl}${url}`);
  }

  get(url: string): Observable<any> {
    this.headers = new HttpHeaders({
      'Cache-control': 'no-cache; no-store',
      Pragma: 'no-cache',
      Expires: '0',
      'Content-Type': 'application/json; charset=utf-8',
      Authorization : `Bearer ${sessionStorage.getItem('msal.accessToken')}`
    });
    return this.httpClient
      .get(`${this.baseUrl}${url}`, {
        headers: this.headers,
        observe: 'response',
      })
      .pipe(
        map((res) => {
          // If request fails, throw an Error that will be caught
          if (res.status === 400) {
            throw new Error('This request has failed ' + res.status);
          }
          // If everything went fine, return the response
          else {
            return res.body;
          }
        })
      );
  }

  post(url: string, body: any): Observable<any> {
    this.headers = new HttpHeaders({
      'Cache-control': 'no-cache; no-store',
      Pragma: 'no-cache',
      Expires: '0',
      'Content-Type': 'application/json; charset=utf-8',
      Authorization : `Bearer ${sessionStorage.getItem('msal.accessToken')}`
    });
    const jsonBody = JSON.stringify(body);

    return this.httpClient
      .post(`${this.baseUrl}${url}`, jsonBody, {
        headers: this.headers,
        observe: 'response',
      })
      .pipe(
        map((res) => {
          switch (res.status) {
            case 200:
              return res.body;
            case 204:
              return {};
            case 404:
              return observableThrowError('unauthorized');
            default:
              throw new Error('This request has failed ' + res.status);
          }
        })
      );
  }

  put(url: string, body: any): Observable<any> {
    this.headers = new HttpHeaders({
      'Cache-control': 'no-cache; no-store',
      Pragma: 'no-cache',
      Expires: '0',
      'Content-Type': 'application/json; charset=utf-8',
      Authorization : `Bearer ${sessionStorage.getItem('msal.accessToken')}`
    });
    const jsonBody = JSON.stringify(body);

    return this.httpClient
      .put(`${this.baseUrl}${url}`, jsonBody, {
        headers: this.headers,
        observe: 'response',
      })
      .pipe(map((res) => res.body));
  }

  delete(url: string): Observable<any> {
    this.headers = new HttpHeaders({
      'Cache-control': 'no-cache; no-store',
      Pragma: 'no-cache',
      Expires: '0',
      'Content-Type': 'application/json; charset=utf-8',
      Authorization : `Bearer ${sessionStorage.getItem('msal.accessToken')}`
    });
    return this.httpClient
      .delete(`${this.baseUrl}${url}`, {
        headers: this.headers,
        observe: 'response',
      })
      .pipe(map((res) => res.body));
  }

  request(method: string, url: string, formData: FormData) {
    const req = new HttpRequest(method, `${this.baseUrl}${url}`, formData, {
      headers: new HttpHeaders({}),
      withCredentials: true,
    });
    return this.httpClient.request(req).pipe(
      map((res) => {
        if (res instanceof HttpResponse) {
          if (res['status'] === 200) {
            return res['body'];
          }
        }
        return res;
      })
    );
  }

  /**
   * Genric based http client request
   * @param method Http verbs name
   * @param url Url
   * @param formData Data to send
   */
  req<T>(
    method: string,
    url: string,
    formData: FormData
  ): Observable<HttpEvent<T>> {
    const req = new HttpRequest(method, `${this.baseUrl}${url}`, formData, {
      headers: new HttpHeaders({}),
    });
    return this.httpClient.request(req);
  }

  /**
   * Method to send post request with formData
   * @param url url for post request
   * @param body formdata to send in request
   * @returns Observable
   */
  postFormData(url: string, body: FormData): Observable<any> {
    return this.httpClient
      .post(`${this.baseUrl}${url}`, body, {
        observe: 'response',
      })
      .pipe(
        map((res) => {
          switch (res.status) {
            case 200:
              return res.body;
            case 204:
              return {};
            case 404:
              return observableThrowError('unauthorized');
            default:
              throw new Error('This request has failed ' + res.status);
          }
        })
      );
  }
}
