import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IDENTITY4_SERVER_BASE_URL } from 'src/shared/constants/urlConstants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _httpClient: HttpClient ) {
    
   }

  async getClientCredentialToken(){
    const headers = { 'content-type': 'application/x-www-form-urlencoded' }
    const body = new HttpParams()
    .set('client_id', "SelinOzogluUI")
    .set('client_secret', "4sxQ54123!1x8Ss23.?")
    .set('grant_type', "client_credentials");
    let result= await this._httpClient.post<ClientCredentialTokenModel>(`${IDENTITY4_SERVER_BASE_URL}/connect/token`,body.toString(),{headers:headers});
    return result;
  }
  getPasswordToken(password:string,email:string){
    const headers = { 'content-type': 'application/x-www-form-urlencoded' }
    const body = new HttpParams()
    .set('client_id', "SelinOzogluUIAdminPanel")
    .set('client_secret', "7sxQ54123!.19DSs23")
    .set('grant_type', "password")
    .set('username', email)
    .set('password', password);
    return this._httpClient.post<PasswordTokenModel>(`${IDENTITY4_SERVER_BASE_URL}/connect/token`,body.toString(),{headers:headers});
  }
   getRefreshToken(refreshToken:string){
    const headers = { 'content-type': 'application/x-www-form-urlencoded' }
    const body = new HttpParams()
    .set('client_id', "SelinOzogluUIAdminPanel")
    .set('client_secret', "7sxQ54123!.19DSs23")
    .set('grant_type', "refresh_token")
    .set('refresh_token', refreshToken)
    return this._httpClient.post<RefreshTokenModel>(`${IDENTITY4_SERVER_BASE_URL}/connect/token`,body.toString(),{headers:headers});;
  }


}


export class ClientCredentialTokenModel{
  access_token:string;
  expires_in:number;
  scope:string;
  token_type:string;
}
export class PasswordTokenModel{
  access_token:string;
  expires_in:number;
  scope:string;
  token_type:string;
  refresh_token:string;
}
export class RefreshTokenModel{
  id_token:string;
  access_token:string;
  expires_in:number;
  scope:string;
  token_type:string;
  refresh_token:string;
}