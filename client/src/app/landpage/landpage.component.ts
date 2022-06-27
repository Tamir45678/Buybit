import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';
import { AccountService } from '../_services/account.service';
import { BalanceService } from '../_services/balance.service';
import { MarketplaceService } from '../_services/marketplace.service';
 
@Component({
  selector: 'app-landpage',
  templateUrl: './landpage.component.html',
  styleUrls: ['./landpage.component.css']
})

export class LandpageComponent implements OnInit {
  
  isMember:boolean = true;
  constructor(private router:Router,public accountService:AccountService,private balanceService:BalanceService,private marketService:MarketplaceService) { }

  ngOnInit(): void {
    this.checkUser();
  }
    
  setFormModal(event:boolean){
    this.isMember=event;
  }
  
  setUser(){
    this.accountService.currentUser$.subscribe(response=>{   
      if(response){
        this.balanceService.getBalance(response).subscribe()
        this.marketService.getProducts().subscribe();
        this.router.navigateByUrl("Marketplace");
      }
    }) 
  }

  checkUser(){
    if(localStorage.getItem('user')) 
      this.setUser();
  }
}
