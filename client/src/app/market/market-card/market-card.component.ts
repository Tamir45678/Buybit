import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PlaceOrder } from 'src/app/models/order.model';
import { Product } from 'src/app/models/product.model';
import { User } from 'src/app/models/user.model';
import { AccountService } from 'src/app/_services/account.service';
import { BalanceService } from 'src/app/_services/balance.service';
import { MarketplaceService } from 'src/app/_services/marketplace.service';
import { OrdersService } from 'src/app/_services/orders.service';
import { AddProductModalComponent } from '../add-product-modal/add-product-modal.component';

@Component({
  selector: 'app-market-card',
  templateUrl: './market-card.component.html',
  styleUrls: ['./market-card.component.css']
})
export class MarketCardComponent implements OnInit {
  @Input() product:Product 
  user:User
  placeOrder:PlaceOrder 
  deliveryDate:Date;
  balanceBeforeOrder:number;

  constructor(
    private marketService:MarketplaceService,
    private accountService:AccountService,
    private datePipe:DatePipe,
    public dialog:MatDialog,
    private orderService:OrdersService,
    private balanceService:BalanceService,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(response=>
      this.user = response
      )
  }

  addProduct(){
    if(this.user.balance < this.product.price){
      this._snackBar.open("Not Enough money","Something else",{
        duration:3000
      })
      return;
    }
    this.balanceBeforeOrder = this.user.balance;
    this.accountService.currentUser$.subscribe((response:User)=>{
      this.placeOrder={
        UserId:response.id,
        ProductId:this.product.id,
        DeliveryDate: this.deliveryDate
        }
      }
    )
    this.marketService.placeOrder(this.placeOrder).subscribe(_=>{
        setTimeout(()=>{
          this.marketService.getProducts().subscribe();
        },2000)
        
      }
    );
  }


  openDialog(): void {
    const dialogRef = this.dialog.open(AddProductModalComponent, {
      width: '300px',
      data: {deliveryDate:this.deliveryDate}
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        //Converting Datetime to String for backend
        this.deliveryDate = new Date(result)
          this.addProduct();
      }
      
    });
  }

  
}
