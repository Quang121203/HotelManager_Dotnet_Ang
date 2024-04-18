import { Component } from '@angular/core';
import { BillService} from '../../services/bill.service';
import { Bill,Response } from 'src/app/models';
declare var google: any

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent {

  constructor(private billService: BillService) { }

  bills: Bill[]=[];
  sumBillByMonth: any[]=[];
  async ngOnInit() {
    this.bills = []
    this.sumBillByMonth = []
    this.billService.getAllBill().subscribe((response:Response) => {
      this.bills = response.dt;
      this.bills = this.bills.filter(bill => bill.status)
      let colors = ['yellow','red','blue','pink','green','brown']
      for (let i = 0; i < 6; ++i) {
        let date1 = new Date()
        let date2 = new Date()
        date1.setMonth(date1.getMonth() - i)
        date2.setMonth(date2.getMonth() - i + 1)
        let str1 = `${date1.getFullYear()}-${date1.getMonth() + 1}-01 00:00:00`
        let str2 = `${date2.getFullYear()}-${date2.getMonth() + 1}-01 00:00:00`
        date1 = new Date(str1)
        date2 = new Date(str2)
        let obj = { name: "Tháng " + (date1.getMonth() + 1), sum: 0, color: colors[i] }
        for (let bill of this.bills!) {
          let dateBill = new Date(bill.dateCreated.slice(0, 10)+ " 00:00:00")
          if (dateBill >= date1 && dateBill < date2) {
            obj.sum += bill.sum
          }
        }
        this.sumBillByMonth.push(obj)
      }
      google.charts.load('current', { packages: ['corechart'] });
  
      this.buildChart(this.sumBillByMonth)
    })
    
  }

  buildChart(sumbill: any[]) {
    var renderChart = (chart: any) => {
      let mang = []
      mang.push(['Element', 'Doanh thu', { role: 'style' }])
      sumbill.reverse()
      sumbill.forEach(i => {
        mang.push([i.name, i.sum, i.color])
      })
      var data = google.visualization.arrayToDataTable(mang);

      var options = {
        title: "Doanh thu 6 tháng gần nhất",
        length: "none"
      }

      chart().draw(data, options)
    }
    var columnChart = () => new google.visualization.ColumnChart(document.getElementById("chart"));
    var callback = () => renderChart(columnChart)
    google.charts.setOnLoadCallback(callback)
  }
}
