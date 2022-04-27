import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IResponse } from 'src/shared/sharedModels/Response';
import { WorkAndWorkITemsSMO, WorkItemSMO, WorkSMO } from 'src/app/models/serviceModels/WorkServiceModel';
import { SaveWorkSMO } from 'src/app/models/serviceModels/SaveWorkSMO';
import { WorkAddModel, WorkFilterModel } from 'src/app/models/inputModels/WorkAddModel';
import { WORK_API_BASE_URL } from 'src/shared/constants/urlConstants';
import { WorkUpdateModel } from 'src/app/models/inputModels/WorkUpdateModel';
import { getClientCredential, getPasswordCredential } from 'src/app/handler/ApiCredentialInterceptorService';


@Injectable({
  providedIn: 'root'
})
export class WorkServiceService {

  constructor(private _httpClient: HttpClient) { }

  getAllWork(filter: WorkFilterModel) {
    console.log("filter",filter);
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(filter);
    console.log(WORK_API_BASE_URL);
    return this._httpClient.post<IResponse<WorkSMO[]>>(
      `${WORK_API_BASE_URL}/works/getworks`,
      body,
      { 'headers': headers, observe: 'response' ,context: getClientCredential()}
    );
  }
  getWorkByWorkId(workId: number) {
    return this._httpClient.get<IResponse<WorkAndWorkITemsSMO>>(`${WORK_API_BASE_URL}/works/get/${workId}`,{context: getClientCredential()});
  }
  getWorkItemById(workId: number) {
    return this._httpClient.get<IResponse<WorkItemSMO[]>>(`${WORK_API_BASE_URL}/workitems/getworkitems/${workId}`,{context: getClientCredential()});
  }
  saveWork(work: WorkAddModel) {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(work);
    return this._httpClient.post<IResponse<SaveWorkSMO>>(
      `${WORK_API_BASE_URL}/Works/savework`,
      body,
      { 'headers': headers, observe: 'response', context: getPasswordCredential() }
    );
  }
  updateWork(work:WorkUpdateModel){
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(work);
    return this._httpClient.post<IResponse<SaveWorkSMO>>(
      `${WORK_API_BASE_URL}/Works/updatework`,
      body,
      { 'headers': headers, observe: 'response', context: getPasswordCredential() }
    );
  }
  deleteWork(workId:number){
    return this._httpClient.delete(`${WORK_API_BASE_URL}/works/delete/${workId}`,{context: getPasswordCredential()});
  }
}
