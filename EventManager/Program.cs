using EventManager.DataAccess;
using EventManager.DataAccess.Repository;
using EventManager.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;
using EventManager.DataAccess.DbInitializer;
using EventManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.Configure<MailOptions>(options =>
{
    options.Host_Address = builder.Configuration["MailSettings:Address"]!;
    options.Host_Port = Convert.ToInt32(builder.Configuration["MailSettings:Port"]);
    options.Host_Username = builder.Configuration["MailSettings:Account"]!;
    options.Host_Password = builder.Configuration["MailSettings:Password"]!;
    options.Sender_EMail = builder.Configuration["MailSettings:SenderEmail"]!;
    options.Sender_Name = builder.Configuration["MailSettings:SenderName"]!;
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddRazorPages();

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

app.UseAuthentication();
app.UseAuthorization();
SeedDatabase();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    
    dbInitializer.Initialize();
}