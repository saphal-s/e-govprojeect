using empmgmt;
using empmgmt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//db connect

builder.Services.AddDbContext<EmpDbContext>(op =>
{
    op.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=empDb;Integrated Security = True; ");
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<EmpDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = new PathString("/Home/Login");
    //op.AccessDeniedPath = new PathString("/Home/Login");
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
