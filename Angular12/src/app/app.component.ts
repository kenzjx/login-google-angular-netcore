import { SignalrService } from './signalr.service';
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
  constructor(private authService: AuthenticateService) {
  }
  ngOnInit(): void {
  }

  ngOnDestroy() {
  }
}
