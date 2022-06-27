import { Component, OnInit } from '@angular/core';
import { OrdersService } from 'src/app/_services/orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders:any = []

  constructor(public orderService:OrdersService) { }

  ngOnInit(): void {
    this.showOrders();
  }

  showOrders(){
    this.orderService.getOrders().subscribe()
  }
}
