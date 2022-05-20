using System.IO;
using Microsoft.EntityFrameworkCore;
using Server.Infrastructure;
using Server.Interfaces;
using Server.Models.Employee;
using Server.Models.Photo;

namespace Server.Services
{
    public class EmployeeService : IEmployee
    {

        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly AppDbContext context;

        public EmployeeService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<EmployeeReponse>> AllEmployee()
        {   
            List<EmployeeReponse> listReponse = new List<EmployeeReponse>();
            
            var employes = await context.Employees.ToListAsync();
            foreach (var item in employes)
            {
                var Emp = new EmployeeReponse();
                Emp.Id = item.Id;
                Emp.Name = item.Name;
                Emp.BirthDay = item.BirthDay;
                Emp.Address = item.Address;

                listReponse.Add(Emp);
            }
            return listReponse;
        }

        public async Task CreateEmployee(EmployeeRequest model)
        {      
            var ep = new Employee()
            {
                Name = model.Name,
                BirthDay = model.BirthDay,
                Address = model.Address,
            };
            await context.Employees.AddAsync(ep);
            await context.SaveChangesAsync();            
        }

       

        public async Task<(byte[] Data, string FileType, string FileName)> GetPhoto(int Id)
        {
            var file = context.Photos.Where(p => p.EmployeeId == Id).FirstOrDefault();
            var path = Path.Combine(AppDirectory, file?.FileName);
            var memory = new MemoryStream();
            using(var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var contenType = $"image/{file.FileExtension}";
            var fileName = Path.GetFileName(path);

            return (memory.ToArray(), contenType, fileName);
        }

       
    }
}