import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './../dashboard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthGuard } from '../dashboard.guard';
import { NgxPaginationModule } from 'ngx-pagination';

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
    NgxPaginationModule,
    RouterModule.forChild(dashboardRoutes)
  ],
  exports: [NgxPaginationModule]
})
export class DashboardModule { }
