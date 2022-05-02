import { CanActivate, ActivatedRouteSnapshot, RouterState, RouterStateSnapshot, Router } from '@angular/router';
import {Injectable} from '@angular/core';
import { PasswordTokenModel } from '../services/auth/auth.service';
import { TokenSaveModel } from '../handler/ApiCredentialInterceptorService';

@Injectable()
export class AdminGuard implements CanActivate {

    constructor(private router:Router) {
     
        
    }

   canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot):boolean{
    let logged=localStorage.getItem("admin_token");
    console.log("accessToken",logged)
    if(logged==null){
        this.router.navigateByUrl("/login");
        return false;
    }
    let accessToken:TokenSaveModel<PasswordTokenModel>=new TokenSaveModel<PasswordTokenModel>();

    accessToken=<TokenSaveModel<PasswordTokenModel>>JSON.parse(logged);
    if(new Date()>new Date(accessToken.expire)){
        this.router.navigateByUrl("/login");
        return false;
    }
    return true;

   }




}