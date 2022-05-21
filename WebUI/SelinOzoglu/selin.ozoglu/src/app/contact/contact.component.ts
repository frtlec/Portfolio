import { animate, keyframes, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { faUserLarge ,faUserTie,faUserCheck} from '@fortawesome/free-solid-svg-icons';
import { siteTitle } from 'src/shared/constants/general';
import Swal from 'sweetalert2';
import { ContactModel } from '../models/inputModels/MailSenderContactInput';
import { CategorySMO } from '../models/serviceModels/CategorySMO';
import { GetValueFromLocalization } from '../pipes/_localization';
import { CategoryService } from '../services/category/category.service';
import { ContactService } from '../services/contact/contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
  animations: [
    trigger('gender', [
      // ...
      transition(':enter', animate('500ms linear', keyframes([
        // style({ opacity: 0 }),
        // style({ opacity: 0 }),
        // style({ opacity: 0.25 }),
        // style({ opacity: 0.30 }),
        // style({ opacity: 0.50 }),
        // style({ opacity: 1 }),
        style({ transform: "rotateY(0)", color: "transparent" }),
        style({ transform: "rotateY(180deg)", color: "transparent"}),
        style({ transform: "rotateY(360deg)",color: "#f8f8f8" }),
      ])))
    ]),
  ],
})
export class ContactComponent implements OnInit {
  faUserLarge=faUserLarge;
  faUserTie=faUserTie;
  faUserCheck=faUserCheck;

  contactModel:ContactModel=new ContactModel();
  availableMessageCount:number=1000;
  messageCount:number=this.availableMessageCount;

  categories:CategorySMO[]=new Array<CategorySMO>();
  isSubmit:boolean=false;

  constructor(private contactService:ContactService,private categoryService:CategoryService,private getValueFromLocalization:GetValueFromLocalization, private titleService:Title) { 
    this.getValueFromLocalization.transform("İletişim").then(f=>this.titleService.setTitle(f+siteTitle));


  }

  ngOnInit(): void {
    this.categoryService.getCategoriesByFilter().subscribe(res=>{
      this.categories=res.data;

      let other=new CategorySMO();
      other.description="Diğer";
      other.title="Diğer"
      this.categories.push(other)
    })

    this.contactModel.categoryId=-1;
    this.contactModel.gender="";
  
  }
  messageCounter(){
    this.messageCount=this.availableMessageCount-this.contactModel.message?.length;
  }
  errorMessageWriter(errorMessage: string) {
    Swal.fire({
      icon: "error",
      text: errorMessage
    })
  }
 validateEmail = (email) => {
    return String(email)
      .toLowerCase()
      .match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      );
  };
  validator(){
    let errors:Array<string>=[];

    if(this.contactModel.categoryId==undefined || this.contactModel.categoryId==0){
      errors.push("Konu boş bırakılamaz!")
    }
    if(this.contactModel.email==undefined || this.contactModel.email==""){
      errors.push("Email alanı boş bırakılamaz!")
    }else if(this.contactModel.email?.length>=150){
      errors.push("Email alanı en fazla 150 karakter olabilir ")
    }
    if(this.contactModel.fullName==undefined || this.contactModel.fullName==""){;
      errors.push("Ad ve soyad alanı boş bırakılamaz!")
    }else if(this.contactModel.fullName?.length>=100){
      errors.push("Ad ve soyad alanı en fazla 100 karakter olabilir ")
    }
    if(this.contactModel.message==undefined || this.contactModel.message==""){
      errors.push("Mesaj alanı boş bıraklamaz!")
    }
    else if(this.contactModel.message?.length>1000){
      errors.push("Mesaj alanı en fazla 1000 karakter olabilir!")
    }
    if(this.validateEmail(this.contactModel.email)==null){
      errors.push("Email adresi geçersiz!")
    }


    


    if(errors.length>0){
      errors.forEach(f=>{
        this.getValueFromLocalization.transform(f).then(x=>{
          Swal.fire({
            icon: "error",
            html: x
          });
        });
      })
  
      return false;
    }
    return true;
    
  }
  submit():void{
    if(this.validator()==false){
      return;
    }
    this.isSubmit=true;
    this.contactModel.categoryName=this.categories.find(f=>f.id==this.contactModel.categoryId)?.title;
    this.contactService.createContact(this.contactModel).subscribe(res=>{
      Swal.fire({
        icon: "success",
        text: "Success Sent"
      });

      this.resetForm();
    },
    null,
    ()=>{
      this.isSubmit=false;
    }
    );
  }
  resetForm(){
    this.contactModel=new ContactModel();
    this.contactModel.categoryId=-1;
    this.contactModel.gender="";
  }

}
