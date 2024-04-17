import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { PrivateComponent } from './pages/private/private.component';
import { authGuard } from './services/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', component: LoginComponent, pathMatch: 'full' },
  {
    path: 'hotel',
    component: PrivateComponent,
    children: [
      { path:'',loadChildren: ()=> import('./pages/private/profile/profile.module').then(module=>module.ProfileModule),pathMatch:'full'},
      { path:'profile',loadChildren: ()=> import('./pages/private/profile/profile.module').then(module=>module.ProfileModule)},
      { path:'materialmanage',loadChildren: ()=> import('./pages/private/materialmanage/materialmanage.module').then(module=>module.MaterialmanageModule)},
      { path:'table',loadChildren: ()=> import('./pages/private/table/table.module').then(module=>module.TableModule)},
      { path:'hotelmanage',loadChildren: ()=> import('./pages/private/hotelmanager/hotelmanager.module').then(module=>module.HotelmanagerModule)},
      { path:'report',loadChildren: ()=> import('./pages/private/report/report.module').then(module=>module.ReportModule)},
    ],
    canActivate: [authGuard],
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
