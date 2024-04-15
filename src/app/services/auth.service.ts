import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url: string = eviroments.url;

  constructor(private http: HttpClient) { }

  login(data:{email:string,password:string}):Observable<Response>{
    let api = `${this.url}Auth/Login`;
    return this.http.post<Response>(api,data);
  }
}
