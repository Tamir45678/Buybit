import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable, ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import {User} from '../models/user.model';
import { BalanceService } from './balance.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'http://localhost:5021/';

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  httpOptions = new HttpHeaders(
    {
      'Content-Type':'application/json',
      'Accept':'*/*'
    })
  constructor(private http:HttpClient,private balanceService:BalanceService) {

   }

   login(model:any){
     return this.http.post(this.baseUrl+'login',model,{observe:'response'}).pipe(
       map((response:any)=>{
         if(response.status===200){
           const user = response.body
           this.currentUserSource.next(user);
         }
       })
     )
   }

   register(model:any){
     console.log(model)
     return this.http.post(this.baseUrl+'register',model,{observe:'response'}).pipe(
       map((response:any)=>{
        if(response.status!==201) return response.body;
        return response
      })
     )
   }

   setCurrentUser(user:User){
     this.currentUserSource.next(user);
   }

   logout(){
     localStorage.removeItem('user');
     this.currentUserSource.next(null);
   }
}
