using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ThucTap.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ThucTapDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ThucTapConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(options =>
 {
	 options.Cookie.Name = "ThucTap.Cookie";
	 options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
	 options.SlidingExpiration = true;
	 options.LoginPath = "/Home/Login";
	 options.LogoutPath = "/Home/Logout";
	 options.AccessDeniedPath = "/Home/Forbidden";
 });
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options => {
	options.Cookie.Name = "ThucTap.Session";
	options.IdleTimeout = TimeSpan.FromMinutes(15);
});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(name: "adminareas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();