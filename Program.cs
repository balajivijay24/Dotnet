//  Title:Employee Management
//  Discription:Employee management is the practice of creating a unique 
//      digital identity for each employee, which is then used to identify, authenticate,
//      and authorize their access to the organization’s IT resources. The digital attributes of an 
//      employee’s identity are stored in a central database,
//      making it easier to instantly and securely verify their access and activities
//   Author:Sargunan.P 
//   Last Review:13/6/2023
//   Reviewd by:Anitha 
using IdentityManagement;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using IdentityManagement.Filters;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

builder.Services.AddDbContext<EmployeeDataDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnections")));
//////web api
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>{
        option.LoginPath="/Admin/Success";
        option.ExpireTimeSpan=TimeSpan.FromMinutes(20);

    });
var apiEndpoint = configuration["ApiEndpoint"];
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri(apiEndpoint);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("IdentityManagement/json"));
            });
///Action Filter//
builder.Services.AddScoped<LogActionFilterAttribute>();
//Action Filter//



///web//  
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var _logger=new LoggerConfiguration().
WriteTo.File("C:\\Users\\sargu\\Desktop\\Finalproject\\IdentityManagement\\loger\\Loger-.log",rollingInterval:RollingInterval.Day).CreateLogger();
 builder.Logging.AddSerilog(_logger);
///web Api///
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Signup", Version = "v1" });
            });
/////web Api//
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseSession();
app.UseAuthorization();
////web api//
app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Signup v1");
            });



app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
app.Run();
