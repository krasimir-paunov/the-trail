using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager);
            await SeedErasAsync(context);
            await SeedChaptersAsync(context);
        }

        private static async Task SeedChaptersAsync(TheTrailDbContext context)
        {
            if (!await context.Chapters.AnyAsync())
            {
                int prehistoricEraId = await context.Eras
                    .Where(e => e.Name == "Prehistoric")
                    .Select(e => e.Id)
                    .FirstOrDefaultAsync();

                if (prehistoricEraId == 0) return;

                string content = System.Text.Json.JsonSerializer.Serialize(new object[]
                {
            new { type = "paragraph", text = "Long before humans walked the Earth, our planet was ruled by creatures so magnificent, so terrifying, that even today — 66 million years after their extinction — they capture the imagination of every child and scientist alike. The age of the dinosaurs was not a single moment in time, but a vast dynasty spanning over 165 million years." },
            new { type = "fact", title = "Scale of Time", text = "Dinosaurs ruled the Earth for 165 million years. Humans have existed for roughly 300,000 years. If Earth's history were compressed into a single day, dinosaurs appeared at 10:56 PM and humans at 11:58:43 PM." },
            new { type = "paragraph", text = "The Mesozoic Era — the age of dinosaurs — is divided into three periods: the Triassic, the Jurassic and the Cretaceous. Each period saw different species dominate, rise and fall. The first dinosaurs were relatively small, bipedal creatures that appeared during the Late Triassic period, approximately 230 million years ago." },
            new { type = "timeline", date = "230 Million BCE", evnt = "First dinosaurs appear during the Late Triassic period in what is now South America" },
            new { type = "paragraph", text = "During the Jurassic period, dinosaurs diversified explosively. Giants like the Brachiosaurus stretched their necks to reach the tops of towering conifers. Predators like Allosaurus stalked the ancient landscapes. The skies, not yet claimed by birds, were dominated by pterosaurs — flying reptiles with wingspans that could exceed ten meters." },
            new { type = "fact", title = "The T-Rex", text = "Tyrannosaurus Rex lived during the very end of the Cretaceous period — closer in time to us than to the Stegosaurus. A T-Rex could consume up to 500 pounds of meat in a single bite, and its bite force was the strongest of any land animal ever discovered." },
            new { type = "paragraph", text = "Then, 66 million years ago, everything changed. A asteroid approximately 10 kilometers wide struck what is now the Yucatan Peninsula in Mexico. The impact released energy equivalent to billions of nuclear bombs. The sky darkened with debris. Temperatures plummeted. The food chain collapsed. In geological terms, it happened in an instant." },
            new { type = "timeline", date = "66 Million BCE", evnt = "Chicxulub asteroid impact triggers the fifth mass extinction. 75% of all species perish, including all non-avian dinosaurs." },
            new { type = "quote", text = "The dinosaurs never really went extinct. They just learned to fly.", source = "Modern Paleontology" },
            new { type = "paragraph", text = "But the story did not end there. A small group of feathered dinosaurs survived. Over millions of years, they diversified into the 10,000 species of birds we see today. Every time you see a crow, an eagle or a sparrow — you are looking at a living dinosaur. The trail of life continued." }
                });

                context.Chapters.Add(new Domain.Entities.Chapter
                {
                    Title = "The Age of Dinosaurs",
                    Subtitle = "165 million years of dominance, ended in an instant",
                    Content = content,
                    Order = 1,
                    EstimatedMinutes = 8,
                    EraId = prehistoricEraId,
                    IsPublished = true,
                    CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });

                await context.SaveChangesAsync();

                // Seed quiz for this chapter
                int chapterId = await context.Chapters
                    .Where(c => c.Title == "The Age of Dinosaurs")
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();

                if (chapterId == 0) return;

                var quiz = new Domain.Entities.Quiz
                {
                    ChapterId = chapterId,
                    PassMarkPercent = 60,
                    Questions = new List<Domain.Entities.Question>
            {
                new Domain.Entities.Question
                {
                    Text = "How long did dinosaurs rule the Earth?",
                    OptionA = "65 million years",
                    OptionB = "165 million years",
                    OptionC = "300,000 years",
                    OptionD = "230 million years",
                    CorrectOption = "B",
                    Order = 1
                },
                new Domain.Entities.Question
                {
                    Text = "During which period did the first dinosaurs appear?",
                    OptionA = "Jurassic",
                    OptionB = "Cretaceous",
                    OptionC = "Triassic",
                    OptionD = "Permian",
                    CorrectOption = "C",
                    Order = 2
                },
                new Domain.Entities.Question
                {
                    Text = "What caused the mass extinction 66 million years ago?",
                    OptionA = "Volcanic eruptions",
                    OptionB = "Ice age",
                    OptionC = "Disease",
                    OptionD = "Asteroid impact",
                    CorrectOption = "D",
                    Order = 3
                },
                new Domain.Entities.Question
                {
                    Text = "Which modern animals are direct descendants of dinosaurs?",
                    OptionA = "Crocodiles",
                    OptionB = "Birds",
                    OptionC = "Lizards",
                    OptionD = "Sharks",
                    CorrectOption = "B",
                    Order = 4
                },
                new Domain.Entities.Question
                {
                    Text = "What was the wingspan of the largest pterosaurs?",
                    OptionA = "Up to 3 meters",
                    OptionB = "Up to 6 meters",
                    OptionC = "Up to 10 meters",
                    OptionD = "Up to 15 meters",
                    CorrectOption = "C",
                    Order = 5
                }
            }
                };

                context.Quizzes.Add(quiz);
                await context.SaveChangesAsync();
            }
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