import { Component } from '@angular/core';
import { RoomService } from 'src/app/services/room.service';
import { RoomTypeService } from 'src/app/services/room-type.service';
import { GuestService } from 'src/app/services/guest.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { Room, Reservation, Response } from 'src/app/models';
import { InfomationRoomDialogComponent } from '../dialog/infomation-room-dialog/infomation-room-dialog.component';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-manageroom',
  templateUrl: './manageroom.component.html',
  styleUrls: ['./manageroom.component.css']
})
export class ManageroomComponent {
  roomsByRoomType: any[] = [];
  check: string = 'all'

  change() {
    this.ngOnInit()
  }

  constructor(private roomTypeService: RoomTypeService, private roomService: RoomService,
            private guestService: GuestService, private reservationService: ReservationService,
            private dialog: MatDialog,
  ) { }

  openRoomDialog(room:Room) {
    let dialogRef = this.dialog.open(InfomationRoomDialogComponent, {
      width: '80%',
      data:room,
    })

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
      console.log("test");
    });
  }

  ngOnInit() {
    this.roomsByRoomType = []

    this.roomTypeService.getAllRoomTypes().subscribe((responseRoomType: Response) => {
      if (responseRoomType && responseRoomType.ec == 0) {
        for (let roomType of responseRoomType.dt) {
          this.roomService.getRoomByRoomTypeID(roomType.roomTypeID).subscribe((responseRoom:Response)=>{
            if(responseRoom && responseRoom.ec==0){
              let rooms:Room[] = responseRoom.dt;
              if (this.check == 'available') {
                rooms = rooms.filter(room => room.isAvaiable)
              }
              else if (this.check == 'unavailable') {
                rooms = rooms.filter(room => !room.isAvaiable)
              }
              for (let room of rooms) {
                if (!room.isAvaiable) {
                  this.guestService.getGuestByRoomId(room.roomID).subscribe((response:Response) => {
                    if(response && response.ec==0){
                      room.guest=response.dt;
                      
                    }
                  })

                  this.reservationService.getReservationByRoomId(room.roomID).subscribe((reservation:Reservation)=>{
                    room.reservation= reservation;
                    let d1 = new Date(room.reservation.endTime)
                    let d2 = new Date(room.reservation.startTime)
                    room.diffDays = (Math.ceil(d1.getTime() - d2.getTime()) / (1000 * 60 * 60 * 24))
                  });
                 
                 
                }
              }
              let obj = { roomType, rooms }
              this.roomsByRoomType.push(obj)
            }
          });
        }
      }
    })
  }

        
}
