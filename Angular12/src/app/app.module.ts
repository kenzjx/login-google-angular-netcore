import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing/app-routing.module';
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
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
const routes : Routes = [
  {path: '', component: GoogleLoginComponent, pathMatch: 'full'},
  {path: 'home', component: HomeComponent},
  {path: 'admin', component: DashboardComponent},
  {path:'**', redirectTo: 'home'}
]

@NgModule({
  declarations: [
    AppComponent,
    GoogleLoginComponent,
    HomeComponent,
    DashboardComponent,
    HomeComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SocialLoginModule,
    RouterModule.forRoot(routes),
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
