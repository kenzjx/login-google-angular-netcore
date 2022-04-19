import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtService } from './jwt.service';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from "rxjs/operators";
import {  UserToken } from '../models/response/user-token';
import { GoogleUserRequest } from '../models/request/google-user';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService extends BaseService{

  private currentUserSubject = new BehaviorSubject<UserToken | any>({} as UserToken );
  public currentUser = this.currentUserSubject.asObservable();


  constructor(httpClient: HttpClient, private jwtService: JwtService, private router: Router) {
    super(httpClient);
    this.currentUserSubject = new BehaviorSubject<UserToken>(this.jwtService.getUser());
    this.currentUser = this.currentUserSubject.asObservable();
  }

  googleLogin(googleUser: GoogleUserRequest): Observable<UserToken>
  {
    return this.HttpClient.post<UserToken>(`${this.baseUrl}/api/Auth/GoogleAuthenticate`, googleUser)
    .pipe(map(profile =>{
        this.setAuth(profile);
        return profile
    }))
  }

  setAuth(user: UserToken)
  {
    this.jwtService.saveUser(user);
    this.currentUserSubject.next(user);
  }

  logout() {
    this.purgeAuth();
  }



  purgeAuth() {
    this.jwtService.destroyUser();
    this.currentUserSubject.next(null);
  }
}
