import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ReservationRoomService } from 'src/app/services/reservation-room.service';
import { RoomTypeService } from 'src/app/services/room-type.service';
import { RoomService } from 'src/app/services/room.service';
import { Response,RoomType,ReservationRoom } from 'src/app/models';
import { BillService } from 'src/app/services/bill.service';
import { BillDialogComponent } from '../bill-dialog/bill-dialog.component';

import { Room,Bill } from 'src/app/models';

@Component({
  selector: 'app-infomation-room-dialog',
  templateUrl: './infomation-room-dialog.component.html',
  styleUrls: ['./infomation-room-dialog.component.css']
})
export class InfomationRoomDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: Room, private ref: MatDialogRef<InfomationRoomDialogComponent>,
              private reservationRoomService:ReservationRoomService, private roomTypeService:RoomTypeService,
              private roomService:RoomService, private billService:BillService,
              private dialog: MatDialog
  ) { }
  
  guestRoom: any[] = []
  ngOnInit() {
    this.guestRoom = []
    
    if(this.data.reservation){
      this.reservationRoomService.getReservationRoomByReservationId(this.data.reservation.reservationID).subscribe((responseReservation:Response) => {
        if(responseReservation && responseReservation.ec==0){
          let reservationRooms:ReservationRoom[]=responseReservation.dt;
          this.roomTypeService.getAllRoomTypes().subscribe((responseRoomType:Response) => {
            if(responseRoomType && responseRoomType.ec==0){
              let roomTypes:RoomType[]=responseRoomType.dt;
              for (let roomType of roomTypes) {
                let number = 0;
                for (let reservationRoom of reservationRooms) {
                  this.roomService.getRoomById(reservationRoom.roomID).subscribe((responseRoom:Response)=>{
                    if (responseRoom && responseRoom.ec==0){
                       
                        if(responseRoom.dt.roomTypeID == roomType.roomTypeID) {number++}
                       
                        if (number > 0) this.guestRoom.push({ name: roomType.name, price: roomType.dailyPrice, number: number, priceTotal: number * roomType.dailyPrice! * this.data.diffDays })   
                    }
                  })
                  
                }
                
              }
            }
          });
        }
        
      });
    }
   
  }
  
  closeDialog() {
    this.ref.close()
  }


  openBillDialog() {
    this.data.guest && this.billService.getBillByIdGuest(this.data.guest.guestID).subscribe((response:Response)=>{
      if(response && response.ec==0){
        var bills:Bill[]=response.dt;
        let bill = bills.find(b => !b.status);

        let dialog = this.dialog.open(BillDialogComponent, {
          width: '60%',
          data: {guestRoom:this.guestRoom,room:this.data,bill:bill},
        })
    
        dialog.afterClosed().subscribe(data => {
          if(data == "checkout"){
            this.ref.close()
          }
        });

      }
    });
    
  }

  
}
