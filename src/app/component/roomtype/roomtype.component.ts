import { Component } from '@angular/core';
import { RoomType,Response} from 'src/app/models';
import { RoomTypeService } from 'src/app/services/room-type.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import {MatDialog} from '@angular/material/dialog';
import { RoomTypeDialogComponent } from '../dialog/room-type-dialog/room-type-dialog.component';

@Component({
  selector: 'app-roomtype',
  templateUrl: './roomtype.component.html',
  styleUrls: ['../room/room.component.css']
})
export class RoomtypeComponent {
  roomTypes: RoomType[] = [];

  constructor(private dialog: MatDialog,
    private roomTypeService: RoomTypeService,
    private snackbarService: SnackbarService
  ) { }



  ngOnInit() {
    this.roomTypeService.getAllRoomTypes().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        this.roomTypes = response.dt;
        this.roomTypes.sort((a, b) => (a.dateCreated < b.dateCreated ? 1 : -1));
      }
    });

  }

  openUpdateDialog(roomType: RoomType): void {
    const dialogRef = this.dialog.open(RoomTypeDialogComponent, {
      width: '500px',
      data: { roomType: roomType, type: "update" }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(RoomTypeDialogComponent, {
      width: '500px',
      data: { roomType: new RoomType, type: "add" }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  deleteRoomType(id: string): void {
    this.roomTypeService.deleteRoomType(id).subscribe((response: Response) => {
      this.snackbarService.openSnackBar(response.em);
      this.ngOnInit();
    });
  }
}
