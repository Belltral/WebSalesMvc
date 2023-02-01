using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMvcContext"), 
    MySqlServerVersion.LatestSupportedServerVersion));
    /*options.UseSqlServer(Configuration.GetConnectionString("SalesWebMvcContext"), builder =>
    builder.MigrationsAssembly("SalesWebMvc")));*/
    /* ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));*/

// Add services to the container.
builder.Services.AddControllersWithViews();

    //Escopo de servi�os adicionados:
builder.Services.AddScoped<SeedingService>();  //Adicionado devido ao .NET 6
builder.Services.AddScoped<SellerService>(); //Adicionado devido ao .NET 6
builder.Services.AddScoped<DepartmentService>(); //Adicionado devido ao .NET 6
builder.Services.AddScoped<SalesRecordService>(); //Adicionado devido ao .NET 6

var enUS = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed(); /* Adicionado devido ao .NET 6
                                                                                           para o servi�o inserir os dados 
                                                                                           no banco */
app.UseRequestLocalization(localizationOptions);

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
