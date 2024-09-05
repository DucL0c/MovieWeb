import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map } from 'rxjs';
import { AuthenService } from './authen.service';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient, private authenService: AuthenService) {}

  // get(url: string) {
  //   return this.http.get(this.baseUrl + url);
  // }
  get(url: string) {
    let headers = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*')
      .delete('Authorization')
      .append(
        'Authorization',
        this.authenService.getLoggedInUser().token || ''
      );
    return this.http
      .get(this.baseUrl + url, { headers: headers })
      .pipe(map((extractData) => extractData));
  }
  // post(uri: string, data?: any) {
  //   return this.http.post(this.baseUrl + uri, data);
  // }
  // put(uri: string, data?: any) {
  //   return this.http.put(this.baseUrl + uri, data);
  // }
  post(uri: string, data?: any) {
    let headers = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*')
      .delete('Authorization')
      .append(
        'Authorization',
        this.authenService.getLoggedInUser().token || ''
      );
    return this.http
      .post(this.baseUrl + uri, data, { headers: headers })
      .pipe(map((extractData) => extractData));
  }
  put(uri: string, data?: any) {
    let headers = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('responseType', 'text')
      .set('Access-Control-Allow-Origin', '*')
      .delete('Authorization')
      .append(
        'Authorization',
        this.authenService.getLoggedInUser().token || ''
      );
    return this.http
      .put(this.baseUrl + uri, data, { headers: headers })
      .pipe(map((extractData) => extractData));
  }
}
