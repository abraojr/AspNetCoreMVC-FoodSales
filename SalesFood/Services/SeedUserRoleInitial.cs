using Microsoft.AspNetCore.Identity;

namespace SalesFood.Services;

public class SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    public void SeedRoles()
    {
        if (!roleManager.RoleExistsAsync("Member").Result)
        {
            IdentityRole role = new()
            {
                Name = "Member",
                NormalizedName = "MEMBER"
            };

            roleManager.CreateAsync(role);
        }

        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            _ = roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUsers()
    {
        if (userManager.FindByEmailAsync("user@localhost").Result == null)
        {
            IdentityUser user = new()
            {
                UserName = "user@localhost",
                Email = "user@localhost",
                NormalizedUserName = "USER@LOCALHOST",
                NormalizedEmail = "USER@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = userManager.CreateAsync(user, "Numsey#2025").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Member").Wait();
            }
        }

        if (userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            IdentityUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMINLOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = userManager.CreateAsync(user, "Numsey#2025").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}