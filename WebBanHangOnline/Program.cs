using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Models;
using WebBanHangOnline.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WebBanHangDemoContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<ICategoryWebRepository, CategoryWebRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


