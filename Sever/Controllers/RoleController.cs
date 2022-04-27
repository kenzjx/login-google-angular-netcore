using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models.Role;
using Sever.Infrastructure;

namespace Server.Controllers
{

    public class RoleController : BaseController
    {
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : base(userManager)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleId(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string newName)
        {
           
            var newRole = new IdentityRole(newName);
            var result = await roleManager.CreateAsync(newRole);
            if(!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
                
             
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleRequest model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            role.Name = model.NameRole;
            var result = await roleManager.UpdateAsync(role);
            return Ok();
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            var reult = await roleManager.DeleteAsync(role);
            return Ok();
        }
    }
}