import { Component, ElementRef, OnInit } from '@angular/core';
import { PhotostockService } from 'src/app/services/photostock/photostock.service';
import { faImage, faSave, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import Swal from 'sweetalert2';
import { WorkServiceService } from 'src/app/services/work/work-service.service';
import { WorkAddModel, WorkItemAddModel } from 'src/app/models/inputModels/WorkAddModel';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ViewChild } from '@angular/core';
import { CategorySMO } from 'src/app/models/serviceModels/CategorySMO';
import { CategoryService } from 'src/app/services/category/category.service';
@Component({
  selector: 'app-admin-work-add',
  templateUrl: './admin-work-add.component.html',
  styleUrls: ['./admin-work-add.component.css']
})
export class AdminWorkAddComponent implements OnInit {

  faImage = faImage;
  faSave = faSave;
  faPlus = faPlus;
  faTrash = faTrash;

  title: string;
  description: string;
  mainPicture: string;
  mainPictureSRC: string;
  detailPicture: string;
  detailPictureSRC: string;
  isActive: boolean;
  categoryID:number=0;
  photoStockApiURL: string = "https://localhost:5012/photos/";
  workItem: WorkItemAddModel = new WorkItemAddModel();
  closeResult: string = '';
  categories: CategorySMO[]=[];

  @ViewChild('detailPhotoInput')
  detailPhotoInputVariable: ElementRef;
  @ViewChild('mainPhotoInput')
  mainPhotoInputVariable: ElementRef;

  constructor(private photostockService: PhotostockService, private workService: WorkServiceService, private modalService: NgbModal, private categoryService: CategoryService) {
    this.description = "";
    this.title = "";
    this.mainPicture = "";
    this.isActive = true;
    categoryService.getCategoriesByFilter(null).subscribe(res => {
      console.log(res);
      this.categories =res.data;
    });
    
  }
  open(content) {
    if (this.detailPicture == undefined || this.detailPicture == "")
      return;
    this.modalService.open(content, { windowClass: "modal-xxl", size: "xl" }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  ngOnInit(): void {
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
  resetElement(element: ElementRef<any>) {
    element.nativeElement.value = "";
  }
  mainPhotoChange(event: any) {
    if (this.title == "") {
      Swal.fire({
        icon: "error",
        text: "Fotoğraf yükleyebilmek için iş adı dolu olmalıdır"
      })
      this.resetElement(this.mainPhotoInputVariable);
      return;
    }
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      let image: File = fileList[0];

      this.photoDelete(this.mainPicture);
      this.photostockService.savePhoto(image, this.title + "-main", 1)
        .subscribe(
          res => {

            this.mainPictureSRC = `${this.photoStockApiURL}${res.data.url}?${Math.floor(Math.random() * 5555555555552)}`;
            this.mainPicture = res.data.url;
            this.resetElement(this.mainPhotoInputVariable);
          }
        );
    }
  }

  photoDelete(photoName: string) {
    if (photoName == undefined || photoName == "")
      return;
    this.photostockService.deletePhoto(photoName).subscribe(res => console.log(res));
  }
  clearForm() {
    this.mainPictureSRC = undefined;
    this.detailPicture = undefined;
    this.detailPictureSRC = undefined;
    this.description = "";
    this.title = "";
    this.categoryID=0;
    this.mainPicture = "";
    this.workItem.pictures = [];
    this.isActive = true;
  }


  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: 'auto',
    minHeight: '500px',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: 'Enter text here...',
    defaultParagraphSeparator: '',
    defaultFontName: '',
    defaultFontSize: '',
    fonts: [
      { class: 'arial', name: 'Arial' },
      { class: 'times-new-roman', name: 'Times New Roman' },
      { class: 'calibri', name: 'Calibri' },
      { class: 'comic-sans-ms', name: 'Comic Sans MS' }
    ],
    customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    uploadWithCredentials: false,
    sanitize: true,
    toolbarPosition: 'top',
    toolbarHiddenButtons: [
      ['bold', 'italic', 'insertVideo', 'insertImage'],
      ['fontSize']
    ]
  };
  detailPhotoChange(event: any) {
    if (this.title == "") {
      Swal.fire({
        icon: "error",
        text: "Fotoğraf yükleyebilmek için Metin dolu olmalıdır"
      })
      this.resetElement(this.detailPhotoInputVariable);
      return;
    }
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      let image: File = fileList[0];

      this.photoDelete(this.detailPicture);
      this.photostockService.savePhoto(image, this.title + "-detail", 2)
        .subscribe(
          res => {

            this.detailPictureSRC = `${this.photoStockApiURL}${res.data.url}?${Math.floor(Math.random() * 5555555555552)}`;
            this.detailPicture = res.data.url;
            this.resetElement(this.detailPhotoInputVariable);
          }
        );
    }
  }
  validationAddModel(model: WorkAddModel): string[] {
    let errorMessage: string[] = [];

    console.log("model", model)
    if (!model.description || model.description == "") {
      errorMessage.push("Alt metin alanı girilmelidir");
    }
    if (!model.title || model.title == "") {
      errorMessage.push("İş adı alanı girilmelidir");
    }
    if (!model.mainPicture || model.mainPicture == "") {
      errorMessage.push("Ana fotoğraf alanı girilmelidir");
    }
    if (!model.categoryId || model.categoryId == 0) {
      errorMessage.push("Kategori seçimi zorunludur.");
    }
    console.log("errorMessage", errorMessage)
    return errorMessage;
  }
  validationItemAddModel(model: WorkItemAddModel[]): string[] {
    let errorMessage: string[] = [];

    for (let item of model) {
      if (!item.pictures || item.pictures.length < 1) {
        errorMessage.push("Detay fotoğrafı boş bırakılamaz");
      }
    }

    return errorMessage;

  }

  errorsMessageWriter(errorMessage: string[]) {
    console.log(errorMessage);
    for (let error of errorMessage) {
      Swal.fire({
        icon: "error",
        text: error
      })
    }
  }
  errorMessageWriter(errorMessage: string) {
    Swal.fire({
      icon: "error",
      text: errorMessage
    })
  }
  submit() {
    const WorkModel = new WorkAddModel();
    WorkModel.description = this.description;
    WorkModel.mainPicture = this.mainPicture;
    WorkModel.title = this.title;
    WorkModel.workItems = new Array<WorkItemAddModel>();
    WorkModel.isActive = this.isActive;
    WorkModel.categoryId=this.categoryID;

    this.workItem.pictures = [this.detailPicture]
    WorkModel.workItems.push(this.workItem);
    const validationAddModel: string[] = this.validationAddModel(WorkModel);
    if (validationAddModel.length > 0) {
      this.errorsMessageWriter(validationAddModel);
      return;
    }
    const validationItemAddModel: string[] = this.validationItemAddModel(WorkModel.workItems);
    if (validationItemAddModel.length > 0) {
      this.errorsMessageWriter(validationItemAddModel);
      return;
    }


    console.log(WorkModel);
    this.workService.saveWork(WorkModel)
      .subscribe(
        response => {
          Swal.fire({
            icon: "success",
            text: "Başarıyla eklendi"
          });

          this.clearForm();
        }
      )
  }

}
