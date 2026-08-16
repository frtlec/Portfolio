import { Pipe } from "@angular/core";

@Pipe({
    name: 'addUIDphotoUrl',
    standalone: false
})
export class AddUIDphotoUrl {
  constructor(){
   
  }

  transform(value: any, args?: any): any {
    return `${value}?${Math.floor(Math.random() * 5555555555552)}`;
  }
}