import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthService } from './dashboard.service';

//3Tutorial
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(public authService: AuthService) {}

  canActivate(): boolean {

    return true;
  }
}
