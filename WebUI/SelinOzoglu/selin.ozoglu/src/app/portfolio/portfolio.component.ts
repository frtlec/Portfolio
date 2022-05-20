import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/internal/Observable';
import { siteTitle } from 'src/shared/constants/general';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL } from 'src/shared/constants/urlConstants';
import { WorkFilterModel } from '../models/inputModels/WorkAddModel';
import { CategorySMO } from '../models/serviceModels/CategorySMO';
import { WorkSMO } from '../models/serviceModels/WorkServiceModel';
import { GetValueFromLocalization } from '../pipes/_localization';
import { CategoryService } from '../services/category/category.service';
import { WorkServiceService } from '../services/work/work-service.service';
import { closeRightMenu } from '../state/defaultMenu/defaultMenu.actions';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.css'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({transform: 'translateY(10%)'}),
        animate('200ms ease-in-out', style({transform: 'translateY(0%)'}))
      ])
    ])
  ]
})
export class PortfolioComponent implements OnInit {
  photoStockPhotosFile: string = PHOTO_STOCK_API_PHOTOS_FILE_URL;
  currentPicture: string;
  currentDescription: string;
  works: WorkSMO[] = [];
  categories:CategorySMO[]=[];
  filter = new WorkFilterModel();
  closeResult: string = '';
  currentCategoryId:number=0;
  imageLoading:boolean=false;
  constructor(private workService: WorkServiceService, private modalService: NgbModal,private categoryService:CategoryService,private getValueFromLocalization:GetValueFromLocalization,private titleService:Title) {
    getValueFromLocalization.transform("Portfolyo").then(f=>titleService.setTitle(f+siteTitle));
    this.categoryService.getCategoriesByFilter().subscribe(
      res=>{
        this.categories=res.data;

        let All=new CategorySMO();
        All.id=0;
        All.title="All";
        this.categories.splice(0,0,All);
        this.currentCategoryId=res.data[0]?.id;
      }
    )
    this.filter.isActive = true;
    this.filter.limit = 9999;
    this.filter.search = "";
    this.filter.categoryId=0;
    this.filter.isShowMainPage=null;
    this.workService.getAllWork(this.filter).subscribe(
      res => {
        this.works = res.body.data;
      }
    )
   
  }

  filterCategory(categoryId:number=0){
    console.log(categoryId);
    this.currentCategoryId=categoryId;
    this.filter.categoryId=categoryId;

    this.workService.getAllWork(this.filter).subscribe(
      res => {
        this.works = res.body.data;
      }
    )
  }
  search(event) {
    this.filter.search = event.target.value;
    this.workService.getAllWork(this.filter).subscribe(
      res => {
        this.works = res.body.data;
      }
    )
  }
  open(content, workId) {

    this.imageLoading=true;
    if (!workId)
      return;
    this.workService.getWorkItemById(Number(workId)).subscribe(res => {
      this.currentPicture = this.photoStockPhotosFile + res.data[0]?.pictures[0];
      this.currentDescription = res.data[0]?.description;
      console.log(workId, this.currentPicture, this.currentDescription);
    });

    this.modalService.open(content, { windowClass: "modal-xxl", size: "xl" }).result.then(
      (result) => {
        this.currentPicture = undefined;
        this.currentDescription = undefined;
        this.closeResult = `Closed with: ${result}`;
      }, (reason) => {
        this.currentPicture = undefined;
        this.currentDescription = undefined;
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
