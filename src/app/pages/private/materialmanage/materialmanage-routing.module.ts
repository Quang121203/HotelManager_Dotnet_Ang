import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialmanageComponent } from './materialmanage.component';




const routes: Routes = [
  {
    path: '',
    component:MaterialmanageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaterialmanageRoutingModule { }
