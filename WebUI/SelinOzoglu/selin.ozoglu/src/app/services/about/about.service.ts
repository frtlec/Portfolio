import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResponse } from 'src/shared/sharedModels/Response';
import {SETTING_API_BASE_URL} from 'src/shared/constants/urlConstants';
import { getClientCredential, getPasswordCredential } from 'src/app/handler/ApiCredentialInterceptorService';

@Injectable({
  providedIn: 'root'
})
export class AboutService {

  constructor(private _httpClient: HttpClient ) {
    
  }

  getAllByActive(active:boolean=null){
     
    
    const query=active==null?'':`?isActive=${active}`;
    return this._httpClient.get<IResponse<AboutPage>>(
      `${SETTING_API_BASE_URL}/aboutsetting${query}`,
      {  observe: 'response' ,context: getClientCredential()}
    );
  }
  save(inputModel:AboutPage){
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(inputModel);
    return this._httpClient.put<IResponse<AboutPage>>(
      `${SETTING_API_BASE_URL}/aboutsetting`,
      body,
      { 'headers': headers, observe: 'response', context: getPasswordCredential() }
    );
  }

}

export class AboutPage {
  portreFileName: string;
  slogan: string;
  summary: string;
  softwares: Software[];
  businesses: Business[];
  educations: Education[];
  certifacates: Certifacate[];
  projects: Project[];
  id: string;
  createdUserId: number;
  active: boolean;
  updatedUserId: number;
  createdDate: Date;
  updatedDate: Date;
}


export class Software {
  svgPath: string;
  softwareName: string;
  active: boolean;
  rowId:string;
}

export class Business {
  head: string;
  value: string;
  foot: string;
  active: boolean;
  rowId:string;
}

export class Education {
  head: string;
  value: string;
  foot: string;
  active: boolean;
  rowId:string;
}

export class Certifacate {
  head: string;
  value: string;
  active: boolean;
  rowId:string;
}
export class Project {
head: string;
value: string;
link: string;
languageType: number;
active: boolean;
rowId:string;
}
