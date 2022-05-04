import { Component, Input, OnInit } from '@angular/core';
import { SocialAuthService } from 'angularx-social-login';
import { AuthenticateService } from '../core/services/authenticate.service';
import { GoogleLoginProvider } from "angularx-social-login";
import { Router } from '@angular/router';
@Component({
  selector: 'app-google-login',
  templateUrl: './google-login.component.html',
  styleUrls: ['./google-login.component.css']
})
export class GoogleLoginComponent implements OnInit {

  @Input() capture : any ;

     isLogging: boolean = false;
     isWarning: boolean = false;

  constructor(private authenService : AuthenticateService,
    private socialLoginService : SocialAuthService, private router : Router) { }

  ngOnInit(): void {
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
        // this.router.navigateByUrl("/home");
    });

  }
}
