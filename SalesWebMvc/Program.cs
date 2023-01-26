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

builder.Services.AddScoped<SeedingService>();  //Adicionado devido ao .NET 6
builder.Services.AddScoped<SellerService>(); //Adicionado devido ao .NET 6

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed(); /* Adicionado devido ao .NET 6
                                                                                           para o serviço inserir os dados 
                                                                                           no banco */

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
