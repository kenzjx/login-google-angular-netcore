namespace Server.Infrastructure.Hubs
{
    public partial class Connections
    {
        public Guid Id { get; set; }
        public string PersonId { get; set; }
        public string SignalrId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}