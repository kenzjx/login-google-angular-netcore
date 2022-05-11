import { JwtService } from './../jwt.service';
import { Observable } from 'rxjs';
import { UserRole, UserRoleRequest } from './../../models/request/UserRole';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUserRoleReponse } from '../../models/response/UserRole';

@Injectable({
  providedIn: 'root'
})
export class UserroleService implements  OnInit{
  //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
  //Add 'implements OnInit' to the class.

  // email : string

  constructor(private http: HttpClient, private jwt :JwtService ) {
    // this.email = this.jwt.getIdUser();
   }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  protected baseUrl = environment.baseUrl;

  getUserRoles() : Observable<UserRoleRequest>
  {
    return this.http.get<any>(`${this.baseUrl}/api/UserRole/GetUserRole`).pipe(map((res:any)=>{
      return res;
    }))
  }



  // getUserRoleId() : Observable<UserRoleRequest>
  // {
  //   return this.http.get<any>(``)
  // }


  updateUserRole(UR : IUserRoleReponse) : Observable<IUserRoleReponse>
  {
    return this.http.put<IUserRoleReponse>(`${this.baseUrl}/api/UserRole/UpdateRole`, UR)
  }

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.

  }

}




