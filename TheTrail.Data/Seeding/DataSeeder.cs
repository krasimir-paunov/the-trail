using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager);
            await SeedErasAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "admin@thetrail.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    DisplayName = "The Curator",
                    Email = adminEmail,
                    UserName = adminEmail,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                };

                IdentityResult result = await userManager.CreateAsync(admin, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Administrator");
                }
                else
                {
                    string errors = string.Join(", ", result.Errors.Select(e => e.Description));

                    throw new InvalidOperationException($"Failed to create admin user: {errors}");
                }
            }
        }

        private static async Task SeedErasAsync(TheTrailDbContext context)
        {
            if (!await context.Eras.AnyAsync())
            {
                context.Eras.Add(new Domain.Entities.Era
                {
                    Name = "Prehistoric",
                    Description = "From the formation of Earth to the first human civilizations — billions of years of life, extinction, and evolution.",
                    ColorTheme = "prehistoric",
                    Order = 1,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });

                await context.SaveChangesAsync();
            }
        }
    }
}