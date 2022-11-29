using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Task7.Application;
using Task7.Application.Common.Mappings;
using Task7.Application.Hubs.Game;
using Task7.Application.Hubs.Player;
using Task7.Application.Interfaces;
using Task7.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson()
    .AddRazorRuntimeCompilation();

builder.Services.AddSignalR();

builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { options.LoginPath = "/Login/Index"; });

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ITicTacToeDbContext).Assembly));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");

    endpoints.MapHub<GameHub>("/game-hub");
    endpoints.MapHub<PlayerHub>("/player-hub");
});

app.Run();