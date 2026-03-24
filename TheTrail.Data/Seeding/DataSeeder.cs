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
                context.Eras.AddRange(
                    new Domain.Entities.Era
                    {
                        Name = "Prehistoric",
                        Description = "From the formation of Earth to the first human civilizations — billions of years of life, extinction, and evolution.",
                        ColorTheme = "prehistoric",
                        Order = 1,
                        IsPublished = true,
                        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Domain.Entities.Era
                    {
                        Name = "Ancient Civilizations",
                        Description = "The rise of the world's first great empires — Egypt, Greece, Rome, Mesopotamia — and the foundations of human culture.",
                        ColorTheme = "ancient",
                        Order = 2,
                        IsPublished = true,
                        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Domain.Entities.Era
                    {
                        Name = "Medieval",
                        Description = "A thousand years of castles, crusades, plague and faith — the age of knights, kings and the struggle for power.",
                        ColorTheme = "medieval",
                        Order = 3,
                        IsPublished = true,
                        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Domain.Entities.Era
                    {
                        Name = "Renaissance",
                        Description = "The rebirth of art, science and human thought — from Leonardo to Galileo, the world was reimagined.",
                        ColorTheme = "renaissance",
                        Order = 4,
                        IsPublished = true,
                        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Domain.Entities.Era
                    {
                        Name = "Modern History",
                        Description = "Revolutions, world wars, independence movements — the turbulent birth of the world we live in today.",
                        ColorTheme = "modern",
                        Order = 5,
                        IsPublished = true,
                        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    },
                    new Domain.Entities.Era
                    {
                        Name = "Digital Age",
                        Description = "The internet, artificial intelligence, space exploration — humanity's most rapid transformation in history.",
                        ColorTheme = "digital",
                        Order = 6,
                        IsPublished = true,
                        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}