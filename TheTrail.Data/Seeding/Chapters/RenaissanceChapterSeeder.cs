using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class RenaissanceChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Leonardo da Vinci"))
            {
                return;
            }

            int eraId = await context.Eras
                .Where(e => e.Name == "Renaissance")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Leonardo da Vinci",
                Subtitle = "The man who wanted to know everything",
                Content = BuildContent(),
                Order = 1,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Leonardo da Vinci")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Where was Leonardo da Vinci born?", OptionA = "Rome", OptionB = "Florence", OptionC = "Vinci, near Florence", OptionD = "Milan", CorrectOption = "C", Order = 1 },
                    new Question { Text = "How many notebooks did Leonardo fill during his lifetime?", OptionA = "Approximately 50", OptionB = "Approximately 13,000 pages across many notebooks", OptionC = "Exactly 100", OptionD = "Around 500 pages", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Which technique did Leonardo pioneer to show depth in painting?", OptionA = "Fresco", OptionB = "Chiaroscuro and sfumato", OptionC = "Pointillism", OptionD = "Tempera", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What is the Vitruvian Man?", OptionA = "A Roman statue", OptionB = "A drawing showing ideal human proportions", OptionC = "A self-portrait of Leonardo", OptionD = "A design for a flying machine", CorrectOption = "B", Order = 4 },
                    new Question { Text = "Which of Leonardo's paintings is the most famous in the world?", OptionA = "The Last Supper", OptionB = "The Virgin of the Rocks", OptionC = "The Mona Lisa", OptionD = "Lady with an Ermine", CorrectOption = "C", Order = 5 }
                }
            });

            await context.SaveChangesAsync();
        }

        private static string BuildContent() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On April 15, 1452, in a small hilltop village in Tuscany, an illegitimate child was born to a notary and a peasant woman. No one could have predicted that this boy — raised without formal education, denied the privileges of legitimate birth, apprenticed to a painter at fourteen — would become the most curious mind in human history. His name was Leonardo di ser Piero da Vinci. We know him simply as Leonardo." },
            new { type = "fact", title = "The Scope of His Curiosity", text = "Leonardo's notebooks contain designs for helicopters, solar power, calculators, and plate tectonics — all conceived in the 15th century. He was the first person to correctly explain why the sky is blue, first to study the aortic valve of the heart, and first to understand how birds generate lift. He filled approximately 13,000 pages of notes in his lifetime." },
            new { type = "image", url = "/images/chapters/renaissance/vitruvian.png", caption = "The Vitruvian Man, c. 1490 — Leonardo's exploration of the ideal proportions of the human body, combining art and science." },
            new { type = "paragraph", text = "Leonardo's genius was inseparable from his method: observation. Where medieval scholars trusted ancient texts, Leonardo trusted his eyes. He dissected over thirty human corpses to understand anatomy. He watched how water moved and how birds flew, filling page after page with sketches. He understood that to paint nature, you had to understand nature — not as an artist, but as a scientist." },
            new { type = "timeline", date = "1482", evnt = "Leonardo moves to Milan to serve Ludovico Sforza as court artist and engineer, beginning his most productive period" },
            new { type = "paragraph", text = "His painting technique was revolutionary. Leonardo developed sfumato — from the Italian word for smoke — a method of blending transitions between light and shadow so gradually that there are no harsh lines. Combined with chiaroscuro, the dramatic contrast of light and dark, these techniques gave his subjects an uncanny sense of life. The Mona Lisa's famous smile seems to shift because Leonardo used different techniques in different areas of the face, exploiting the way peripheral vision processes light." },
            new { type = "quote", text = "Learning never exhausts the mind.", source = "Leonardo da Vinci" },
            new { type = "image", url = "/images/chapters/renaissance/studio.png", caption = "A Renaissance artist's workshop — where art, science and philosophy were inseparable disciplines." },
            new { type = "timeline", date = "1503–1519", evnt = "Leonardo paints the Mona Lisa and completes his final years in France under the patronage of King Francis I" },
            new { type = "paragraph", text = "Yet Leonardo finished remarkably few works. He was plagued by perfectionism and relentless curiosity — always starting new investigations before completing old ones. His patron Ludovico once complained that Leonardo spent more time studying mathematics than painting. This was true. Leonardo did not see himself primarily as a painter. He saw himself as a man who wanted to understand everything." },
            new { type = "fact", title = "Ahead of His Time", text = "Leonardo designed a working robot knight in 1495, a rudimentary calculator, a solar power concentrator, and an armoured vehicle resembling a tank. He understood that the Earth was far older than the Bible suggested, noting fossilised sea shells on mountaintops as evidence of ancient seas. Most of his scientific insights went unrecognised for centuries." },
            new { type = "image", url = "/images/chapters/renaissance/notebooks.png", caption = "Pages from Leonardo's notebooks — written in mirror script from right to left, filled with drawings, diagrams and theories." }
        });
    }
}