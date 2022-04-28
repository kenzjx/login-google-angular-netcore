using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Infrastructure;
using Server.Interfaces;
using Server.Options;
using Server.SeedData;
using Server.Services;
using Sever.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddOptions();
var authenOptions = builder.Configuration.GetSection("Authentication");
builder.Services.Configure<Authentication>(authenOptions);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppContextString"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>{
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(
   JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Jwt:Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
         ClockSkew = TimeSpan.Zero
    };
}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = "/nhap-tu-google";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
   
var app = builder.Build();


// using(var scope = app.Services.CreateScope())
// {
//     var servicesDb = scope.ServiceProvider;
//     try
//     {
//         var dbConxext = servicesDb.GetRequiredService<AppDbContext>();
//         if(dbConxext.Database.IsSqlServer())
//         {
//             dbConxext.Database.Migrate();
//         }
//     }
//     catch (System.Exception ex)
//     {
//         var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex , "An Error white migraion");
//         throw;
//     }
// }


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
      app.UseDeveloperExceptionPage();
   
}
 app.UseSwagger();
   
 app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GOOGLE AUTH SAMPLE API V1");
                c.RoutePrefix = string.Empty;
            });
app.UseRouting();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

app.Run();
