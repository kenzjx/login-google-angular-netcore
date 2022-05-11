import { JwtService } from './../../core/services/jwt.service';
import { SignalrService } from './../../signalr.service';
import { UserRole, UserRoleRequest } from './../../core/models/request/UserRole';
import { UserroleService } from './../../core/services/UserRole/userrole.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserRoleReponse } from 'src/app/core/models/response/UserRole';
import { Subscription, Subject } from 'rxjs';
import { AuthenticateService } from 'src/app/core/services/authenticate.service';
import { UserSignalR } from 'src/app/core/models/response/UserSignalR';
import { UserToken } from 'src/app/core/models/response/user-token';
import { filter, find, map, takeUntil } from 'rxjs/operators';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {


  subscription : Subscription | any ;
  userData : UserToken | any = null;

  private destroy$:Subject<boolean> = new Subject<boolean>();

   userRole: UserRole |any;
  formValue : FormGroup;
  Roles : any;
  roleChange :string ='';
  showSave: boolean = true;;
  constructor(private jwt : JwtService, private formBuilder : FormBuilder, private userroleService :UserroleService) {
    this.formValue = this.formBuilder.group({
      id: [''],
      userName : [''],
      roleName : ['']
    })
  }
  ngOnDestroy(): void {
    this.destroy$.next(true);
  }

  ngOnInit(): void {
    this.getUserRoles();
    this.userroleService.getUserRoles().pipe(find(x => x.role === 'Admin')).subscribe(
      (data) => console.log(data)
    )


  }

  getUserRoles(){
    this.userroleService.getUserRoles().pipe(takeUntil(this.destroy$)).subscribe((data:any)=>{
      this.userRole = data;
    })
  }

  changeRole(event :any)
  {
    this.roleChange = event.target.value;
    console.log(event.target.value)
  }

  updateUserRole(row: any){
    const userRoleRepose : UserRoleReponse = new UserRoleReponse();
    userRoleRepose.userName = row;
    userRoleRepose.role = this.roleChange;
    this.userroleService.updateUserRole(userRoleRepose).pipe(takeUntil(this.destroy$)).subscribe(res =>{
      this.getUserRoles();
    })
    this.getUserRoles();
    this.formValue.reset();
  }

}
