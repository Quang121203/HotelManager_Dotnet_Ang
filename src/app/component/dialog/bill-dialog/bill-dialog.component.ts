import { Component,Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReservationService } from 'src/app/services/reservation.service';
import { Room,Bill,Response } from 'src/app/models';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-bill-dialog',
  templateUrl: './bill-dialog.component.html',
  styleUrls: ['./bill-dialog.component.css']
})
export class BillDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data:{guestRoom: any,room:Room,bill:Bill},
              private ref:MatDialogRef<BillDialogComponent>,
              private reservationService:ReservationService,
              private snackbarService: SnackbarService
            )
  {}

  date:string="";

  closeDialog(){
    this.ref.close()
  }

  checkOut(){
    this.data.room.reservation&&this.reservationService.checkOut(this.data.room.reservation.reservationID).subscribe((response:Response)=>{
      this.snackbarService.openSnackBar(response.em);
    });

    this.ref.close("checkout")
  }

  ngOnInit(){
    console.log(this.data);
    this.date = new Date().
    toLocaleString('en-us', {year: 'numeric', month: '2-digit', day: '2-digit'}).
    replace(/(\d+)\/(\d+)\/(\d+)/, '$3-$1-$2');
  }
}
