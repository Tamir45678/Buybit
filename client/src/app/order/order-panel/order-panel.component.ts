import { Component, Input, OnInit } from '@angular/core';
import { OrderDetails } from 'src/app/models/order.model';
import { Product } from 'src/app/models/product.model';
import { MarketplaceService } from 'src/app/_services/marketplace.service';

@Component({
  selector: 'app-order-panel',
  templateUrl: './order-panel.component.html',
  styleUrls: ['./order-panel.component.css']
})
export class OrderPanelComponent implements OnInit {

  panelOpenState = false;
  @Input() order:OrderDetails 
  product:Product

  constructor(public marketService:MarketplaceService) { }

  ngOnInit(): void {
    this.marketService.currentProduct$.subscribe((response:Product[])=>{
      this.product = response.find(x=>x.id==this.order.productId)
    })
  }

}
