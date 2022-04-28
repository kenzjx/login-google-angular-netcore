using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Infrastructure.Hubs;
using Server.Models.Hub;
using Server.Models.UserRole;
using Sever.Infrastructure;

namespace Server.Controllers
{
    public class UserRoleController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        
        private readonly AppDbContext context;

        private readonly IHubContext<BroadcastHub, IHubClient> hubContext;
        public UserRoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext) : base(userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRole()
        {
            List<UserRoleRepose> users = new List<UserRoleRepose>();

            var lusers = (from u in userManager.Users
                          orderby u.UserName
                          select new {Id = u.Id, Name = u.UserName});
             var result = await lusers.ToListAsync();
             
            foreach (var user in result)
            {
                var roles = await userManager.GetRolesAsync(new AppUser{Id = user.Id});
                var Role = string.Join(",", roles.ToList());
                users.Add(new UserRoleRepose{Id = user.Id, UserName = user.Name, Role = Role});
            }

            var reulst1 =  users.ToList();
            return Ok(reulst1);
        }

        [HttpPost]
        public async Task<IActionResult> postUserRole()
        {
             var lusers = (from u in userManager.Users
                          orderby u.UserName
                          select u);
            var result = await lusers.ToListAsync();

            foreach (var user in result)
            {
                await userManager.AddToRoleAsync(user, "Other");
            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole(UserRoleRequest model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            var notifi = new Notification()
            {
                UserName = user.UserName,
                TranType = $"Role của bạn đã thay đổi thành {model.Role} lúc{DateTime.Now} hãy đăng nhập lại"
            };
            context.Notifications.Add(notifi);

            await context.SaveChangesAsync();
            await hubContext.Clients.All.BroadcastMessage();

            IList<string> roles = await userManager.GetRolesAsync(user);
            foreach (var rolename in roles)
            {
                if (model.Role.Contains(rolename)) continue;
                await userManager.RemoveFromRoleAsync(user, rolename);
            }
            await userManager.AddToRoleAsync(user, model.Role);
            
            return Ok();
        }
    }
}