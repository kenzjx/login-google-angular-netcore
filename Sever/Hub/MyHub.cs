
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Server.Infrastructure;
using Server.Infrastructure.Hubs;
using Server.Models.Hub;
using Sever.Infrastructure;

namespace Server.Hub.Interface
{
    public class MyHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly AppDbContext context;

        private readonly UserManager<AppUser> userManager;
        public MyHub(AppDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string currUserId = context.Connections.Where(c => c.SignalrId == Context.ConnectionId).Select(c => c.PersonId).SingleOrDefault();
            context.Connections.RemoveRange(context.Connections.Where(p => p.PersonId == currUserId).ToList());
            context.SaveChanges();
            Clients.Others.SendAsync("userOff", currUserId);
            return base.OnDisconnectedAsync(exception);
        }

         public async Task ConnectionsUser(string  Email)
         {
             string currSignalrID = Context.ConnectionId;
            AppUser tempPerson = await userManager.FindByEmailAsync(Email);
                
            if (tempPerson != null) //if credentials are correct
            {
              
                Connections currUser = new Connections
                {
                    PersonId = tempPerson.Id,
                    SignalrId = currSignalrID,
                    TimeStamp = DateTime.Now
                };
                await context.Connections.AddAsync(currUser);
                await context.SaveChangesAsync();            
                await Clients.Caller.SendAsync("MeResponseSuccess");
            }
            else 
            {
                await Clients.Caller.SendAsync("MeResponseFail");
            }
         }

         public async Task reConnectionUser(string Email)
         {
            string currSignalrID = Context.ConnectionId;

            AppUser tempPerson = await userManager.FindByEmailAsync(Email);

            Connections currUser = new Connections
                {
                    PersonId = tempPerson.Id,
                    SignalrId = currSignalrID,
                    TimeStamp = DateTime.Now
                };
            await context.Connections.AddAsync(currUser);
            await context.SaveChangesAsync();
            await Clients.Caller.SendAsync("Connect", "Succes");
         }

        //  public async Task getOnlineUsers()
        // {
        //     string currUserId =  context.Connections.Where(c => c.SignalrId == Context.ConnectionId).Select(c => c.PersonId).SingleOrDefault();
        //     List<User> onlineUsers = context.Connections
        //         .Where(c => c.PersonId != currUserId)
        //         .Select(c =>
        //             new User(c.PersonId, context.Users.Where(p => p.Id == c.PersonId).Select(p => p.UserName).SingleOrDefault(), c.SignalrId)
        //         ).ToList();
        //     await Clients.Caller.SendAsync("getOnlineUsersResponse", onlineUsers);
        // }
         
        public void logOut(string email)
        {
           var personId = context.Users.Where(u => u.Email == email).Select(u => u.Id).SingleOrDefault();
            context.Connections.RemoveRange(context.Connections.Where(p => p.PersonId == personId).ToList());
            context.SaveChanges();
              Clients.Caller.SendAsync("Logout");
        }
    }
}