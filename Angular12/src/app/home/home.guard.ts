import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticateService } from '../core/services/authenticate.service';

@Injectable({
  providedIn: 'root'
})
export class HomeGuard implements CanActivate {
   logging : boolean = false;
  constructor(private authent : AuthenticateService){
    this.authent.currentUser.subscribe(data => {
      if(data)
      {
        this.logging = true;
      }else {
        this.logging = false;
      }
    })
  }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      return this.logging
  }

}
