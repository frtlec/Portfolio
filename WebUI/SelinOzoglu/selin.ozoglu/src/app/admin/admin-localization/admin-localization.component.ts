import { Component, OnInit } from '@angular/core';
import { faImage, faPlus, faPlusSquare, faSave, faTrash } from '@fortawesome/free-solid-svg-icons';
import { LocalizationAddDto, LocalizationDto, LocalizationService, LocalizationType } from 'src/app/services/localization/localization.service';

@Component({
  selector: 'app-admin-localization',
  templateUrl: './admin-localization.component.html',
  styleUrls: ['./admin-localization.component.css']
})
export class AdminLocalizationComponent implements OnInit {
  faImage = faImage;
  faSave = faSave;
  faPlus = faPlus;
  faTrash = faTrash;
  faPlusSquare = faPlusSquare;
  localizationsData:LocalizationDto[];
  localizationTypes=[];
  localizationAdd:LocalizationAddDto=new LocalizationAddDto();
  constructor(private localizationService:LocalizationService) {
    this.getAll();
    this.localizationAdd.localizationType=0;
   this.localizationTypes= Object.keys(LocalizationType).map(key => LocalizationType[key]).filter(value => typeof value === 'string') as string[];
    
  }
  getAll(){
    this.localizationService.getAll().subscribe(res=>{
      this.localizationsData=res.body.data;
    })
  }

  submit(){
    this.localizationService.add(this.localizationAdd).subscribe(res=>{
      this.localizationsData.push(res.body.data)
    })
  }
  delete(_id:string){
    this.localizationService.delete(_id).subscribe(res=>{
      this.getAll();
    })
  }
  ngOnInit(): void {
  }

}
