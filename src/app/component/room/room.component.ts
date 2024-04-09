import { Component } from '@angular/core';
import { RoomService } from 'src/app/services/room.service';
import { RoomTypeService } from 'src/app/services/room-type.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Response, Room, RoomType } from '../../models';
import {RoomDialogComponent} from '../dialog/room-dialog/room-dialog.component'
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})




export class RoomComponent {
  rooms: Room[] = [];

  constructor(private dialog: MatDialog,
    private roomService: RoomService, private roomTypeService: RoomTypeService,
    private snackbarService: SnackbarService
  ) { }



  ngOnInit() {
    this.roomService.getAllRooms().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        this.rooms = response.dt;
        this.rooms.sort((a, b) => (a.dateCreated < b.dateCreated ? 1 : -1));
        for (let room of this.rooms) {
          this.roomTypeService.getRoomType(room.roomTypeID).subscribe((rt: RoomType) => room.roomTypeName = rt.name);
        }
      }
    });
    
  }

  openUpdateDialog(room:Room): void {
    const dialogRef = this.dialog.open(RoomDialogComponent, {
      width: '500px',
      data: {room:room,type:"update"}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(RoomDialogComponent, {
      width: '500px',
      data: {room:new Room,type:"add"}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  deleteRoom(id:string):void {
    this.roomService.deleteRoom(id).subscribe((response:Response) => {
      this.snackbarService.openSnackBar(response.em);
      this.ngOnInit();
    });
  }

}


