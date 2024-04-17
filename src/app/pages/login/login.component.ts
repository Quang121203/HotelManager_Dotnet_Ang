import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { Response } from 'src/app/models';
import { Store } from '@ngrx/store';
import { login } from 'src/app/store/store.actions';
import { User } from 'src/app/models';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {

  loginForm!: FormGroup;
  submitted: boolean = false;


  constructor(private formBuilder: FormBuilder, private router: Router,
    private snackService: SnackbarService,
    private authService: AuthService,
    private store: Store<{ user: User }>,
    private cookieService: CookieService
  ) { }

  private tokenExpired(token: string) {
    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    })


    this.cookieService.delete('accessToken', '/');
    this.cookieService.delete( 'accessToken' , '/hotel' );
    this.cookieService.delete('refeshToken', '/');
    this.cookieService.delete( 'refeshToken' , '/hotel' );
    
  }

  onSubmit() {
    this.submitted = true;
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe((response: Response) => {
        if (response.ec == 0) {
          this.router.navigate(['hotel']);

          var user: User = response.dt.user;
          this.cookieService.set('accessToken', response.dt.token.accessToken);
          this.cookieService.set('refeshToken', response.dt.token.refeshToken);

          this.store.dispatch(login({ user }));
        }
        this.snackService.openSnackBar(response.em);
      });
    }
    else {
      this.snackService.openSnackBar('Please enter your email address and password');
    }
  }


}
