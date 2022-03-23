import { Component, OnInit } from '@angular/core';
import { faEdit,faTrash,faPlusSquare,faPlus } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-admin-works',
  templateUrl: './admin-works.component.html',
  styleUrls: ['./admin-works.component.css']
})
export class AdminWorksComponent implements OnInit {
  faEdit = faEdit;
  faTrash=faTrash;
  faPlusSquare=faPlusSquare;
  faPlus=faPlus;
  constructor() { }

  lenght:number=19;
  ngOnInit(): void {
  }

}
