global using Model.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebBanHangOnline.Data.Repository;
using WebBanHangOnline.Data.Repository.IRepository;
using WebBanHangOnline.Helper;
using WebBanHangOnline.Models;
using Microsoft.AspNetCore.Identity;
using WebBanHangOnline.Data;
using WebBanHangOnline.Models.Entity;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebBanHangOnline.Models.Utility;
using Stripe;
using WebBanHangOnline.Models.DbContext;
using Microsoft.AspNetCore.Authorization;
using WebBanHangOnline.Areas.Identity.Pages.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WebBanHangOnlineContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WebBanHangDemoContext>();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<WebBanHangOnlineContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WebBanHangOnlineContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//    .AddRoles<ApplicationRole>()
//    .AddEntityFrameworkStores<WebBanHangOnlineContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender, TbEmailSender>();

// Tự động gán các giá trị SerectKey, PublishableKey vào model
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));


builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Sử dụng lệnh này sau đăng ký Identity
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Identity/Account/Login";
    option.LogoutPath = "/Identity/Account/Logout";
    option.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//https://docs.automapper.org/en/stable/Dependency-injection.html
//builder.Services.AddAutoMapper(typeof(autoMapperProfile));

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SerectKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

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


