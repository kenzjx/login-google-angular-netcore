using Microsoft.AspNetCore.SignalR;

namespace Server.Models.Hub
{
    public class BroadcastHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("asdf", new List<string>());
        }
    }
}