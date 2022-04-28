namespace Server.Hub.Interface
{
    public interface IUserConnectManager
    {
        void KeepUserConnect(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnects(string userId);
    }
}