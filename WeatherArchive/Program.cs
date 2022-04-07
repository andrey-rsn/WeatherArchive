using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherArchive.DBContext;
using WeatherArchive.Models;
using WeatherArchive.Services.AutoMapper;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
