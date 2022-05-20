import { Pipe } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { LocalizationService, LocalizationType } from "../services/localization/localization.service";

@Pipe({name: 'getValueFromLocalization'})
export class GetValueFromLocalization {
  constructor(private localizationService:LocalizationService){
   
  }

  async transform(value: any, args?: any): Promise<any> {

    let localStorageLang=localStorage.getItem("lang");
    let localizationType=<number>JSON.parse(localStorageLang)==null?0:<number>JSON.parse(localStorageLang);
    if(localizationType==0)
     return value;

    let val=value;
    if(val==null){
      val="";
    }
    if(typeof(val)=='object'){
        val=value.changingThisBreaksApplicationSecurity;
    }
    
    
    let result= await (await this.localizationService.getByCulture(val)).toPromise();
    val=result.body.data.value;
    return val;
  }
}