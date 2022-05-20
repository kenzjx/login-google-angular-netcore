using System.ComponentModel.DataAnnotations;

namespace Server.Infrastructure
{
    public class Employee
    {
        [Key]
        public int Id {set;get;}

        public string Name {set;get;}

        public string? Address {set;get;}

        public DateTime BirthDay {set;get;}

        public ICollection<Photo> Photos {set;get;}
    }
}