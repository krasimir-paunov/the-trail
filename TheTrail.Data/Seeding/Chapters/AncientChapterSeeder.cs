using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class AncientChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Ancient Civilizations")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — The Rise of Egypt ─────────────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Rise of Egypt")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Rise of Egypt",
                Subtitle = "How the Nile built the most enduring civilisation in human history",
                Content = BuildChapter1Content(),
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
                    new Question { Text = "What natural feature made Egyptian civilisation possible?", OptionA = "The Sahara Desert", OptionB = "The Nile River", OptionC = "The Red Sea", OptionD = "The Atlas Mountains", CorrectOption = "B", Order = 1 },
                    new Question { Text = "When was the Great Pyramid of Giza built?", OptionA = "Around 1000 BCE", OptionB = "Around 3000 BCE", OptionC = "Around 2560 BCE", OptionD = "Around 4000 BCE", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What was the Egyptian writing system called?", OptionA = "Cuneiform", OptionB = "Linear B", OptionC = "Hieroglyphics", OptionD = "Sanskrit", CorrectOption = "C", Order = 3 },
                    new Question { Text = "Which pharaoh is associated with the Exodus story in the Bible?", OptionA = "Tutankhamun", OptionB = "Ramesses II", OptionC = "Cleopatra", OptionD = "Akhenaten", CorrectOption = "B", Order = 4 },
                    new Question { Text = "How long did ancient Egyptian civilisation last?", OptionA = "Around 500 years", OptionB = "Around 1000 years", OptionC = "Around 3000 years", OptionD = "Around 5000 years", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 2 — Mesopotamia and the First Cities ──────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Mesopotamia and the First Cities")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Mesopotamia and the First Cities",
                Subtitle = "Between two rivers, humanity invented civilisation itself",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Mesopotamia and the First Cities")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What does 'Mesopotamia' mean?", OptionA = "Land of the pharaohs", OptionB = "Land between the rivers", OptionC = "Land of plenty", OptionD = "Land of the gods", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which was the world's first writing system?", OptionA = "Hieroglyphics", OptionB = "Linear A", OptionC = "Cuneiform", OptionD = "The alphabet", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What is the name of the world's oldest known epic story?", OptionA = "The Iliad", OptionB = "The Epic of Gilgamesh", OptionC = "The Mahabharata", OptionD = "The Odyssey", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which Mesopotamian king created one of the first law codes?", OptionA = "Sargon of Akkad", OptionB = "Nebuchadnezzar", OptionC = "Hammurabi", OptionD = "Gilgamesh", CorrectOption = "C", Order = 4 },
                    new Question { Text = "What were the great stepped temple towers of Mesopotamia called?", OptionA = "Pyramids", OptionB = "Ziggurats", OptionC = "Obelisks", OptionD = "Minarets", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — The Greek Golden Age ─────────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Greek Golden Age")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Greek Golden Age",
                Subtitle = "When a small city on a rocky peninsula changed the course of thought forever",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Greek Golden Age")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Who is considered the father of democracy?", OptionA = "Pericles", OptionB = "Solon", OptionC = "Cleisthenes", OptionD = "Themistocles", CorrectOption = "C", Order = 1 },
                    new Question { Text = "Which battle saved Greece from Persian invasion in 480 BCE?", OptionA = "Marathon", OptionB = "Plataea", OptionC = "Thermopylae", OptionD = "Salamis", CorrectOption = "D", Order = 2 },
                    new Question { Text = "Who was Socrates' most famous student?", OptionA = "Aristotle", OptionB = "Plato", OptionC = "Xenophon", OptionD = "Epicurus", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What building stands atop the Athenian Acropolis?", OptionA = "The Temple of Zeus", OptionB = "The Colosseum", OptionC = "The Parthenon", OptionD = "The Pantheon", CorrectOption = "C", Order = 4 },
                    new Question { Text = "In which year did the first Olympic Games take place?", OptionA = "776 BCE", OptionB = "490 BCE", OptionC = "508 BCE", OptionD = "431 BCE", CorrectOption = "A", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — The Roman Republic ───────────────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Roman Republic")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Roman Republic",
                Subtitle = "From seven hills above a river, a republic that would swallow the world",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Roman Republic")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "In what year was the Roman Republic traditionally founded?", OptionA = "753 BCE", OptionB = "509 BCE", OptionC = "264 BCE", OptionD = "44 BCE", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What was the name of the Roman Senate's governing body of two elected leaders?", OptionA = "Tribunes", OptionB = "Praetors", OptionC = "Consuls", OptionD = "Censors", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Who was Julius Caesar's most trusted general before becoming his rival?", OptionA = "Mark Antony", OptionB = "Pompey", OptionC = "Crassus", OptionD = "Brutus", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What were the three Punic Wars fought against?", OptionA = "Greece", OptionB = "Egypt", OptionC = "Carthage", OptionD = "Persia", CorrectOption = "C", Order = 4 },
                    new Question { Text = "On what date was Julius Caesar assassinated?", OptionA = "January 1, 44 BCE", OptionB = "March 15, 44 BCE", OptionC = "December 25, 44 BCE", OptionD = "April 21, 44 BCE", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — The Persian Empire ───────────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Persian Empire")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Persian Empire",
                Subtitle = "The largest empire the ancient world had ever seen — and how it rose and fell",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Persian Empire")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Who founded the Achaemenid Persian Empire?", OptionA = "Darius the Great", OptionB = "Xerxes I", OptionC = "Cyrus the Great", OptionD = "Cambyses II", CorrectOption = "C", Order = 1 },
                    new Question { Text = "What was the Persian Empire's royal road used for?", OptionA = "Military parades", OptionB = "Trade and communication", OptionC = "Religious processions", OptionD = "Chariot racing", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Which Persian king invaded Greece with a massive army in 480 BCE?", OptionA = "Cyrus the Great", OptionB = "Darius I", OptionC = "Artaxerxes", OptionD = "Xerxes I", CorrectOption = "D", Order = 3 },
                    new Question { Text = "Who conquered the Persian Empire in 330 BCE?", OptionA = "Julius Caesar", OptionB = "Alexander the Great", OptionC = "Hannibal", OptionD = "Scipio Africanus", CorrectOption = "B", Order = 4 },
                    new Question { Text = "What was the capital city of the Persian Empire?", OptionA = "Babylon", OptionB = "Nineveh", OptionC = "Persepolis", OptionD = "Susa", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Of all the civilisations that have risen and fallen in human history, none captured the imagination of the ancient world — or of posterity — quite like Egypt. For three thousand years, longer than the gap between Julius Caesar and our own time, Egyptian civilisation endured along the banks of the Nile. Pharaohs rose and fell, dynasties came and went, empires invaded and retreated — and Egypt survived them all." },
            new { type = "fact", title = "The Gift of the Nile", text = "The Greek historian Herodotus called Egypt 'the gift of the Nile' — and he was right. Every year the Nile flooded, depositing rich black silt across the floodplain, creating some of the most fertile agricultural land on Earth. Without the Nile, Egypt would have been just another stretch of Saharan desert. The river made civilisation possible." },
            new { type = "image", url = "/images/chapters/ancient/nile.png", caption = "The Nile Valley — the lifeblood of Egyptian civilisation for three thousand years." },
            new { type = "paragraph", text = "Egyptian civilisation began around 3100 BCE when the legendary King Menes unified Upper and Lower Egypt into a single kingdom. The pharaoh — a living god on Earth, believed to be the embodiment of Horus and, in death, of Osiris — stood at the apex of a rigid social hierarchy. Below him were the priests, scribes and nobles; below them the craftsmen and merchants; at the base, the farmers whose labour sustained everything above." },
            new { type = "timeline", date = "3100 BCE", evnt = "King Menes unifies Upper and Lower Egypt, founding the First Dynasty and beginning three thousand years of pharaonic civilisation" },
            new { type = "paragraph", text = "The Old Kingdom period, from around 2686 to 2181 BCE, produced the monuments that define Egypt in the popular imagination. The Step Pyramid of Djoser, designed by the genius architect Imhotep, was the world's first monumental stone structure. The Great Pyramid of Giza, built for Pharaoh Khufu around 2560 BCE, remained the tallest man-made structure on Earth for nearly 4,000 years." },
            new { type = "fact", title = "Building the Pyramids", text = "The Great Pyramid contains approximately 2.3 million stone blocks, each weighing an average of 2.5 tonnes, with some weighing up to 80 tonnes. Modern experiments and discoveries have shown that the builders were not slaves but skilled workers — fed, housed and given medical care by the state. The logistics of feeding and organising 20,000 workers was itself a triumph of ancient administration." },
            new { type = "image", url = "/images/chapters/ancient/pyramids.png", caption = "The Great Pyramid of Giza — built around 2560 BCE and the tallest structure on Earth for nearly four millennia." },
            new { type = "timeline", date = "1279 BCE", evnt = "Ramesses II begins his 66-year reign — the longest of any pharaoh. He fights the Hittites to a standstill at Kadesh and signs history's first peace treaty" },
            new { type = "quote", text = "My name is Ozymandias, King of Kings; Look on my Works, ye Mighty, and despair!", source = "Percy Bysshe Shelley, Ozymandias (inspired by Ramesses II)" },
            new { type = "paragraph", text = "Egyptian civilisation finally ended not with a bang but with a slow absorption. Conquered by Alexander the Great in 332 BCE, Egypt became a Hellenistic kingdom under the Ptolemaic dynasty. The last of the Ptolemies was Cleopatra VII, who allied herself first with Julius Caesar and then with Mark Antony. After her death in 30 BCE, Egypt became a Roman province. The pharaohs were gone — but their monuments remained, and remain still." },
            new { type = "image", url = "/images/chapters/ancient/sphinx.png", caption = "The Great Sphinx of Giza — guardian of the pyramids, symbol of an empire that endured for three thousand years." }
        });

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Between the Tigris and Euphrates rivers, in the land the Greeks called Mesopotamia — 'the land between the rivers' — humanity invented civilisation. Not once, but repeatedly. City after city rose in this flat, fertile plain in what is now Iraq: Eridu, Uruk, Ur, Lagash, Nippur, Babylon. Here, for the first time in history, humans built temples, wrote laws, kept accounts and told stories. Everything we recognise as civilisation was invented here." },
            new { type = "fact", title = "The First City", text = "Uruk, founded around 4500 BCE, is considered the world's first true city. By 3000 BCE it had a population of perhaps 50,000 people — larger than any settlement that had ever existed. It had monumental temples, specialised craftsmen, long-distance trade and the world's first writing system. Uruk gave its name to an entire period of Mesopotamian history — and possibly to the word 'Iraq' itself." },
            new { type = "image", url = "/images/chapters/ancient/mesopotamia.png", caption = "The ruins of Ur — one of the great Sumerian city-states that flourished between the Tigris and Euphrates." },
            new { type = "paragraph", text = "Writing was invented in Mesopotamia around 3200 BCE — not for literature or religion, but for accounting. The earliest cuneiform tablets record grain rations and livestock counts. Writing began as a technology of bureaucracy, a way of keeping track of surplus in a complex urban economy. Only later did it become a medium for law, literature and history." },
            new { type = "timeline", date = "3200 BCE", evnt = "Cuneiform writing invented in Sumer — the first writing system in human history, initially used for record-keeping" },
            new { type = "paragraph", text = "The greatest king of the Old Babylonian period was Hammurabi, who ruled from around 1792 to 1750 BCE. His Code of Hammurabi — 282 laws carved onto a black stone stele — is one of the earliest and most complete written legal codes in history. It established the principle that the strong should not oppress the weak, and that the law should apply to all. Its famous lex talionis — 'an eye for an eye' — is still quoted today." },
            new { type = "fact", title = "The Epic of Gilgamesh", text = "The Epic of Gilgamesh, written in Sumer around 2100 BCE, is the world's oldest known work of literature. It tells the story of Gilgamesh, king of Uruk, who seeks immortality after the death of his friend Enkidu. The epic contains a flood story remarkably similar to that of Noah — suggesting a common Mesopotamian source for one of humanity's oldest myths." },
            new { type = "image", url = "/images/chapters/ancient/babylon.png", caption = "The reconstructed Ishtar Gate of Babylon — the magnificent entrance to the greatest city of the ancient Near East." },
            new { type = "timeline", date = "539 BCE", evnt = "Cyrus the Great of Persia conquers Babylon, ending Mesopotamian independence but preserving its culture within the Persian Empire" },
            new { type = "quote", text = "When the gods created mankind, they allotted death to mankind, but life they retained in their own keeping.", source = "The Epic of Gilgamesh" },
            new { type = "paragraph", text = "Mesopotamian civilisation bequeathed to the world the wheel, the plough, the arch, the 60-minute hour, the 360-degree circle, the seven-day week and the foundations of mathematics and astronomy. When Alexander the Great conquered Babylon in 331 BCE, he found a city still producing astronomical observations that would influence Greek and eventually modern science. The roots of our world run deep into that flat land between the rivers." },
            new { type = "image", url = "/images/chapters/ancient/cuneiform.png", caption = "A cuneiform clay tablet — the world's first writing, invented for accounting but soon used for law, literature and history." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In the fifth century BCE, on a rocky peninsula in the eastern Mediterranean, a small city of perhaps 300,000 people produced a concentration of genius unprecedented in human history. In the space of a single century, Athens gave the world democracy, tragedy, comedy, philosophy, history, medicine and the foundations of Western art and architecture. The Greek Golden Age was brief — barely a hundred years — but its effects have never ended." },
            new { type = "fact", title = "The Athenian Experiment", text = "Athenian democracy, introduced by Cleisthenes around 508 BCE, was radically different from modern democracy. All male citizens — perhaps 30,000–50,000 people — could vote directly on laws and policies in the Assembly. Officials were chosen by lottery, not election, to prevent wealthy elites from monopolising power. Women, slaves and foreigners were excluded — a profound limitation — but the principle that citizens should govern themselves was revolutionary." },
            new { type = "image", url = "/images/chapters/ancient/parthenon.png", caption = "The Parthenon — built under Pericles between 447 and 432 BCE, the supreme achievement of Greek classical architecture." },
            new { type = "paragraph", text = "The Golden Age was made possible by victory against the Persian Empire. When Xerxes' massive army invaded Greece in 480 BCE, it seemed that Greek civilisation was doomed. But the Greek city-states united — improbably, given their endless quarrels — and defeated the Persian fleet at Salamis. The victory gave Athens confidence, wealth and the security to embark on its extraordinary cultural experiment." },
            new { type = "timeline", date = "480 BCE", evnt = "Battle of Salamis — the Athenian fleet defeats the Persian armada, saving Greek civilisation and enabling the Golden Age" },
            new { type = "paragraph", text = "The philosophers of Athens asked questions that have never been fully answered. Socrates, who wrote nothing but talked to everyone, insisted that the unexamined life is not worth living — and was executed for it. His student Plato founded the Academy and explored justice, beauty and the ideal state. Plato's student Aristotle systematised virtually every field of knowledge — logic, biology, ethics, politics, aesthetics — and dominated European thought for two thousand years." },
            new { type = "fact", title = "The Death of Socrates", text = "In 399 BCE, Socrates was tried by 501 Athenian citizens on charges of impiety and corrupting the youth. He was found guilty by a narrow margin and sentenced to death by drinking hemlock. He could have escaped — his friends arranged it — but he refused, arguing that a philosopher must accept the judgement of the law. His death became the defining image of intellectual integrity against political power." },
            new { type = "image", url = "/images/chapters/ancient/athens.png", caption = "The Athenian Agora — the marketplace and civic heart of ancient Athens, where democracy and philosophy were born." },
            new { type = "timeline", date = "399 BCE", evnt = "The execution of Socrates — the defining moment of ancient philosophy and the beginning of Plato's written work" },
            new { type = "quote", text = "The unexamined life is not worth living.", source = "Socrates, at his trial, 399 BCE" },
            new { type = "paragraph", text = "The Golden Age ended with the Peloponnesian War — a catastrophic conflict between Athens and Sparta that lasted 27 years and destroyed the power of both. But the ideas of Athens proved more durable than Athenian power. Carried across the known world by Alexander the Great, translated into Latin by Roman scholars, preserved by Islamic philosophers during Europe's Dark Ages and rediscovered in the Renaissance — the thought of Athens never died." },
            new { type = "image", url = "/images/chapters/ancient/greek-philosophy.png", caption = "Raphael's 'School of Athens' — a Renaissance vision of the golden age of Greek philosophy." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "According to tradition, Rome was founded on the seven hills above the Tiber River in 753 BCE by the twin brothers Romulus and Remus, raised by a she-wolf. The reality was less dramatic but no less remarkable: a collection of Latin-speaking villages that gradually coalesced into a city, overthrew their Etruscan kings in 509 BCE, and established a republic that would endure for nearly five centuries before transforming into the greatest empire the western world had ever seen." },
            new { type = "fact", title = "The Roman Constitution", text = "The Roman Republic had no written constitution but an elaborate system of unwritten laws and precedents designed to prevent any single person from gaining too much power. Two consuls were elected annually, each with the power to veto the other. A Senate of 300 aristocrats advised them. Tribunes protected the rights of common citizens. It was imperfect, unequal and corrupt — but it lasted nearly 500 years and inspired the framers of the American Constitution." },
            new { type = "image", url = "/images/chapters/ancient/roman-forum.png", caption = "The Roman Forum — the political, religious and commercial heart of the Roman Republic." },
            new { type = "paragraph", text = "Rome's expansion began with the conquest of the Italian peninsula, completed by around 270 BCE. Then came the Punic Wars against Carthage — three devastating conflicts that made Rome master of the western Mediterranean. The general Hannibal crossed the Alps with his elephants and nearly destroyed Rome, winning battle after battle on Italian soil. Rome survived, counterattacked and destroyed Carthage so thoroughly that its site was sown with salt." },
            new { type = "timeline", date = "264 BCE", evnt = "The First Punic War begins — the start of Rome's century-long struggle with Carthage for control of the Mediterranean" },
            new { type = "paragraph", text = "By the first century BCE, Rome controlled the Mediterranean world but was tearing itself apart from within. A series of civil wars pitted general against general: Marius against Sulla, then Caesar against Pompey. Julius Caesar crossed the Rubicon with his army in 49 BCE — an illegal act that meant civil war — and defeated his rivals to become dictator perpetuo. His assassination on the Ides of March 44 BCE by senators who feared the end of the republic triggered another round of civil wars that would finally end the republic itself." },
            new { type = "fact", title = "The Ides of March", text = "On 15 March 44 BCE, Julius Caesar was stabbed 23 times by a group of senators led by Brutus and Cassius in the Theatre of Pompey. The conspirators believed they were saving the republic. Instead they destroyed it. Caesar's death triggered the final civil wars. His heir Octavian — later called Augustus — emerged victorious and became Rome's first emperor. The republic was dead." },
            new { type = "image", url = "/images/chapters/ancient/julius-caesar.png", caption = "Julius Caesar — general, statesman, dictator. His assassination ended the Republic and began the Empire." },
            new { type = "timeline", date = "44 BCE", evnt = "Assassination of Julius Caesar on the Ides of March — the death of the Roman Republic and the beginning of its transformation into Empire" },
            new { type = "quote", text = "Et tu, Brute?", source = "Julius Caesar's last words, according to Shakespeare" },
            new { type = "paragraph", text = "The Roman Republic left a legacy that still shapes our world. Roman law became the foundation of legal systems across Europe and Latin America. The Latin language gave birth to French, Spanish, Portuguese, Italian and Romanian. Roman engineering — roads, aqueducts, concrete — connected and sustained an empire. And the republican ideals of Rome — however imperfectly realised — inspired revolutions in America and France two thousand years later." },
            new { type = "image", url = "/images/chapters/ancient/roman-road.png", caption = "A Roman road — built to last millennia, they connected an empire and the phrase 'all roads lead to Rome' endures today." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In the sixth century BCE, a Persian king named Cyrus did something extraordinary. Having conquered the Median, Lydian and Babylonian empires in rapid succession, creating the largest empire the world had yet seen, he issued a proclamation unlike any from a conqueror before him. He freed the peoples his predecessors had enslaved, allowed conquered peoples to worship their own gods, and returned the Jews exiled to Babylon to their homeland. The Cyrus Cylinder — sometimes called the world's first human rights charter — proclaimed his tolerance to the world." },
            new { type = "fact", title = "The Scale of the Empire", text = "At its height under Darius I around 500 BCE, the Achaemenid Persian Empire stretched from the Balkans in the west to the Indus River in the east, and from the Caucasus in the north to Egypt in the south. It encompassed perhaps 50 million people — roughly 44% of the world's entire population at the time. No empire before or since has governed such a proportion of humanity." },
            new { type = "image", url = "/images/chapters/ancient/persepolis.png", caption = "Persepolis — the ceremonial capital of the Persian Empire, built by Darius and Xerxes on a grand terrace in modern Iran." },
            new { type = "paragraph", text = "Darius I, who came to power in 522 BCE, transformed the Persian Empire from a military conquest into a functioning state. He divided the empire into provinces called satrapies, each governed by a satrap — a royal governor — and connected by the Royal Road, a 2,700-kilometre highway from Sardis to Susa along which royal messengers could travel in a week what would take an ordinary traveller three months. He standardised coinage, weights and measures across the empire." },
            new { type = "timeline", date = "490 BCE", evnt = "Battle of Marathon — a smaller Athenian force defeats the Persian army, stunning the ancient world and beginning the Greco-Persian Wars" },
            new { type = "paragraph", text = "Darius' son Xerxes launched the greatest invasion force the ancient world had seen against Greece in 480 BCE — an army ancient sources numbered in the millions (modern estimates suggest 100,000–300,000). The Spartan king Leonidas and 300 soldiers held the pass at Thermopylae for three days against the full Persian army, buying time that arguably saved Greek civilisation. But it was the Athenian naval victory at Salamis that decided the war." },
            new { type = "fact", title = "The Fall of Persepolis", text = "In 330 BCE, Alexander the Great sacked and burned Persepolis — the ceremonial heart of the Persian Empire. Ancient sources suggest Alexander was drunk when he ordered the burning and regretted it the next day. The fire destroyed palaces, libraries and art that had accumulated over two centuries. It was the symbolic end of the Achaemenid Empire — though Persian culture, language and identity would survive and eventually produce new empires." },
            new { type = "image", url = "/images/chapters/ancient/xerxes.png", caption = "The Gate of All Nations at Persepolis — through which the subjects of 127 nations passed to pay tribute to the Persian king." },
            new { type = "timeline", date = "330 BCE", evnt = "Alexander the Great burns Persepolis, ending the Achaemenid Persian Empire after two centuries of dominance" },
            new { type = "quote", text = "I am Cyrus, king of the world. I will not allow anyone to terrorise the land.", source = "The Cyrus Cylinder, 539 BCE" },
            new { type = "paragraph", text = "The Persian Empire's legacy was enormous. Its administrative systems influenced Alexander's empire and the Roman Empire. Zoroastrianism — the Persian religion — influenced Judaism, Christianity and Islam. The Persian language and culture survived conquest after conquest, eventually producing the magnificent civilisation of Islamic Persia. And the image of Cyrus the Great — the just conqueror who freed the enslaved — has inspired rulers and revolutionaries ever since." },
            new { type = "image", url = "/images/chapters/ancient/cyrus-cylinder.png", caption = "The Cyrus Cylinder — sometimes called the world's first human rights charter, now in the British Museum." }
        });
    }
}