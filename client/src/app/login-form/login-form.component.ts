import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  
  @Output() newAccount = new EventEmitter()
  @Output() userLogged = new EventEmitter()
  hide=true;
  model:any={}
  constructor(public accountService:AccountService,private _snackBar: MatSnackBar,private router:Router) {
    
   }

  ngOnInit(): void {
    
  }

  login():void{
    this.accountService.login(this.model).subscribe(_=>{
      this.userLogged.emit();
    },
    error=>{
      console.log(error.statusText + ": " + error.error)
      this.ErrorSnackBar(error.error,"Try Again");
    })
  }

  goToRegister(){
    this.newAccount.emit(false);
  }

  ErrorSnackBar(message:string,action:string){
    this._snackBar.open(message,action,{
      duration:4000
    })
  }


}
