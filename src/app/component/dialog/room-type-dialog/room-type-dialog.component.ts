import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';

import { Validators, FormBuilder, FormGroup } from '@angular/forms';

import { RoomType, Response } from '../../../models'
import { RoomTypeService } from '../../../services/room-type.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-room-type-dialog',
  templateUrl: './room-type-dialog.component.html'
})
export class RoomTypeDialogComponent {
  roomTypes: RoomType[] = [];
  roomTypeForm!: FormGroup;

  constructor(public dialogRef: MatDialogRef<RoomTypeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { roomType: RoomType, type: string },
    public roomTypeService: RoomTypeService,
    private formBuilder: FormBuilder,
    private snackbarService: SnackbarService
  ) { }


  ngOnInit() {
    this.roomTypeService.getAllRoomTypes().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        this.roomTypes = response.dt;
      }
    });

    this.roomTypeForm = this.formBuilder.group({
      name: [this.data.roomType.name, Validators.required],
      dailyPrice: [this.data.roomType.dailyPrice, Validators.required],
      description: [this.data.roomType.description, Validators.required]
    });

  }

  onSubmit(): void {
    if (this.roomTypeForm.valid) {
      var roomType: RoomType = this.roomTypeForm.value;
      
      if (this.data.type == "update") {
        roomType.roomTypeID = this.data.roomType.roomTypeID;
        this.roomTypeService.updateRoomType(roomType).subscribe((response: Response) => {
          if (response) {
            this.snackbarService.openSnackBar(response.em);
            this.roomTypeForm.reset();
            this.closeDialog();
          }
        });
      }
      else {
        this.roomTypeService.addRoomType(roomType).subscribe((response: Response) => {
          if (response) {
            this.snackbarService.openSnackBar(response.em);
            this.roomTypeForm.reset();
            this.closeDialog();
          }
        });
      }
    }
    else {
      this.snackbarService.openSnackBar("Please fill roomType's information!");
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
