using Microsoft.AspNetCore.Identity;
namespace Marknadsplatsen.Services;


public class DatabaseSetupService(
    RoleManager<IdentityRole> roleManager,
    UserManager<IdentityUser> userManager)
{
    public async Task InitialAsync()
    {
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            var result = await roleManager.CreateAsync(new IdentityRole("Administrator"));
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create the Administrator role.");
            }
        }

        var adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create the admin user.");
            }
            await userManager.AddToRoleAsync(adminUser, "Administrator");
        }
    }
}
