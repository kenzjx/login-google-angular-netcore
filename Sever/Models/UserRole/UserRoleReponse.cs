namespace Server.Models.UserRole
{
    public class UserRoleRepose
    {

        public string Id {set;get;}
        public string UserName {set;get;}

        public string Role {set;get;}

        public ICollection<string> ListRoles {set;get;}
    }

    public class ListUserRoleRepose : List<UserRoleRepose>
    {
        public ListUserRoleRepose(){
            
        }
    }
    
  
}