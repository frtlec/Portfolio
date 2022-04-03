import { Component, OnInit } from '@angular/core';
import { faEdit, faTrash, faPlusSquare, faPlus } from '@fortawesome/free-solid-svg-icons';
import { WorkFilterModel } from 'src/app/models/inputModels/WorkAddModel';
import { WorkSMO } from 'src/app/models/serviceModels/WorkServiceModel';
import { WorkServiceService } from 'src/app/services/work/work-service.service';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL } from 'src/shared/constants/urlConstants';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-admin-works',
  templateUrl: './admin-works.component.html',
  styleUrls: ['./admin-works.component.css']
})
export class AdminWorksComponent implements OnInit {
  faEdit = faEdit;
  faTrash = faTrash;
  faPlusSquare = faPlusSquare;
  faPlus = faPlus;


  works: WorkSMO[];
  photoStockPhotosFile: string = PHOTO_STOCK_API_PHOTOS_FILE_URL;
  constructor(private workService: WorkServiceService) {
    this.render();
  }
  render(){
    
    const filter = new WorkFilterModel();
    filter.isActive = null;
    filter.limit = 9999;
    filter.search = "";
    this.workService.getAllWork(filter).subscribe(
      res => {
        this.works = res.body.data;
      }
    )

  }

  deleteWork(workId:number){
    Swal.fire({
      title: 'Hoppp! &#128561; işi silmek istediğine emin misin?',
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: 'Sil',
      denyButtonText: `Silme`,
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        this.workService.deleteWork(workId).subscribe(res=>{
          Swal.fire('Silindi!', '', 'success')
          this.render();
        })
      
      } else if (result.isDenied) {
       
        Swal.fire('İşi silinmekten son anda kurtuldun &#128515;', '', 'info')
      }
    })
  }

  lenght: number = 19;
  ngOnInit(): void {
  }

}
