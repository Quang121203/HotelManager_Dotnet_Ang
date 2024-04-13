import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportRoutingModule } from './report-routing.module';
import { ReportComponent } from './report.component';
import { BillComponent } from 'src/app/component/bill/bill.component';
import { FormsModule } from '@angular/forms';
import { ChartComponent } from 'src/app/component/chart/chart.component';
import { MatTabsModule } from "@angular/material/tabs";


@NgModule({
  declarations: [
    ReportComponent,
    BillComponent,
    ChartComponent
  ],
  imports: [
    CommonModule,
    ReportRoutingModule,
    FormsModule,
    MatTabsModule
  ]
})
export class ReportModule { }
