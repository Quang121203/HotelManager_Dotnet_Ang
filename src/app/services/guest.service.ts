import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GuestService {
  url: string = eviroments.url;

  constructor(private http: HttpClient) { }

  getGuestByRoomId(roomId: string):Observable<Response>{
    let api = `${this.url}Guest/GetGuestByRoom?RoomId=${roomId}`
    return this.http.get<Response>(api)
  }

  getGuestById(id: string):Observable<Response>{
    let api = `${this.url}Guest/${id}`
    return this.http.get<Response>(api)
  }
}
