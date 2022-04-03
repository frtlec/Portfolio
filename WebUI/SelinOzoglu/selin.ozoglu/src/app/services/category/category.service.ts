import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getClientCredential,getPasswordCredential } from 'src/app/handler/ApiCredentialInterceptorService';
import { CategoryInput } from 'src/app/models/inputModels/CategoryInput';
import { CategoryAddSMO, CategorySMO, CategoryUpdateSMO } from 'src/app/models/serviceModels/CategorySMO';
import { WORK_API_BASE_URL } from 'src/shared/constants/urlConstants';
import { IResponse } from 'src/shared/sharedModels/Response';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private _httpClient:HttpClient) { }
  getCategoriesByFilter(isActive: boolean=true) {

    let linkQuery:string="";
    if(isActive!=null){
      linkQuery="?isActive="+isActive;
    }
    return this._httpClient.get<IResponse<CategorySMO[]>>(`${WORK_API_BASE_URL}/api/categories/getcategories${linkQuery}`,{
      context: getClientCredential()
    });
  }
  getCategoryById(categoryId:number){
    return this._httpClient.get<IResponse<CategorySMO>>(`${WORK_API_BASE_URL}/api/categories/get/${categoryId}`,{
      context: getClientCredential()
    });
  }
  createCategory(inputModel:CategoryInput){
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(inputModel);
    return this._httpClient.post<IResponse<CategoryAddSMO>>(
      `${WORK_API_BASE_URL}/api/categories/addcategory`,
      body,
      { 'headers': headers, observe: 'response', context: getPasswordCredential() }
    );
  }
  updateCategory(inputModel:CategoryInput){
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(inputModel);
    return this._httpClient.post<IResponse<CategoryUpdateSMO>>(
      `${WORK_API_BASE_URL}/api/categories/UpdateCategory`,
      body,
      { 'headers': headers, observe: 'response', context: getPasswordCredential() }
    );
  }
  deleteCategory(categoryId:number){
    return this._httpClient.delete(`${WORK_API_BASE_URL}/api/categories/delete/${categoryId}`,{ context: getPasswordCredential()});
  }
}
