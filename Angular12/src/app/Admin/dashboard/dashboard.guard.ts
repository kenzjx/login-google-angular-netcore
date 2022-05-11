import { JwtService } from './../../core/services/jwt.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { find, map } from 'rxjs/operators';
import { UserroleService } from 'src/app/core/services/UserRole/userrole.service';
import { AuthService } from './dashboard.service';

//3Tutorial
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  logging : boolean = false;
  constructor(private userroleService :UserroleService, private jwt: JwtService) {
    this.userroleService.getUserRoles().pipe(find(rl => rl.userName == this.jwt.getEmailUser())).subscribe(data => {
      if(data)
      {
        this.logging = true;
      }else { this.logging = false}
    })
  }

  canActivate(): boolean {

   return true;
  }
}
