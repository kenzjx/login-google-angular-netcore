using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure;
using Server.Interfaces;
using Server.Models.Employee;
using Sever.Infrastructure;

namespace Server.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly AppDbContext context;

        private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly IEmployee employeeService;
        private readonly UserManager<AppUser> userManager;
        public EmployeeController(UserManager<AppUser> userManager, IEmployee employee, AppDbContext context) : base(userManager)
        {
            this.employeeService = employee;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllEmployee()
        {
            var reult = await employeeService.AllEmployee();
            return Ok(reult);
        }

        [HttpGet("id")]
        public async Task<IActionResult> EmployeeFile(int id)
        {
            var (Data, type, name) = await employeeService.GetPhoto(id);
            return File(Data, type, name);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostEmployee([FromForm] EmployeeRequest model)
        {
            try
            {
                await employeeService.CreateEmployee(model);
                var employeeId = context.Employees.OrderBy(x => x.Id).Select(x => x.Id).LastOrDefault();
                var file = model.MyFile;
                var result = await SaveFile(file, employeeId);
                return Ok(result);

            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> SaveFile(IFormFile myFile, int Id)
        {
            var photo = new Photo();
            if (myFile.Length > 0)
            {
                var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName);
                var path = Path.Combine(AppDirectory, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    myFile.CopyTo(stream);
                }
                photo.EmployeeId = Id;
                photo.FileName = fileName;
                photo.PathName = path;
                photo.FileExtension = Path.GetExtension(myFile.FileName).Substring(1);
                await context.Photos.AddAsync(photo);
                await context.SaveChangesAsync();
                return path;
            }
            return "Error";

        }


        // [HttpPost, DisableRequestSizeLimit]
        // [Consumes("multipart/form-data")]
        // public IActionResult Upload()
        // {
        //     try
        //     {
        //         var file = Request.Form.Files[0];

        //         if (file.Length > 0)
        //         {

        //             var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
        //             var path = Path.Combine(AppDirectory, fileName);
        //             using (var stream = new FileStream(path, FileMode.Create))
        //             {
        //                 file.CopyTo(stream);
        //             }
        //             return Ok(new { path });
        //         }
        //         else
        //         {
        //             return BadRequest();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Internal server error: {ex}");
        //     }
        // }

    }
}