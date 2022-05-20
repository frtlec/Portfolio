import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DefaultNavBarComponent } from './default-nav-bar/default-nav-bar.component';
import { DefaultHeaderComponent } from './default-header/default-header.component';
import { Store, StoreModule } from '@ngrx/store';
import { rightMenuReducer } from './state/defaultMenu/defaultMenu.reducer';
import { RightMenuComponent } from './right-menu/right-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminLayoutComponent } from './admin/admin-layout/admin-layout.component';
import { LoginComponent } from './admin/login/login.component';
import { FormsModule } from '@angular/forms';
import { AdminWorksComponent } from './admin/admin-works/admin-works.component';
import { AdminWorkAddComponent } from './admin/admin-work-add/admin-work-add.component';
import { HttpClientErrorHandler } from './handler/HttpClientErrorHandler';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { AboutComponent } from './about/about.component';
import { AdminCategoriesComponent } from './admin/admin-categories/admin-categories.component';
import { AdminAboutComponent } from './admin/admin-about/admin-about.component';
import { AdminSettingsComponent } from './admin/admin-settings/admin-settings.component';
import { ContactComponent } from './contact/contact.component';
import { Observable } from 'rxjs/internal/Observable';
import { Router } from '@angular/router';
import { closeRightMenu } from './state/defaultMenu/defaultMenu.actions';
import { ApiCredentialInterceptorService } from './handler/ApiCredentialInterceptorService';
import { AuthService } from './services/auth/auth.service';
import { CategoryService } from './services/category/category.service';
import { ContactService } from './services/contact/contact.service';
import { PhotostockService } from './services/photostock/photostock.service';
import { WorkServiceService } from './services/work/work-service.service';
import { AdminGuard } from './admin/AdminGuard';
import { Safe } from './pipes/safeHtml';
import { AdminLocalizationComponent } from './admin/admin-localization/admin-localization.component';
import { GetLocalizationName } from './pipes/getEnumKeyString';
import { GetValueFromLocalization } from './pipes/_localization';
import { AddUIDphotoUrl } from './pipes/AddUIDphotoUrl';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DefaultNavBarComponent,
    DefaultHeaderComponent,
    RightMenuComponent,
    AdminDashboardComponent,
    AdminLayoutComponent,
    LoginComponent,
    AdminWorksComponent,
    AdminWorkAddComponent,
    AdminLocalizationComponent,
    PortfolioComponent,
    AboutComponent,
    AdminCategoriesComponent,
    AdminAboutComponent,
    AdminSettingsComponent,
    ContactComponent,
    Safe,
    GetLocalizationName,
    GetValueFromLocalization,
    AdminLocalizationComponent,
    AddUIDphotoUrl
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    StoreModule.forRoot({
      rightMenu: rightMenuReducer
    }),
    FontAwesomeModule,
    NgbModule,
    AngularEditorModule
  ],
  providers: [Store,AdminGuard,GetValueFromLocalization,
                    { provide: ErrorHandler, useClass: HttpClientErrorHandler },
                    { provide: HTTP_INTERCEPTORS, useClass: ApiCredentialInterceptorService,multi:true }
                    
                ],
  bootstrap: [AppComponent]
})
export class AppModule {

  constructor(private router: Router, private store: Store) {
    router.events.subscribe((val) => {
      // see also 
      this.store.dispatch(closeRightMenu());//her route değiştiğinde right menu kapanır
    });
  }
}
