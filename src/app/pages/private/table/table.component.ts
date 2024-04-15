import { Component } from '@angular/core';
import { User, Response } from 'src/app/models';
import { UserService } from 'src/app/services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserDialogComponent } from 'src/app/component/dialog/user-dialog/user-dialog.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {
  users: User[] = [];

  constructor(private userService: UserService, public dialog: MatDialog, private snackService: SnackbarService) { }

  ngOnInit() {
    this.userService.getAllUser().subscribe((response:Response) => {
      this.users = response.dt;
    });
  }


  // openAddDialog(): void {
  //   const dialogRef = this.dialog.open(ModalUserComponent, {
  //     width: '50vw', // Chỉnh kích thước theo chiều ngang

  //     data: { action: "Add" }
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.ngOnInit();
  //   });
  // }

  openUpdateDialog(user: User): void {
    const dialogRef = this.dialog.open(UserDialogComponent, {
      width: '50vw', 

      data: { action: "Update", user }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  deleteUser(id: string) {
    this.userService.deleteUser(id).subscribe((response:Response) => {
      this.snackService.openSnackBar(response.em);
    });
  }
}
