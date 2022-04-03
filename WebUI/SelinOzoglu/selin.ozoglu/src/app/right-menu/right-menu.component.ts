import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { faFacebook,faInstagram,faBehance,faLinkedin } from '@fortawesome/free-brands-svg-icons';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { closeRightMenu } from '../state/defaultMenu/defaultMenu.actions';
import { library } from '@fortawesome/fontawesome-svg-core';
@Component({
  selector: 'app-right-menu',
  templateUrl: './right-menu.component.html',
  styleUrls: ['./right-menu.component.css'],
  animations: [
    trigger("openClose", [
      state("open", style({
        transform: "translateX(0)",
     
      })),
      state('closed', style({
        transform: "translateX(150%)",
      
      })),
      transition('open => closed', [
        animate('1s')
      ]),
      transition('closed => open', [
        animate('0.5s')
      ]),
    ])
  ]
})
export class RightMenuComponent implements OnInit {
  faFacebook=faFacebook;
  faInstagram=faInstagram;
  faBehance=faBehance;
  faLinkedin=faLinkedin;
 
  rightMenu$: Observable<boolean>;
    constructor(private store: Store<{ rightMenu: boolean }>) {
      this.rightMenu$ = store.select('rightMenu');
      library.add(faFacebook as any);
    }
  ngOnInit(): void {
  }
  closeRightMenu(){
    this.store.dispatch(closeRightMenu());
  }
}
