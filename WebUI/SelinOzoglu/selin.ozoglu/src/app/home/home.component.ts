import { Component, OnInit } from '@angular/core';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL } from 'src/shared/constants/urlConstants';
import { WorkSMO } from '../models/serviceModels/WorkServiceModel';
import { WorkServiceService } from '../services/work/work-service.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { WorkFilterModel } from '../models/inputModels/WorkAddModel';
import { Title } from '@angular/platform-browser';
import { GetValueFromLocalization } from '../pipes/_localization';
import { siteTitle } from 'src/shared/constants/general';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  works: WorkSMO[] = [];
  photoStockPhotosFile: string = `${PHOTO_STOCK_API_PHOTOS_FILE_URL}`;
  currentPicture: string = "";
  currentDescription:string="";
  closeResult: string = '';
  imageLoading:boolean=false;
  constructor(private workService: WorkServiceService, private modalService: NgbModal,private titleService:Title,private getValueFromLocalization:GetValueFromLocalization) {
   
    getValueFromLocalization.transform("Anasayfa").then(f=>titleService.setTitle(f+siteTitle));
    
   
    const filter=new WorkFilterModel();
    filter.isActive=true;
    filter.limit=9999;
    filter.search="";
    this.workService.getAllWork(filter).subscribe(res => {
      this.works=res.body.data;
    })

  }
  open(content,workId) {

    this.imageLoading=true;
   if (!workId)
      return;
    this.workService.getWorkItemById(Number(workId)).subscribe(res => {
        this.currentPicture=this.photoStockPhotosFile+res.data[0]?.pictures[0];
        this.currentDescription=res.data[0]?.description;
        console.log(workId,this.currentPicture, this.currentDescription);
    });

    this.modalService.open(content, { windowClass: "modal-xxl", size: "xl" }).result.then(
    (result) => {
      this.currentPicture=undefined;
      this.currentDescription=undefined;
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.currentPicture=undefined;
      this.currentDescription=undefined;
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
   onImageLoad(){
    setTimeout(() => {
      this.imageLoading=false;
    }, 600);
   }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  ngOnInit(): void {
  }

}
