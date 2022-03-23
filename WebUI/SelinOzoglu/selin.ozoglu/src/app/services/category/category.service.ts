import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CategorySMO } from 'src/app/models/serviceModels/CategorySMO';
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
    return this._httpClient.get<IResponse<CategorySMO[]>>(`${WORK_API_BASE_URL}/api/categories/getcategories${linkQuery}`);
  }
}
