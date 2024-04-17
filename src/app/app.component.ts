import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Response,User } from './models';
import { Store } from '@ngrx/store';
import { login } from './store/store.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'client';

  constructor(private authService: AuthService, private store: Store<{ user: User }>,){}

  ngOnInit(){
    if( window.location.href.includes("/login") || window.location.href=="http://localhost:4200/"){
      return;
    }
    this.authService.getInfomation().subscribe((response:Response) =>{
      if(response && response.ec==0){

        this.store.dispatch(login(response.dt));
      }
    });
  }
}
