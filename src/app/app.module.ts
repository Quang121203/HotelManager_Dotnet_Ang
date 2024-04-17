import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { LoginComponent } from './pages/login/login.component';
import { PrivateComponent } from './pages/private/private.component';
import { HeaderComponent } from './component/header/header.component';
import { SidebarComponent } from './component/sidebar/sidebar.component';
import { FooterComponent } from './component/footer/footer.component';
import { HttpClientModule } from '@angular/common/http';
import { RoomDialogComponent } from './component/dialog/room-dialog/room-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { RoomTypeDialogComponent } from './component/dialog/room-type-dialog/room-type-dialog.component';
import { InfomationRoomDialogComponent } from './component/dialog/infomation-room-dialog/infomation-room-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { BillDialogComponent } from './component/dialog/bill-dialog/bill-dialog.component';
import { CheckinDialogComponent } from './component/dialog/checkin-dialog/checkin-dialog.component';
import { MatStepperModule } from "@angular/material/stepper";
import { BookRoomDialogComponent } from './component/dialog/book-room-dialog/book-room-dialog.component';
import { FormsModule } from '@angular/forms';
import { UserDialogComponent } from './component/dialog/user-dialog/user-dialog.component';

import { StoreModule } from '@ngrx/store';
import { counterReducer } from './counter/counter.reducer';
import { CounterComponent } from './counter/counter.component';
import { storeReducer } from './store/store.reducer';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './config/AuthInterceptor.service';
import { Router } from '@angular/router';
import { SnackbarService } from './services/snackbar.service';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './services/auth.service';
import { Store } from '@ngrx/store';
import { User } from './models';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PrivateComponent,
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    RoomDialogComponent,
    RoomTypeDialogComponent,
    InfomationRoomDialogComponent,
    BillDialogComponent,
    CheckinDialogComponent,
    BookRoomDialogComponent,
    UserDialogComponent,
    CounterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatIconModule,
    MatStepperModule,
    FormsModule,
    StoreModule.forRoot({ counter: counterReducer, user: storeReducer })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useFactory: function (router: Router, snackbarSerivce: SnackbarService, cookieService: CookieService, 
        authService:AuthService) {
        return new AuthInterceptor(router, snackbarSerivce, cookieService, authService);
      },
      multi: true,
      deps: [Router, SnackbarService, CookieService,AuthService] 
    }
  ],
  
  bootstrap: [AppComponent]
})
export class AppModule { }
