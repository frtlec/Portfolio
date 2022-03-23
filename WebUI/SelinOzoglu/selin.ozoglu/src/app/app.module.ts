import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
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
import { PhotostockService } from './services/photostock/photostock.service';
import { AlertifyService } from './services/alertify.service';
import { HttpClientErrorHandler } from './handler/HttpClientErrorHandler';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { AboutComponent } from './about/about.component';
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
    PortfolioComponent,
    AboutComponent,
 
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    StoreModule.forRoot({
      rightMenu:rightMenuReducer
    }),
    FontAwesomeModule,
    NgbModule,
    AngularEditorModule
  ],
  providers: [Store,{provide:ErrorHandler,useClass:HttpClientErrorHandler}],
  bootstrap: [AppComponent]
})
export class AppModule { }
