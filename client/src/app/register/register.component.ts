import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormControl,Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 
  @Output() isMember = new EventEmitter()
  loginFormControl = new FormControl("",Validators.required)
  model:any={}
  hide=true;
  constructor(public accountService:AccountService,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    
  }

  register(){
    this.accountService.register(this.model).subscribe(
      response=>{
      console.log(response)
     },
    error=>{
      if(error.status===201){
        this._snackBar.open("User " +this.model.Username+ " Created Successfully!","",{
          duration:4000
        });
        this.goToLogin();
      }
      else
        this._snackBar.open(error.error,"",{
          duration:4000
        })
     }
    )
  }

  goToLogin(){
    this.isMember.emit(true);
  }

}
