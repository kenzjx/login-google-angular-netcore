export interface UserRoleRequest{
  userName :string,
  role: string,
  ListRoles : any
}
export class UserRole implements UserRoleRequest{
  userName: string = '';
  role: string = '';
  ListRoles : any;
}
