import { Component } from '@angular/core';
import { ReservationService } from 'src/app/services/reservation.service';
import { MatDialog } from '@angular/material/dialog';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Reservation, ReservationRoom, Response } from 'src/app/models';
import { ReservationRoomService } from 'src/app/services/reservation-room.service';
import { CheckinDialogComponent } from '../dialog/checkin-dialog/checkin-dialog.component';
import { GuestService } from 'src/app/services/guest.service';

@Component({
  selector: 'app-bookroom',
  templateUrl: './bookroom.component.html',
  styleUrls: ['./bookroom.component.css']
})
export class BookroomComponent {
  constructor(private reservationService: ReservationService, public dialog: MatDialog,
    private snkbr: SnackbarService,
    private reservationRoomService: ReservationRoomService,
    private guestService:GuestService
  ) { }

  reservationsWithNumber: any[] = [];

  ngOnInit() {
    this.reservationsWithNumber = [];
    this.reservationService.getAllReservation().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        let reservations: Reservation[] = response.dt;
        reservations.sort((a, b) => (a.dateCreated > b.dateCreated ? 1 : -1));
        reservations = reservations.filter((rs: Reservation) => rs.isConfirmed == false);

        reservations.map(reservation => {

          this.guestService.getGuestById(reservation.guestID).subscribe((responseGuest: Response) => {
            if(responseGuest && responseGuest.ec == 0) {
              reservation.guest = responseGuest.dt;
            }
          });

          this.reservationRoomService.getReservationRoomByReservationId(reservation.reservationID).subscribe((responseReservationRoom: Response) => {
            if (responseReservationRoom && responseReservationRoom.ec == 0) {
              this.reservationsWithNumber.push({ reservation, number: responseReservationRoom.dt.length });
            }
          });
        })
        
      }

    });
  }



  openDialogCheckIn(idReservation: string, idGuest: string): void {
    const dialogRef = this.dialog.open(CheckinDialogComponent, {
      width: '80%',
      height: '80%',
      data: { idReservation, idGuest }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }


  // openDialogBook(): void {
  //   const dialogRef = this.dialog.open(ModalBookingComponent, {
  //     width: '80%',
  //     height: '90%',
  //     data: { /* Thêm dữ liệu nếu cần thiết */ }
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.reservationsWithNumber = [];
  //     this.ngOnInit();
  //   });
  // }

  // Cancel(id: string) {
  //   this.reservationService.Cancel(id).subscribe((result: any) => {
  //     this.ngOnInit();
  //     this.snkbr.openSnackBar("Delete reservation successfully", "success");
  //   });
  // }
}
