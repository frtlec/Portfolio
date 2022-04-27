import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getClientCredential } from 'src/app/handler/ApiCredentialInterceptorService';
import { ContactModel, MailSenderContactCreaeteInput } from 'src/app/models/inputModels/MailSenderContactInput';
import { MAIL_SENDER_API_BASE_URL } from 'src/shared/constants/urlConstants';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(private _httpClient:HttpClient) { }

  createContact(contactModel:ContactModel){

    const inputModel=new MailSenderContactCreaeteInput();
    if(contactModel.message && contactModel.categoryId!=0 && contactModel.email){
      inputModel.fromMail="selinportfolio@gmail.com";
      inputModel.subject="SelinOzoglu.com iletisim sayfasi";
      inputModel.content=`
        <ul>
            <li>İsim:${contactModel.fullName}</li>
            <li>Email:${contactModel.email}</li>
            <li>Telefon:${contactModel.phone}</li>
            <li>Kategori:${contactModel.categoryName}</li>
            <li>Cinsiyet:${contactModel.gender}</li>
        <ul>
        <br/>
        <br/>
        <h4>Mesaj</h4>
        <p>${contactModel.message}</p>
  
      `;
      inputModel.categoryId=contactModel.categoryId;
      inputModel.categoryName=contactModel.categoryName;
    }


    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(inputModel);
    return this._httpClient.post(
      `${MAIL_SENDER_API_BASE_URL}/Contacts/create`,
      body,
      { 'headers': headers, observe: 'response',context:getClientCredential() }
    );
  }
}
