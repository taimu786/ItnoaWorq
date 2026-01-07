using ItnoaWorq.Domain.Entities;
using ItnoaWorq.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ItnoaWorq.Infrastructure.Seeders;

public static class IdentitySeed
{
    public static async Task SeedRolesAndSuperAdminAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        string[] roles = ["Public", "Employee", "HR", "Admin", "SuperAdmin"];
        foreach (var r in roles)
            if (!await roleMgr.RoleExistsAsync(r))
                await roleMgr.CreateAsync(new AppRole { Name = r });

        var superAdminEmail = "superadmin@itnoaworq.com";
        var superAdmin = await userMgr.FindByEmailAsync(superAdminEmail);
        if (superAdmin == null)
        {
            superAdmin = new AppUser { UserName = superAdminEmail, Email = superAdminEmail, EmailConfirmed = true };
            var result = await userMgr.CreateAsync(superAdmin, "Super@123");
            if (result.Succeeded)
                await userMgr.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }
}
