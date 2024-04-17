import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UserService } from 'src/app/services/user.service';
import { User,Response } from 'src/app/models';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: {action:string,user:User},
                                       private dialogRef: MatDialogRef<UserDialogComponent>,
                                       private formBuilder: FormBuilder,
                                       private snackService: SnackbarService,
                                       private userService: UserService
  ) { }

  userForm!: FormGroup; 
  
  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      userName: [this.data.action=="Update"?this.data.user.userName:"",Validators.required],
      email: [this.data.action=="Update"?this.data.user.email:"",Validators.required],
    })
   
  }

  //tắt modal
  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(){
    console.log(this.userForm.value);
    if(!this.userForm.invalid){
      var user: User = this.userForm.value;
      if(this.data.action=="Update"){
        user.id=this.data.user.id;
        this.userService.updateUser(this.userForm.value).subscribe((response:Response) =>{
          if(response.ec==0){
            this.onNoClick();
          }
          this.snackService.openSnackBar(response.em);
        })
      }
      else{
        user.id="string";
        this.userService.addUser(user).subscribe((response:Response)=>{
          if(response.ec==0){
            this.onNoClick();
          }
          this.snackService.openSnackBar(response.em);
        });
      }
    }
    else{
      this.snackService.openSnackBar("please fill in the input");
    }
  }
 

}
