import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';

import { Validators, FormBuilder, FormGroup } from '@angular/forms';

import { Room, RoomType, Response } from '../../../models'
import { RoomTypeService } from '../../../services/room-type.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { RoomService } from 'src/app/services/room.service';

@Component({
  selector: 'app-room-dialog',
  templateUrl: './room-dialog.component.html'
})
export class RoomDialogComponent {
  roomTypes: RoomType[] = [];
  roomForm!: FormGroup;

  constructor(public dialogRef: MatDialogRef<RoomDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { room: Room, type: string },
    public roomTypeService: RoomTypeService,
    private formBuilder: FormBuilder,
    private snackbarService: SnackbarService,
    private roomService: RoomService
  ) { }


  ngOnInit() {
    this.roomTypeService.getAllRoomTypes().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        this.roomTypes = response.dt;
      }
    });

    this.roomForm = this.formBuilder.group({
      roomNumber: [this.data.room.roomNumber, Validators.required],
      roomTypeID: [this.data.room.roomTypeID, Validators.required]
    });

  }

  onSubmit(): void {
    if (this.roomForm.valid) {
      var room: Room = this.roomForm.value;
      
      if (this.data.type == "update") {
        room.roomID = this.data.room.roomID;
        room.isAvaiable = this.data.room.isAvaiable;
        this.roomService.updateRoom(room).subscribe((response: Response) => {
          if (response) {
            this.snackbarService.openSnackBar(response.em);
            this.roomForm.reset();
            this.closeDialog();
          }
        });
      }
      else {
        this.roomService.addRoom(room).subscribe((response: Response) => {
          if (response) {
            this.snackbarService.openSnackBar(response.em);
            this.roomForm.reset();
            this.closeDialog();
          }
        });
      }
    }
    else {
      this.snackbarService.openSnackBar("Please fill room's information!");
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
