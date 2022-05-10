import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getPasswordCredential } from 'src/app/handler/ApiCredentialInterceptorService';
import { PhotoSMO } from 'src/app/models/serviceModels/PhotoSMO';
import { PHOTO_STOCK_API_BASE_URL } from 'src/shared/constants/urlConstants';
import { IResponse } from 'src/shared/sharedModels/Response';

@Injectable({
  providedIn: 'root'
})
export class PhotostockService {

  constructor(private _httpClient: HttpClient) { }

  savePhotoWithPNAme(image:File,title:string,photoType:number){
    let formData = new FormData();
    formData.append('image', image);
    formData.append('title', title);
 
    let result;
    if(photoType==1){
      result=this._httpClient.post<IResponse<PhotoSMO>>(
        `${PHOTO_STOCK_API_BASE_URL}/photos/PhotoSquareSave`,
        formData,
        {context:getPasswordCredential()}
      );
    }
    if(photoType==2){
      result=this._httpClient.post<IResponse<PhotoSMO>>(
        `${PHOTO_STOCK_API_BASE_URL}/photos/PhotoSaveWithPName`,
        formData,
        {context:getPasswordCredential()}
      );
    }
    return result;
  }
  savePhoto(image:File,photoType:number){
    let formData = new FormData();
    formData.append('photo', image);
 
    let result;
    if(photoType==1){
      result=this._httpClient.post<IResponse<PhotoSMO>>(
        `${PHOTO_STOCK_API_BASE_URL}/photos/PhotoSquareSave`,
        formData,
        {context:getPasswordCredential()}
      );
    }
    if(photoType==2){
      result=this._httpClient.post<IResponse<PhotoSMO>>(
        `${PHOTO_STOCK_API_BASE_URL}/photos/PhotoSaveRectangle`,
        formData,
        {context:getPasswordCredential()}
      );
    }
    return result;
  }
  deletePhoto(photoName:string){
    
    return this._httpClient.delete(`${PHOTO_STOCK_API_BASE_URL}/photos/photodelete/${photoName}`,{context:getPasswordCredential()});
  }
}
