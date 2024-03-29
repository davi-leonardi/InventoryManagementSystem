using InventoryManSys.Data;
using InventoryManSys.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InventoryManSys.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using InventoryManSys.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["KVURL"]),
    new DefaultAzureCredential()
);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //changed connectionString
    options.UseNpgsql(builder.Configuration.GetConnectionString("IMS6"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddDefaultTokenProviders()
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    //options.Cookie.Expiration 

    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
    //options.ReturnUrlParameter=""
});

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddAutoMapper(typeof(WarehouseProfille));

builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
