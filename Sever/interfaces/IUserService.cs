
using Server.Models.Auth;
using Sever.Infrastructure;

namespace Server.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> AuthenticateGoogleUserAsync(GoogleUserRequest request);
        
    }
}