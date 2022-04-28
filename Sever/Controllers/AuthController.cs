using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models.Auth;
using Sever.Infrastructure;
using Server.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Auth;

namespace Server.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService userService;

        private readonly Authentication options;

        private readonly UserManager<AppUser> userManager;
        public AuthController(UserManager<AppUser> userManager, IUserService userService, IOptions<Authentication> options) : base(userManager)
        {
            this.userService = userService;
            this.options = options.Value;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
      
        public async Task<IActionResult> GoogleAuthenticate([FromBody] GoogleUserRequest request)
        {
            if(!ModelState.IsValid)
            {
               return BadRequest();
            }

            var user = GenerateUserToken(await userService.AuthenticateGoogleUserAsync(request));

           try
           {
               if (user !=null)
                return Ok(user);
                else return BadRequest();
           }
           catch (System.Exception ex)
           {
                // TODO
           }
           return Ok("Test");
        }


        [HttpPost]
         public async Task<IActionResult> Register(RegisterModel model)
        {
            if(model == null)
            {
                return NotFound();
            }
           
            var user = new AppUser{
                Email = model.Email,
                UserName = model.Name,
                
            };

            var result =await userManager.CreateAsync(user, "123");
            var role = await userManager.FindByEmailAsync(model.Email);
            await userManager.AddToRoleAsync(role, "Other");
            return Ok();
        }

       
        private UserToken GenerateUserToken(AppUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(options.Jwt.Secret);

            var expires = DateTime.UtcNow.AddDays(7);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, appUser.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, options.Jwt.Subject ),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, appUser.Id),
                    new Claim(ClaimTypes.Surname, appUser.FirstName),
                    new Claim(ClaimTypes.GivenName, appUser.LastName),
                    new Claim(ClaimTypes.NameIdentifier, appUser.UserName),
                    new Claim(ClaimTypes.Email, appUser.Email)
                }),

                Expires = expires,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
     
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken
            {
                UserId = appUser.Id,
                Email = appUser.Email,
                Token = token,
                Expires = expires,
                
            };
        }
    }
}