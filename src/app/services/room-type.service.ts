import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { RoomType,Response } from '../models';

@Injectable({
  providedIn: 'root'
})
export class RoomTypeService {

  url: string = eviroments.url;

  constructor(private http: HttpClient) { }

  getRoomType(id: string): Observable<RoomType> {
    let api = `${this.url}RoomType/GetRoomType/${id}`;
    return this.http.get<RoomType>(api);
  }

  getAllRoomTypes():Observable<Response>{
    let api = `${this.url}RoomType/GetAllRoomType`;
    return this.http.get<Response>(api);
  }

  addRoomType(roomType:RoomType):Observable<Response>{
    let api = `${this.url}RoomType/CreateRoomType`;
    return this.http.post<Response>(api,roomType);
  }

  updateRoomType(roomType: RoomType): Observable<Response> {
    let api = `${this.url}RoomType/UpdateRoomType`;
    return this.http.put<Response>(api,roomType); 
  }

  deleteRoomType(id: string): Observable<Response> {
    let api = `${this.url}RoomType/DeleteRoomType/${id}`;
    return this.http.delete<Response>(api); 
  }
}
