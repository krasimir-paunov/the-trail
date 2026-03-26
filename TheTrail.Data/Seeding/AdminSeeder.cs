using Microsoft.AspNetCore.Identity;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding
{
    public static class AdminSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "admin@thetrail.com";

            if (await userManager.FindByEmailAsync(adminEmail) != null)
            {
                return;
            }

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
}