using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sirius.Data.Access.Auth;
using Sirius.Shared.Auth;
using Sirius.Shared.Constants;

namespace Sirius.Data.Access.Initialization
{
    internal class DbSeeder: IDbSeeder
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeeder(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task SeedAsync(SiriusDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded.
            }

            // Creates Roles.
            await _roleManager.CreateAsync(new IdentityRole(AuthRoles.Admin));
            await _roleManager.CreateAsync(new IdentityRole(AuthRoles.User));
            await context.SaveChangesAsync();

            // Seeds an admin user.
            var user = new UserEntity()
            {
                GivenName = "Admin",
                AccessFailedCount = 0,
                Email = "horovody@gmail.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                NormalizedEmail = "HOROVODY@GMAIL.COM",
                NormalizedUserName = "HOROVODY@GMAIL.COM",
                TwoFactorEnabled = false,
                UserName = "horovody@gmail.com"
            };

            var result = await _userManager.CreateAsync(user, "Admin123");

            if (result.Succeeded)
            {
                var adminUser = await _userManager.FindByNameAsync(user.UserName);
                // Assigns the administrator role.
                await _userManager.AddToRoleAsync(adminUser, AuthRoles.Admin);
                await _userManager.AddToRoleAsync(adminUser, AuthRoles.User);
                // Assigns claims.
                var claims = new List<Claim> {
                    new Claim(type: SiriusClaimTypes.GivenName, value: user.GivenName),
                };
                await _userManager.AddClaimsAsync(adminUser, claims);
                await context.SaveChangesAsync();
            }
        }
    }
}
