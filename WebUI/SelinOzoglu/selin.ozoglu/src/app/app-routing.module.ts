import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminLayoutComponent } from './admin/admin-layout/admin-layout.component';
import { AdminWorkAddComponent } from './admin/admin-work-add/admin-work-add.component';
import { AdminWorksComponent } from './admin/admin-works/admin-works.component';
import { LoginComponent } from './admin/login/login.component';
import { HomeComponent } from './home/home.component';
import { PortfolioComponent } from './portfolio/portfolio.component';

const routes: Routes = [
  {path:"",component:HomeComponent},
  {path:"home",component:HomeComponent},
  {path:"portfolio",component:PortfolioComponent},
  {path:"about",component:AboutComponent},
  {path:"login",component:LoginComponent},
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: '', component: AdminDashboardComponent }]},
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'works', component: AdminWorksComponent }]},
  {path: 'admin', component: AdminLayoutComponent,children: [{ path: 'works/add', component: AdminWorkAddComponent }]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
