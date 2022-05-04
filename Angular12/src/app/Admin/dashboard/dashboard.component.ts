import { SignalrService } from './../../signalr.service';
import { UserRole } from './../../core/models/request/UserRole';
import { UserroleService } from './../../core/services/UserRole/userrole.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserRoleReponse } from 'src/app/core/models/response/UserRole';
import { Subscription } from 'rxjs';
import { AuthenticateService } from 'src/app/core/services/authenticate.service';
import { UserSignalR } from 'src/app/core/models/response/UserSignalR';
import { UserToken } from 'src/app/core/models/response/user-token';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {


  subscription : Subscription | any ;
  userData : UserToken | any = null;


   userRole: UserRole |any;
  formValue : FormGroup;
  Roles : any;
  roleChange :string ='';
  showSave: boolean = true;;
  constructor(private authService: AuthenticateService, private sinalR:SignalrService, private formBuilder : FormBuilder, private userroleService :UserroleService) {
    this.formValue = this.formBuilder.group({
      id: [''],
      userName : [''],
      roleName : ['']
    })
  }

  ngOnInit(): void {
    this.getUserRoles();

  }

  getUserRoles(){
    this.userroleService.getUserRoles().subscribe((data:any)=>{
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
    this.userroleService.updateUserRole(userRoleRepose).subscribe(res =>{
      this.getUserRoles();
    })
    this.getUserRoles();
    this.formValue.reset();
  }

}
