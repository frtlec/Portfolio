import { HttpClient, HttpContext, HttpContextToken, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpXhrBackend } from "@angular/common/http";
import { nullSafeIsEquivalent } from "@angular/compiler/src/output/output_ast";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { from } from "rxjs";
import * as moment from 'moment';
import { AuthService, ClientCredentialTokenModel, PasswordTokenModel } from "../services/auth/auth.service";
import { IDENTITY4_SERVER_BASE_URL, WORK_API_BASE_URL } from "src/shared/constants/urlConstants";
const CLIENTCREDENTIAL = new HttpContextToken<boolean>(() => false);
const PASSWORDCREDENTIAL = new HttpContextToken<boolean>(() => false);
export function getClientCredential() {
  return new HttpContext().set(CLIENTCREDENTIAL, true);
}
export function getPasswordCredential() {
  return new HttpContext().set(PASSWORDCREDENTIAL, true);
}
@Injectable({ providedIn: 'root' })
export class ApiCredentialInterceptorService implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router, private _httpClient: HttpClient) { }
  intercept(req: HttpRequest<any>, next: HttpHandler) {

    if (req.context.get(PASSWORDCREDENTIAL)) {
      return from(this.adminAccess(req, next));
    }
    if (req.context.get(CLIENTCREDENTIAL)) {
      return from(this.clientAccess(req, next));
    }
    return next.handle(req);

  }
  private async adminAccess(req: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {
    Date.prototype.toJSON = function () { return moment(this).format(); }
    let getLocalStoreToken = localStorage.getItem("admin_token");

    let accessToken: TokenSaveModel<PasswordTokenModel> = new TokenSaveModel<PasswordTokenModel>();

    if (getLocalStoreToken == null) {

      this.router.navigateByUrl("/login")

    } else {
      accessToken = <TokenSaveModel<PasswordTokenModel>>JSON.parse(getLocalStoreToken);
    }

    if (new Date() >= new Date(accessToken.expire)) {
      this.router.navigateByUrl("/login");
      return next.handle(req).toPromise();
    }
    let modifiedRequest = req.clone({
      headers: req.headers.set('Authorization', `${accessToken.token.token_type} ${accessToken.token.access_token}`)
    });
    return next.handle(modifiedRequest).toPromise();
  }
  private async clientAccess(req: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {
    Date.prototype.toJSON = function () { return moment(this).format(); }

    let getLocalStoreToken = localStorage.getItem("client_token");
    let accessToken: TokenSaveModel<ClientCredentialTokenModel> = new TokenSaveModel<PasswordTokenModel>();



    if (getLocalStoreToken == null) {

      let authServiceResult = await (await this.authService.getClientCredentialToken()).toPromise();
      accessToken.token = authServiceResult;
      accessToken.expire = new Date(new Date().getTime() + authServiceResult.expires_in * 1000);
      localStorage.setItem("client_token", JSON.stringify(accessToken));
    } else {

      accessToken = <TokenSaveModel<ClientCredentialTokenModel>>JSON.parse(getLocalStoreToken);
      const checkIsValidClientToken= await this.checkIsValidClientToken(accessToken.token.token_type,accessToken.token.access_token);
     
      if (new Date() >= new Date(accessToken.expire) || checkIsValidClientToken==false) {
        let authServiceResult = await (await this.authService.getClientCredentialToken()).toPromise();
        accessToken.token = authServiceResult;
        accessToken.expire = new Date(new Date().getTime() + authServiceResult.expires_in * 1000);
        localStorage.removeItem("client_token");
        localStorage.setItem("client_token", JSON.stringify(accessToken));
      }
    }
    let modifiedRequest = req.clone({
      headers: req.headers.set('Authorization', `${accessToken.token.token_type} ${accessToken.token.access_token}`)
    });
    return next.handle(modifiedRequest).toPromise();
  }
  async checkIsValidClientToken(type:string,currentToken: string) {

    const headers = { 'content-type': 'application/json', 'Authorization':`${type} ${currentToken}` }
    try {
       await this._httpClient.get(
        `${WORK_API_BASE_URL}/values/ClientTokenValidator`,
        {headers,responseType:"text"}
      ).toPromise();
      return true;
    } catch (error) {
      return false;
    }
  }
}

export class TokenSaveModel<T>{
  expire: Date;
  token: T;
}


