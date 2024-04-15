import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response,User} from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url: string = eviroments.url;

  constructor(private http: HttpClient) { } 

  getUser(id: string): Observable<Response> {
    let api = `${this.url}User/GetUser/${id}`;
    return this.http.get<Response>(api);
  }

  getAllUser():Observable<Response>{
    let api = `${this.url}User/GetAllUser`;
    return this.http.get<Response>(api);
  }

  addUser(user:User):Observable<Response>{
    let api = `${this.url}User/CreateUser`;
    return this.http.post<Response>(api,user);
  }

  updateUser(user: User): Observable<Response> {
    let api = `${this.url}User/UpdateUser`;
    return this.http.put<Response>(api,user); 
  }

  deleteUser(id: string): Observable<Response> {
    let api = `${this.url}User/DeleteUser/${id}`;
    return this.http.delete<Response>(api); 
  }
}
