import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoginFormComponent } from './login-form/login-form.component';
import { User } from './models/user.model';
import { AccountService } from './_services/account.service';
import { BalanceService } from './_services/balance.service';
import { MarketplaceService } from './_services/marketplace.service';
import { OrdersService } from './_services/orders.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(
    private accountService:AccountService,
    private marketServer:MarketplaceService,
    private balanceService:BalanceService,
    private orderService:OrdersService
    ){}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user'))
    if(user){
      this.accountService.setCurrentUser(user);
      this.marketServer.getProducts().subscribe();
      this.orderService.getOrders().subscribe();
      this.balanceService.getBalance(user).subscribe();
    }
    
  }
}
