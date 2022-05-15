using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Hub.Interface;
using Server.Infrastructure;
using Server.Infrastructure.Hubs;
using Server.Models.Hub;
using Server.Models.UserRole;
using Sever.Infrastructure;

namespace Server.Controllers
{
    public class UserRoleController : BaseController
    {
        private readonly ILogger logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;


        private readonly AppDbContext context;

        private readonly IHubContext<MyHub> hubContext;
        public UserRoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHubContext<MyHub> hubContext, ILogger<UserRoleController> logger) : base(userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
            this.hubContext = hubContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRole()
        {

            ListUserRoleRepose users = new ListUserRoleRepose();
            var lusers = (from u in userManager.Users
                          orderby u.UserName
                          select new { Id = u.Id, Name = u.UserName });
            var result = await lusers.ToListAsync();
            ICollection<string> listroles;
            foreach (var user in result)
            {
                var roles = await userManager.GetRolesAsync(new AppUser { Id = user.Id });
                logger.LogWarning(Environment.NewLine, roles.ToArray());
                var Role = string.Join(",", roles.ToList());
                listroles = await roleManager.Roles.Select(s => s.Name).ToListAsync();
                users.Add(new UserRoleRepose { Id = user.Id, UserName = user.Name, Role = Role, ListRoles = listroles });
            }

            var reulst1 = users.ToList();
            return Ok(reulst1);
        }

        // [HttpPost]
        // public async Task<IActionResult> postUserRole()
        // {
        //     for (var i = 1; i < 100; i++)
        //     {
        //         await userManager.CreateAsync(new AppUser { UserName = "user" + i, Email = "user" + 1 + "@gmail.com" }, "123");
        //     }

        //     var lusers = (from u in userManager.Users
        //                   orderby u.UserName
        //                   select u);
        //     var result = await lusers.ToListAsync();

        //     foreach (var user in result)
        //     {
        //         await userManager.AddToRoleAsync(user, "Other");
        //     }
        //     return Ok();
        // }
        [HttpPut]
        public async Task<IActionResult> UpdateRole(UserRoleRequest model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            await context.SaveChangesAsync();
            IList<string> roles = await userManager.GetRolesAsync(user);
            foreach (var rolename in roles)
            {
                if (model.Role.Contains(rolename)) continue;
                await userManager.RemoveFromRoleAsync(user, rolename);
            }
            var connect = await context.Connections.Where(c => c.PersonId == user.Id).Select(c => c.SignalrId).ToListAsync();
            var result = await userManager.AddToRoleAsync(user, model.Role);
            await context.SaveChangesAsync();
            if (result.Succeeded)
            {
                var message = $"Role của bạn đã thay đổi thành {model.Role} bạn hãy đăng nhập lại để tiếp tục";
                await hubContext.Clients.Clients(connect).SendAsync("RoleChangeSucce", message);
            }
            else
            {
                await hubContext.Clients.Clients(connect).SendAsync("RoleChangeFail", "Error change Role");
            }
            return Ok();
        }
    }
}