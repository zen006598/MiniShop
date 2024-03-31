using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using minishop.Data;
using Serilog;
using minishop.Services;
using minishop.Models;
using minishop.Repositories.Interfaces;
using minishop.Repositories;
using minishop.Services.Interfaces;
using minishop.SeedData;

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

    builder.Services.AddControllers();

    builder.Services.AddSession();

    builder.Services.AddHttpContextAccessor();

    builder.Host.UseSerilog();
    //Auto mapper
    builder.Services.AddAutoMapper(typeof(Program));

    //DI Services / Repositories
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

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
        var adminUser = await userManager.FindByEmailAsync("admin123@admin.com");
        if (adminUser is not null)
        {
            ProductSeedData.Initialize(scope.ServiceProvider, adminUser.Id);
        }
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

    app.UseSession();

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
