import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import * as signalR from '@aspnet/signalr';
import { Observable, Subject } from 'rxjs';
import { UserSignalR } from './core/models/response/UserSignalR';


@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(public toastr : ToastrService, public router :Router) { }

  protected baseUrl = environment.baseUrl;

  ssSubj = new Subject<any>();
    ssObs(): Observable<any> {
        return this.ssSubj.asObservable();
    }

    hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${this.baseUrl}/toastr`).build();
  startConnection (){
    this.hubConnection.start().then(() =>{
      this.ssSubj.next({type: "HubConnStarted"});
    })
    .catch(err => console.log('Error while starting connection: ' + err))
  }
}
