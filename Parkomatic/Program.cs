using Microsoft.EntityFrameworkCore;
using Parkomatic.Data;
using Parkomatic.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ParkomaticContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ParkomaticContext") ?? throw new InvalidOperationException("Connection string 'ParkomaticContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<Vehicle>), typeof(VehicleRepository));
builder.Services.AddScoped(typeof(IRepository<ParkingSpot>), typeof(ParkingSpotRepository));
builder.Services.AddScoped(typeof(IRepository<Pass>), typeof(PassRepository));
builder.Services.AddScoped(typeof(IRepository<Reservation>), typeof(ReservationRepository));
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
