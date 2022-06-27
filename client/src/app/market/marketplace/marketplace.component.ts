import { Component, OnInit } from '@angular/core';
import { MarketplaceService } from '../../_services/marketplace.service';
@Component({
  selector: 'app-marketplace',
  templateUrl: './marketplace.component.html',
  styleUrls: ['./marketplace.component.css']
})
export class MarketplaceComponent implements OnInit {
  
  products:any = []
  
  constructor(public marketplaceService:MarketplaceService ) { }
  ngOnInit(): void {
  }

  // getProducts(){
  //   this.marketplaceService.currentProduct$.subscribe(response=>{
  //     this.products=response;
  //   })
  // }

}
