using EPROJECT.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the DbContext for the app (use your actual connection string name)
builder.Services.AddDbContext<insurance_companyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

// Configure session middleware (make sure it's added before authentication)
builder.Services.AddDistributedMemoryCache();  // Adds memory cache for sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);  // Adjust session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;  // Essential for session cookies
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("NonAdminOnly", policy =>
        policy.RequireAssertion(context =>
            !context.User.IsInRole("Admin")));
});


// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Where to redirect if unauthenticated
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);  // Cookie expiration time
        options.SlidingExpiration = true;  // Enable sliding expiration for cookies
        options.AccessDeniedPath = "/insurance/error";  // Access denied path
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use Session middleware before Authentication and Authorization
app.UseSession();  // Ensure session is enabled here

app.UseRouting();
app.UseAuthentication();  // Authentication comes after session
app.UseAuthorization();   // Authorization comes after session and authentication

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=insurance}/{action=index}/{id?}");

app.Run();
