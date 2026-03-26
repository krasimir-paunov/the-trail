using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class PrehistoricChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Age of Dinosaurs"))
            {
                return;
            }

            int eraId = await context.Eras
                .Where(e => e.Name == "Prehistoric")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Age of Dinosaurs",
                Subtitle = "165 million years of dominance, ended in an instant",
                Content = BuildContent(),
                Order = 1,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Age of Dinosaurs")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "How long did dinosaurs rule the Earth?", OptionA = "65 million years", OptionB = "165 million years", OptionC = "300,000 years", OptionD = "230 million years", CorrectOption = "B", Order = 1 },
                    new Question { Text = "During which period did the first dinosaurs appear?", OptionA = "Jurassic", OptionB = "Cretaceous", OptionC = "Triassic", OptionD = "Permian", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What caused the mass extinction 66 million years ago?", OptionA = "Volcanic eruptions", OptionB = "Ice age", OptionC = "Disease", OptionD = "Asteroid impact", CorrectOption = "D", Order = 3 },
                    new Question { Text = "Which modern animals are direct descendants of dinosaurs?", OptionA = "Crocodiles", OptionB = "Birds", OptionC = "Lizards", OptionD = "Sharks", CorrectOption = "B", Order = 4 },
                    new Question { Text = "What was the wingspan of the largest pterosaurs?", OptionA = "Up to 3 meters", OptionB = "Up to 6 meters", OptionC = "Up to 10 meters", OptionD = "Up to 15 meters", CorrectOption = "C", Order = 5 }
                }
            });

            await context.SaveChangesAsync();
        }

        private static string BuildContent() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Long before humans walked the Earth, our planet was ruled by creatures so magnificent, so terrifying, that even today — 66 million years after their extinction — they capture the imagination of every child and scientist alike. The age of the dinosaurs was not a single moment in time, but a vast dynasty spanning over 165 million years." },
            new { type = "fact", title = "Scale of Time", text = "Dinosaurs ruled the Earth for 165 million years. Humans have existed for roughly 300,000 years. If Earth's history were compressed into a single day, dinosaurs appeared at 10:56 PM and humans at 11:58:43 PM." },
            new { type = "image", url = "/images/chapters/prehistoric/landscape.png", caption = "The prehistoric Earth — a world of ancient ferns, towering conifers and vast untamed wilderness." },
            new { type = "paragraph", text = "The Mesozoic Era — the age of dinosaurs — is divided into three periods: the Triassic, the Jurassic and the Cretaceous. Each period saw different species dominate, rise and fall. The first dinosaurs were relatively small, bipedal creatures that appeared during the Late Triassic period, approximately 230 million years ago." },
            new { type = "timeline", date = "230 Million BCE", evnt = "First dinosaurs appear during the Late Triassic period in what is now South America" },
            new { type = "paragraph", text = "During the Jurassic period, dinosaurs diversified explosively. Giants like the Brachiosaurus stretched their necks to reach the tops of towering conifers. Predators like Allosaurus stalked the ancient landscapes. The skies, not yet claimed by birds, were dominated by pterosaurs — flying reptiles with wingspans that could exceed ten meters." },
            new { type = "fact", title = "The T-Rex", text = "Tyrannosaurus Rex lived during the very end of the Cretaceous period — closer in time to us than to the Stegosaurus. A T-Rex could consume up to 500 pounds of meat in a single bite, and its bite force was the strongest of any land animal ever discovered." },
            new { type = "paragraph", text = "Then, 66 million years ago, everything changed. An asteroid approximately 10 kilometers wide struck what is now the Yucatan Peninsula in Mexico. The impact released energy equivalent to billions of nuclear bombs. The sky darkened with debris. Temperatures plummeted. The food chain collapsed. In geological terms, it happened in an instant." },
            new { type = "image", url = "/images/chapters/prehistoric/meteor.png", caption = "The Chicxulub impact — the moment that ended 165 million years of dinosaur dominance." },
            new { type = "timeline", date = "66 Million BCE", evnt = "Chicxulub asteroid impact triggers the fifth mass extinction. 75% of all species perish, including all non-avian dinosaurs." },
            new { type = "quote", text = "The dinosaurs never really went extinct. They just learned to fly.", source = "Modern Paleontology" },
            new { type = "paragraph", text = "But the story did not end there. A small group of feathered dinosaurs survived. Over millions of years, they diversified into the 10,000 species of birds we see today. Every time you see a crow, an eagle or a sparrow — you are looking at a living dinosaur. The trail of life continued." },
            new { type = "image", url = "/images/chapters/prehistoric/birds.png", caption = "Modern birds — the living descendants of the dinosaurs, carrying their legacy into the present day." }
        });
    }
}