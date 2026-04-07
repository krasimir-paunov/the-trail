using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class RenaissanceChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Renaissance")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — Leonardo da Vinci ─────────────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Leonardo da Vinci")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Leonardo da Vinci",
                Subtitle = "The man who wanted to know everything — and nearly did",
                Content = BuildChapter1Content(),
                Order = 1,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Leonardo_da_Vinci",
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
                    new Question { Text = "In what Italian town was Leonardo da Vinci born?", OptionA = "Florence", OptionB = "Milan", OptionC = "Vinci", OptionD = "Rome", CorrectOption = "C", Order = 1 },
                    new Question { Text = "Which famous painting by Leonardo depicts a woman with an enigmatic smile?", OptionA = "The Last Supper", OptionB = "The Mona Lisa", OptionC = "The Vitruvian Man", OptionD = "Lady with an Ermine", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Leonardo filled his notebooks with designs for what future invention?", OptionA = "The printing press", OptionB = "The steam engine", OptionC = "Flying machines", OptionD = "The telescope", CorrectOption = "C", Order = 3 },
                    new Question { Text = "Which artistic technique did Leonardo perfect to create soft transitions between light and shadow?", OptionA = "Fresco", OptionB = "Chiaroscuro and sfumato", OptionC = "Tempera", OptionD = "Pointillism", CorrectOption = "B", Order = 4 },
                    new Question { Text = "For which patron did Leonardo paint The Last Supper?", OptionA = "The Pope", OptionB = "Lorenzo de Medici", OptionC = "Ludovico Sforza", OptionD = "Francis I of France", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 2 — The Printing Press ────────────────────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Printing Press")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Printing Press",
                Subtitle = "How a goldsmith's invention ended the Middle Ages and began the modern world",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Printing_press",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Printing Press")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Who invented the movable type printing press in Europe?", OptionA = "Leonardo da Vinci", OptionB = "Johannes Gutenberg", OptionC = "Aldus Manutius", OptionD = "William Caxton", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What was the first major book printed on Gutenberg's press?", OptionA = "The Iliad", OptionB = "The Canterbury Tales", OptionC = "The Gutenberg Bible", OptionD = "The Divine Comedy", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Approximately when did Gutenberg complete his printing press?", OptionA = "Around 1350", OptionB = "Around 1440", OptionC = "Around 1500", OptionD = "Around 1520", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which movement was most directly enabled by the printing press?", OptionA = "The Crusades", OptionB = "The Black Death", OptionC = "The Protestant Reformation", OptionD = "The Mongol conquests", CorrectOption = "C", Order = 4 },
                    new Question { Text = "Before the printing press, how were books produced in Europe?", OptionA = "By woodblock printing", OptionB = "By hand copying in monasteries", OptionC = "By engraving on metal plates", OptionD = "By clay tablets", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — Michelangelo ──────────────────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Michelangelo and the Sistine Chapel")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Michelangelo and the Sistine Chapel",
                Subtitle = "Four years on his back, and the greatest ceiling in the history of art",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Michelangelo",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Michelangelo and the Sistine Chapel")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "How long did Michelangelo spend painting the Sistine Chapel ceiling?", OptionA = "One year", OptionB = "Four years", OptionC = "Ten years", OptionD = "Two years", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which pope commissioned the Sistine Chapel ceiling?", OptionA = "Pope Leo X", OptionB = "Pope Alexander VI", OptionC = "Pope Julius II", OptionD = "Pope Clement VII", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What is the central scene of the Sistine Chapel ceiling?", OptionA = "The Last Judgement", OptionB = "The Creation of Adam", OptionC = "The Flood", OptionD = "The Expulsion from Eden", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which of Michelangelo's sculptures depicts the biblical hero David?", OptionA = "The Pietà", OptionB = "Moses", OptionC = "David", OptionD = "The Dying Slave", CorrectOption = "C", Order = 4 },
                    new Question { Text = "In which city is the Sistine Chapel located?", OptionA = "Florence", OptionB = "Milan", OptionC = "Venice", OptionD = "Vatican City", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — Copernicus ────────────────────────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Copernicus and the Scientific Revolution")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Copernicus and the Scientific Revolution",
                Subtitle = "The moment humanity discovered it was not the centre of the universe",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Nicolaus_Copernicus",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Copernicus and the Scientific Revolution")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What did Copernicus propose that contradicted the Church's view of the cosmos?", OptionA = "That the Earth was round", OptionB = "That the Sun, not the Earth, was at the centre of the solar system", OptionC = "That the universe was infinite", OptionD = "That the Moon caused tides", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Who was put under house arrest for supporting the Copernican model?", OptionA = "Johannes Kepler", OptionB = "Tycho Brahe", OptionC = "Galileo Galilei", OptionD = "Isaac Newton", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What instrument did Galileo use to observe the moons of Jupiter?", OptionA = "The microscope", OptionB = "The astrolabe", OptionC = "The telescope", OptionD = "The quadrant", CorrectOption = "C", Order = 3 },
                    new Question { Text = "Which scientist formulated the laws of planetary motion?", OptionA = "Galileo Galilei", OptionB = "Johannes Kepler", OptionC = "Tycho Brahe", OptionD = "René Descartes", CorrectOption = "B", Order = 4 },
                    new Question { Text = "What did Isaac Newton publish in 1687 that completed the Scientific Revolution?", OptionA = "Principia Mathematica", OptionB = "On the Revolutions of Celestial Spheres", OptionC = "The New Organon", OptionD = "Dialogue Concerning Two Sciences", CorrectOption = "A", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — The Protestant Reformation ────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Protestant Reformation")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Protestant Reformation",
                Subtitle = "One monk's protest that split Christianity and remade Europe",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Reformation",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Protestant Reformation")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Who started the Protestant Reformation by posting his 95 Theses?", OptionA = "John Calvin", OptionB = "Henry VIII", OptionC = "Martin Luther", OptionD = "Ulrich Zwingli", CorrectOption = "C", Order = 1 },
                    new Question { Text = "In what year did Martin Luther post his 95 Theses?", OptionA = "1492", OptionB = "1517", OptionC = "1534", OptionD = "1545", CorrectOption = "B", Order = 2 },
                    new Question { Text = "What was Luther's primary objection to the Catholic Church?", OptionA = "The use of Latin in services", OptionB = "The sale of indulgences", OptionC = "The wealth of the Pope", OptionD = "The Crusades", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which English king broke with Rome to create the Church of England?", OptionA = "Henry VII", OptionB = "Edward VI", OptionC = "Henry VIII", OptionD = "James I", CorrectOption = "C", Order = 4 },
                    new Question { Text = "What was the Catholic Church's response to the Reformation called?", OptionA = "The Inquisition", OptionB = "The Counter-Reformation", OptionC = "The Council of Trent", OptionD = "The Holy League", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 15 April 1452, an illegitimate child was born in the small Tuscan town of Vinci. His mother was a peasant girl; his father a notary. He received no formal education beyond basic reading, writing and arithmetic. Yet Leonardo di ser Piero da Vinci would become the most curious mind in human history — painter, sculptor, architect, musician, mathematician, engineer, inventor, anatomist, geologist, botanist and cartographer. The Renaissance had many giants. Leonardo stands alone." },
            new { type = "fact", title = "The Notebooks", text = "Leonardo filled approximately 13,000 pages of notebooks with observations, sketches and ideas — mirror-written in Italian from right to left. They contained designs for flying machines, armoured vehicles, solar power, a calculator and plate tectonics — concepts that would not be realised or understood for centuries. Most of his notebooks were lost after his death. Those that survive are among the most extraordinary documents in human history." },
            new { type = "image", url = "/images/chapters/renaissance/vitruvian.png", caption = "The Vitruvian Man — Leonardo's study of human proportions, one of the most recognised images in the world." },
            new { type = "paragraph", text = "Leonardo was apprenticed at age fourteen to the Florentine painter Andrea del Verrocchio. Within a few years he had surpassed his master. He moved to Milan in 1482 to serve Ludovico Sforza, the Duke, as a military engineer, court entertainer, designer of festivals — and painter. It was in Milan that he painted The Last Supper on the wall of Santa Maria delle Grazie, using an experimental technique that would cause it to begin deteriorating almost immediately." },
            new { type = "timeline", date = "1495–1498", evnt = "Leonardo paints The Last Supper in Milan — a revolutionary composition that transformed how artists depicted narrative scenes" },
            new { type = "paragraph", text = "The Mona Lisa — painted between approximately 1503 and 1519 — is the most famous painting in the world. Its fame rests not only on the quality of the painting but on its mysteries: the identity of the sitter, the meaning of the smile, the extraordinary sfumato technique that blurs the boundary between figure and landscape. Leonardo used dozens of layers of glaze, each thinner than a human hair, to achieve an effect of luminous depth that no painter before had achieved." },
            new { type = "fact", title = "The Anatomist", text = "Leonardo performed over thirty illegal human dissections, producing anatomical drawings of such accuracy that they were not surpassed for centuries. He was the first to correctly describe the double curvature of the spine, the action of the heart valves and the position of the foetus in the womb. Had his notebooks been published, they might have advanced medicine by a century." },
            new { type = "image", url = "/images/chapters/renaissance/last-supper.png", caption = "The Last Supper — Leonardo's masterpiece of narrative painting, still visible in Milan after five centuries." },
            new { type = "timeline", date = "1519", evnt = "Leonardo da Vinci dies at Amboise in France, in the service of King Francis I. He leaves behind the Mona Lisa, The Last Supper and 13,000 pages of notebooks" },
            new { type = "quote", text = "I have been impressed with the urgency of doing. Knowing is not enough; we must apply. Being willing is not enough; we must do.", source = "Leonardo da Vinci" },
            new { type = "paragraph", text = "Leonardo died on 2 May 1519, reportedly in the arms of King Francis I of France. He was sixty-seven years old. He had finished very few of his projects — the perfectionism that drove his genius also paralysed his productivity. But what he left behind — the paintings, the drawings, the notebooks — represents a vision of human possibility that has never been surpassed." },
            new { type = "image", url = "/images/chapters/renaissance/leonardo-studio.png", caption = "Leonardo's studio — where observation, experimentation and art were inseparable disciplines." }
        });

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Around 1440, in the German city of Mainz, a goldsmith named Johannes Gutenberg combined technologies that had existed for decades — the screw press, oil-based ink, and movable metal type — into something entirely new: a mechanical printing press capable of producing books at a speed and cost that would transform the world. The information revolution had begun." },
            new { type = "fact", title = "Before Gutenberg", text = "Before the printing press, producing a single book required months of labour by a skilled monk or scribe. A large monastery might produce a few dozen books per year. Books were so expensive that universities chained them to desks. Literacy was confined to clergy, nobles and a small merchant class. Gutenberg's press could produce 3,600 pages per day. Within fifty years of his invention, over 20 million books had been printed in Europe." },
            new { type = "image", url = "/images/chapters/renaissance/gutenberg-press.png", caption = "A reconstruction of Gutenberg's printing press — the machine that democratised knowledge." },
            new { type = "paragraph", text = "Gutenberg's first major project was the Bible — the Gutenberg Bible of 1455, of which approximately 180 copies were printed. It was a magnificent object, designed to look like a hand-copied manuscript. But the implications of what Gutenberg had created went far beyond beautiful books. For the first time in European history, information could be reproduced identically, quickly and cheaply — and distributed across vast distances." },
            new { type = "timeline", date = "1455", evnt = "The Gutenberg Bible is completed — the first major book printed with movable type in Europe, beginning the print revolution" },
            new { type = "paragraph", text = "The consequences were staggering. Within decades, printing spread across Europe. By 1500, over 250 towns had printing presses. The price of books fell by perhaps 80%. Literacy rates began to climb. Vernacular languages — French, German, English, Spanish — gained prestige as books were printed in them. And ideas — good and bad — spread with a speed that no authority could control." },
            new { type = "fact", title = "The Press and the Reformation", text = "The printing press made the Protestant Reformation possible. When Martin Luther nailed his 95 Theses to the church door in Wittenberg in 1517, they might have remained a local theological dispute — as dozens of such protests had before. Instead, within weeks, copies had been printed and distributed across Germany. Within months, they had spread across Europe. The Church could not suppress an idea once it was in print." },
            new { type = "image", url = "/images/chapters/renaissance/early-books.png", caption = "Early printed books — the first mass medium, transforming how knowledge was produced and shared." },
            new { type = "timeline", date = "1476", evnt = "William Caxton sets up the first printing press in England, beginning the transformation of the English language through standardised print" },
            new { type = "quote", text = "The printing press is the greatest weapon in the armoury of modern criticism.", source = "Karl Marx, reflecting on Gutenberg's legacy" },
            new { type = "paragraph", text = "Gutenberg himself died in poverty — his press had been seized by his creditor Johann Fust, who commercialised the invention that Gutenberg had created. But the world he made is the world we still inhabit. Every newspaper, every website, every text message is a descendant of that press in Mainz. The democratisation of information that Gutenberg began has never stopped." },
            new { type = "image", url = "/images/chapters/renaissance/printing-workshop.png", caption = "A 16th century printing workshop — the nerve centre of the information revolution." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In 1508, Pope Julius II summoned a young Florentine sculptor to Rome. Michelangelo di Lodovico Buonarroti Simoni was thirty-three years old, already famous for his sculpture of David and the Pietà in St Peter's Basilica. He had never painted a fresco. He protested that he was a sculptor, not a painter. Julius — who had a habit of getting what he wanted — was unmoved. Michelangelo was to paint the ceiling of the Sistine Chapel. The result would be the greatest single artwork ever created by a human hand." },
            new { type = "fact", title = "The Physical Challenge", text = "The Sistine Chapel ceiling covers 5,000 square feet and rises 68 feet above the floor. Michelangelo spent four years, from 1508 to 1512, painting it. He worked standing on a specially constructed scaffold — not, as legend has it, lying on his back. He frequently worked alone, dismissing his assistants. By the end, he could only read by holding a book above his head, so accustomed had his neck become to looking upward." },
            new { type = "image", url = "/images/chapters/renaissance/sistine.png", caption = "The Sistine Chapel ceiling — 5,000 square feet of fresco painted by Michelangelo between 1508 and 1512." },
            new { type = "paragraph", text = "The ceiling depicts nine scenes from the Book of Genesis, from the Creation to the Drunkenness of Noah. At its centre is the Creation of Adam — perhaps the most recognised image in Western art. God, borne aloft by angels, extends a finger toward the languorous Adam, and in the gap between their fingertips, something electric seems to pass. It is a painting about the moment of divine contact, and it has lost none of its power after five centuries." },
            new { type = "timeline", date = "1512", evnt = "Michelangelo unveils the completed Sistine Chapel ceiling — Pope Julius II and the assembled cardinals are reportedly struck silent" },
            new { type = "paragraph", text = "Michelangelo was a difficult man — solitary, irascible, obsessive. He feuded with Julius II, with Leo X, with rival artists including Leonardo. He lived frugally despite enormous wealth. He never married. His letters and poetry reveal a man of profound religious feeling, tortured by the gap between the ideals of beauty he could envision and what his hands could actually create. He destroyed far more work than he completed." },
            new { type = "fact", title = "The Last Judgement", text = "Twenty-three years after completing the ceiling, Michelangelo returned to the Sistine Chapel to paint The Last Judgement on the altar wall. He was sixty-three years old. The finished work shocked Rome: Christ as a powerful, beardless Apollo figure, the saints not serene but anguished, the damned plunging into hell. A papal official complained that it belonged in a bathhouse, not a chapel. Michelangelo painted the official's face onto one of the damned." },
            new { type = "image", url = "/images/chapters/renaissance/david.png", caption = "Michelangelo's David — carved from a single block of marble between 1501 and 1504, the supreme achievement of Renaissance sculpture." },
            new { type = "timeline", date = "1564", evnt = "Michelangelo dies in Rome aged 88 — still working on his final Pietà three days before his death" },
            new { type = "quote", text = "If people knew how hard I had to work to gain my mastery, it would not seem so wonderful at all.", source = "Michelangelo Buonarroti" },
            new { type = "paragraph", text = "Michelangelo lived to be eighty-eight — an extraordinary age for any era. He outlived four popes who had employed him, two of his greatest rivals, and the entire first generation of Renaissance masters. He was working on sculpture three days before he died. He left behind the David, the Pietà, the Sistine ceiling, the design of St Peter's dome and enough unfinished work to fill a museum. The Renaissance produced many geniuses. Michelangelo produced a ceiling." },
            new { type = "image", url = "/images/chapters/renaissance/sistine-detail.png", caption = "The Creation of Adam — the central panel of the Sistine ceiling, the most recognised image in Western art." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "For over a thousand years, educated Europeans believed that the Earth stood motionless at the centre of the universe, with the Sun, Moon, planets and stars revolving around it. This was not merely scientific consensus — it was theological certainty, endorsed by Aristotle, confirmed by Ptolemy and blessed by the Church. Then, in 1543, a Polish canon named Nicolaus Copernicus published a book that moved the Earth — and humanity's sense of its own centrality — out of the centre of everything." },
            new { type = "fact", title = "Why It Mattered", text = "The Copernican revolution was not merely astronomical. It was philosophical and theological. If the Earth was not the centre of the universe, what did that mean for humanity's special status in God's creation? If the Church had been wrong about the cosmos, what else might it be wrong about? The heliocentric model did not immediately destroy religious faith — Copernicus himself was a canon — but it opened a crack in the edifice of medieval certainty through which the Scientific Revolution would pour." },
            new { type = "image", url = "/images/chapters/renaissance/copernicus.png", caption = "Nicolaus Copernicus — the Polish astronomer who moved Earth from the centre of the universe." },
            new { type = "paragraph", text = "Copernicus had worked on his heliocentric model for decades, sharing it only with trusted friends. He was reportedly shown the first printed copy of his book on his deathbed in 1543. His caution was justified: the theory was controversial, though the Church's formal condemnation came later. It was Galileo Galilei, who used the telescope to find observational evidence for heliocentrism, who faced the Inquisition's wrath." },
            new { type = "timeline", date = "1543", evnt = "Copernicus publishes On the Revolutions of the Celestial Spheres — the heliocentric model that would trigger the Scientific Revolution" },
            new { type = "paragraph", text = "Galileo Galilei was everything Copernicus was not — confrontational, polemical, politically naive. His Dialogue Concerning the Two Chief World Systems, published in 1632, was a barely disguised argument for heliocentrism in which the defender of the traditional view was named Simplicio — simpleton. Pope Urban VIII, who had previously been Galileo's patron, was furious. Galileo was tried by the Inquisition and forced to recant. He spent his last years under house arrest." },
            new { type = "fact", title = "The Scientific Method", text = "The greatest achievement of the Scientific Revolution was not any single discovery but a new method of investigating the world: systematic observation, hypothesis, experiment and peer review. Francis Bacon articulated the empirical method; Descartes the rationalist method; Newton united them in the greatest scientific synthesis in history. Science became not a body of received knowledge but a process of continuous questioning — a method that would transform every aspect of human life." },
            new { type = "image", url = "/images/chapters/renaissance/galileo.png", caption = "Galileo before the Inquisition — tried for defending the heliocentric model that observation had confirmed." },
            new { type = "timeline", date = "1687", evnt = "Newton publishes Principia Mathematica — unifying celestial and terrestrial mechanics and completing the Scientific Revolution" },
            new { type = "quote", text = "And yet it moves.", source = "Attributed to Galileo Galilei, after being forced to recant his support for heliocentrism, 1633" },
            new { type = "paragraph", text = "Isaac Newton, born in 1643 — the year Galileo died — synthesised the work of Copernicus, Kepler and Galileo into a unified system of universal gravitation and mechanics. His Principia Mathematica of 1687 described the laws governing the motion of every object in the universe, from falling apples to orbiting planets. It was the crowning achievement of the Scientific Revolution — and the foundation on which modern physics, engineering and technology are built." },
            new { type = "image", url = "/images/chapters/renaissance/newton.png", caption = "Isaac Newton — whose laws of motion and universal gravitation unified the heavens and the Earth into a single physical system." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 31 October 1517, a German Augustinian monk named Martin Luther nailed — or possibly posted — a document to the door of the Castle Church in Wittenberg. The Ninety-Five Theses were written in academic Latin, intended for scholarly debate. Within weeks, they had been translated into German and printed across the empire. Within months, all of Europe was arguing about them. The Protestant Reformation had begun — and Western Christianity would never be the same." },
            new { type = "fact", title = "What Were Indulgences?", text = "The immediate trigger for Luther's protest was the sale of indulgences — documents sold by the Church that promised remission of punishment for sins in purgatory. A Dominican friar named Johann Tetzel was travelling through Germany selling them with the slogan: 'As soon as the coin in the coffer rings, the soul from purgatory springs.' Luther was outraged. His Theses argued that salvation came through faith alone — not through works, sacraments or payments to the Church." },
            new { type = "image", url = "/images/chapters/renaissance/luther.png", caption = "Martin Luther — the monk whose protest against indulgences split Christianity and remade Europe." },
            new { type = "paragraph", text = "Luther was summoned before the Diet of Worms in 1521 and ordered to recant. His reply has echoed through history: 'Here I stand. I can do no other. God help me.' He was declared an outlaw by the Holy Roman Emperor. A sympathetic prince, Frederick the Wise, hid him in Wartburg Castle where, in ten weeks, Luther translated the New Testament into German — a text that both shaped the German language and made the Bible directly accessible to ordinary people." },
            new { type = "timeline", date = "1517", evnt = "Martin Luther posts his 95 Theses in Wittenberg — the opening shot of the Protestant Reformation" },
            new { type = "paragraph", text = "The Reformation spread with the speed that only the printing press could enable. John Calvin in Geneva developed a systematic Protestant theology that spread to France, Scotland, the Netherlands and eventually New England. Henry VIII of England broke with Rome for personal and political reasons — wanting a divorce the Pope would not grant — but the English Reformation he triggered produced the Anglican Church. By 1600, Western Christianity was permanently divided." },
            new { type = "fact", title = "The Wars of Religion", text = "The Reformation triggered a century of religious warfare that devastated Europe. The French Wars of Religion (1562–98) killed perhaps three million. The Thirty Years War (1618–48) killed eight million — a third of the German-speaking population. The Peace of Westphalia that ended it established the principle of state sovereignty and religious tolerance that forms the basis of the modern international order. The Reformation's violence helped create the secular state." },
            new { type = "image", url = "/images/chapters/renaissance/reformation.png", caption = "The Diet of Worms, 1521 — where Luther refused to recant and was declared an outlaw of the Holy Roman Empire." },
            new { type = "timeline", date = "1648", evnt = "The Peace of Westphalia ends the Thirty Years War — establishing religious tolerance and the modern system of sovereign states" },
            new { type = "quote", text = "Here I stand. I can do no other. God help me.", source = "Martin Luther, at the Diet of Worms, 1521" },
            new { type = "paragraph", text = "The Protestant Reformation reshaped everything it touched. It challenged the authority of institutions and elevated the individual conscience. It promoted literacy — Protestants needed to read the Bible themselves. It contributed to capitalism — Weber argued that the Protestant work ethic drove economic development. It created the conditions for the Enlightenment and ultimately for modern democracy. One monk's theological protest, amplified by the printing press, changed the world." },
            new { type = "image", url = "/images/chapters/renaissance/bible-translation.png", caption = "Luther's German Bible — the text that shaped the German language and made scripture accessible to ordinary people for the first time." }
        });
    }
}