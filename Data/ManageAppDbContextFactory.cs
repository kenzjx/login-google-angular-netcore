using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SignalR.Data
{
    public class ManageAppDbContextFactory : IDesignTimeDbContextFactory<ManageAppDbContext>
    {
        public ManageAppDbContext CreateDbContext(string[] args)
        {
            var enviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{enviromentName}.json")
            .Build();

             var buider = new DbContextOptionsBuilder<ManageAppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            buider.UseSqlServer(connectionString);
            return new ManageAppDbContext(buider.Options);
        }
    }
}