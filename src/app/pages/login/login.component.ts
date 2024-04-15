import { Component } from '@angular/core';
// import { AuthService } from '../../Services/auth.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
// import { UserStorageService } from '../../Services/userStorage.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Response } from 'src/app/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {

  loginForm!: FormGroup;
  submitted :boolean = false;

  constructor(private formBuilder: FormBuilder, private router: Router, 
              private snackService: SnackbarService,
              private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

  onSubmit() {
    this.submitted=true;
    if (this.loginForm.valid) {
      console.log(this.loginForm.value);
      this.authService.login(this.loginForm.value).subscribe((response:Response) => {
        if(response.ec==0){
          this.router.navigate(['hotel']);
          console.log(response.dt);
        }
        this.snackService.openSnackBar(response.em);
      });
    } 
    else{
      this.snackService.openSnackBar('Please enter your email address and password');
    }
  }

  
}
