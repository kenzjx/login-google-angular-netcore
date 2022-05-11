import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './../dashboard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from '../dashboard.guard';

const dashboardRoutes : Routes = [
  {
    path: '',
    component : DashboardComponent,
    canActivate: [AuthGuard]
  }

]

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes)
  ]
})
export class DashboardModule { }
