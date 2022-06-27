import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { User } from '../models/user.model';
import { AccountService } from './account.service';
import { BalanceService } from './balance.service';
import { OrdersService } from './orders.service';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceService {
  baseUrl = 'http://localhost:5021/products';
  private CurrentProductsSource = new ReplaySubject<Product[]>(1);
  currentProduct$ = this.CurrentProductsSource.asObservable();
  user:User

  constructor(private http:HttpClient,private accountService:AccountService,private orderService:OrdersService,private balanceService:BalanceService) { 
    this.accountService.currentUser$.subscribe(response=>{
      this.user=response;
    })
  }
  
  placeOrder(modal:any){
    console.log(modal)
    return this.http.post(this.baseUrl,modal).pipe(
      map((response:any)=>{
        setTimeout(()=>{
          this.balanceService.getBalance(this.user).subscribe()
          this.orderService.getOrders().subscribe();
        },2000)
      })
    )
  }

  getProducts(){
    return this.http.get(this.baseUrl).pipe(
      map((response:Product[])=>{
        this.CurrentProductsSource.next(response);
      })
    )
  }






}
