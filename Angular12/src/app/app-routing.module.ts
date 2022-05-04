import { HomeComponent } from './home/home.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './Admin/dashboard/dashboard.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {path: '', component: AppComponent, pathMatch: 'full'},
{path: 'home', component: HomeComponent},
{path: 'admin', component: DashboardComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
