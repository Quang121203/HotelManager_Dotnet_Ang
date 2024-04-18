import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response, User } from '../models';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url: string = eviroments.url;
  accessToken: string;
  

  constructor(private http: HttpClient,
    private cookieService: CookieService
  ) {
    this.accessToken = cookieService.get('accessToken');
  }

  login(data: { email: string, password: string }): Observable<Response> {
    let api = `${this.url}Auth/Login`;
    return this.http.post<Response>(api, data);
  }

  getInfomation(): Observable<Response> {
    let api = `${this.url}Auth/GetInfomation`;
    return this.http.get<Response>(api);
  }

  refeshToken(refresh:string): Observable<Response> {
    let api = `${this.url}Auth/RefeshToken`;
    return this.http.post<Response>(api,'"'+refresh+'"');
  }

  changePassword(data:{oldPassword:string,newPassword:string}): Observable<Response> {
    let api = `${this.url}Auth/ChangePassword`;
    return this.http.post<Response>(api,data);
  }

  changeInfo(data: User): Observable<Response> {
    let api = `${this.url}Auth/ChangeInfo`;
    return this.http.post<Response>(api,data);
  }
}
