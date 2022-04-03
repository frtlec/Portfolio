import { animate, keyframes, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { faUserLarge ,faUserTie,faUserCheck} from '@fortawesome/free-solid-svg-icons';
import Swal from 'sweetalert2';
import { ContactModel } from '../models/inputModels/MailSenderContactInput';
import { CategorySMO } from '../models/serviceModels/CategorySMO';
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

  categories:CategorySMO[]=new Array<CategorySMO>();
  constructor(private contactService:ContactService,private categoryService:CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getCategoriesByFilter().subscribe(res=>{
      this.categories=res.data;
    })

    this.contactModel.categoryId=-1;
    this.contactModel.gender="";
  
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
      errors.push("Please enter category subject");
    }
    if(this.contactModel.email==undefined || this.contactModel.email==""){
      errors.push("Please enter your mail");
    }
    if(this.contactModel.fullName==undefined || this.contactModel.fullName==""){
      errors.push("Please enter your fullname");
    }
    if(this.contactModel.message==undefined || this.contactModel.message==""){
      errors.push("Please enter your message");
    }
    if(this.validateEmail(this.contactModel.email)==null){
      errors.push("your email address is invalid");
    }
    if(errors.length>0){
      Swal.fire({
        icon: "error",
        html: errors.join('<br/>')
      });
      return false;
    }
    return true;
    
  }
  submit():void{
    if(this.validator()==false){
      return;
    }
    this.contactModel.categoryName=this.categories.find(f=>f.id==this.contactModel.categoryId)?.title;
    this.contactService.createContact(this.contactModel).subscribe(res=>{
      Swal.fire({
        icon: "success",
        text: "Success Sent"
      });

      this.resetForm();
    });
  }
  resetForm(){
    this.contactModel=new ContactModel();
    this.contactModel.categoryId=-1;
    this.contactModel.gender="";
  }

}
