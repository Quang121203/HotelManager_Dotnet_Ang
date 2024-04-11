import { FormBuilder, Validators } from '@angular/forms';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoomTypeService } from 'src/app/services/room-type.service';
import { RoomService } from 'src/app/services/room.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { GuestService } from 'src/app/services/guest.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { ReservationRoomService } from 'src/app/services/reservation-room.service';
import { RoomType, Response, Room, ReservationRoom, Guest } from 'src/app/models';

@Component({
  selector: 'app-checkin-dialog',
  templateUrl: './checkin-dialog.component.html',
  styleUrls: ['./checkin-dialog.component.css']
})
export class CheckinDialogComponent {
  roomsByRoomType: any[] = [];
  number: number = 0;
  guest: Guest = new Guest();

  constructor(private fb: FormBuilder,
    public dialogRef: MatDialogRef<CheckinDialogComponent>,
    private roomTypeService: RoomTypeService,
    private roomService: RoomService,
    private reservationService: ReservationService,
    private guestService: GuestService,
    private snackService: SnackbarService,
    private reservationRoomService: ReservationRoomService,
    @Inject(MAT_DIALOG_DATA) public data: {idReservation:string,idGuest:string}) { }

  ngOnInit() {
    console.log(this.data);
    this.roomTypeService.getAllRoomTypes().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        let roomTypes: RoomType[] = response.dt;
        this.reservationRoomService.getReservationRoomByReservationId(this.data.idReservation).subscribe((responseReservationRoom: Response) => {
          if (responseReservationRoom && responseReservationRoom.ec == 0) {
            let reservationRooms: ReservationRoom[] = responseReservationRoom.dt;
            this.number = reservationRooms.length;

            for (let roomType of roomTypes) {

              this.roomService.getRoomByRoomTypeID(roomType.roomTypeID).subscribe((responseRoomType: Response) => {
                if (responseRoomType && responseRoomType.ec == 0) {
                  let rooms: Room[] = responseRoomType.dt;

                  rooms.forEach(room => {
                    if (reservationRooms.find(reservationRoom => room.roomID === reservationRoom.roomID)) {
                      room.isReserved = true;
                    }
                  });

                  let obj = { roomType, rooms }
                  this.roomsByRoomType.push(obj)
                }
              })

            }
          }
        })
      }
    })


    this.guestService.getGuestById(this.data.idGuest).subscribe((response: Response) => {
      if (response && response.ec == 0) {
        this.guest = response.dt;
        console.log(response.dt);
      }
    })


  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  checkIn(){
    this.reservationService.checkIn(this.data.idReservation).subscribe((response:Response)=>{
      this.snackService.openSnackBar(response.em);
      this.onNoClick();
    })   
  }
}
