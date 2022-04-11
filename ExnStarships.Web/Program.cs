using AutoMapper;
using ExnStarships.Data;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Crews;
using ExnStarships.Services.Navigation;
using ExnStarships.Services.Ships;
using ExnStarships.Web;
using ExnStarships.Web.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MapperProfile>();
});

builder.Services.AddDbContext<MainContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MainContext"));
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IShipModelService, ShipModelService>();
builder.Services.AddScoped<IShipService, ShipService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMPR();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();