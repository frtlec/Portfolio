import { Component, Input, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { closeRightMenu } from '../state/defaultMenu/defaultMenu.actions';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
  styleUrls: ['./default-header.component.css']
})
export class DefaultHeaderComponent implements OnInit {
  @Input() whiteNavbar:boolean=true;
  
  constructor(){
  }
  ngOnInit(): void {
  }

}
