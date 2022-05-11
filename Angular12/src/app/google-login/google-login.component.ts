import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { SocialAuthService } from 'angularx-social-login';
import { AuthenticateService } from '../core/services/authenticate.service';
import { GoogleLoginProvider } from "angularx-social-login";
import { Router } from '@angular/router';
import { UserToken } from '../core/models/response/user-token';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-google-login',
  templateUrl: './google-login.component.html',
  styleUrls: ['./google-login.component.css']
})
export class GoogleLoginComponent implements OnInit, OnDestroy {

  title = 'Google Login';
  logged = false;
  model: UserToken | any = null;
   subscription: Subscription | any = null ;
  webApiUrl: string = `${environment.baseUrl}`;
     isLogging: boolean = false;
     isWarning: boolean = false;
  constructor(private authenService : AuthenticateService,
    private socialLoginService : SocialAuthService, private router : Router) { }
  ngOnDestroy(): void {

  }

  ngOnInit(): void {
    this.subscription = this.authenService.currentUser.subscribe(data => {
      if (data) {
        this.model = data;
        this.logged = true;
       
      }
      else {
        this.model = null;
        this.logged = false;
      }
    });

  }
  logout() {
    this.authenService.logout();
  }

  signInWithGoogle(): void {
    this.isLogging = true;
    this.socialLoginService.signIn(GoogleLoginProvider.PROVIDER_ID).then(googleUser => {
      // let reg =  /(\w+)@(beetsoft\.com\.vn)/;
      // let ggEmail = googleUser.email.match(reg);
      // if(ggEmail != null)
      // {
      //   this.authenService.googleLogin(googleUser)
      //   .subscribe((data) => {
      //     this.isLogging = true;
      //   });
      //   this.isWarning = false;
      // }else { this.isWarning = true;}
      this.authenService.googleLogin(googleUser)
      .subscribe((data) => {
        this.isLogging = true;
      });
      this.router.navigateByUrl("/home");
    });

  }
}
