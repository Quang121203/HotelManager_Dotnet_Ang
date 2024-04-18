// profile.component.ts
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { UserService } from 'src/app/services/user.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User,Response } from 'src/app/models';
import { Observable } from 'rxjs';
import { login } from 'src/app/store/store.actions';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent implements OnInit {

  userInfoForm!: FormGroup;
  passwordForm!: FormGroup
  user$: Observable<User>;
  userId: string = '';
  userRole:string ='';

  constructor(private userSerivce: UserService, private snackbarService: SnackbarService,
    private authService: AuthService, private formBuilder: FormBuilder,
    private store: Store<{ user: User }>,private cookieService: CookieService
  ) {
    this.user$ = store.select('user');
  }



  ngOnInit(): void {
    this.user$.subscribe(value => {
      this.userInfoForm = this.formBuilder.group({
        userName: [value.userName, Validators.required],
        email: [value.email, Validators.required],
      });

      this.userId = value.id;
      this.userRole = value.role;
    });

    this.passwordForm = this.formBuilder.group({
      oldPassword: ['',Validators.required],
      newPassword: ['',Validators.required],
    })
  }


  updateUserSettings() {
    if (this.userInfoForm.valid) {
      var user: User = this.userInfoForm.value;
      user.id = this.userId;
      user.role= this.userRole;
      this.authService.changeInfo(this.userInfoForm.value).subscribe((response:Response)=>{       
        if(response.ec==0){
          this.cookieService.set('accessToken', response.dt.accessToken);
          this.cookieService.set('refeshToken', response.dt.refeshToken);
          this.snackbarService.openSnackBar(response.em);
          this.store.dispatch(login({user}));   
        }
        else{
          this.snackbarService.openSnackBar(response.dt.errors[0].description);
        }
      })
    }
    else {
      this.snackbarService.openSnackBar('Please update your Name or Email');
    }
  }

  updatePassword()
  {
    if (this.passwordForm.valid) {
      this.authService.changePassword(this.passwordForm.value).subscribe((response:Response)=>{       
        if(response.ec==0){
          this.snackbarService.openSnackBar(response.em);
          this.passwordForm.reset(); 
        }
        else{
          this.snackbarService.openSnackBar(response.dt.errors[0].description);
        }
      })
    }
    else {
      this.snackbarService.openSnackBar('Please fill the input');
    }     
  }
}