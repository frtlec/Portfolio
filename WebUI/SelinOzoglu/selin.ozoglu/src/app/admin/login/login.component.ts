import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenSaveModel } from 'src/app/handler/ApiCredentialInterceptorService';
import { AuthService, PasswordTokenModel } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email:string;
  password:string;
  errorMessage:string="";
  errors:string[]=[];
  constructor(private router:Router,private authService:AuthService) { 
    this.email="";
    this.password="";
  }
  loginValidator(){
      this.errors=[];
      if(this.email==undefined || this.email==""){
        this.errors.push("Email adresi girilmedi")
      }
      if(this.password==undefined || this.password==""){
        this.errors.push("Şifre girilmedi")
      }
  }
  login(){
    this.loginValidator();

    if(this.errors.length>0){
      return;
    }
    this.authService.getPasswordToken(this.password,this.email).subscribe(
      success=>{
        const tokenModel=new TokenSaveModel<PasswordTokenModel>();
        tokenModel.token=new PasswordTokenModel();
        tokenModel.expire=new Date(new Date().getTime() + success.expires_in);
        tokenModel.token.access_token=success.access_token;
        tokenModel.token.refresh_token=success.refresh_token;
        tokenModel.token.token_type=success.token_type;
        localStorage.setItem("admin_token",JSON.stringify(tokenModel));
        this.router.navigateByUrl("/admin");
      },
      err=>{
       this.errors=err.error.errors
      }
    
    )
  }
  ngOnInit(): void {
  }

}
