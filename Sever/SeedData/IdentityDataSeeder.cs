using Microsoft.AspNetCore.Identity;
using Server.Infrastructure;
using Sever.Infrastructure;

namespace Server.SeedData
{
    public static class  IdentityDataSeeder
    {

        public static void SeedData(UserManager<AppUser> userManager)
        {
           
            SeedUsers(userManager);
        }
         public static void SeedUsers(UserManager<AppUser> userManager)
         {
             for(int i = 0; i< 20; i++)
             {
                 var user = new AppUser{
                     UserName = $"User{i}",
                     Email = $"User{i}@gmail.com"
                 };
                 userManager.CreateAsync(user, "123");
                 
             }
         }
    }
}