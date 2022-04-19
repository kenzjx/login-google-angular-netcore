using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sever.Infrastructure;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public BaseController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
     
    }
}