import { Injectable } from '@angular/core';
import { HttpClient } from '@aspnet/signalr';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {Observable} from "zen-observable-ts";
import { UserRoleRequest } from '../../models/request/UserRole';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http : HttpClient) { }

  protected baseUrl = environment.baseUrl;

  getUser(data: any)
  {
    this.http.post(``, data).then(data => {return data;})
  }

  getUserRoles() : Observable<UserRoleRequest>
  {
    return this.http.get<any>(`${this.baseUrl}/api/UserRole/GetUserRole`).pipe(map((res:any)=>{
      return res;
    }))
  }
}
