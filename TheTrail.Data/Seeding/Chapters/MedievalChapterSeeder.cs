using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class MedievalChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Black Death"))
            {
                return;
            }

            int eraId = await context.Eras
                .Where(e => e.Name == "Medieval")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Black Death",
                Subtitle = "The plague that killed half of Europe and changed the world forever",
                Content = BuildContent(),
                Order = 1,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Black Death")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "When did the Black Death reach Europe?", OptionA = "1215", OptionB = "1348", OptionC = "1492", OptionD = "1066", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What percentage of Europe's population died in the Black Death?", OptionA = "10–15%", OptionB = "20–25%", OptionC = "30–60%", OptionD = "70–80%", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What caused the Black Death?", OptionA = "A virus", OptionB = "The bacterium Yersinia pestis", OptionC = "A fungal infection", OptionD = "Contaminated water", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which unexpected consequence did the Black Death have on survivors?", OptionA = "It weakened feudalism and raised wages", OptionB = "It strengthened the Church's power", OptionC = "It caused widespread famine", OptionD = "It led to immediate democracy", CorrectOption = "A", Order = 4 },
                    new Question { Text = "Where did the Black Death originate before spreading to Europe?", OptionA = "Sub-Saharan Africa", OptionB = "Central Asia", OptionC = "The Arabian Peninsula", OptionD = "India", CorrectOption = "B", Order = 5 }
                }
            });

            await context.SaveChangesAsync();
        }

        private static string BuildContent() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In the autumn of 1347, twelve Genoese trading ships docked at the Sicilian port of Messina. When port officials boarded the vessels, most of the sailors were dead. Those still alive were covered in black, oozing boils that seeped blood and pus. The ships were ordered out of the harbour immediately — but it was already too late. The Black Death had arrived in Europe." },
            new { type = "fact", title = "The Scale of Death", text = "Between 1347 and 1351, the Black Death killed an estimated 30 to 60 percent of Europe's entire population — somewhere between 25 and 50 million people. Some regions lost 70 to 80 percent of their inhabitants. It took Europe nearly 200 years to recover its pre-plague population levels." },
            new { type = "image", url = "/images/chapters/medieval/plague.png", caption = "Medieval physicians wore beak-shaped masks filled with herbs, believing disease spread through 'bad air' — miasma." },
            new { type = "paragraph", text = "The plague was caused by the bacterium Yersinia pestis, carried by fleas living on rats. As rats died in enormous numbers, their fleas jumped to humans. The disease took three terrible forms: bubonic plague — the swelling of lymph nodes into painful black lumps called buboes; septicaemic plague — infection of the bloodstream; and pneumonic plague — infection of the lungs, which spread through the air and was almost universally fatal." },
            new { type = "timeline", date = "October 1347", evnt = "Twelve Genoese ships carrying plague arrive at Messina, Sicily. The Black Death begins its sweep across Europe." },
            new { type = "paragraph", text = "Medieval people had no understanding of bacteria or germ theory. The Church taught that the plague was God's punishment for human sin. Physicians believed in miasma — the idea that disease spread through bad air. Some communities blamed Jewish people, leading to horrific pogroms across Germany and France. Others formed flagellant movements, marching from town to town whipping themselves bloody as penance. None of it stopped the dying." },
            new { type = "quote", text = "So many died that all believed it was the end of the world.", source = "Agnolo di Tura, Siena, 1348" },
            new { type = "image", url = "/images/chapters/medieval/danse-macabre.png", caption = "The Danse Macabre — the Dance of Death — became a powerful artistic motif across medieval Europe during the plague years." },
            new { type = "timeline", date = "1351", evnt = "The first major wave of the Black Death subsides, having killed between 30 and 60 percent of Europe's population in four years." },
            new { type = "paragraph", text = "Yet from this catastrophe came unexpected transformation. With so many dead, surviving peasants and labourers suddenly found themselves in demand. Serfdom — the feudal system that had bound peasants to the land for centuries — began to collapse. Wages rose. Workers demanded rights. The rigid social hierarchy of the Middle Ages cracked. In a very real sense, the Black Death helped birth the conditions that would eventually lead to the Renaissance and the modern world." },
            new { type = "fact", title = "An Unexpected Legacy", text = "The Black Death may have accelerated European development in surprising ways. The labour shortage it created forced technological innovation — better ploughs, windmills, mechanical clocks — as landowners sought to do more with fewer workers. Some historians argue that the plague was a brutal but pivotal catalyst for modernity." },
            new { type = "image", url = "/images/chapters/medieval/recovery.png", caption = "A medieval village slowly recovering — the survivors inherited a world with more land, higher wages, and a shattered certainty in the old order." }
        });
    }
}