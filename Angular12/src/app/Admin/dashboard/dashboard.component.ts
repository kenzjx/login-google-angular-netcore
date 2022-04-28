import { UserRole } from './../../core/models/request/UserRole';
import { UserroleService } from './../../core/services/UserRole/userrole.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserRoleReponse } from 'src/app/core/models/response/UserRole';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {


   userRole: UserRole = new UserRole();
  formValue : FormGroup;
  Roles : any;
  UserRole: any | UserRoleReponse;
  showSave: boolean = true;;

  constructor(private formBuilder : FormBuilder, private userroleService :UserroleService) {
    this.formValue = this.formBuilder.group({
      id: [''],
      userName : [''],
      roleName : ['']
    })
  }

  ngOnInit(): void {
    this.getUserRoles();
    this.getAllRoles();
    console.log(UserRole);
  }

  getAllRoles(){
    this.userroleService.getRoles().subscribe((data:any) =>{
      this.Roles = data;
    })
  }

  getUserRoles(){
    this.userroleService.getUserRoles().subscribe((data:any)=>{
      this.UserRole = data;
    })
  }
  updateUserRole(row: any){
    this.userRole.userName = row.userName;
    this.userRole.role = row.role;
    this.userroleService.updateUserRole(this.userRole).subscribe(res =>{
      this.getUserRoles();
    })
    this.formValue.reset();
  }

}
