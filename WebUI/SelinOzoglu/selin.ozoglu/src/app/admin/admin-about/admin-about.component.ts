import { Component, OnInit } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { faImage, faSave, faPlus, faTrash, faPlusSquare } from '@fortawesome/free-solid-svg-icons';
import {  AboutPage, AboutService, Business, Software,Certifacate, Project, Education } from 'src/app/services/about/about.service';
import { v4 as uuidv4 } from 'uuid';
import Swal from 'sweetalert2';
import { PhotostockService } from 'src/app/services/photostock/photostock.service';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL, PHOTO_STOCK_API_SVG_FILE_URL } from 'src/shared/constants/urlConstants';
const groupBy = <T, K extends keyof any>(list: T[], getKey: (item: T) => K) =>
  list.reduce((previous, currentItem) => {
    const group = getKey(currentItem);
    if (!previous[group]) previous[group] = [];
    previous[group].push(currentItem);
    return previous;
  }, {} as Record<K, T[]>);
@Component({
  selector: 'app-admin-about',
  templateUrl: './admin-about.component.html',
  styleUrls: ['./admin-about.component.css']
})

export class AdminAboutComponent implements OnInit {
  faImage = faImage;
  faSave = faSave;
  faPlus = faPlus;
  faTrash = faTrash;
  faPlusSquare = faPlusSquare;

  aboutPage: AboutPage = new AboutPage();
  portreFileSRC:string;
  PHOTO_STOCK_API_PHOTOS_FILE_URL=PHOTO_STOCK_API_PHOTOS_FILE_URL;
  PHOTO_STOCK_API_SVG_FILE_URL=PHOTO_STOCK_API_SVG_FILE_URL;

  constructor(private aboutService: AboutService,private photostockService:PhotostockService) {
    this.aboutService.getAllByActive().subscribe(res => {
      console.log(res.body.data)
      this.aboutPage = res.body.data;
      this.portreFileSRC=PHOTO_STOCK_API_PHOTOS_FILE_URL+this.aboutPage.portreFileName;
    })
    console.log(uuidv4());
 
  }
  newSoftware() {
    if (this.aboutPage.softwares == null || this.aboutPage.softwares == undefined) {
      this.aboutPage.softwares = [];
    }
    const item = new Software();
    item.rowId = uuidv4();
    this.aboutPage.softwares.push(item)
    console.log(this.aboutPage.softwares);
  }
  removeSoftware(rowId: string) {
    console.log(rowId);
    Swal.fire({
      title: 'Hoppp! &#128561;  silmek istediğine emin misin?',
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: 'Sil',
      denyButtonText: `Silme`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.aboutPage.softwares = this.aboutPage.softwares.filter(f => f.rowId != rowId)
      }
    })
  }
  newBusiness() {
    console.log(this.aboutPage.businesses);
    if (this.aboutPage.businesses == null || this.aboutPage.businesses == undefined) {
      this.aboutPage.businesses = [];
    }
    const item = new Business();
    item.rowId = uuidv4();
    this.aboutPage.businesses.push(item)
    console.log(this.aboutPage.businesses);
  }
  removeBusiness(rowId: string) {
    Swal.fire({
      title: 'Hoppp! &#128561;  silmek istediğine emin misin?',
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: 'Sil',
      denyButtonText: `Silme`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.aboutPage.businesses = this.aboutPage.businesses.filter(f => f.rowId != rowId)
      }
    })
  }
  newCerficate() {
    if (this.aboutPage.certifacates == null || this.aboutPage.certifacates == undefined) {
      this.aboutPage.certifacates = [];
    }
    const item = new Certifacate();
    item.rowId = uuidv4();
    this.aboutPage.certifacates.push(item)
    console.log(this.aboutPage.certifacates);
  }
  removeCerficate(rowId: string) {
    Swal.fire({
      title: 'Hoppp! &#128561;  silmek istediğine emin misin?',
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: 'Sil',
      denyButtonText: `Silme`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.aboutPage.certifacates = this.aboutPage.certifacates.filter(f => f.rowId != rowId)
      }
    })
  }
  newProject(){
    if (this.aboutPage.projects == null || this.aboutPage.projects == undefined) {
      this.aboutPage.projects = [];
    }
    const item = new Project();
    item.rowId = uuidv4();
    this.aboutPage.projects.push(item)
    console.log(this.aboutPage.projects);
  }
  removeProject(rowId:string){
    Swal.fire({
      title: 'Hoppp! &#128561;  silmek istediğine emin misin?',
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: 'Sil',
      denyButtonText: `Silme`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.aboutPage.projects = this.aboutPage.projects.filter(f => f.rowId != rowId)
      }
    })
  }
  newEducation(){
    if (this.aboutPage.educations == null || this.aboutPage.educations == undefined) {
      this.aboutPage.educations = [];
    }
    const item = new Education();
    item.rowId = uuidv4();
    this.aboutPage.educations.push(item)
    console.log(this.aboutPage.educations);
  }
  removeEducation(rowId:string){
    Swal.fire({
      title: 'Hoppp! &#128561;  silmek istediğine emin misin?',
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: 'Sil',
      denyButtonText: `Silme`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.aboutPage.educations = this.aboutPage.educations.filter(f => f.rowId != rowId)
      }
    })
  }
  ngOnInit(): void {
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

  
  photoDelete(photoName: string) {
    if (photoName == undefined || photoName == "")
      return;
    this.photostockService.deletePhoto(photoName).subscribe(res => console.log(res));
  }
  portrePhotoChange(event: any) {
    let fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      let image: File = fileList[0];

      this.photoDelete(this.aboutPage.portreFileName);
      this.photostockService.savePhoto(image, 1)
        .subscribe(
          res => {

            this.portreFileSRC = `${PHOTO_STOCK_API_PHOTOS_FILE_URL}${res.data.url}?${Math.floor(Math.random() * 5555555555552)}`;
            this.aboutPage.portreFileName = res.data.url;
          }
        );
    }
  }

