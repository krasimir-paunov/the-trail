using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class ModernChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The First World War"))
            {
                return;
            }

            int eraId = await context.Eras
                .Where(e => e.Name == "Modern History")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The First World War",
                Subtitle = "The war that was supposed to be over by Christmas",
                Content = BuildContent(),
                Order = 1,
                EstimatedMinutes = 10,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The First World War")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What event directly triggered the First World War?", OptionA = "The sinking of the Lusitania", OptionB = "The assassination of Archduke Franz Ferdinand", OptionC = "Germany's invasion of Belgium", OptionD = "The Russian Revolution", CorrectOption = "B", Order = 1 },
                    new Question { Text = "How many people died in the First World War?", OptionA = "Around 1 million", OptionB = "Around 5 million", OptionC = "Around 17–20 million", OptionD = "Around 50 million", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What was the name of the defensive system of trenches on the Western Front?", OptionA = "The Hindenburg Line", OptionB = "The Maginot Line", OptionC = "The Western Trench System", OptionD = "No Man's Land", CorrectOption = "A", Order = 3 },
                    new Question { Text = "Which treaty officially ended the First World War?", OptionA = "The Treaty of Versailles", OptionB = "The Treaty of Vienna", OptionC = "The Treaty of Paris", OptionD = "The Treaty of Berlin", CorrectOption = "A", Order = 4 },
                    new Question { Text = "Which new technology made its combat debut in WWI?", OptionA = "The submarine", OptionB = "The tank", OptionC = "The machine gun", OptionD = "Artillery", CorrectOption = "B", Order = 5 }
                }
            });

            await context.SaveChangesAsync();
        }

        private static string BuildContent() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On the morning of June 28, 1914, a nineteen-year-old Bosnian Serb named Gavrilo Princip fired two shots in Sarajevo that would kill Archduke Franz Ferdinand of Austria-Hungary and his wife Sophie — and set in motion a chain of events that would kill seventeen million people. The First World War had no single cause. It was the product of decades of imperial rivalry, military build-up, entangled alliances and nationalist fury — a powder keg waiting for a spark." },
            new { type = "fact", title = "The Alliance System", text = "Europe in 1914 was divided into two armed camps: the Triple Alliance (Germany, Austria-Hungary, Italy) and the Triple Entente (France, Russia, Britain). When Austria-Hungary declared war on Serbia, Russia mobilised. When Russia mobilised, Germany declared war on Russia and France. When Germany invaded Belgium, Britain declared war on Germany. An assassination in Sarajevo had become a world war in 37 days." },
            new { type = "image", url = "/images/chapters/modern/trenches.png", caption = "Allied soldiers in the trenches of the Western Front — a system of defensive earthworks stretching 700 kilometres from Belgium to Switzerland." },
            new { type = "paragraph", text = "Everyone expected the war to be short. German military strategy — the Schlieffen Plan — called for a rapid six-week victory over France before turning to face Russia. British soldiers were told they would be 'home before the leaves fall.' Instead, the war settled into four years of grinding attrition. The development of the machine gun, barbed wire and artillery made offensive advances suicidal. Armies dug into the earth. The Western Front — a line of trenches stretching 700 kilometres — barely moved for three years." },
            new { type = "timeline", date = "July 28, 1914", evnt = "Austria-Hungary declares war on Serbia. The alliance system triggers a cascade of declarations — the Great War begins." },
            new { type = "paragraph", text = "The Battle of the Somme, beginning July 1, 1916, remains one of the deadliest days in military history. On the first day alone, the British Army suffered 57,470 casualties — 19,240 of them killed. Over the four-month battle, over one million men were killed or wounded on both sides. The territory gained by the Allies: approximately ten kilometres." },
            new { type = "quote", text = "We are all going to die. We might as well be brave about it.", source = "A British soldier's diary, the Somme, 1916" },
            new { type = "image", url = "/images/chapters/modern/somme.png", caption = "The Somme battlefield, 1916 — a landscape reduced to mud and shell craters, the front lines barely visible." },
            new { type = "timeline", date = "November 11, 1918", evnt = "The Armistice is signed at 11:00 AM. The guns fall silent after four years, three months and two weeks of industrial warfare." },
            new { type = "paragraph", text = "When it was finally over, the map of the world had been redrawn. Four empires had collapsed — the Ottoman, Austro-Hungarian, Russian and German. The Treaty of Versailles imposed crippling reparations and humiliations on Germany that many historians argue made the Second World War inevitable. The war that was supposed to end all wars had in fact planted the seeds of an even greater catastrophe twenty years later." },
            new { type = "fact", title = "The Overlooked Pandemic", text = "Weakened soldiers and the mass movement of troops across continents helped spread the 1918 influenza pandemic — the Spanish Flu. It infected 500 million people worldwide and killed between 50 and 100 million — more than the war itself. It remains the deadliest disease outbreak in recorded human history." },
            new { type = "image", url = "/images/chapters/modern/armistice.png", caption = "Crowds celebrate the Armistice in London, November 11, 1918 — four years of unimaginable loss finally at an end." }
        });
    }
}