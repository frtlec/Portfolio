import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL } from 'src/shared/constants/urlConstants';
import { WorkFilterModel } from '../models/inputModels/WorkAddModel';
import { CategorySMO } from '../models/serviceModels/CategorySMO';
import { WorkSMO } from '../models/serviceModels/WorkServiceModel';
import { CategoryService } from '../services/category/category.service';
import { WorkServiceService } from '../services/work/work-service.service';

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
  constructor(private workService: WorkServiceService, private modalService: NgbModal,private categoryService:CategoryService) {

    this.filter.isActive = true;
    this.filter.limit = 9999;
    this.filter.search = "";
    this.filter.categoryId=0;
    this.workService.getAllWork(this.filter).subscribe(
      res => {
        this.works = res.body.data;
      }
    )
    this.categoryService.getCategoriesByFilter().subscribe(
      res=>{
        this.categories=res.data;
      }
    )
  }

  filterCategory(categoryId:number){
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
