import { Component, Input, OnInit } from '@angular/core';
import { AuthenticateService } from '../core/services/authenticate.service';
import { SocialAuthService, GoogleLoginProvider } from 'angularx-social-login';
import { UserToken } from '../core/models/response/user-token';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {



  ngOnInit(): void {
  }

  @Input() model: UserToken |any ;

  constructor() { }


}
