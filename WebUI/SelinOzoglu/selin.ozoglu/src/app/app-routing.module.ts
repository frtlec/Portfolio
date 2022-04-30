import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AdminAboutComponent } from './admin/admin-about/admin-about.component';
import { AdminCategoriesComponent } from './admin/admin-categories/admin-categories.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminLayoutComponent } from './admin/admin-layout/admin-layout.component';
import { AdminSettingsComponent } from './admin/admin-settings/admin-settings.component';
import { AdminWorkAddComponent } from './admin/admin-work-add/admin-work-add.component';
import { AdminWorksComponent } from './admin/admin-works/admin-works.component';
import { AdminGuard } from './admin/AdminGuard';
import { LoginComponent } from './admin/login/login.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { PortfolioComponent } from './portfolio/portfolio.component';

const routes: Routes = [
  {path:"",component:HomeComponent},
  {path:"home",component:HomeComponent},
  {path:"portfolio",component:PortfolioComponent},
  {path:"about",component:AboutComponent},
  {path:"login",component:LoginComponent},
  {path:"contact",component:ContactComponent},
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: '', component: AdminDashboardComponent }],canActivate:[AdminGuard] },
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'works', component: AdminWorksComponent }],canActivate:[AdminGuard] },
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'works/add', component: AdminWorkAddComponent }],canActivate:[AdminGuard] },
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'works/edit/:workId', component: AdminWorkAddComponent }],canActivate:[AdminGuard] },
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'categories', component:AdminCategoriesComponent }],canActivate:[AdminGuard] },
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'about', component:AdminAboutComponent }],canActivate:[AdminGuard] },
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'settings', component:AdminSettingsComponent }],canActivate:[AdminGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
 }
