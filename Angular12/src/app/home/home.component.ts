import { SignalrService } from './../signalr.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthenticateService } from '../core/services/authenticate.service';
import { Subscription } from 'rxjs';
import { UserSignalR } from '../core/models/response/UserSignalR';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})


// export class UserSignalR {
//      public email: string ='';
//      public connId : string ='';

// }

export class HomeComponent implements OnInit, OnDestroy {

  subscription: Subscription | any;
  // UserData : UserSignalR = new UserSignalR();
  constructor(private authService: AuthenticateService, private signalrService: SignalrService) { }

  userData: UserSignalR | any;
  protected baseUrl = environment.baseUrl;

  messages: string = '';

  ngOnInit(): void {
    this.subscription = this.authService.currentUser.subscribe(data => {
      if (data) {
        this.userData = data;
        console.log(data.email)
      }
    })
    console.log(this.messages);
    this.Connect();
    this.getMeessageSuccess();

  }

  async Connect() {
    await this.signalrService.hubConnection.start();
    this.signalrService.hubConnection.invoke("ConnectionsUser", this.userData.email)
      .catch(err => console.error(err));
  }

  // async getConnectionId() {
  //   await this.signalrService.hubConnection?.invoke("ConnectionsUser", this.userData.email)
  //     .catch(err => console.error(err));
  // }

  getMeessageSuccess(): void {
    this.signalrService.hubConnection?.on("RoleChangeSucce", (data: any) => {
      if (data) {
        this.signalrService.toastr.success(data);
      }
    })
  }

  getMeessageFail(): void{
    this.signalrService.hubConnection?.on("RoleChangeFail", (data: any) =>{
      this.signalrService.toastr.error(data);

    })
  }
  logOut(): void {
    this.signalrService.hubConnection?.invoke("logOut", this.userData.email)
      .catch(err => console.error(err));
  }



  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    this.signalrService.hubConnection?.off("RoleChangeSucce");
  }
}
