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
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
