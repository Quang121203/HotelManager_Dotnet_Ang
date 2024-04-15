import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: {action:string,user:User},
                                       private dialogRef: MatDialogRef<UserDialogComponent>,
                                       private formBuilder: FormBuilder,
                                       private snkbr: SnackbarService,
                                       private userService: UserService
  ) { }

  userForm!: FormGroup; 
  
  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      userName: '',
      email: '',
    })
   
  }

  //tắt modal
  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(){
    console.log(this.userForm.value);
    console.log(this.data);
  }
 

}
