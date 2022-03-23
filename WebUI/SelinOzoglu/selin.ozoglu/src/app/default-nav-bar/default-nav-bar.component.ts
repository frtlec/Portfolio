import { Component, Input, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { openRightMenu } from '../state/defaultMenu/defaultMenu.actions';

@Component({
  selector: 'app-default-nav-bar',
  templateUrl: './default-nav-bar.component.html',
  styleUrls: ['./default-nav-bar.component.css']
})
export class DefaultNavBarComponent implements OnInit {
  isOpenRightMenu$: Observable<number>;
  @Input() whiteNavbar:boolean=true;

  constructor(private store: Store<{ isOpenRightMenu: number }>) {
    this.isOpenRightMenu$ = store.select('isOpenRightMenu');

    console.log(this.whiteNavbar)
  }
  ngOnInit(): void {
  }
  openRightMenu(){
    this.store.dispatch(openRightMenu());
  }
}
