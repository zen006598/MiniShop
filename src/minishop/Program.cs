using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using minishop.Data;

var builder = WebApplication.CreateBuilder(args);

//Db connection with user secrets
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
{
    Password = builder.Configuration["DefaultConnection:Password"]
} ?? throw new InvalidOperationException("Connection string 'minishopIdentityDbContextConnection' not found.");
var mssqlConnection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(mssqlConnection));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
   {
       options.Password.RequireDigit = false;
       options.Password.RequiredLength = 6;
       options.Password.RequireNonAlphanumeric = false;
       options.Password.RequireUppercase = false;
       options.Password.RequireLowercase = true;
   }
).AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
