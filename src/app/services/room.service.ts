import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response,Room,Booking} from '../models';

@Injectable({
  providedIn: 'root'
})
export class RoomService{
  url: string = eviroments.url;

  constructor(private http: HttpClient) { }

  getAllRooms(): Observable<Response> {
    let api = `${this.url}Room/GetAllRoom`;
    return this.http.get<Response>(api); 
  }

  getRoomByRoomTypeID(id:string): Observable<Response> {
    let api = `${this.url}Room/GetRoomByRoomTypeId?roomTypeId=${id}`;
    return this.http.get<Response>(api); 
  }

  updateRoom(room: Room): Observable<Response> {
    let api = `${this.url}Room/UpdateRoom`;
    return this.http.put<Response>(api,room); 
  }

  addRoom(room: Room): Observable<Response> {
    let api = `${this.url}Room/AddRoom`;
    return this.http.post<Response>(api,room); 
  }

  deleteRoom(id: string): Observable<Response> {
    let api = `${this.url}Room/DeleteRoom/${id}`;
    return this.http.delete<Response>(api); 
  }

  getRoomById(id:string):Observable<Response>{
    let api = `${this.url}Room/GetRoom/${id}`;
    return this.http.get<Response>(api);
  }

  public getRoomNotServe(booking:Booking):Observable<Room[]>{
    let api = `${this.url}Room/GetRoomNotReser`;
    return this.http.post<Room[]>(api, booking);
  }

}
