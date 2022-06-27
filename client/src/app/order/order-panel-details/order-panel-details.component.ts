import { Component, Input, OnInit } from '@angular/core';
import { OrderDetails } from 'src/app/models/order.model';
import { Product } from 'src/app/models/product.model';
import { AccountService } from 'src/app/_services/account.service';
import { BalanceService } from 'src/app/_services/balance.service';
import { MarketplaceService } from 'src/app/_services/marketplace.service';
import { OrdersService } from 'src/app/_services/orders.service';

@Component({
  selector: 'app-order-panel-details',
  templateUrl: './order-panel-details.component.html',
  styleUrls: ['./order-panel-details.component.css']
})
export class OrderPanelDetailsComponent implements OnInit {
  @Input() order:OrderDetails
  @Input() product:Product;
 
  constructor(private orderService:OrdersService,private balanceService:BalanceService) { }

  ngOnInit(): void {
  }

  cancelOrder(){
    this.orderService.cancelOrder(this.order).subscribe();
  }

  isShipped(){
    return this.order.deliveryDate>=new Date();
  }

}
