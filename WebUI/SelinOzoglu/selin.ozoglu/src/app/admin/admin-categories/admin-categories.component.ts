import { Component, OnInit } from '@angular/core';
import { CategorySMO } from 'src/app/models/serviceModels/CategorySMO';
import { CategoryService } from 'src/app/services/category/category.service';
import { faGear } from '@fortawesome/free-solid-svg-icons';
import { CategoryInput } from 'src/app/models/inputModels/CategoryInput';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrls: ['./admin-categories.component.css']
})
export class AdminCategoriesComponent implements OnInit {
  faGear = faGear;
  categories: CategorySMO[] = [];
  edit: boolean = false;
  category: CategoryInput;

  constructor(private categoryService: CategoryService) {
    this.render();

    this.category = new CategoryInput();
    this.category.isShowMainPage=true;
    this.category.isActive=true;

  }
  render() {
    this.categoryService.getCategoriesByFilter(null).subscribe(
      res => {
        this.categories = res.data;
      }
    )
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
  deleteClick(categoryId:number){
    this.categoryService.deleteCategory(categoryId).subscribe(res=>{
      Swal.fire({
        icon: "success",
        text: "Başarıyla silindi"
      });
      this.render();
    })
  }
  private validation(category: CategoryInput) {
    let errorMessages: string[] = [];
    if (category.title == "" || category.title == undefined) {
      errorMessages.push("Kategori adı girilmelidir");
    }
    if (category.description == "" || category.description == undefined) {
      errorMessages.push("Kategori açıklaması girilmelidir");
    }

    return errorMessages;
  }
  submit() {

    let erorrs: string[] = this.validation(this.category);

    if (erorrs.length > 0) {
      this.errorsMessageWriter(erorrs);
      return;
    }

    if(this.edit){
      this.editRequest()
    }else{
      this.addRequest()
    }

  
  }


  addRequest(){
    this.categoryService.createCategory(this.category).subscribe(res => {
      Swal.fire({
        icon: "success",
        text: "Başarıyla kaydedildi."
      })
      this.clearForm();
      this.render();
    })
  }
  editRequest(){
    this.categoryService.updateCategory(this.category).subscribe(res => {
      Swal.fire({
        icon: "success",
        text: "Başarıyla güncellendi."
      })
      this.clearForm();
      this.render();
    })
  }

  clearForm() {
    this.category = new CategoryInput();
  }

  editClick(categoryId: number) {

    this.categoryService.getCategoryById(categoryId).subscribe(res=>{
      this.edit=true;
      this.category.categoryId=res.data.id;
      this.category.description=res.data.description;
      this.category.isActive=res.data.isActive;
      this.category.sort=res.data.sort;
      this.category.title=res.data.title;
      this.category.isShowMainPage=res.data.isShowMainPage;
    })

   
  }

  ngOnInit(): void {
  }

}
