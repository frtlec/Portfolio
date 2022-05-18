import { Pipe } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { LocalizationType } from "../services/localization/localization.service";

@Pipe({name: 'getLocalizationName'})
export class GetLocalizationName {
  constructor(){
   
  }

  transform(value: any, args?: any): any {


    let keys = Object.keys(LocalizationType).filter(x => LocalizationType[x] == value);
    return keys.length > 0 ? keys[0] : null;
  }
}