import { GoogleLoginComponent } from './google-login/google-login.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './Admin/dashboard/dashboard.component';
import { CommonModule } from '@angular/common';
import { HomeGuard } from './home/home.guard';
import { DashboardModule } from './Admin/dashboard/dashboard/dashboard.module';


const routes: Routes = [
  {path: '', component: GoogleLoginComponent, pathMatch: 'full'},
  {path: 'home', component: HomeComponent, canActivate: [HomeGuard]},
{path: 'admin', loadChildren: () => import('./Admin/dashboard/dashboard/dashboard.module').then(m => m.DashboardModule)},
{path:'**', redirectTo: 'home'}
];

@NgModule({
  imports: [CommonModule ,RouterModule.forRoot(routes)],
  exports: [RouterModule,],
  declarations: []
})
export class AppRoutingModule { }
