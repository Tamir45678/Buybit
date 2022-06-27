import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user.model';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  constructor(private router:Router,public accountService:AccountService) {
    
  }

  ngOnInit(): void {
  }

  backToLandpage() : void{
    this.router.navigateByUrl('');
  }

  logout():void{
    this.accountService.logout();
    this.backToLandpage()
  }


}
