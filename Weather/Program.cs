using Microsoft.EntityFrameworkCore;
using Weather.Models;
using Weather.Services;
using Weather.Models.repositories;
using System.Globalization;
using static Weather.Services.WeatherRecordService;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IWeatherRecordService, WeatherRecordService>();
builder.Services.AddScoped<IWeatherRecordRepository, WeatherRecordRepository>();
builder.Services.AddHostedService<WeatherRecordGeneratorService>();

builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
var defaultCulture = new CultureInfo("en-US");
defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
});
app.MapControllerRoute(
    name: "weatherRecord",
    pattern: "{action=Index}/{id?}",
    defaults: new { controller = "weatherRecords" });

app.Run();
