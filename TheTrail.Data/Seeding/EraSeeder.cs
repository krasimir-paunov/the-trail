using Microsoft.EntityFrameworkCore;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding
{
    public static class EraSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Eras.AnyAsync())
            {
                return;
            }

            context.Eras.AddRange(
                new Era
                {
                    Name = "Prehistoric",
                    Description = "From the formation of Earth to the first human civilizations — billions of years of life, extinction, and evolution.",
                    ColorTheme = "prehistoric",
                    Order = 1,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Era
                {
                    Name = "Ancient Civilizations",
                    Description = "The rise of the world's first great empires — Egypt, Greece, Rome, Mesopotamia — and the foundations of human culture.",
                    ColorTheme = "ancient",
                    Order = 2,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Era
                {
                    Name = "Medieval",
                    Description = "A thousand years of castles, crusades, plague and faith — the age of knights, kings and the struggle for power.",
                    ColorTheme = "medieval",
                    Order = 3,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Era
                {
                    Name = "Renaissance",
                    Description = "The rebirth of art, science and human thought — from Leonardo to Galileo, the world was reimagined.",
                    ColorTheme = "renaissance",
                    Order = 4,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Era
                {
                    Name = "Age of Exploration",
                    Description = "The age of discovery — when Europeans sailed beyond the horizon and connected the world's civilisations for the first time.",
                    ColorTheme = "exploration",
                    Order = 5,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Era
                {
                    Name = "Modern History",
                    Description = "Revolutions, world wars, independence movements — the turbulent birth of the world we live in today.",
                    ColorTheme = "modern",
                    Order = 6,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Era
                {
                    Name = "Digital Age",
                    Description = "The internet, artificial intelligence, space exploration — humanity's most rapid transformation in history.",
                    ColorTheme = "digital",
                    Order = 7,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            await context.SaveChangesAsync();
        }
    }
}