import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Route, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticateService } from '../core/services/authenticate.service';
import { JwtService } from '../core/services/jwt.service';

@Injectable({
  providedIn: 'root'
})
export class HomeGuard implements CanActivate {
   logging : boolean = false;
  constructor(private authent : AuthenticateService, private jwt: JwtService, private routes :Router){
    // this.authent.currentUser.subscribe(data => {
    //   if(data)
    //   {
    //     this.logging = true;
    //   }else {
    //     this.logging = false;
    //   }
    // })
  }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
     const data = this.jwt.getUser();
     if(data)
     {
       return true;
     }
     this.routes.navigateByUrl("");
     return false;
  }

}
