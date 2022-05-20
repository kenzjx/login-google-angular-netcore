using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Infrastructure
{
    public class Photo
    {
        [Key]
        public int Id {set;get;}

        public string PathName {set;get;}

        public string FileName {set;get;}

        public string FileExtension {set;get;}
        
        public int EmployeeId {set;get;}

        [ForeignKey("EmployeeId")]
        public Employee Employee {set;get;}


    }
}