import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { OrderDetails } from '../models/order.model';
import { User } from '../models/user.model';
import { AccountService } from './account.service';
import { BalanceService } from './balance.service';
import { MarketplaceService } from './marketplace.service';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  user:User = null;
  private CurrentOrderSource = new ReplaySubject<OrderDetails[]>(1);
  currentOrders$ = this.CurrentOrderSource.asObservable();
  baseUrl = 'http://localhost:5021/orders';
  
  constructor(private http:HttpClient,private accountService:AccountService,private balanceService:BalanceService) { 
    this.accountService.currentUser$.subscribe(response=>{
      this.user=response;
    })
  }



  cancelOrder(order:any){
    return this.http.delete(this.baseUrl+"/"+order.id).pipe(
      map((response:any)=>{
        this.balanceService.getBalance(this.user).subscribe()
        this.getOrders().subscribe();
      })
    )
  }

  getOrders(){
    return this.http.get(this.baseUrl+'/'+this.user.id).pipe(
      map((response:OrderDetails[])=>{
        this.CurrentOrderSource.next(response);
      })
    );
  }



}
