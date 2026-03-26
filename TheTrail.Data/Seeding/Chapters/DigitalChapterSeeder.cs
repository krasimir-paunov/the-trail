using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class DigitalChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Birth of the Internet"))
            {
                return;
            }

            int eraId = await context.Eras
                .Where(e => e.Name == "Digital Age")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Birth of the Internet",
                Subtitle = "How a Cold War military network became the nervous system of civilization",
                Content = BuildContent(),
                Order = 1,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });

            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Birth of the Internet")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What was the name of the first network that became the internet?", OptionA = "DARPANET", OptionB = "ARPANET", OptionC = "MILNET", OptionD = "NSFNET", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Who invented the World Wide Web?", OptionA = "Bill Gates", OptionB = "Steve Jobs", OptionC = "Tim Berners-Lee", OptionD = "Vint Cerf", CorrectOption = "C", Order = 2 },
                    new Question { Text = "In what year was the first message sent over ARPANET?", OptionA = "1965", OptionB = "1969", OptionC = "1975", OptionD = "1983", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What were the first two letters of the first internet message before the system crashed?", OptionA = "HI", OptionB = "LO", OptionC = "HE", OptionD = "IN", CorrectOption = "B", Order = 4 },
                    new Question { Text = "How many internet users were there worldwide by 2024?", OptionA = "Around 1 billion", OptionB = "Around 3 billion", OptionC = "Around 5.4 billion", OptionD = "Around 7 billion", CorrectOption = "C", Order = 5 }
                }
            });

            await context.SaveChangesAsync();
        }

        private static string BuildContent() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On October 29, 1969, a UCLA computer science student named Charley Kline sat down at a terminal and typed the word 'LOGIN' to send the first message over ARPANET — the experimental military network that would eventually become the internet. The system crashed after two letters. The first message ever transmitted across the internet was 'LO.' It was, inadvertently, one of the most poetic beginnings in technological history." },
            new { type = "fact", title = "The Cold War Origin", text = "ARPANET was funded by the US Department of Defense's Advanced Research Projects Agency in 1969. Its original purpose was not communication but survival — a decentralised network that could continue functioning even if nuclear strikes destroyed major nodes. The technology designed to survive World War Three became the infrastructure of everyday life." },
            new { type = "image", url = "/images/chapters/digital/arpanet.png", caption = "The original ARPANET map, 1969 — four connected nodes at UCLA, Stanford, UC Santa Barbara and the University of Utah." },
            new { type = "paragraph", text = "For its first two decades, the internet was the exclusive domain of academics, military researchers and computer scientists. Email was invented in 1971. File transfer protocols allowed researchers to share data. But the network remained incomprehensible to ordinary people — until a British scientist working at a particle physics laboratory in Switzerland had an idea that would change everything." },
            new { type = "timeline", date = "1989", evnt = "Tim Berners-Lee proposes the World Wide Web at CERN — a system of hyperlinked documents that would make the internet navigable for everyone" },
            new { type = "paragraph", text = "Tim Berners-Lee invented the World Wide Web in 1989, not to become rich or famous, but to help physicists at CERN share research papers. He invented HTML, HTTP and the URL — the three fundamental technologies that still underpin every website today. Crucially, he gave his invention away for free, refusing to patent it. Had he not done so, the web might have become a collection of competing proprietary systems rather than a single universal platform." },
            new { type = "quote", text = "The web is more a social creation than a technical one. I designed it for a social effect — to help people work together — and not as a technical toy.", source = "Tim Berners-Lee" },
            new { type = "image", url = "/images/chapters/digital/www.png", caption = "Tim Berners-Lee's original proposal for the World Wide Web — his supervisor wrote 'Vague but exciting' across the top." },
            new { type = "timeline", date = "1991", evnt = "The World Wide Web goes public. The first website — info.cern.ch — goes live on August 6, 1991." },
            new { type = "paragraph", text = "The web's growth was exponential. In 1993 there were 130 websites. By 1996, 100,000. By 2000, 17 million. Today there are over 1.9 billion. More information has been created in the last two years than in all of prior human history combined. The internet has become the largest single repository of human knowledge, creativity, commerce and communication ever built — and it grew from a crashed login attempt and two accidental letters." },
            new { type = "fact", title = "The Scale of Today", text = "By 2024, approximately 5.4 billion people — 67 percent of the world's population — were connected to the internet. Every minute, users send 231 million emails, watch 1 million hours of video on YouTube, conduct 5.9 million Google searches and make 1.4 million Zoom calls. The network that started with four computers now connects half the human race." },
            new { type = "image", url = "/images/chapters/digital/connected.png", caption = "A visualization of global internet traffic — the digital nervous system of modern civilization, pulsing with the activity of billions." }
        });
    }
}