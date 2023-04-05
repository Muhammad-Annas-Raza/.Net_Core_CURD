using CRUD.DAL;
using CRUD.Interface;
using CRUD.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// For Session Use
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(20);
//    //options.Cookie.HttpOnly = true;
//    //options.Cookie.IsEssential = true;
//});


builder.Services.AddAuthentication
    (
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Home/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });


builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

var mycon = builder.Configuration.GetConnectionString("cs_Bus");
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(mycon));

builder.Services.AddTransient<IRepository<tbl_user>, Gen_Repo<tbl_user>>();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


