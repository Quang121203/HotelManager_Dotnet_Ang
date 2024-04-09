import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoomComponent } from '../../../component/room/room.component';
import { RoomtypeComponent } from '../../../component/roomtype/roomtype.component';


import {MatTabsModule} from '@angular/material/tabs';
import { MaterialmanageRoutingModule } from './materialmanage-routing.module';
import { MaterialmanageComponent } from './materialmanage.component';

@NgModule({
  declarations: [
    MaterialmanageComponent,
    RoomComponent,
    RoomtypeComponent
  ],
  imports: [
    CommonModule,
    MatTabsModule,
    MaterialmanageRoutingModule,
    
  ]
})
export class MaterialmanageModule { }
