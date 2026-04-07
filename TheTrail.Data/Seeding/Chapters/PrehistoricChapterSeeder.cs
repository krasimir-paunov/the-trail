using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class PrehistoricChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Prehistoric")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — The Age of Dinosaurs ──────────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Age of Dinosaurs")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Age of Dinosaurs",
                Subtitle = "165 million years of dominance, ended in an instant",
                Content = BuildChapter1Content(),
                Order = 1,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Dinosaur",
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

        // ── Chapter 2 — The First Humans ──────────────────────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The First Humans")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The First Humans",
                Subtitle = "How one small species walked out of Africa and claimed the world",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Human_evolution",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The First Humans")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "When did Homo sapiens first appear?", OptionA = "Around 2 million years ago", OptionB = "Around 300,000 years ago", OptionC = "Around 50,000 years ago", OptionD = "Around 1 million years ago", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Where did modern humans originate?", OptionA = "Asia", OptionB = "Europe", OptionC = "Africa", OptionD = "Australia", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Which early human species did Homo sapiens coexist with?", OptionA = "Homo erectus only", OptionB = "Neanderthals", OptionC = "Homo habilis", OptionD = "Australopithecus", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What is the name of the earliest known stone tool culture?", OptionA = "Acheulean", OptionB = "Mousterian", OptionC = "Oldowan", OptionD = "Aurignacian", CorrectOption = "C", Order = 4 },
                    new Question { Text = "When did humans first reach Australia?", OptionA = "Around 10,000 years ago", OptionB = "Around 65,000 years ago", OptionC = "Around 100,000 years ago", OptionD = "Around 30,000 years ago", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — The Ice Age ───────────────────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Ice Age")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Ice Age",
                Subtitle = "When glaciers swallowed continents and humanity clung to survival",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Ice_age",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Ice Age")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "When did the last Ice Age peak?", OptionA = "Around 100,000 years ago", OptionB = "Around 20,000 years ago", OptionC = "Around 50,000 years ago", OptionD = "Around 10,000 years ago", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What caused the Ice Ages?", OptionA = "Volcanic eruptions", OptionB = "Changes in Earth's orbit and tilt", OptionC = "Asteroid impacts", OptionD = "Solar flares", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Which famous megafauna went extinct at the end of the Ice Age?", OptionA = "Sabre-tooth cat and woolly mammoth", OptionB = "T-Rex and triceratops", OptionC = "Giant sloth and pterodactyl", OptionD = "Cave lion and dodo", CorrectOption = "A", Order = 3 },
                    new Question { Text = "What is the name of the land bridge that connected Asia to North America during the Ice Age?", OptionA = "Pangaea", OptionB = "Gondwana", OptionC = "Beringia", OptionD = "Zealandia", CorrectOption = "C", Order = 4 },
                    new Question { Text = "Where is the most famous Ice Age cave art located?", OptionA = "Germany", OptionB = "France and Spain", OptionC = "Russia", OptionD = "South Africa", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — The Agricultural Revolution ───────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Agricultural Revolution")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Agricultural Revolution",
                Subtitle = "The moment humanity stopped following food and started growing it",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Neolithic_Revolution",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Agricultural Revolution")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "When did the Agricultural Revolution begin?", OptionA = "Around 20,000 BCE", OptionB = "Around 10,000 BCE", OptionC = "Around 5,000 BCE", OptionD = "Around 3,000 BCE", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Where did agriculture first develop?", OptionA = "The Nile Valley", OptionB = "The Indus Valley", OptionC = "The Fertile Crescent", OptionD = "The Yellow River", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What was the first domesticated crop?", OptionA = "Rice", OptionB = "Maize", OptionC = "Wheat and barley", OptionD = "Potatoes", CorrectOption = "C", Order = 3 },
                    new Question { Text = "What is the term for the shift from nomadic to settled life?", OptionA = "Sedentarisation", OptionB = "Urbanisation", OptionC = "Industrialisation", OptionD = "Civilisation", CorrectOption = "A", Order = 4 },
                    new Question { Text = "What was the first animal to be domesticated?", OptionA = "Cattle", OptionB = "Sheep", OptionC = "Pigs", OptionD = "Dogs", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — The Bronze Age ────────────────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Bronze Age")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Bronze Age",
                Subtitle = "When metal changed everything — war, trade and the rise of kings",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Bronze_Age",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Bronze Age")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What two metals are combined to make bronze?", OptionA = "Iron and copper", OptionB = "Copper and tin", OptionC = "Gold and silver", OptionD = "Lead and zinc", CorrectOption = "B", Order = 1 },
                    new Question { Text = "When did the Bronze Age begin in the Near East?", OptionA = "Around 5000 BCE", OptionB = "Around 3300 BCE", OptionC = "Around 1200 BCE", OptionD = "Around 800 BCE", CorrectOption = "B", Order = 2 },
                    new Question { Text = "What writing system was invented during the Bronze Age?", OptionA = "Hieroglyphics and cuneiform", OptionB = "The alphabet", OptionC = "Sanskrit", OptionD = "Chinese characters", CorrectOption = "A", Order = 3 },
                    new Question { Text = "What event marked the end of the Bronze Age?", OptionA = "The rise of Rome", OptionB = "The Bronze Age Collapse", OptionC = "The invention of iron", OptionD = "The building of the pyramids", CorrectOption = "B", Order = 4 },
                    new Question { Text = "Which Bronze Age civilisation built the palace at Knossos?", OptionA = "Mycenaean", OptionB = "Sumerian", OptionC = "Minoan", OptionD = "Hittite", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
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

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Approximately 300,000 years ago, in what is now Morocco, a new kind of creature appeared on Earth. They were not the largest animals on the savanna, not the fastest, not the strongest. But they had something no other species possessed in quite the same way — a mind capable of abstract thought, language, planning and imagination. They were Homo sapiens. They were us." },
            new { type = "fact", title = "Out of Africa", text = "Genetic evidence confirms that every living human being descends from ancestors who lived in Africa. The oldest known Homo sapiens fossils — found at Jebel Irhoud in Morocco — date to approximately 300,000 years ago. Our entire species spread across the globe from this single origin point." },
            new { type = "image", url = "/images/chapters/prehistoric/first-humans.png", caption = "Early Homo sapiens — anatomically modern humans who first appeared in Africa around 300,000 years ago." },
            new { type = "paragraph", text = "For most of our early history, humans lived in small nomadic bands, following animals and gathering plants. We were hunter-gatherers — supremely adaptable, capable of surviving in almost any environment. Around 70,000 years ago, something extraordinary happened: a cognitive revolution. Art, music, complex language, religious ritual, long-distance trade — all appeared in the archaeological record within a relatively short window." },
            new { type = "timeline", date = "70,000 BCE", evnt = "The Cognitive Revolution — humans begin producing art, complex tools and symbolic thinking, transforming our species' capabilities overnight" },
            new { type = "paragraph", text = "The great migration out of Africa began in earnest around 60,000 years ago. Following coastlines, tracking animal herds, crossing land bridges exposed by lower sea levels during the Ice Age, our ancestors spread across the globe with remarkable speed. They reached Asia, then Australia — a sea crossing of at least 60 kilometers, proof of sophisticated watercraft and navigation — then Europe, and finally the Americas." },
            new { type = "fact", title = "Meeting the Neanderthals", text = "Homo sapiens were not alone when they arrived in Europe. Neanderthals had lived there for 400,000 years. For tens of thousands of years the two species coexisted — and interbred. Today, most people outside sub-Saharan Africa carry 1–4% Neanderthal DNA in their genome." },
            new { type = "image", url = "/images/chapters/prehistoric/cave-art.png", caption = "Cave paintings at Lascaux, France — created around 17,000 years ago, evidence of the fully modern human mind." },
            new { type = "timeline", date = "15,000 BCE", evnt = "Humans cross the Bering land bridge into the Americas, completing the colonisation of every habitable continent on Earth" },
            new { type = "quote", text = "We are the only species that tells stories about itself. That is what makes us human.", source = "Yuval Noah Harari, Sapiens" },
            new { type = "paragraph", text = "By 15,000 years ago, Homo sapiens had reached every habitable corner of the Earth. We had driven the Neanderthals to extinction, hunted the megafauna of multiple continents to oblivion, and painted the walls of caves with images that still move us today. The story of humanity was just beginning." },
            new { type = "image", url = "/images/chapters/prehistoric/migration.png", caption = "The human migration out of Africa — one species, one continent, spreading to claim the entire world." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Twenty thousand years ago, much of the world looked nothing like it does today. Vast sheets of ice — in some places three kilometers thick — covered North America, northern Europe and much of Asia. Sea levels were 120 meters lower than today, exposing land bridges that connected continents. The world was locked in the grip of the Last Glacial Maximum — the coldest point of the most recent Ice Age." },
            new { type = "fact", title = "Ice Age Earth", text = "During the Last Glacial Maximum, ice sheets covered approximately 30% of Earth's land surface. Britain was connected to mainland Europe. Alaska was connected to Siberia. The Amazon rainforest was largely dry savanna. The Mediterranean was smaller and saltier. Earth looked like an entirely different planet." },
            new { type = "image", url = "/images/chapters/prehistoric/ice-age.png", caption = "The Last Glacial Maximum — vast ice sheets covered the northern hemisphere 20,000 years ago." },
            new { type = "paragraph", text = "Ice Ages are not caused by any single dramatic event, but by slow, cyclical wobbles in Earth's orbit and axial tilt — the Milankovitch cycles. These cycles alter how much solar radiation reaches different parts of Earth across tens of thousands of years. We are currently living in a warm interglacial period — and have been for approximately 12,000 years." },
            new { type = "timeline", date = "20,000 BCE", evnt = "The Last Glacial Maximum — ice sheets at their greatest extent, sea levels 120 metres lower than today" },
            new { type = "paragraph", text = "For early humans, the Ice Age was both a challenge and an opportunity. The cold drove extraordinary innovation. Humans developed sewn clothing, complex shelters, and stored food for the first time. They hunted woolly mammoths, woolly rhinoceroses, cave bears and giant deer — megafauna that would all disappear by the end of the Ice Age, victims of both climate change and human hunting." },
            new { type = "fact", title = "Cave Art", text = "Some of the most breathtaking human creativity in history dates to the Ice Age. The cave paintings at Chauvet in France are 36,000 years old — twice as old as those at Lascaux. They show lions, rhinoceroses, mammoths and horses rendered with a skill and confidence that rivals any later art. In the cold darkness of those caves, the fully modern human mind announced itself." },
            new { type = "image", url = "/images/chapters/prehistoric/mammoth.png", caption = "The woolly mammoth — the iconic megafauna of the Ice Age, hunted to extinction by around 10,000 BCE." },
            new { type = "timeline", date = "10,000 BCE", evnt = "The Ice Age ends. Global temperatures rise rapidly, ice sheets retreat and sea levels rise 120 metres over the following millennia" },
            new { type = "quote", text = "The cave painters were not primitive. They were us.", source = "Werner Herzog, Cave of Forgotten Dreams" },
            new { type = "paragraph", text = "The end of the Ice Age around 12,000 years ago was one of the most consequential events in human history. As the ice retreated, new landscapes opened. Rivers formed. Forests spread. The environmental conditions that would make agriculture possible emerged for the first time. The stage was set for the next great revolution in human history." },
            new { type = "image", url = "/images/chapters/prehistoric/ice-age-end.png", caption = "As the ice retreated, new worlds opened — forests, rivers and fertile plains that would transform human civilization." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "For 290,000 years, Homo sapiens lived as hunter-gatherers. Then, in a geological eyeblink — roughly 10,000 years ago — everything changed. In the arc of land stretching from modern-day Turkey through Syria, Iraq and Iran, known as the Fertile Crescent, humans began doing something no species had ever done before: they started deliberately cultivating plants and raising animals for food. The Agricultural Revolution had begun." },
            new { type = "fact", title = "Why Agriculture?", text = "The shift to farming was not an obvious improvement. Hunter-gatherers typically worked fewer hours per day than farmers, ate a more varied diet and suffered from fewer infectious diseases. The Agricultural Revolution was driven not by luxury but by necessity — rising populations, declining game and the end of the Ice Age creating new environmental conditions." },
            new { type = "image", url = "/images/chapters/prehistoric/fertile-crescent.png", caption = "The Fertile Crescent — the arc of land where agriculture first emerged around 10,000 BCE." },
            new { type = "paragraph", text = "The first crops were wild varieties of wheat and barley, selected and saved over generations until they became domesticated species quite different from their wild ancestors. The first domesticated animals — dogs, then sheep, goats, cattle and pigs — followed. Agriculture spread from the Fertile Crescent across Europe, Asia and Africa over thousands of years, as well as developing independently in China, Mesoamerica, and sub-Saharan Africa." },
            new { type = "timeline", date = "10,000 BCE", evnt = "First evidence of deliberate wheat and barley cultivation in the Fertile Crescent — the birth of agriculture" },
            new { type = "paragraph", text = "Agriculture changed everything. Settled communities replaced nomadic bands. Permanent villages appeared, then towns. Surplus food allowed specialisation — not everyone needed to farm. Potters, weavers, metalworkers, priests and eventually soldiers could be fed by the surplus production of others. Social hierarchy appeared. So did war. So did writing — invented to keep track of grain stores and tax records." },
            new { type = "fact", title = "The Price of Progress", text = "Agriculture brought disease. Living in close proximity to animals meant zoonotic diseases — smallpox, measles, influenza — jumping to humans for the first time. Settled communities accumulated waste. Diets became less varied. Average human height actually decreased after the adoption of agriculture. The Agricultural Revolution was the most important event in human history — and not entirely a good one." },
            new { type = "image", url = "/images/chapters/prehistoric/first-village.png", caption = "Çatalhöyük in modern Turkey — one of the world's first towns, inhabited from around 7500 BCE." },
            new { type = "timeline", date = "7500 BCE", evnt = "Çatalhöyük in Anatolia becomes one of the first true towns, with thousands of inhabitants living in mudbrick houses" },
            new { type = "quote", text = "Agriculture is the most profound revolution in human history. Everything that came after — civilisation, writing, war, religion, inequality — flows from that first planted seed.", source = "Jared Diamond, Guns, Germs and Steel" },
            new { type = "paragraph", text = "By 5000 BCE, farming had spread across much of Eurasia and Africa. The world's population, which had hovered around 5 million for millennia, began to climb rapidly. The ingredients for the next great leap — civilisation itself — were falling into place." },
            new { type = "image", url = "/images/chapters/prehistoric/ancient-farming.png", caption = "Early farmers tending crops — a transformation that would reshape humanity's relationship with the natural world forever." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Around 3300 BCE, humans in the Middle East made a discovery that would transform warfare, trade and civilisation: they learned that by melting copper and tin together, they could create an alloy far harder and more versatile than either metal alone. Bronze — the material that gave its name to an entire age — could hold a sharper edge than stone, could be cast into complex shapes, and could be repaired and reused. The Bronze Age had begun." },
            new { type = "fact", title = "Why Bronze?", text = "Pure copper had been worked for thousands of years before the Bronze Age, but it was too soft for effective weapons or tools. The discovery that adding roughly 12% tin produced a far superior metal was one of the great technological breakthroughs of prehistory. The challenge was that tin and copper rarely occur in the same place, forcing the development of long-distance trade networks across thousands of kilometres." },
            new { type = "image", url = "/images/chapters/prehistoric/bronze-age.png", caption = "Bronze Age weapons and tools — harder, sharper and more versatile than anything that came before." },
            new { type = "paragraph", text = "The Bronze Age was the age of the first great civilisations. In Mesopotamia, the Sumerians built the world's first cities — Ur, Uruk, Lagash — complete with monumental temples called ziggurats, complex administration and the world's first writing system, cuneiform. In Egypt, the Old Kingdom pharaohs built the pyramids. In the Indus Valley, Harappa and Mohenjo-daro created cities with sophisticated drainage systems. In China, the Shang dynasty cast magnificent bronze ritual vessels." },
            new { type = "timeline", date = "3300 BCE", evnt = "Bronze Age begins in the Near East. Cuneiform writing invented in Sumer — the first writing system in human history" },
            new { type = "paragraph", text = "Bronze Age trade networks were astonishing in their reach. Tin from Afghanistan or Cornwall in Britain was transported thousands of kilometres to be smelted with copper from Cyprus or Sinai. Amber from the Baltic, lapis lazuli from Afghanistan, gold from Nubia, ivory from Africa — all moved along trade routes connecting civilisations that had never met face to face. The Bronze Age world was far more interconnected than we once imagined." },
            new { type = "fact", title = "The Bronze Age Collapse", text = "Around 1200 BCE, the Bronze Age ended in one of history's greatest mysteries. Within fifty years, virtually every major palace civilisation in the eastern Mediterranean — the Mycenaeans, the Hittites, Ugarit, Cyprus — collapsed simultaneously. Trade networks broke down, populations plummeted, writing disappeared in some regions for centuries. The causes remain debated: drought, invasion, earthquake, systems collapse — or all of them together." },
            new { type = "image", url = "/images/chapters/prehistoric/bronze-collapse.png", caption = "The destruction of Ugarit around 1185 BCE — one of many cities lost in the mysterious Bronze Age Collapse." },
            new { type = "timeline", date = "1200 BCE", evnt = "The Bronze Age Collapse — the simultaneous fall of nearly every major Mediterranean civilisation within fifty years" },
            new { type = "quote", text = "In the space of a few decades, the interconnected world of the Late Bronze Age simply ceased to exist.", source = "Eric Cline, 1177 BC: The Year Civilisation Collapsed" },
            new { type = "paragraph", text = "From the ruins of the Bronze Age arose the Iron Age — cheaper, more widely available iron democratising metalworking and enabling new civilisations to rise. The Greeks, the Phoenicians, the Assyrians, the Persians — the world of classical antiquity — were built on the ashes of the Bronze Age. The trail of human history continued." },
            new { type = "image", url = "/images/chapters/prehistoric/iron-age.png", caption = "The Iron Age successor civilisations — built on the ruins of the Bronze Age world they inherited." }
        });
    }
}