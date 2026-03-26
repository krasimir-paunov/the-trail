using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class MedievalChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Medieval")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — The Black Death ───────────────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Black Death")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Black Death",
                Subtitle = "The plague that killed half of Europe and remade the world",
                Content = BuildChapter1Content(),
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
                    new Question { Text = "What caused the Black Death?", OptionA = "A virus", OptionB = "Yersinia pestis bacteria", OptionC = "A fungal infection", OptionD = "Poisoned water", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Approximately what fraction of Europe's population died in the Black Death?", OptionA = "One tenth", OptionB = "One quarter", OptionC = "One third to one half", OptionD = "Three quarters", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Where did the Black Death originate?", OptionA = "Egypt", OptionB = "Central Asia", OptionC = "India", OptionD = "Arabia", CorrectOption = "B", Order = 3 },
                    new Question { Text = "How was the plague primarily spread across Europe?", OptionA = "By contaminated water", OptionB = "By fleas on rats carried on trading ships", OptionC = "By airborne spores", OptionD = "By direct human contact only", CorrectOption = "B", Order = 4 },
                    new Question { Text = "What was one long-term social consequence of the Black Death?", OptionA = "The strengthening of feudalism", OptionB = "Increased power of the Church", OptionC = "Higher wages for surviving labourers", OptionD = "Depopulation of cities", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 2 — The Fall of Rome ──────────────────────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Fall of Rome")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Fall of Rome",
                Subtitle = "How the greatest empire in history crumbled — and what rose from the ruins",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Fall of Rome")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "In what year did the Western Roman Empire officially fall?", OptionA = "410 CE", OptionB = "455 CE", OptionC = "476 CE", OptionD = "527 CE", CorrectOption = "C", Order = 1 },
                    new Question { Text = "Who sacked Rome in 410 CE?", OptionA = "Attila the Hun", OptionB = "Alaric the Visigoth", OptionC = "Odoacer", OptionD = "Gaiseric the Vandal", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Which emperor divided the Roman Empire into East and West?", OptionA = "Constantine", OptionB = "Diocletian", OptionC = "Theodosius", OptionD = "Hadrian", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What did the Eastern Roman Empire become known as?", OptionA = "The Holy Roman Empire", OptionB = "The Ottoman Empire", OptionC = "The Byzantine Empire", OptionD = "The Carolingian Empire", CorrectOption = "C", Order = 4 },
                    new Question { Text = "Which barbarian leader deposed the last Western Roman Emperor?", OptionA = "Attila", OptionB = "Alaric", OptionC = "Theodoric", OptionD = "Odoacer", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — The Crusades ──────────────────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Crusades")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Crusades",
                Subtitle = "Two centuries of holy war that left the world permanently changed",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Crusades")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What event triggered the First Crusade?", OptionA = "The fall of Constantinople", OptionB = "Pope Urban II's speech at Clermont", OptionC = "The capture of Jerusalem by Saladin", OptionD = "The sack of Antioch", CorrectOption = "B", Order = 1 },
                    new Question { Text = "In what year did Crusaders capture Jerusalem?", OptionA = "1066", OptionB = "1095", OptionC = "1099", OptionD = "1187", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Who recaptured Jerusalem from the Crusaders in 1187?", OptionA = "Mehmed II", OptionB = "Suleiman the Magnificent", OptionC = "Saladin", OptionD = "Baybars", CorrectOption = "C", Order = 3 },
                    new Question { Text = "Which Crusade sacked the Christian city of Constantinople?", OptionA = "The First Crusade", OptionB = "The Third Crusade", OptionC = "The Fourth Crusade", OptionD = "The Seventh Crusade", CorrectOption = "C", Order = 4 },
                    new Question { Text = "How many major Crusades are generally recognised by historians?", OptionA = "Four", OptionB = "Six", OptionC = "Eight", OptionD = "Ten", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — Magna Carta ───────────────────────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Magna Carta and the Birth of Rights")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Magna Carta and the Birth of Rights",
                Subtitle = "The day a king was forced to admit that even kings must obey the law",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Magna Carta and the Birth of Rights")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "In what year was Magna Carta signed?", OptionA = "1066", OptionB = "1215", OptionC = "1265", OptionD = "1348", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which English king was forced to sign Magna Carta?", OptionA = "Richard I", OptionB = "Henry II", OptionC = "King John", OptionD = "Edward I", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What does 'Magna Carta' mean in Latin?", OptionA = "Great Charter", OptionB = "Royal Decree", OptionC = "Noble Agreement", OptionD = "Golden Bull", CorrectOption = "A", Order = 3 },
                    new Question { Text = "Where was Magna Carta signed?", OptionA = "Windsor Castle", OptionB = "Westminster", OptionC = "Runnymede", OptionD = "Canterbury", CorrectOption = "C", Order = 4 },
                    new Question { Text = "Which principle established by Magna Carta is still fundamental to law today?", OptionA = "Trial by combat", OptionB = "Habeas corpus — no imprisonment without trial", OptionC = "Divine right of kings", OptionD = "Parliamentary sovereignty", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — The Mongol Empire ─────────────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Mongol Empire")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Mongol Empire",
                Subtitle = "The greatest land empire in history — built in a single generation",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Mongol Empire")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What was Genghis Khan's birth name?", OptionA = "Kublai", OptionB = "Temujin", OptionC = "Ogedei", OptionD = "Timur", CorrectOption = "B", Order = 1 },
                    new Question { Text = "At its height, how much of the world's land area did the Mongol Empire control?", OptionA = "About 10%", OptionB = "About 16%", OptionC = "About 25%", OptionD = "About 33%", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Which Mongol Khan completed the conquest of China?", OptionA = "Genghis Khan", OptionB = "Ogedei Khan", OptionC = "Kublai Khan", OptionD = "Hulagu Khan", CorrectOption = "C", Order = 3 },
                    new Question { Text = "What was the Pax Mongolica?", OptionA = "A peace treaty with China", OptionB = "A period of stability enabling trade across Eurasia", OptionC = "The Mongol legal code", OptionD = "The Mongol postal system", CorrectOption = "B", Order = 4 },
                    new Question { Text = "Which city did the Mongols destroy in 1258, ending the Islamic Golden Age?", OptionA = "Jerusalem", OptionB = "Damascus", OptionC = "Cairo", OptionD = "Baghdad", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In October 1347, twelve Genoese trading ships docked at the Sicilian port of Messina. When port officials boarded the ships, they found most of the sailors dead and the rest covered in black, pus-filled boils that oozed blood and pus. The ships were ordered out of the harbour immediately — but it was already too late. The Black Death had arrived in Europe." },
            new { type = "fact", title = "The Scale of Death", text = "The Black Death killed between 30% and 60% of Europe's total population between 1347 and 1351. Some regions lost 70–80% of their people. In total, the plague may have killed 75–200 million people across Eurasia. It was the deadliest pandemic in human history — and it would return repeatedly for the next three centuries." },
            new { type = "image", url = "/images/chapters/medieval/plague.png", caption = "A medieval depiction of the Black Death — the plague that transformed European society forever." },
            new { type = "paragraph", text = "The disease was caused by Yersinia pestis, a bacterium carried by fleas that lived on black rats. It took three forms: bubonic plague, characterised by the swollen lymph nodes called buboes; septicaemic plague, which infected the bloodstream; and pneumonic plague, which attacked the lungs and could spread through the air. The pneumonic form was almost invariably fatal." },
            new { type = "timeline", date = "1347", evnt = "Black Death arrives in Sicily from Crimea via Genoese trading ships. Within months it spreads across the Italian peninsula" },
            new { type = "paragraph", text = "Medieval medicine was helpless. Physicians believed disease was caused by miasma — bad air — or by divine punishment. They prescribed bleeding, purging and aromatic herbs. Flagellants marched through towns whipping themselves bloody in penance. Jews were blamed and massacred across Germany and France. The Church, unable to explain or prevent the catastrophe, lost enormous moral authority." },
            new { type = "fact", title = "The Long-term Consequences", text = "The Black Death transformed European society. Labour became scarce, giving surviving peasants unprecedented bargaining power. Wages rose. The feudal system — already strained — began to crack. The Church's authority declined. A new emphasis on individual life and its transience entered art and literature. The Renaissance, some historians argue, was partly a product of a society that had stared death in the face and decided to celebrate life." },
            new { type = "image", url = "/images/chapters/medieval/danse-macabre.png", caption = "The Danse Macabre — a medieval artistic motif showing Death leading all people, regardless of rank, to their end." },
            new { type = "timeline", date = "1351", evnt = "The first wave of the Black Death ends, having killed perhaps half of Europe's population in less than four years" },
            new { type = "quote", text = "So many died that all believed it was the end of the world.", source = "Agnolo di Tura, Sienese chronicler, 1348" },
            new { type = "paragraph", text = "Europe took over a century to recover its pre-plague population levels. But it emerged transformed. The old certainties — the power of the Church, the permanence of feudalism, the meaninglessness of individual life — had been shattered. From the ruins of the medieval world, something new was beginning to grow." },
            new { type = "image", url = "/images/chapters/medieval/recovery.png", caption = "Medieval Europe slowly recovers — but the world that emerged from the plague was fundamentally different from the one that entered it." }
        });

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 4 September 476 CE, a barbarian chieftain named Odoacer deposed the last Roman Emperor of the West — a teenage boy named Romulus Augustulus — and sent the imperial regalia to Constantinople. There was no dramatic last stand, no heroic final battle. The Western Roman Empire simply ceased to exist, not with a bang but with a bureaucratic transaction. The ancient world was over. The Middle Ages had begun." },
            new { type = "fact", title = "Was Rome Really 'Fallen'?", text = "Historians debate whether 476 CE really marks the 'fall' of Rome. The Eastern Roman Empire — what we call the Byzantine Empire — survived for another thousand years until 1453. Roman culture, law and the Latin language persisted across Europe. The Catholic Church preserved Roman administrative structures. Some historians prefer to speak of Rome's 'transformation' rather than its fall — but something fundamental undeniably ended." },
            new { type = "image", url = "/images/chapters/medieval/rome-fall.png", caption = "The sack of Rome by the Visigoths in 410 CE — the first time Rome had fallen to an enemy in 800 years." },
            new { type = "paragraph", text = "The decline of Rome was not sudden but stretched across centuries. The third century saw the empire nearly torn apart by civil war — fifty years of chaos with dozens of emperors, most murdered. Diocletian stabilised the empire by dividing it administratively. Constantine moved the capital to Constantinople in 330 CE. The empire was baptised Christian in 380 CE. By the time the Visigoths sacked Rome in 410 CE, the shock was psychological as much as military." },
            new { type = "timeline", date = "410 CE", evnt = "Alaric and the Visigoths sack Rome — the first time the city had fallen to an enemy in 800 years, sending shockwaves across the Mediterranean world" },
            new { type = "paragraph", text = "The barbarian migrations that overwhelmed the Western Empire were themselves driven by pressure from the east. The Huns — a nomadic people from the Eurasian steppe — pushed Germanic tribes westward into Roman territory. The Visigoths, fleeing the Huns, crossed the Danube into Roman territory in 376 CE. Mistreated by Roman officials, they revolted and destroyed a Roman army at Adrianople in 378 CE — a shock that foreshadowed the empire's end." },
            new { type = "fact", title = "What Came After", text = "The fall of the Western Roman Empire did not produce the immediate dark age of popular imagination. In many areas, life continued largely as before — the same farms, the same towns, the same roads. But over generations, trade networks contracted, cities shrank, literacy declined and the complex administrative machinery of empire dissolved. It took centuries for Western Europe to recover the population levels and economic complexity of the Roman period." },
            new { type = "image", url = "/images/chapters/medieval/byzantine.png", caption = "Constantinople — capital of the Eastern Roman Empire, which survived the fall of the West by a thousand years." },
            new { type = "timeline", date = "476 CE", evnt = "Odoacer deposes Romulus Augustulus — the last Western Roman Emperor. The Western Roman Empire ceases to exist" },
            new { type = "quote", text = "I shudder when I think of the catastrophes of our time. For twenty years and more the blood of Romans has been shed daily. Where are those cities now? Where are their people?", source = "Saint Jerome, writing from Bethlehem in 396 CE" },
            new { type = "paragraph", text = "From the rubble of Rome, medieval Europe slowly assembled itself. Germanic kingdoms replaced Roman provinces. The Catholic Church preserved literacy, learning and Latin. Charlemagne briefly reunited much of western Europe in the early ninth century, claiming the mantle of Roman emperors. The idea of Rome never died — it just changed shape, haunting the political imagination of Europe for a thousand years." },
            new { type = "image", url = "/images/chapters/medieval/charlemagne.png", caption = "Charlemagne crowned Holy Roman Emperor in 800 CE — the most ambitious attempt to revive the Roman Empire in the medieval West." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 27 November 1095, Pope Urban II stood before a vast crowd at Clermont in France and delivered one of the most consequential speeches in history. He called on the Christian knights of Europe to take up arms and march to Jerusalem, to wrest the Holy City from Muslim control and defend the Byzantine Empire against the Seljuk Turks. The crowd roared: 'Deus vult!' — God wills it. The age of the Crusades had begun." },
            new { type = "fact", title = "Who Went on Crusade?", text = "The First Crusade attracted an extraordinarily diverse army: French and Norman knights, Italian merchants, German peasants, and a bizarre vanguard of the poor known as the People's Crusade, led by a preacher called Peter the Hermit. They were motivated by a complex mixture of genuine religious devotion, the promise of papal absolution, the lure of land and wealth, and the adventure of holy war. Most never returned home." },
            new { type = "image", url = "/images/chapters/medieval/crusaders.png", caption = "Crusader knights — the military vanguard of a movement that would reshape three continents." },
            new { type = "paragraph", text = "The First Crusade succeeded beyond all expectation. Jerusalem fell to the Crusaders on 15 July 1099 after a brutal siege. The massacre that followed — of Muslim and Jewish inhabitants — shocked even contemporaries. Four Crusader states were established in the Holy Land: the Kingdom of Jerusalem, the County of Tripoli, the Principality of Antioch and the County of Edessa. For a moment, it seemed the Christian holy places were secure." },
            new { type = "timeline", date = "1099", evnt = "Crusaders capture Jerusalem after a five-week siege. The massacre of the city's Muslim and Jewish inhabitants horrified the Islamic world" },
            new { type = "paragraph", text = "The Muslim response came in the form of Saladin — Salah ad-Din Yusuf ibn Ayyub — the Kurdish general who united Egypt and Syria and then turned his attention to the Crusader states. In 1187, he annihilated the Crusader army at the Battle of Hattin and retook Jerusalem. Unlike the Crusaders in 1099, Saladin treated the city's Christian inhabitants with remarkable mercy — a contrast that was noted and celebrated across the Islamic world." },
            new { type = "fact", title = "The Fourth Crusade's Shame", text = "The Fourth Crusade of 1202–04 never reached the Holy Land. Diverted by Venetian creditors and dynastic politics, the Crusaders instead sacked Constantinople — the greatest Christian city in the world. They looted its treasures, desecrated its churches and established a Latin Empire in its place. The Eastern Orthodox and Roman Catholic churches, already split since 1054, never fully reconciled. The Byzantine Empire never truly recovered." },
            new { type = "image", url = "/images/chapters/medieval/jerusalem.png", caption = "Jerusalem — holy to three faiths, fought over for two centuries, the beating heart of the Crusading age." },
            new { type = "timeline", date = "1291", evnt = "The fall of Acre — the last Crusader stronghold in the Holy Land. The Crusading experiment in the East is over" },
            new { type = "quote", text = "The Franks are gone. The Muslims hold the land. And the holy city is ours once more.", source = "Imad ad-Din al-Isfahani, after Saladin's capture of Jerusalem, 1187" },
            new { type = "paragraph", text = "The Crusades failed militarily — Jerusalem would not return to Christian control. But their long-term consequences were enormous. They accelerated the transfer of knowledge from the Islamic world to Europe, contributing to the twelfth-century renaissance. They opened trade routes that enriched the Italian city-states. They deepened the wounds between Christianity and Islam — wounds that have never fully healed. And they showed that faith alone, however passionate, is no substitute for strategy." },
            new { type = "image", url = "/images/chapters/medieval/crusade-end.png", caption = "The fall of Acre in 1291 — the last Crusader city in the Holy Land, ending two centuries of Christian rule." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "King John of England was, by any measure, a bad king. He lost Normandy and most of England's French territories to King Philip II of France. He quarrelled with Pope Innocent III so severely that England was placed under interdict — all church services suspended — for five years. He taxed his barons ruinously. He was widely suspected of murdering his own nephew. By 1215, the English barons had had enough. They rose in revolt, captured London, and forced John to meet them at Runnymede, a meadow beside the Thames." },
            new { type = "fact", title = "What Magna Carta Actually Said", text = "Magna Carta's 63 clauses were mostly concerned with the very specific grievances of medieval barons — feudal dues, inheritance rights, fish weirs on the Thames. Only a handful had wider significance. Clause 39 stated that no free man could be imprisoned, dispossessed or harmed except by the lawful judgement of his peers or the law of the land. Clause 40 stated: 'To no one will we sell, to no one deny or delay right or justice.' These two clauses became the foundation of due process and habeas corpus." },
            new { type = "image", url = "/images/chapters/medieval/magna-carta.png", caption = "Magna Carta — signed at Runnymede on 15 June 1215, the document that began the long journey toward the rule of law." },
            new { type = "paragraph", text = "John signed Magna Carta on 15 June 1215 — and immediately asked Pope Innocent III to annul it. The Pope obliged, declaring it 'null and void of all validity for ever'. John died the following year, and the document might have been forgotten. But his nine-year-old son Henry III needed the support of the barons, so Magna Carta was reissued — repeatedly, in slightly modified forms — becoming part of the permanent fabric of English law." },
            new { type = "timeline", date = "1215", evnt = "Magna Carta sealed at Runnymede — the first written limitation on royal power in English history" },
            new { type = "paragraph", text = "The document's true significance grew over centuries. English lawyers of the seventeenth century, fighting against Stuart absolutism, rediscovered Magna Carta and made it the symbol of English liberties. The American Founding Fathers, steeped in English legal tradition, drew on its principles when drafting the Constitution and Bill of Rights. Today, copies of Magna Carta are displayed in the US National Archives alongside the Declaration of Independence." },
            new { type = "fact", title = "How Many Copies Survive?", text = "Four original copies of the 1215 Magna Carta survive. Two are held in the British Library, one at Salisbury Cathedral and one at Lincoln Cathedral. They are written in medieval Latin on sheepskin parchment in a hand so small it requires magnification to read. Each is a physical link to the meadow at Runnymede where, for the first time in English history, a king was made to acknowledge that he was not above the law." },
            new { type = "image", url = "/images/chapters/medieval/runnymede.png", caption = "Runnymede — the meadow beside the Thames where Magna Carta was sealed and the rule of law was born." },
            new { type = "timeline", date = "1297", evnt = "Magna Carta entered into English statute law by Edward I — cementing its place as a permanent foundation of English legal rights" },
            new { type = "quote", text = "To no one will we sell, to no one deny or delay right or justice.", source = "Magna Carta, Clause 40, 1215" },
            new { type = "paragraph", text = "Magna Carta did not create democracy. It did not free the serfs. Most of its clauses were obsolete within decades. But it established a principle that has never been abandoned: that the law is above even the most powerful individual. From that meadow beside the Thames, a line runs through English common law, the American Constitution, the Universal Declaration of Human Rights and every courtroom in the democratic world." },
            new { type = "image", url = "/images/chapters/medieval/parliament.png", caption = "The Palace of Westminster — the institution of Parliament that grew, in part, from the principles of Magna Carta." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In 1162, a boy was born on the Mongolian steppe to the chief of a minor clan. His name was Temujin. By the time he died in 1227 under the title Genghis Khan — Universal Ruler — he had conquered more territory in twenty-five years than the Roman Empire had accumulated in four centuries. The Mongol Empire he founded would become the largest contiguous land empire in human history, stretching from Korea to Hungary." },
            new { type = "fact", title = "The Mongol War Machine", text = "The Mongol army was unlike anything the medieval world had faced. Every warrior was a skilled horseman who could fire a composite bow with lethal accuracy at full gallop. They could cover 100 miles a day — three times the speed of any European army. They used sophisticated siege technology learned from conquered Chinese engineers. They were masters of psychological warfare, spreading terror as a deliberate military strategy. And they were absolutely loyal to Genghis Khan." },
            new { type = "image", url = "/images/chapters/medieval/mongols.png", caption = "Mongol cavalry — the most feared military force of the medieval world, capable of conquering empires in months." },
            new { type = "paragraph", text = "The Mongol conquests were staggering in their speed and scale. China, the most populous nation on Earth, fell in stages. Central Asia — the heartland of Islamic civilisation, including the great cities of Samarkand, Bukhara and Merv — was conquered with appalling destruction. Persia fell. Then the Mongols turned west: Russia was devastated, Poland and Hungary invaded, and the Mongol advance only halted when Ögedei Khan died in 1241 and his generals returned east for the succession." },
            new { type = "timeline", date = "1206", evnt = "Temujin proclaimed Genghis Khan — Universal Ruler — at a great assembly on the Mongolian steppe, beginning the greatest military expansion in history" },
            new { type = "paragraph", text = "The destruction wrought by the Mongols was real and catastrophic — cities razed, populations massacred, irrigation systems destroyed that would not be repaired for centuries. But the Mongol Empire also created something unprecedented: the Pax Mongolica, a period of relative stability across Eurasia under a single political authority. For the first time, merchants, diplomats and missionaries could travel safely from China to Europe. Marco Polo made his famous journey during this period. The Black Death, too, travelled west along these same routes." },
            new { type = "fact", title = "The Destruction of Baghdad", text = "In 1258, Hulagu Khan's Mongol army sacked Baghdad — the greatest city in the Islamic world, home of the Abbasid Caliphate, and the centre of Islamic learning for five centuries. The Caliph was killed. The libraries of the House of Wisdom were destroyed. The rivers ran black with ink from the manuscripts thrown into them, and red with the blood of scholars. It was one of the most catastrophic acts of cultural destruction in history." },
            new { type = "image", url = "/images/chapters/medieval/mongol-empire.png", caption = "The Mongol Empire at its height — stretching from the Pacific Ocean to Eastern Europe, the largest land empire in history." },
            new { type = "timeline", date = "1258", evnt = "Mongols sack Baghdad, killing the Caliph and destroying the House of Wisdom — ending the Islamic Golden Age" },
            new { type = "quote", text = "If you had not committed great sins, God would not have sent a punishment like me upon you.", source = "Genghis Khan, to the people of Bukhara, 1220" },
            new { type = "paragraph", text = "The Mongol Empire fragmented after Genghis Khan's grandsons fell to quarrelling, splitting into four successor khanates. The Mongols who remained in conquered territories gradually assimilated into local cultures — converting to Islam in Persia and Central Asia, to Buddhism in China. But the world they had remade persisted. The trade routes they opened connected East and West. The diseases they inadvertently spread reshaped the populations of three continents. And the terror of their name echoes in history still." },
            new { type = "image", url = "/images/chapters/medieval/mongol-legacy.png", caption = "The aftermath of Mongol conquest — destruction and transformation on a scale the medieval world had never seen." }
        });
    }
}