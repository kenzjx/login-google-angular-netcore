import { Component } from '@angular/core';
import { AuthenticateService } from './core/services/authenticate.service';
import {  UserToken } from './core/models/response/user-token';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Google Login';
  logged = false;
  model: UserToken | any = null;
   subscription: Subscription | any = null ;
  webApiUrl: string = `${environment.baseUrl}`;

  constructor(private authService: AuthenticateService) {
  }

  ngOnInit(): void {
    this.subscription = this.authService.currentUser.subscribe(data => {
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

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  logout() {
    this.authService.logout();
  }
}
