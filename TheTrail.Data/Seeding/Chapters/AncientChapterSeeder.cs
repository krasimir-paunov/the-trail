using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class AncientChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Rise of Egypt"))
            {
                return;
            }

            int eraId = await context.Eras
                .Where(e => e.Name == "Ancient Civilizations")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Rise of Egypt",
                Subtitle = "How a desert people built the most enduring civilization in human history",
                Content = BuildContent(),
                Order = 1,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Rise of Egypt")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "When did ancient Egyptian civilization begin?", OptionA = "Around 1000 BCE", OptionB = "Around 3100 BCE", OptionC = "Around 5000 BCE", OptionD = "Around 500 BCE", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What does the word 'pharaoh' mean?", OptionA = "Son of Ra", OptionB = "God of the Nile", OptionC = "Great House", OptionD = "Eternal King", CorrectOption = "C", Order = 2 },
                    new Question { Text = "For how long did ancient Egyptian civilization endure?", OptionA = "500 years", OptionB = "1000 years", OptionC = "2000 years", OptionD = "3000 years", CorrectOption = "D", Order = 3 },
                    new Question { Text = "What was the primary purpose of the pyramids?", OptionA = "Astronomical observatories", OptionB = "Royal tombs", OptionC = "Grain storage", OptionD = "Military fortresses", CorrectOption = "B", Order = 4 },
                    new Question { Text = "Which river was essential to Egyptian civilization?", OptionA = "The Tigris", OptionB = "The Euphrates", OptionC = "The Nile", OptionD = "The Congo", CorrectOption = "C", Order = 5 }
                }
            });

            await context.SaveChangesAsync();
        }

        private static string BuildContent() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In the northeastern corner of Africa, where the Sahara Desert meets the sea, a civilization was born that would endure for three thousand years — longer than any other in human history. Ancient Egypt was not built despite its harsh environment, but because of it. The Nile River, flooding predictably every year, deposited rich black silt across the valley floor, turning desert into farmland and making abundance possible in one of the driest places on Earth." },
            new { type = "fact", title = "The Gift of the Nile", text = "The ancient Greek historian Herodotus called Egypt 'the gift of the Nile.' Every July, the river would flood, retreating by October to leave behind nutrient-rich black soil. Egyptian farmers could produce three harvests per year — feeding a civilization of millions in the middle of a desert." },
            new { type = "image", url = "/images/chapters/ancient/nile.png", caption = "The Nile River at dusk — the lifeblood of ancient Egyptian civilization for over three millennia." },
            new { type = "paragraph", text = "Around 3100 BCE, a king named Narmer — or Menes, as the Greeks called him — unified Upper and Lower Egypt into a single kingdom. This moment marks the beginning of one of history's greatest experiments: could a single ruler govern an entire civilization stretching hundreds of miles along a river? The answer, it turned out, was yes — for three thousand years." },
            new { type = "timeline", date = "3100 BCE", evnt = "King Narmer unifies Upper and Lower Egypt, establishing the first dynasty and founding one of history's longest-lasting civilizations" },
            new { type = "paragraph", text = "At the heart of Egyptian society was the pharaoh — a word meaning 'great house' — who was not merely a king but a living god. The pharaoh was believed to be the earthly embodiment of Horus, the falcon god, and upon death became Osiris, ruler of the underworld. This divine status meant that everything the pharaoh did — wars won, floods controlled, harvests gathered — was considered the will of the gods made manifest on Earth." },
            new { type = "fact", title = "The Great Pyramid", text = "The Great Pyramid of Giza was built around 2560 BCE for Pharaoh Khufu. It contains approximately 2.3 million stone blocks, each weighing between 2.5 and 15 tonnes. It stood as the tallest man-made structure in the world for over 3,800 years — a record that held until the completion of Lincoln Cathedral in England in 1311 CE." },
            new { type = "image", url = "/images/chapters/ancient/pyramids.png", caption = "The Great Pyramids of Giza — monuments to eternity, built by a civilization that refused to accept the finality of death." },
            new { type = "timeline", date = "2560 BCE", evnt = "Construction of the Great Pyramid of Giza is completed under Pharaoh Khufu — the largest building project in ancient history" },
            new { type = "paragraph", text = "Egyptian writing — hieroglyphics — was one of the first writing systems ever developed. To the Egyptians, words were not merely communication but magic. Writing a name gave it power. Erasing a name from a monument could erase a person from existence and the afterlife itself. This is why defeated pharaohs and disgraced officials had their names systematically chiselled away — a practice known today as damnatio memoriae." },
            new { type = "quote", text = "To speak the name of the dead is to make them live again.", source = "Ancient Egyptian Proverb" },
            new { type = "paragraph", text = "After three thousand years, Egypt fell to Alexander the Great in 332 BCE, then to Rome in 30 BCE with the death of Cleopatra VII — the last pharaoh. But Egypt's legacy never truly died. Its art, architecture, religion and ideas permeated Greek, Roman and ultimately Western civilization. When you look at a column, a obelisk, or the eye on the back of a dollar bill — you are looking at an echo of the Nile." },
            new { type = "image", url = "/images/chapters/ancient/sphinx.png", caption = "The Great Sphinx of Giza — guardian of the plateau, its face turned east toward the rising sun." }
        });
    }
}