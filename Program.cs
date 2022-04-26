using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SignalR.Data;
using SignalR.Data.Entities;
using SignalR.IdentityServer;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ManageAppDbContext>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ManageUser, IdentityRole>().AddEntityFrameworkStores<ManageAppDbContext>()
.AddDefaultTokenProviders();

var bui = builder.Services.AddIdentityServer(options =>{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents =true;
    options.Events.RaiseFailureEvents  = true;
    options.Events.RaiseSuccessEvents = true;
}).AddInMemoryApiResources(Config.Apis)
.AddInMemoryClients(Config.Clients)
.AddInMemoryIdentityResources(Config.Ids)
.AddAspNetIdentity<ManageUser>()
.AddDeveloperSigningCredential();

builder.Services.AddAuthentication().AddLocalApi("Bearer", options =>{
    options.ExpectedScope = "api.WebApp";
});

builder.Services.AddAuthorization(options =>{
    options.AddPolicy("Bearer", policy =>{
        policy.AddAuthenticationSchemes("Bearer");
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddRazorPages(options =>{
    options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account", model=>{
        foreach(var selector in model.Selectors)
        {
            var atributeRouteModel = selector.AttributeRouteModel;
            atributeRouteModel.Order = -1;
            atributeRouteModel.Template = atributeRouteModel.Template.Remove(0, "Identity".Length);
        }
    });
});
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp Space Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(builder.Configuration["AuthorityUrl"] + "/connect/authorize"),
                            Scopes = new Dictionary<string, string> { { "api.WebApp", "WebApp API" } }
                        },
                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>{ "api.WebApp" }
                    }
                });


            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseIdentityServer();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
app.UseSwagger();
app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("swagger");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp Space Api V1");
            });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