svgFileChange(event: any,rowId:string) {
  let fileList: FileList = event.target.files;
  if (fileList.length > 0) {
    let file: File = fileList[0];

    // this.photoDelete(this.aboutPage.portreFileName);
    this.photostockService.saveSvgFile(file, 1)
      .subscribe(
        res => {
          let x= this.aboutPage.softwares.find(f=>f.rowId==rowId)
          // x.svgPath=`${PHOTO_STOCK_API_PHOTOS_FILE_URL}${res.data.url}?${Math.floor(Math.random() * 5555555555552)}`;
          x.svgPath = res.data.url;
        }
      );
  }
}


  submit() {
    console.log(this.aboutPage);
    this.aboutPage.softwares= this.aboutPage.softwares?.filter(f=> 
      this.isNullOrEmptyOrUndefined(f.rowId)==false &&  
      this.isNullOrEmptyOrUndefined(f.svgPath)==false &&
      this.isNullOrEmptyOrUndefined(f.softwareName)==false
    );
    this.aboutPage.businesses= this.aboutPage.businesses?.filter(f=> 
      this.isNullOrEmptyOrUndefined(f.rowId)==false &&  
      this.isNullOrEmptyOrUndefined(f.head)==false &&
      this.isNullOrEmptyOrUndefined(f.value)==false &&
      this.isNullOrEmptyOrUndefined(f.foot)==false
    );
    this.aboutPage.certifacates= this.aboutPage.certifacates?.filter(f=> 
      this.isNullOrEmptyOrUndefined(f.rowId)==false &&  
      this.isNullOrEmptyOrUndefined(f.head)==false &&
      this.isNullOrEmptyOrUndefined(f.value)==false
    );
    this.aboutPage.educations= this.aboutPage.educations?.filter(f=> 
      this.isNullOrEmptyOrUndefined(f.rowId)==false &&  
      this.isNullOrEmptyOrUndefined(f.head)==false &&
      this.isNullOrEmptyOrUndefined(f.value)==false && 
      this.isNullOrEmptyOrUndefined(f.foot)==false
    );
    this.aboutPage.projects= this.aboutPage.projects?.filter(f=> 
      this.isNullOrEmptyOrUndefined(f.rowId)==false &&  
      this.isNullOrEmptyOrUndefined(f.head)==false &&
      this.isNullOrEmptyOrUndefined(f.value)==false
    );
    
    console.log("after",this.aboutPage);

    this.aboutService.save(this.aboutPage).subscribe(res=>{
      console.log("saved",res);
    })
  }
  isNullOrEmptyOrUndefined(value){

    if(value==undefined || value==null || value==''){
      return true;
    }
    return false;
  }
}
