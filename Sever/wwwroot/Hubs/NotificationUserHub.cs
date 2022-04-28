
using Server.Hub.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class NotificationUserHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IUserConnectManager _userConnectionManager;

        public NotificationUserHub(IUserConnectManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }

        public string GetConnectId()
        {
            var httpContext = this.Context.GetHttpContext();
            
        }
    }
}