using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Server.Hub.Interface;
using Server.Interfaces;
using Server.Models.Auth;
using Server.Options;
using Sever.Infrastructure;
using static Google.Apis.Auth.GoogleJsonWebSignature;


namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly Authentication options;
        private readonly UserManager<AppUser> userManage;



        public UserService(UserManager<AppUser> userManage, IOptions<Authentication> options)
        {
            this.userManage = userManage;
            this.options = options.Value;

        }

        public async Task<AppUser> AuthenticateGoogleUserAsync(GoogleUserRequest request)
        {
            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { "557580645532-f2om83vuokm89evq4t70b722eq57rvtk.apps.googleusercontent.com" }
            });


            return await GetOrCreateExternalLoginUser(GoogleUserRequest.PROVIDER, payload.Subject, payload.Email, payload.GivenName, payload.FamilyName); ;

        }

        private async Task<AppUser> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lasName)
        {
            var user = await userManage.FindByLoginAsync(provider, key);
            if (user != null)
            {
                await userManage.AddToRoleAsync(user, "Other");
                return user;
            }

            user = await userManage.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = email,
                    UserName = firstName,
                    FirstName = firstName,
                    LastName = lasName,
                    Id = key
                };
                await userManage.CreateAsync(user);

            }

            var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());

            var result = await userManage.AddLoginAsync(user, info);
            await userManage.AddToRoleAsync(user, "Other");
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }
    }
}