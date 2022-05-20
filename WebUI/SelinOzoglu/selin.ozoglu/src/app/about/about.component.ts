import { Component, ErrorHandler, OnInit, Pipe } from '@angular/core';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { siteTitle } from 'src/shared/constants/general';
import { PHOTO_STOCK_API_PHOTOS_FILE_URL, PHOTO_STOCK_API_SVG_FILE_URL } from 'src/shared/constants/urlConstants';
import { GetValueFromLocalization } from '../pipes/_localization';
import { AboutPage, AboutService } from '../services/about/about.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  aboutPage: AboutPage = new AboutPage();
  PHOTO_STOCK_API_PHOTOS_FILE_URL = PHOTO_STOCK_API_PHOTOS_FILE_URL;
  PHOTO_STOCK_API_SVG_FILE_URL = PHOTO_STOCK_API_SVG_FILE_URL;
  refresh: boolean = true;
  constructor(private aboutService: AboutService,titleService:Title,getValueFromLocalization:GetValueFromLocalization) {
    getValueFromLocalization.transform("Ben kimim?").then(f=>titleService.setTitle(f+siteTitle));

  }

  ngOnInit(): void {
    this.aboutService.getAllByActive(true).subscribe(
      res => {
        this.aboutPage = res.body.data;
      },
      null,
      () => {
        setTimeout(() => {
          this.refresh=false;
        }, 200);
    
      }
    )
  }

}

