namespace Server.Models.Employee
{
    public class EmployeeRequest
    {
              
        public string Name {set;get;} = "phong" ;

        public string? Address {set;get;} = "asdf";

        public DateTime BirthDay {set;get;}

        public IFormFile MyFile { get; set; }
    }
}