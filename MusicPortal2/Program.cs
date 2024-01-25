using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddMusicPortalContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IMusicClipCervices, MusicClipService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IUsersServices, UsersService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MusicClips}/{action=Index}/{id?}");

app.Run();
