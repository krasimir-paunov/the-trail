using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class ExplorationChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Age of Exploration")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — The Voyages of Columbus ───────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Voyages of Columbus")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Voyages of Columbus",
                Subtitle = "The journey that accidentally changed the world",
                Content = BuildChapter1Content(),
                Order = 1,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Voyages of Columbus")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "In what year did Columbus first reach the Americas?", OptionA = "1488", OptionB = "1492", OptionC = "1498", OptionD = "1504", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which monarchs funded Columbus's voyage?", OptionA = "Henry VII of England and his queen", OptionB = "Ferdinand and Isabella of Spain", OptionC = "John II of Portugal and his queen", OptionD = "Charles VIII of France and his queen", CorrectOption = "B", Order = 2 },
                    new Question { Text = "What was Columbus actually searching for when he reached the Americas?", OptionA = "A new continent", OptionB = "Gold and silver mines", OptionC = "A western sea route to Asia", OptionD = "New lands to colonise", CorrectOption = "C", Order = 3 },
                    new Question { Text = "Which island did Columbus first land on in the Caribbean?", OptionA = "Cuba", OptionB = "Hispaniola", OptionC = "The Bahamas", OptionD = "Puerto Rico", CorrectOption = "C", Order = 4 },
                    new Question { Text = "How many voyages did Columbus make to the Americas in total?", OptionA = "One", OptionB = "Two", OptionC = "Three", OptionD = "Four", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 2 — Vasco da Gama ─────────────────────────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Vasco da Gama and the Route to India")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Vasco da Gama and the Route to India",
                Subtitle = "The voyage that connected Europe to Asia and created the first global trade network",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Vasco da Gama and the Route to India")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Which Portuguese explorer first reached India by sea around Africa?", OptionA = "Bartolomeu Dias", OptionB = "Ferdinand Magellan", OptionC = "Vasco da Gama", OptionD = "Pedro Álvares Cabral", CorrectOption = "C", Order = 1 },
                    new Question { Text = "In what year did Vasco da Gama reach India?", OptionA = "1492", OptionB = "1498", OptionC = "1504", OptionD = "1488", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Which cape at the southern tip of Africa did Portuguese sailors have to navigate?", OptionA = "Cape Horn", OptionB = "Cape Cod", OptionC = "Cape of Good Hope", OptionD = "Cape Verde", CorrectOption = "C", Order = 3 },
                    new Question { Text = "What was the primary trade commodity that made the route to India so valuable?", OptionA = "Silk", OptionB = "Gold", OptionC = "Spices", OptionD = "Cotton", CorrectOption = "C", Order = 4 },
                    new Question { Text = "Which Indian city did Vasco da Gama first reach?", OptionA = "Goa", OptionB = "Calicut", OptionC = "Bombay", OptionD = "Madras", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — Magellan ──────────────────────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Magellan's Circumnavigation")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Magellan's Circumnavigation",
                Subtitle = "The voyage that proved the world was round — and killed the man who led it",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Magellan's Circumnavigation")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Who led the first expedition to circumnavigate the globe?", OptionA = "Vasco da Gama", OptionB = "Francis Drake", OptionC = "Ferdinand Magellan", OptionD = "Juan Sebastián Elcano", CorrectOption = "C", Order = 1 },
                    new Question { Text = "In what year did Magellan's expedition set sail?", OptionA = "1492", OptionB = "1510", OptionC = "1519", OptionD = "1522", CorrectOption = "C", Order = 2 },
                    new Question { Text = "How many of the original five ships completed the circumnavigation?", OptionA = "One", OptionB = "Two", OptionC = "Three", OptionD = "Four", CorrectOption = "A", Order = 3 },
                    new Question { Text = "Where did Magellan die?", OptionA = "The Strait of Magellan", OptionB = "The Philippines", OptionC = "The Spice Islands", OptionD = "Brazil", CorrectOption = "B", Order = 4 },
                    new Question { Text = "Who completed the circumnavigation after Magellan's death?", OptionA = "Francisco Serrano", OptionB = "Antonio Pigafetta", OptionC = "Juan Sebastián Elcano", OptionD = "Gonzalo Gómez de Espinosa", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — The Conquest of the Americas ──────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Conquest of the Americas")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Conquest of the Americas",
                Subtitle = "How a few hundred soldiers toppled empires of millions",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Conquest of the Americas")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Which Spanish conquistador conquered the Aztec Empire?", OptionA = "Francisco Pizarro", OptionB = "Hernán Cortés", OptionC = "Diego de Almagro", OptionD = "Vasco Núñez de Balboa", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Who was the Aztec emperor when Cortés arrived in 1519?", OptionA = "Cuauhtémoc", OptionB = "Itzcoatl", OptionC = "Moctezuma II", OptionD = "Tlacaelel", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Which conquistador conquered the Inca Empire of Peru?", OptionA = "Hernán Cortés", OptionB = "Francisco Pizarro", OptionC = "Pedro de Alvarado", OptionD = "Gonzalo Pizarro", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What was the single greatest weapon the Spanish had against native populations?", OptionA = "Steel armour", OptionB = "Gunpowder weapons", OptionC = "Disease — especially smallpox", OptionD = "War horses", CorrectOption = "C", Order = 4 },
                    new Question { Text = "What was the Aztec capital city called?", OptionA = "Cuzco", OptionB = "Teotihuacan", OptionC = "Chichen Itza", OptionD = "Tenochtitlan", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — The Columbian Exchange ────────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Columbian Exchange")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Columbian Exchange",
                Subtitle = "How two worlds collided and transformed each other forever",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Columbian Exchange")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "What term describes the transfer of plants, animals and diseases between the Old and New Worlds after 1492?", OptionA = "The Great Exchange", OptionB = "The Columbian Exchange", OptionC = "The Atlantic Transfer", OptionD = "The Global Trade", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which crop from the Americas became a staple food in Europe and prevented famines?", OptionA = "Maize", OptionB = "Tomatoes", OptionC = "Potatoes", OptionD = "Chillies", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Which disease from Europe devastated native American populations?", OptionA = "Malaria", OptionB = "Smallpox", OptionC = "Cholera", OptionD = "Typhus", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which animal brought to the Americas transformed the culture of the Plains Indians?", OptionA = "The cow", OptionB = "The pig", OptionC = "The horse", OptionD = "The sheep", CorrectOption = "C", Order = 4 },
                    new Question { Text = "What percentage of the native American population is estimated to have died in the century after Columbus?", OptionA = "Around 10%", OptionB = "Around 25%", OptionC = "Around 50%", OptionD = "Around 90%", CorrectOption = "D", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 3 August 1492, three small ships slipped out of the Spanish port of Palos de la Frontera and headed west into the Atlantic Ocean. Their commander was a Genoese navigator named Cristoforo Colombo — Christopher Columbus — who had spent years trying to convince the courts of Europe to fund his audacious plan: to reach Asia by sailing west. Most educated people of the time knew the Earth was round. The question was whether it was small enough for the plan to work. It was not — but something else was in the way." },
            new { type = "fact", title = "Columbus Was Wrong", text = "Columbus's plan was based on a fundamental geographical error. He calculated the circumference of the Earth as roughly 25,000 kilometres — a figure he had cherry-picked from sources that supported his case. The correct figure is 40,075 kilometres. Had the Americas not existed, Columbus's fleet would have run out of food and water somewhere in the mid-Pacific and his crew would have died. The discovery of the Americas was, in a very real sense, an accident." },
            new { type = "image", url = "/images/chapters/exploration/columbus-ships.png", caption = "The Niña, Pinta and Santa María — the three ships that accidentally changed the world in 1492." },
            new { type = "paragraph", text = "After 36 days at sea — increasingly anxious days, with a near-mutinous crew — a lookout named Rodrigo de Triana spotted land at 2 AM on 12 October 1492. Columbus waded ashore on an island in the Bahamas he named San Salvador, planted the Spanish flag, and claimed it for the Crown. He believed he was in the East Indies. He called the people he found 'Indians'. He was wrong on both counts — but the name stuck for the people, if not the place." },
            new { type = "timeline", date = "12 October 1492", evnt = "Columbus lands in the Bahamas — the moment two worlds, separated for 12,000 years, made contact again" },
            new { type = "paragraph", text = "Columbus made four voyages to the Americas between 1492 and 1504. He explored the Caribbean islands, the coast of Central America and the northern coast of South America. He never set foot on the North American mainland. He never understood what he had found. He died in 1506 still insisting he had reached Asia. It was another Italian navigator — Amerigo Vespucci — who correctly identified the lands as a New World, and whose name they bear." },
            new { type = "fact", title = "The Treatment of the Taíno", text = "Columbus's first encounter with the Taíno people of the Caribbean was described in his journal as peaceful and promising. Within years, Spanish colonists had enslaved them, worked them to death in gold mines and introduced diseases against which they had no immunity. The Taíno population of Hispaniola, estimated at several hundred thousand in 1492, was effectively extinct within fifty years. It was the first chapter of a catastrophe that would claim tens of millions of lives." },
            new { type = "image", url = "/images/chapters/exploration/caribbean.png", caption = "The Caribbean islands — the first point of contact between the Old World and the New, and the site of the first colonial catastrophe." },
            new { type = "timeline", date = "1506", evnt = "Columbus dies in Valladolid, Spain — still believing he had reached the coast of Asia" },
            new { type = "quote", text = "They ought to be good servants and of good intelligence... I could conquer the whole of them with fifty men and govern them as I please.", source = "Christopher Columbus, journal entry, 12 October 1492" },
            new { type = "paragraph", text = "Columbus's legacy is contested in a way that few historical figures' legacies are. He was undeniably one of the greatest navigators in history, a man of extraordinary courage and determination. He was also the man who opened the door to five centuries of colonialism, slavery and cultural destruction. Both things are true. The world he accidentally connected was transformed — and the transformation was not equally beneficial to all its inhabitants." },
            new { type = "image", url = "/images/chapters/exploration/columbus-landing.png", caption = "Columbus landing in the New World — a moment of contact whose consequences neither side could have imagined." }
        });

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "While Columbus was sailing west for Spain, Portugal was pursuing a different route to Asia — south and east, around the continent of Africa. It was a project that had consumed Portuguese ambition for decades, built on the work of Prince Henry the Navigator and the systematic exploration of the African coastline by generations of Portuguese sailors. In 1498, a determined, ruthless navigator named Vasco da Gama completed the mission: he reached India." },
            new { type = "fact", title = "The Spice Trade", text = "The motivation for the route to India was spices — pepper, cinnamon, cloves, nutmeg and ginger. In medieval Europe, spices were worth their weight in gold. They preserved food, masked the taste of rotting meat, and were essential ingredients in medicines. The spice trade from Asia to Europe passed through Arab and Venetian middlemen who took enormous profits. A direct sea route to Asia would cut them out entirely and make Portugal fabulously rich." },
            new { type = "image", url = "/images/chapters/exploration/portuguese-ship.png", caption = "A Portuguese caravel — the ship type that made the Age of Exploration possible, built to handle ocean swells and long voyages." },
            new { type = "paragraph", text = "Da Gama departed Lisbon in July 1497 with four ships and 170 men. He sailed south along the African coast, rounded the Cape of Good Hope — first navigated by Bartolomeu Dias in 1488 — and then struck northeast across the Indian Ocean, guided in part by an Arab pilot named Ibn Majid who knew these waters intimately. He reached Calicut on the southwest coast of India in May 1498. He had sailed 24,000 kilometres from Lisbon." },
            new { type = "timeline", date = "1498", evnt = "Vasco da Gama reaches Calicut, India — completing the first all-sea route from Europe to Asia and transforming global trade" },
            new { type = "paragraph", text = "The reception in Calicut was mixed. The Zamorin — the local ruler — was not impressed by the goods Da Gama had brought to trade, which were suited for African markets rather than the sophisticated merchants of the Indian Ocean. Arab traders, recognising the threat to their monopoly, worked against him. Da Gama returned to Portugal with a small cargo of spices — but the proof of concept was established. The route was open." },
            new { type = "fact", title = "The Human Cost", text = "Of the 170 men who set sail with Da Gama, only 55 returned. Scurvy — caused by vitamin C deficiency — killed more men than storms. Da Gama's second voyage in 1502 was far more brutal: he bombarded Calicut, burned a ship full of Muslim pilgrims alive, and established Portuguese dominance of the Indian Ocean trade routes through a combination of superior firepower and systematic terror. The Age of Exploration was also an age of violence." },
            new { type = "image", url = "/images/chapters/exploration/indian-ocean.png", caption = "The Indian Ocean trade network — dominated by Arab merchants for centuries before the Portuguese arrived." },
            new { type = "timeline", date = "1502", evnt = "Da Gama's second voyage establishes Portuguese naval dominance of the Indian Ocean, redirecting the global spice trade" },
            new { type = "quote", text = "We come in search of Christians and spices.", source = "Vasco da Gama, on arriving in India, 1498" },
            new { type = "paragraph", text = "Da Gama's voyage broke the Arab-Venetian monopoly on the spice trade and made Portugal the dominant maritime power in the Indian Ocean for over a century. It connected Europe, Africa, Asia and eventually the Americas into a single global trading network for the first time. The world had become, for better and worse, one interconnected system. The consequences of that connection are still unfolding." },
            new { type = "image", url = "/images/chapters/exploration/lisbon-port.png", caption = "Lisbon — the capital of the first global maritime empire, where the riches of Asia, Africa and Brazil converged." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 20 September 1519, five ships and 270 men left the Spanish port of Sanlúcar de Barrameda under the command of a Portuguese navigator in Spanish service named Fernão de Magalhães — Ferdinand Magellan. His mission: to find a western route to the Spice Islands by sailing around or through the American continent. Three years later, on 6 September 1522, one ship and 18 men limped back into the same harbour. They had sailed around the world. Magellan was not among them." },
            new { type = "fact", title = "The Scale of the Achievement", text = "Magellan's circumnavigation covered approximately 60,000 kilometres — the longest sea voyage ever undertaken to that point. The expedition crossed three oceans, discovered the strait at the southern tip of South America that bears Magellan's name, and spent 98 days crossing the Pacific — an ocean so vast and so empty that the crew nearly starved and died of thirst before reaching land. Of 270 men who set out, 18 completed the voyage. It remains one of the greatest feats of navigation in history." },
            new { type = "image", url = "/images/chapters/exploration/magellan-route.png", caption = "The route of Magellan's circumnavigation — the longest sea voyage ever undertaken, proving the Earth's true scale." },
            new { type = "paragraph", text = "The voyage nearly ended before it began. In April 1520, at Port St Julian in Patagonia, three of Magellan's five captains mutinied, unwilling to continue into unknown southern waters. Magellan crushed the mutiny with extraordinary brutality — executing one captain, marooning another, leaving the body of a third impaled on a stake on the shore. His authority restored, he pressed south and found the strait that would bear his name in October 1520." },
            new { type = "timeline", date = "November 1520", evnt = "Magellan's fleet completes the passage through the strait at the tip of South America and enters the Pacific Ocean" },
            new { type = "paragraph", text = "The Pacific crossing nearly destroyed the expedition. Magellan had no conception of the ocean's true size. His ships ran out of fresh food within weeks. The crew ate rats, leather, and sawdust mixed with worm-eaten biscuit. Men died of scurvy. The Pacific — Magellan named it for its apparent calmness — offered no landfall for 98 days. When they finally reached Guam in March 1521, the survivors were emaciated and desperate." },
            new { type = "fact", title = "Magellan's Death", text = "Magellan died in the Philippines on 27 April 1521, killed in a skirmish he need not have fought. He had become involved in a local political dispute and led an attack on the island of Mactan. The force he led was outnumbered and outmanoeuvred. Magellan was struck by a bamboo spear, then overwhelmed. He had completed two-thirds of the first circumnavigation but would not finish it. The man who completed it — Juan Sebastián Elcano — is largely forgotten." },
            new { type = "image", url = "/images/chapters/exploration/pacific.png", caption = "The Pacific Ocean — so vast that Magellan's crew nearly died crossing it, spending 98 days without sighting land." },
            new { type = "timeline", date = "6 September 1522", evnt = "The Victoria — the sole surviving ship — returns to Spain with 18 men, completing the first circumnavigation of the Earth" },
            new { type = "quote", text = "The church says the Earth is flat, but I know that it is round, for I have seen the shadow on the moon, and I have more faith in a shadow than in the church.", source = "Ferdinand Magellan" },
            new { type = "paragraph", text = "The circumnavigation proved definitively that the Earth was a globe, established the true scale of the Pacific Ocean, and demonstrated that the Americas were not Asia. It confirmed that there was one continuous world ocean connecting all seas. The survivor Antonio Pigafetta, who kept a meticulous journal of the voyage, noted that on returning to Spain they were one day behind in their calendar — the first empirical demonstration that the Earth rotates, and a puzzle that would not be fully explained for decades." },
            new { type = "image", url = "/images/chapters/exploration/victoria-ship.png", caption = "The Victoria — the only ship to complete Magellan's circumnavigation, returning with 18 survivors from 270." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In November 1519, a Spanish adventurer named Hernán Cortés marched into the Aztec capital of Tenochtitlan at the head of perhaps 500 soldiers, 16 horses and a few small cannon. He was greeted by the Aztec emperor Moctezuma II, who received him with ceremony and gifts. Within two years, Tenochtitlan was rubble and the Aztec Empire — which had ruled central Mexico for a century, collecting tribute from millions of people — was gone. It was one of the most dramatic conquests in history, and one of the most catastrophic." },
            new { type = "fact", title = "How Was It Possible?", text = "How did a few hundred Spaniards conquer an empire of millions? The answer is complex. Disease had already decimated the population before the final battle. Cortés brilliantly exploited the resentment of peoples subjugated by the Aztecs, building a coalition of indigenous allies who provided the bulk of his fighting force. Spanish steel, armour and horses gave tactical advantages. And Aztec warfare, focused on capturing enemies for sacrifice rather than killing them, was strategically unsuited to fighting the Spanish." },
            new { type = "image", url = "/images/chapters/exploration/tenochtitlan.png", caption = "Tenochtitlan — the Aztec capital, built on an island in a lake, was one of the largest cities in the world in 1519." },
            new { type = "paragraph", text = "The conquest of Peru, completed by Francisco Pizarro between 1532 and 1572, was even more audacious. Pizarro, with fewer than 200 men, captured the Inca emperor Atahualpa at the city of Cajamarca, killed thousands of his retinue in a surprise attack, and held him for a ransom of gold and silver that filled a room. Then he killed Atahualpa anyway. The Inca Empire — stretching 4,000 kilometres along the Andes, the largest empire in the pre-Columbian Americas — collapsed in the face of Spanish steel, disease and the shock of losing its sacred emperor." },
            new { type = "timeline", date = "1521", evnt = "The fall of Tenochtitlan — the Aztec Empire destroyed by Cortés and his indigenous allies after a brutal three-month siege" },
            new { type = "paragraph", text = "The conquests unleashed a catastrophe without parallel in human history. Smallpox, measles, influenza and other Old World diseases — against which indigenous Americans had no immunity — swept through the population in waves. Conservative estimates suggest that 50–90% of the indigenous population of the Americas died in the century after contact. Some regions lost 95% of their people. It was the greatest demographic collapse in recorded history." },
            new { type = "fact", title = "The Voices of the Conquered", text = "Indigenous accounts of the conquest survive. Aztec and Maya scribes recorded their perspective in documents that describe the shock, the terror, the smell of disease and the incomprehension of defeat. One Nahuatl text describes the arrival of smallpox: 'Great was the stench of the dead... And the dogs and vultures devoured their bodies. The mortality was terrible.' These voices complicate any triumphalist narrative of European exploration." },
            new { type = "image", url = "/images/chapters/exploration/conquest.png", caption = "The conquest of the Americas — a meeting of worlds that ended in catastrophe for the indigenous civilisations." },
            new { type = "timeline", date = "1572", evnt = "The last Inca emperor Túpac Amaru is executed in Cuzco — the final end of independent Inca resistance to Spanish rule" },
            new { type = "quote", text = "They came with their iron lances and their crossbows, their swords and their firearms... destroying, killing, burning, laying waste.", source = "Anonymous Tlaxcalan account of the Spanish conquest" },
            new { type = "paragraph", text = "The wealth extracted from the Americas — gold and silver from Mexico and Peru — poured into Spain in quantities that transformed the European economy, fuelled inflation, and financed Spanish power in Europe for a century. The indigenous civilisations of the Americas — their architecture, their agriculture, their astronomy, their art, their knowledge systems — were largely destroyed. What was lost can never be fully recovered. What was taken still shapes the world today." },
            new { type = "image", url = "/images/chapters/exploration/silver-mines.png", caption = "The silver mines of Potosí — the source of the wealth that funded Spain's empire and transformed the global economy." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "When Columbus's ships crossed the Atlantic in 1492, they did more than connect two continents — they reconnected two biological worlds that had been separated for 12,000 years, since the last Ice Age closed the land bridge between Asia and the Americas. The transfer of plants, animals, diseases and people that followed — what historian Alfred Crosby named the Columbian Exchange — was the most significant ecological event since the extinction of the dinosaurs." },
            new { type = "fact", title = "What Went East", text = "From the Americas to Europe, Africa and Asia came: maize, potatoes, tomatoes, chillies, chocolate, tobacco, vanilla, rubber, peanuts, squash, beans and sweet potatoes. These crops transformed diets across the world. The potato alone is estimated to have added 25% to the food supply of northern Europe, enabling population growth that fuelled the Industrial Revolution. Ireland became so dependent on the potato that when a fungal blight struck in 1845, a million people died." },
            new { type = "image", url = "/images/chapters/exploration/columbian-exchange.png", caption = "The Columbian Exchange — the transfer of crops, animals and diseases that transformed both hemispheres." },
            new { type = "paragraph", text = "From Europe, Africa and Asia to the Americas came: horses, cattle, pigs, sheep, goats, chickens, wheat, rice, sugar cane, coffee — and disease. The animals transformed indigenous cultures: the horse revolutionised the lives of the Plains Indians, who had never seen one before the Spanish arrival. The diseases were catastrophic. Smallpox, measles, influenza, typhus and plague swept through populations that had no immunity, killing tens of millions in what some historians call the Great Dying." },
            new { type = "timeline", date = "1520", evnt = "Smallpox reaches the Aztec Empire — killing perhaps half the population before Cortés completes his conquest" },
            new { type = "paragraph", text = "The Columbian Exchange also drove the transatlantic slave trade. European colonists needed labour to work the sugar, cotton and tobacco plantations of the New World. They turned to Africa. Over three and a half centuries, approximately 12.5 million Africans were forcibly transported to the Americas. Perhaps 1.8 million died during the Middle Passage crossing. The survivors and their descendants built the agricultural economies of the New World — and their forced labour shaped the modern world in ways that persist to this day." },
            new { type = "fact", title = "The World We Ate", text = "Consider what Europe ate before 1492: no tomatoes in Italian cuisine, no potatoes in Irish stew, no chillies in Hungarian goulash, no chocolate in Swiss confectionery, no vanilla in French pastry, no maize polenta in northern Italy. Consider what the Americas ate: no beef, no pork, no chicken, no wheat bread, no rice, no sugar. The Columbian Exchange created the cuisines of the modern world — proof that the most fundamental exchanges between cultures happen at the dinner table." },
            new { type = "image", url = "/images/chapters/exploration/slave-trade.png", caption = "The transatlantic slave trade — the darkest consequence of the Columbian Exchange, which forcibly moved 12.5 million Africans to the Americas." },
            new { type = "timeline", date = "1619", evnt = "The first enslaved Africans arrive in the English colony of Virginia — the beginning of slavery in what would become the United States" },
            new { type = "quote", text = "The discovery of America, and that of a passage to the East Indies by the Cape of Good Hope, are the two greatest and most important events recorded in the history of mankind.", source = "Adam Smith, The Wealth of Nations, 1776" },
            new { type = "paragraph", text = "The Columbian Exchange created the modern world. It redistributed the plant and animal species of the Earth, fed population booms on every continent, drove the expansion of European empires and the enslavement of millions, and set in motion ecological changes that are still unfolding. Every meal you eat, every landscape you see, every city you inhabit bears the marks of that exchange that began when three small ships crossed the Atlantic in 1492." },
            new { type = "image", url = "/images/chapters/exploration/new-world-map.png", caption = "A 16th century map of the New World — the cartographic record of a transformation that changed every aspect of human life." }
        });
    }
}