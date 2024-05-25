using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcPustok.Data;
using Project.Models;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options => {
  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options => {
  options.ClientId = builder.Configuration["Google:ClientId"];
  options.ClientSecret = builder.Configuration["Google:ClientSecret"];
  options.CallbackPath = "/auth/google-response";
  options.SaveTokens = true;
});


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<StaticService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddDbContext<AppDbContext>(options => {
  options.UseSqlServer(builder.Configuration.GetConnectionString("Home"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt => {
  opt.Password.RequireNonAlphanumeric = false;
  opt.Password.RequireUppercase = false;
  opt.Password.RequireLowercase = false;
  opt.Password.RequiredLength = 8;
  opt.User.RequireUniqueEmail = true;
  opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
  opt.Lockout.MaxFailedAccessAttempts = 5;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(opt => {
  opt.Events.OnRedirectToLogin = opt.Events.OnRedirectToAccessDenied = context => {
    if (context.Request.Path.Value.ToLower().StartsWith("/manage")) {
      var uri = new Uri(context.RedirectUri);
      context.Response.Redirect("/manage/auth/login" + uri.Query);
    }
    else {
      var uri = new Uri(context.RedirectUri);
      context.Response.Redirect("/auth/login" + uri.Query);
    }
    return Task.CompletedTask;
  };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
                     name: "areas",
                     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                 );

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
