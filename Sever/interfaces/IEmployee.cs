using Server.Models.Employee;

namespace Server.Interfaces
{
    public interface IEmployee
    {
        Task CreateEmployee (EmployeeRequest model);

        Task<List<EmployeeReponse>> AllEmployee();

        Task<(byte[] Data, string FileType, string FileName)> GetPhoto(int Id);
    }
}