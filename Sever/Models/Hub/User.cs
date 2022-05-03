namespace Server.Models.Hub
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string connId { get; set; } //signalrId

        public User(string someId, string someName, string someConnId)
        {
            id = someId;
            name = someName;
            connId = someConnId; //
        }

       
    }
     public class PersonInfo
    {
        public string userName { get; set; }
        
    }
}