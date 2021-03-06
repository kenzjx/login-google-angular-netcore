import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { AppComponent } from './app.component';
import {MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatPaginatorModule} from '@angular/material/paginator';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { GoogleLoginComponent } from './google-login/google-login.component';
import { environment } from '../environments/environment';
import { JwtService } from './core/services/jwt.service';
import { AuthenticateService } from './core/services/authenticate.service';
import { TokenInterceptor } from './core/interceptors/token.interceptor';
import { DashboardComponent } from './Admin/dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeGuard } from './home/home.guard';

import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
const routes : Routes = [
  {path: '', component: GoogleLoginComponent, pathMatch: 'full'},
  {path: 'home', component: HomeComponent, canActivate: [HomeGuard]},
  {path: 'admin', loadChildren: () => import('./Admin/dashboard/dashboard/dashboard.module').then(m => m.DashboardModule)},
  {path:'**', redirectTo: 'home'}
]

@NgModule({
  declarations: [
    AppComponent,
    GoogleLoginComponent,
    HomeComponent,
    HomeComponent,
    // DashboardComponent
  ],
  imports: [
    NgxPaginationModule,
    AppRoutingModule,
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    // RouterModule.forRoot(routes, {
    //   preloadingStrategy: PreloadAllModules
    // }),
    HttpClientModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      enableHtml: true,
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: false,
    })
  ],
  providers: [
    JwtService,
    AuthenticateService,
    {
      provide : HTTP_INTERCEPTORS,
      useClass : TokenInterceptor,
      multi : true
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(environment.googleClientId)
          }
        ]
      } as SocialAuthServiceConfig
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
