import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Reservation,Response,Booking } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  url: string = eviroments.url;

  constructor(private http: HttpClient) { }


  getReservationByRoomId(id: string):Observable<Reservation>{
    const url = `${this.url}Reservation/GetReservationByRoom?IDRoom=${id}`
    return this.http.get<Reservation>(url)
  }
 
  checkOut(id:string):Observable<Response> {
    const url = `${this.url}Reservation/CheckOut?IDReservation=${id}`
    return this.http.get<Response>(url)
  }

  checkIn(id:string):Observable<Response>{
    const url = `${this.url}Reservation/CheckIn?IDReservation=${id}`
    return this.http.get<Response>(url)
  }
 
  getAllReservation():Observable<Response> {
    const url = `${this.url}Reservation/GetAllReservations`
    return this.http.get<Response>(url)
  }

  Book(booking: Booking):Observable<Response>{
    const url = `${this.url}Reservation/ReserveRooms`
    return this.http.post<Response>(url, booking)
  }
  

}
