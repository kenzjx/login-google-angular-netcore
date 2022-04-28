export interface UserRoleRequest{
  userName :string,
  role: string
}
export class UserRole implements UserRoleRequest{
  userName: string = '';
  role: string = '';
}
