using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WeatherArchive.DBContext;
using WeatherArchive.Models;
using WeatherArchive.Models.DTOs;
using WeatherArchive.Repositories;
using WeatherArchive.Services.ArchiveManager;
using WeatherArchive.Services.AutoMapper;
using WeatherArchive.Services.FileConverter;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IWeatherConditionsRepository, WeatherConditionsRepository>();
builder.Services.AddScoped<IArchiveManager<WeatherConditionsListViewModel>, ArchiveManager>();
builder.Services.AddSingleton<IFileConverter<IEnumerable<WeatherConditionsDTO>>,ExcelFileConverter>();
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
