import { ErrorHandler, Injectable } from "@angular/core";
import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { Router } from "@angular/router";
import Swal from "sweetalert2";
import { LangApiMessages } from "src/langresource/HttpClientErrorHandlerLangResource";

@Injectable()
export class HttpClientErrorHandler implements ErrorHandler {

  static readonly REFRESH_PAGE_ON_TOAST_CLICK_MESSAGE: string = "An error occurred: Please click this message to refresh";
  static readonly DEFAULT_ERROR_TITLE: string = "Something went wrong";

  constructor(private router: Router) { };


  public handleError(error: HttpErrorResponse) {
    let httpErrorCode = error.status;
    console.log(error);
    switch (httpErrorCode) {
      case HttpStatusCode.Unauthorized:
        this.router.navigateByUrl("/login");
        break;
      case HttpStatusCode.Forbidden:
        this.router.navigateByUrl("/unauthorized");
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


      for (let item of err.errors) {
        let lang = LangApiMessages.find(f => f.key.toLocaleLowerCase() == item.toLocaleLowerCase())
        let message: string = lang == undefined || lang.value == "" ? item : lang.value;
        Swal.fire({
          icon: "error",
          text: message
        })
      }
    }



  }
}