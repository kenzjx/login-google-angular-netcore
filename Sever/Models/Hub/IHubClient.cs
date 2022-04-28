namespace Server.Models.Hub
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}