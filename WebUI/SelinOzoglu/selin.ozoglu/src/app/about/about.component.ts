import { Component, OnInit, Pipe } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL, PHOTO_STOCK_API_SVG_FILE_URL } from 'src/shared/constants/urlConstants';
import { AboutPage, AboutService } from '../services/about/about.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  aboutPage:AboutPage=new AboutPage();
  PHOTO_STOCK_API_PHOTOS_FILE_URL=PHOTO_STOCK_API_PHOTOS_FILE_URL;
  PHOTO_STOCK_API_SVG_FILE_URL=PHOTO_STOCK_API_SVG_FILE_URL;
  constructor(private aboutService:AboutService) {

 
   }

  ngOnInit(): void {
    this.aboutService.getAllByActive(true).subscribe(res=>{
      this.aboutPage=res.body.data;
    })
  }

}

