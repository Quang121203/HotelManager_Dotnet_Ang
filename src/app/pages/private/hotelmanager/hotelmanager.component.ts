import { Component,ViewChild } from '@angular/core';

@Component({
  selector: 'app-hotelmanager',
  templateUrl: './hotelmanager.component.html',
  styleUrls: ['./hotelmanager.component.css']
})
export class HotelmanagerComponent {
  @ViewChild('manageTab') manageTab: any; 
  @ViewChild('checkinTab') checkinTab: any; 

  tabChanged(event: any): void {
    if (event === 0) {
      this.manageTab.roomsByRoomType=[];
      this.manageTab.ngOnInit();
      
    } else if (event === 1) {
      this.checkinTab.reservationsWithNumber=[];
      this.checkinTab.ngOnInit(); 
    }
  }

}
