import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserBalance } from '../models/balance.user';
import { User,UserDetails } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class BalanceService {
  baseUrl:string = "http://localhost:5021/budget"
  private currentUserBalance = new ReplaySubject<UserBalance>(1);
  currentUserBalance$ = this.currentUserBalance.asObservable();
  constructor(private http:HttpClient) { 

  }

  getBalance(user:User){
    return this.http.get(this.baseUrl+'/'+user.id).pipe(
      map((response:UserBalance)=>{
        user.balance = response.budget;
        this.currentUserBalance.next(response);
        localStorage.setItem('user',JSON.stringify(user));
        return user;
      })
    )
    }
}
