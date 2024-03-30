import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HotelmanagerComponent } from './hotelmanager.component';





const routes: Routes = [
  {
    path: '',
    component:HotelmanagerComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HotelmanagerRoutingModule { }
