using Microsoft.AspNetCore.Identity;

namespace ChatApp.Web.Data
{
    public static class IdentitySeed
    {
        public static async Task RunAsync(IServiceProvider sp)
        {
            var roles = new[] { "Admin", "Teacher", "Student", "Parent", "Staff" };

            var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var r in roles)
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));

            var userMgr = sp.GetRequiredService<UserManager<IdentityUser>>();
            var adminEmail = "admin@chat.local";
            var admin = await userMgr.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new IdentityUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true };
                await userMgr.CreateAsync(admin, "Admin123!");
                await userMgr.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
