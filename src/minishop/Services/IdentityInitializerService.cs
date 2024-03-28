using Microsoft.AspNetCore.Identity;
using minishop.Models;

namespace minishop.Services;

public class IdentityInitializerService
{

    public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedUsers(userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
    {
        string email = "admin123@admin.com";
        string password = "admin123";

        if (await userManager.FindByEmailAsync(email) is null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
