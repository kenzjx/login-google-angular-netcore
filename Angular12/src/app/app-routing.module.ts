import { DashboardComponent } from './Admin/dashboard/dashboard.component';

import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { AuthGuard } from './Admin/dashboard/dashboard.guard';

//2Tutorial
const routes: Routes = [
  {
    path: '', redirectTo: 'home', pathMatch: 'full'
  },
  {
    path: 'Admin', component: DashboardComponent
  },
  //3Tutorial
  {
    path: 'home', loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
    canActivate: [AuthGuard]
  },
  {
    path: '**', redirectTo: 'home', pathMatch: 'full'
   }
];

//3Tutorial
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
