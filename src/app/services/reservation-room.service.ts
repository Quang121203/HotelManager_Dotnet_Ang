import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReservationRoomService {
  url: string = eviroments.url;
  constructor(private http:HttpClient) { }

  getReservationRoomByReservationId(id:string):Observable<Response>{
    let api = `${this.url}ReservationRoom/GetReservationRoomByReservationID?id=${id}`;
    return this.http.get<Response>(api);
  }
}
