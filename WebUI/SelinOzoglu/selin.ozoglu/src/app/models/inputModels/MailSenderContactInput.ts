export class MailSenderContactCreaeteInput{
    fromMail:string;
    subject:string;
    content:string;
    categoryId:number;
    categoryName:string;
}

export class ContactModel {
    fullName:string;
    email:string;
    phone:string;
    gender:string;
    categoryId:number;
    categoryName:string;
    message:string;
 }
 