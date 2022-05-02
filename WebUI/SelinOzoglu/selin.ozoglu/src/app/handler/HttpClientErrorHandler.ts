import { ErrorHandler, Injectable } from "@angular/core";
import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { ActivatedRoute, NavigationEnd, Router } from "@angular/router";
import Swal from "sweetalert2";
import { LangApiMessages } from "src/langresource/HttpClientErrorHandlerLangResource";

@Injectable()
export class HttpClientErrorHandler implements ErrorHandler {

  static readonly REFRESH_PAGE_ON_TOAST_CLICK_MESSAGE: string = "An error occurred: Please click this message to refresh";
  static readonly DEFAULT_ERROR_TITLE: string = "Something went wrong";
  static readonly SERVICE_401: string = "services could not be accessed";
  static readonly SERVICE_403: string = "services could not be accessed";
  constructor(private router: Router,private routeA:ActivatedRoute) { };


  public handleError(error: HttpErrorResponse) {
    let httpErrorCode = error.status;
    console.log("x!!",error);
    
    switch (httpErrorCode) {
      case HttpStatusCode.Unauthorized:
       if(this.router.url.includes("admin")){
        localStorage.removeItem("admin_token");
       }else{
        localStorage.removeItem("client_token");
       }
       this.showError(HttpClientErrorHandler.SERVICE_401);
        break;
      case HttpStatusCode.Forbidden:
        this.showError(HttpClientErrorHandler.SERVICE_401);
        break;
      case HttpStatusCode.BadRequest:
        this.showError(error.error);
        break;
      default:
        this.showError(HttpClientErrorHandler.REFRESH_PAGE_ON_TOAST_CLICK_MESSAGE);
    }
  }



  private showError(err: any) {
    if (typeof (err) == "string") {
      Swal.fire({
        icon: "error",
        text: err
      })
    }
    if (typeof (err) == "object") {

      let message: string="";
      for (let item of err.errors) {
        let lang = LangApiMessages.find(f => f.key.toLocaleLowerCase() == item.toLocaleLowerCase())
        message+= lang == undefined || lang.value == "" ? item : lang.value;
        message+="<br/>"
      
      }
      Swal.fire({
        icon: "error",
        html: message
      })
    }
  }
}