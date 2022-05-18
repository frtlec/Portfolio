import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResponse } from 'src/shared/sharedModels/Response';
import { SETTING_API_BASE_URL } from 'src/shared/constants/urlConstants';
import { getClientCredential, getPasswordCredential } from 'src/app/handler/ApiCredentialInterceptorService';


@Injectable({
  providedIn: 'root'
})
export class LocalizationService {

  constructor(private _httpClient: HttpClient) {

  }
  getAll(active: boolean = null) {
    return this._httpClient.get<IResponse<LocalizationDto[]>>(
      `${SETTING_API_BASE_URL}/Localization/GetAll`,
      { observe: 'response', context: getClientCredential() }
    );
  }
  async getByCulture(key:string){

    const inputModel=new LocalizationGetByCultureDto();
    const headers = { 'content-type': 'application/json' }
    inputModel.localizationType=1;
    inputModel.key=key;
    const body = JSON.stringify(inputModel);
    let result= await this._httpClient.post<IResponse<LocalizationGetByCultureDtoResponse>>(
      `${SETTING_API_BASE_URL}/Localization/GetByCulture`,
      inputModel,
      { 'headers': headers, observe: 'response', context: getClientCredential() }
    );

    return result;
  }
  add(inputModel: LocalizationAddDto){
    const headers = { 'content-type': 'application/json' }
    inputModel.localizationType=parseInt(inputModel.localizationType.toString());
    const body = JSON.stringify(inputModel);
    return this._httpClient.put<IResponse<LocalizationDto>>(
      `${SETTING_API_BASE_URL}/Localization/Add`,
      inputModel,
      { 'headers': headers, observe: 'response', context: getPasswordCredential() }
    );
  }
  delete(id:string){
    return this._httpClient.delete<IResponse<LocalizationDto[]>>(
      `${SETTING_API_BASE_URL}/Localization?id=${id}`,
      { observe: 'response', context: getPasswordCredential() }
    );
  }
}

export class LocalizationDto {
  key: string;
  value: string;
  localizationType: number;
  id: string;
  createdUserId: number;
  updatedUserId: number;
  createdDate?: Date;
  updatedDate?: Date;
}
export class LocalizationAddDto{
  key: string;
  value: string;
  localizationType: LocalizationType;
}
export class LocalizationGetByCultureDto{
  key:string;
  localizationType: LocalizationType;
}
export class LocalizationGetByCultureDtoResponse{
  value:string;
  localizationType: LocalizationType;
  message:string;
}
export enum LocalizationType {
  EN = 1,
  FR = 2
}
