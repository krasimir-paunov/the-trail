using Microsoft.AspNetCore.Identity;
using TheTrail.Data.Seeding.Chapters;
using TheTrail.Data.Seeding.Collectibles;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(
            TheTrailDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await RoleSeeder.SeedAsync(roleManager);
            await AdminSeeder.SeedAsync(userManager);
            await EraSeeder.SeedAsync(context);
            await ChapterSeeder.SeedAsync(context);
            await CollectibleSeeder.SeedAsync(context);
        }
    }
}