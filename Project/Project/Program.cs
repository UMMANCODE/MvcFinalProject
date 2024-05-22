using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Helpers.Handlers;
using Project.Models;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<StaticService>();
builder.Services.AddScoped<EmailService>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
	options.Password.RequiredLength = 8;
	options.User.RequireUniqueEmail = true;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<AppDbContext>();

// Configure authentication
builder.Services.AddAuthentication(options => {
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options => {
	options.LoginPath = "/Auth/Login";
	options.AccessDeniedPath = "/Auth/VerifyEmailGet";
});

// Configure authorization
builder.Services.AddAuthorization(options => {
	options.AddPolicy("EmailVerified", policy =>
	{
		// Define your requirements for the EmailVerified policy here
		policy.RequireClaim("email_verified", "true"); // Example requirement
	});
});

// Register email verification handler
builder.Services.AddScoped<IAuthorizationHandler, EmailVerifiedHandler>();

// Configure HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configure cookie events
builder.Services.ConfigureApplicationCookie(options => {
	options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
	{
		var loginPath = context.Request.Path.StartsWithSegments("/Manage", StringComparison.OrdinalIgnoreCase)
				? "/Manage/Auth/Login"
				: "/Auth/Login";
		context.Response.Redirect(loginPath + context.RedirectUri);
		return Task.CompletedTask;
	};
});

var app = builder.Build();

// Configure middleware pipeline
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
