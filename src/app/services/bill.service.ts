import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import eviroments from '../../eviroments';
import { Observable } from 'rxjs';
import { Response } from '../models';

@Injectable({
  providedIn: 'root'
})
export class BillService {
  url: string = eviroments.url;

  constructor(private http: HttpClient) { }

  getBillByIdGuest(id:string):Observable<Response>{
    let api = `${this.url}Bill/GetBillByGuestID/${id}`;
    return this.http.get<Response>(api);
  }

  getAllBill():Observable<Response>{
    let api = `${this.url}Bill/GetAllBill`
    return this.http.get<Response>(api);
  }
}
