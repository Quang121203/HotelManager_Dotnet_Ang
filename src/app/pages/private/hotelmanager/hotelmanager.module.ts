import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HotelmanagerComponent } from './hotelmanager.component';
import { HotelmanagerRoutingModule } from './hotelmanager-routing.module';
import { MatTabsModule } from '@angular/material/tabs';
import {MatIconModule} from '@angular/material/icon';
import { BookroomComponent } from 'src/app/component/bookroom/bookroom.component';
import { ManageroomComponent } from 'src/app/component/manageroom/manageroom.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    HotelmanagerComponent,
    BookroomComponent,
    ManageroomComponent
  ],
  imports: [
    CommonModule,
    HotelmanagerRoutingModule,
    MatTabsModule,
    MatIconModule,
    FormsModule
  ]
})
export class HotelmanagerModule { }
