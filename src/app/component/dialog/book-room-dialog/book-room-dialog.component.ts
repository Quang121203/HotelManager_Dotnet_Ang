import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ReservationService } from 'src/app/services/reservation.service';
import { Room, Bill, Response, RoomType, Booking } from 'src/app/models';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { RoomTypeService } from 'src/app/services/room-type.service';
import { RoomService } from 'src/app/services/room.service';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-book-room-dialog',
  templateUrl: './book-room-dialog.component.html',
  styleUrls: ['./book-room-dialog.component.css']
})
export class BookRoomDialogComponent {
  roomTypes: RoomType[] = [];
  mangSoLuongPhong: any = [];
  dateCheckin: string = ""
  dateCheckout: string = ""
  FormBooking!: FormGroup;
  submitted: boolean = false;

  constructor(public dialogRef: MatDialogRef<BookRoomDialogComponent>,
    private formBuilder: FormBuilder,
    private roomTypeService: RoomTypeService,
    private reservationService: ReservationService,
    private snkbr: SnackbarService,
    private roomService: RoomService) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    this.FormBooking = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      sodienthoai: ['', [Validators.required, Validators.maxLength(10)]],
      yeucau: ['']
    })

    this.roomTypeService.getAllRoomTypes().subscribe((response: Response) => {
      if (response && response.ec == 0) {
        this.roomTypes = response.dt;
      }
    })

  }

  async onSubmit() {
    this.submitted = true;
    if (this.FormBooking.invalid) {
      this.snkbr.openSnackBar("Fill infomation's reservation")
      return
    }
    else {
      if (this.dateCheckin == "" || this.dateCheckout == "") {
        this.snkbr.openSnackBar("Bạn phải nhập ngày đến và ngày đi")
        return
      }
      else {
        let currentdate = new Date();
        let dateStart = new Date(Date.parse(this.dateCheckin));
        let dateEnd = new Date(Date.parse(this.dateCheckout));

        if (dateStart >= dateEnd) {
          this.snkbr.openSnackBar("Ngày đến phải nhỏ hơn ngày đi ít nhất 1 ngày")
          return
        }
        else if (dateStart < currentdate) {
          this.snkbr.openSnackBar("Bạn phải đặt phòng trước ít nhất 1 ngày")
          return
        }

        let check = false;
        this.mangSoLuongPhong = []
        let inputSoLuongPhong: any = document.getElementsByClassName("soluongphong")
        inputSoLuongPhong = [...inputSoLuongPhong]
        for (let i of inputSoLuongPhong) {
          let item = { id: i.id, name: i.name, number: i.value }
          this.mangSoLuongPhong.push(item);
          if (i.value > 0) check = true
        }
        if (!check) {
          this.snkbr.openSnackBar("Bạn phải chọn số lượng phòng")
          return
        }
        let data: Booking = {
          guestFullName: this.FormBooking.value.name,
          guestPhoneNumber: "+" + this.FormBooking.value.sodienthoai,
          guestEmail: this.FormBooking.value.email,
          roomTypeId: "",
          startTime: this.dateCheckin,
          endTime: this.dateCheckout,
          numberOfRooms: 0,
        }

        let checkSoLuongPhong = true
        for (let i of this.mangSoLuongPhong) {
          if (i.number == 0) continue
          data.roomTypeId = i.id
          data.numberOfRooms = Number(i.number)

          this.roomService.getRoomNotServe(data).subscribe((rooms: Room[]) => {
            if (rooms.length < data.numberOfRooms) {
              checkSoLuongPhong = false
            }
          })

        }

        if (checkSoLuongPhong) {
          for (let i of this.mangSoLuongPhong) {
            if (i.number == 0) continue
            data.roomTypeId = i.id
            data.numberOfRooms = Number(i.number)

            await this.reservationService.Book(data).toPromise();
            this.snkbr.openSnackBar("Đặt phòng thành công");
            this.onNoClick();

          }
        }
        else {
          this.snkbr.openSnackBar("Khách sạn không đủ phòng")
        }


      }
    }

  }
}
