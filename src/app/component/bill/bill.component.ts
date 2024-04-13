import { Component } from '@angular/core';
import { BillService } from 'src/app/services/bill.service';
import { Bill, Response } from 'src/app/models';

@Component({
  selector: 'app-bill',
  templateUrl: './bill.component.html'
})
export class BillComponent {
  constructor(private billService: BillService) { }
  dateStart: string = "";
  dateEnd: string = "";
  bills: Bill[] = [];
  sum: number = 0;

  async ngOnInit() {
    this.bills = [];
    this.sum = 0;

   
    let response = await this.billService.getAllBill().toPromise();
    if (response && response.ec == 0) {
      this.bills = response.dt;
      this.bills = this.bills.filter(bill => bill.status);
      this.bills.forEach(bill => this.sum += bill.sum);
    }
  }



  async onSubmit() {
    await this.ngOnInit();

    this.bills = this.bills.filter(bill => {
      let dateBill = new Date(bill.dateCreated.slice(0, 10) + " 00:00:00")
      return dateBill >= new Date(this.dateStart + " 00:00:00") && dateBill <= new Date(this.dateEnd + " 00:00:00")
    });

    console.log(this.bills);
    this.bills.forEach(bill => this.sum += bill.sum);

  }
}
