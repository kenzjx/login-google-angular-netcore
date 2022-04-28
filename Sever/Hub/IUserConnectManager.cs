namespace Server.Hub
{
    public interface IUserConnectManager
    {
        void KeepUserConnect(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnects(string userId);
    }
}