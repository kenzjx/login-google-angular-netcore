import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import * as signalR from '@aspnet/signalr';
import { Observable, Subject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(public toastr : ToastrService, public router :Router) { }

  protected baseUrl = environment.baseUrl;

  hubConnection : signalR.HubConnection | undefined;

  ssSubj = new Subject<any>();
    ssObs(): Observable<any> {
        return this.ssSubj.asObservable();
    }

  startConnection = () =>{
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${this.baseUrl}/toastr`, {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    }).build();

    this.hubConnection.start().then(() =>{
      this.ssSubj.next({type: "HubConnStarted"});
    })
    .catch(err => console.log('Error while starting connection: ' + err))
  }
}
