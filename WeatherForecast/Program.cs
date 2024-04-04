using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Project.DAL.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "Auth";
    options.LoginPath = "/Auth/Login/Index";
    options.AccessDeniedPath = "/Auth/Error/Error";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

});

builder.Services.AddHttpContextAccessor()
    ;
builder.Services.AddScoped<WeatherForecast.Areas.Auth.IServices.IUserService, WeatherForecast.Areas.Auth.Services.UserService>();
builder.Services.AddScoped<WeatherForecast.Areas.User.IServices.IUserService, WeatherForecast.Areas.User.Services.UserService>();
builder.Services.AddScoped<WeatherForecast.Areas.Admin.IServices.IUserService, WeatherForecast.Areas.Admin.Services.UserService>();

builder.Services.AddScoped<WeatherForecast.Areas.Admin.IServices.IWeatherService, WeatherForecast.Areas.Admin.Services.WeatherService>();
builder.Services.AddScoped<WeatherForecast.Areas.User.IService.IWeatherService, WeatherForecast.Areas.User.Service.WeatherService>();

builder.Services.AddScoped<WeatherForecast.Areas.Admin.IServices.IUserLogService, WeatherForecast.Areas.Admin.Services.UserLogService>();

builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new ToastrOptions
    {
        PositionClass = ToastPositions.TopRight,
        TimeOut = 5000,
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Auth/Error/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNToastNotify();

app.UseStatusCodePagesWithRedirects("/Auth/Error/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{area=Auth}/{controller=Login}/{action=Index}/{id?}"
    );
});

app.MapRazorPages();

app.Run();
