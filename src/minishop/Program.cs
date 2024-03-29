using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using minishop.Data;
using Serilog;
using minishop.Services;
using minishop.Models;

var builder = WebApplication.CreateBuilder(args);
//log config from appsettings.json
Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(builder.Configuration)
   .CreateLogger();
try
{
    //Db connection with user secrets
    var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
    {
        Password = builder.Configuration["DefaultConnection:Password"]
    } ?? throw new InvalidOperationException("Connection string 'minishopIdentityDbContextConnection' not found.");
    var mssqlConnection = conStrBuilder.ConnectionString;

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(mssqlConnection));

    builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
       {
           options.SignIn.RequireConfirmedAccount = false;
           options.Password.RequireDigit = false;
           options.Password.RequiredLength = 6;
           options.Password.RequireNonAlphanumeric = false;
           options.Password.RequireUppercase = false;
           options.Password.RequireLowercase = true;
       }
    )
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Host.UseSerilog();
    //Auto mapper
    builder.Services.AddAutoMapper(typeof(Program));

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentityInitializerService.SeedData(userManager, roleManager);
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication(); ;

    app.UseAuthorization();

    app.UseSerilogRequestLogging();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Error(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
