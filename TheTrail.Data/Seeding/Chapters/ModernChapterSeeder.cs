using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class ModernChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Modern History")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — The First World War ───────────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The First World War")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The First World War",
                Subtitle = "The war that was supposed to be over by Christmas — and lasted four years",
                Content = BuildChapter1Content(),
                Order = 1,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "World_War_I",
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
                    new Question { Text = "What event triggered the outbreak of World War One?", OptionA = "The sinking of the Lusitania", OptionB = "The assassination of Archduke Franz Ferdinand", OptionC = "Germany's invasion of Belgium", OptionD = "The Austrian ultimatum to Serbia", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which alliance system dragged multiple nations into the war?", OptionA = "The Triple Alliance and the Triple Entente", OptionB = "The Axis and the Allies", OptionC = "NATO and the Warsaw Pact", OptionD = "The Holy Alliance and the Concert of Europe", CorrectOption = "A", Order = 2 },
                    new Question { Text = "How many people died in the First World War?", OptionA = "Around 5 million", OptionB = "Around 10 million", OptionC = "Around 17 million", OptionD = "Around 25 million", CorrectOption = "C", Order = 3 },
                    new Question { Text = "Which treaty formally ended World War One?", OptionA = "The Treaty of Brest-Litovsk", OptionB = "The Treaty of Versailles", OptionC = "The Treaty of Paris", OptionD = "The Armistice of Compiègne", CorrectOption = "B", Order = 4 },
                    new Question { Text = "On what date did the armistice ending World War One take effect?", OptionA = "11 November 1918", OptionB = "11 November 1919", OptionC = "28 June 1919", OptionD = "4 August 1914", CorrectOption = "A", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 2 — The French Revolution ────────────────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The French Revolution")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The French Revolution",
                Subtitle = "Liberty, equality, fraternity — and the guillotine",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "French_Revolution",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The French Revolution")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "In what year did the French Revolution begin?", OptionA = "1776", OptionB = "1789", OptionC = "1793", OptionD = "1799", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Which famous Paris prison was stormed on 14 July 1789?", OptionA = "The Louvre", OptionB = "Versailles", OptionC = "The Bastille", OptionD = "The Conciergerie", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Who led the Reign of Terror?", OptionA = "Napoleon Bonaparte", OptionB = "Louis XVI", OptionC = "Jean-Paul Marat", OptionD = "Maximilien Robespierre", CorrectOption = "D", Order = 3 },
                    new Question { Text = "What was the name of the French revolutionary slogan?", OptionA = "Life, liberty and the pursuit of happiness", OptionB = "Liberty, equality, fraternity", OptionC = "All power to the people", OptionD = "Death to tyrants", CorrectOption = "B", Order = 4 },
                    new Question { Text = "Who came to power in France after the Revolution ended?", OptionA = "Louis XVIII", OptionB = "The Directory", OptionC = "Napoleon Bonaparte", OptionD = "Robespierre", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — The Industrial Revolution ─────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Industrial Revolution")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Industrial Revolution",
                Subtitle = "How steam and iron remade the world in a single century",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Industrial_Revolution",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Industrial Revolution")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Where did the Industrial Revolution begin?", OptionA = "France", OptionB = "Germany", OptionC = "United States", OptionD = "Britain", CorrectOption = "D", Order = 1 },
                    new Question { Text = "Who invented the steam engine that powered the Industrial Revolution?", OptionA = "Thomas Edison", OptionB = "George Stephenson", OptionC = "James Watt", OptionD = "Isambard Kingdom Brunel", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What was the name of the world's first passenger railway line?", OptionA = "The Great Western Railway", OptionB = "The Liverpool and Manchester Railway", OptionC = "The Stockton and Darlington Railway", OptionD = "The London and Birmingham Railway", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Which book analysed the Industrial Revolution's impact on the working class?", OptionA = "The Wealth of Nations", OptionB = "Das Kapital", OptionC = "The Communist Manifesto", OptionD = "The Condition of the Working Class in England", CorrectOption = "D", Order = 4 },
                    new Question { Text = "What percentage of Britain's population lived in cities by 1850?", OptionA = "Around 20%", OptionB = "Around 35%", OptionC = "Around 50%", OptionD = "Around 75%", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — The Second World War ──────────────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Second World War")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Second World War",
                Subtitle = "The deadliest conflict in human history — and the world it made",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 10,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "World_War_II",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Second World War")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "On what date did Germany invade Poland, triggering World War Two?", OptionA = "1 September 1939", OptionB = "3 September 1939", OptionC = "10 May 1940", OptionD = "22 June 1941", CorrectOption = "A", Order = 1 },
                    new Question { Text = "How many people died in the Holocaust?", OptionA = "Around 3 million", OptionB = "Around 4 million", OptionC = "Around 6 million", OptionD = "Around 9 million", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What was the codename for the Allied invasion of Normandy on 6 June 1944?", OptionA = "Operation Barbarossa", OptionB = "Operation Market Garden", OptionC = "Operation Overlord", OptionD = "Operation Sea Lion", CorrectOption = "C", Order = 3 },
                    new Question { Text = "On which two Japanese cities were atomic bombs dropped in August 1945?", OptionA = "Tokyo and Osaka", OptionB = "Hiroshima and Nagasaki", OptionC = "Kyoto and Hiroshima", OptionD = "Nagasaki and Tokyo", CorrectOption = "B", Order = 4 },
                    new Question { Text = "How many people died in total in the Second World War?", OptionA = "Around 30 million", OptionB = "Around 50 million", OptionC = "Around 70-85 million", OptionD = "Around 100 million", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — The Cold War ──────────────────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Cold War")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Cold War",
                Subtitle = "Forty years of nuclear standoff that shaped every nation on Earth",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                WikiSlug = "Cold_War",
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Cold War")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Which two superpowers were the primary antagonists of the Cold War?", OptionA = "The USA and China", OptionB = "The USA and the USSR", OptionC = "Britain and the USSR", OptionD = "The USA and Germany", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What was the name of the 1962 crisis that brought the world closest to nuclear war?", OptionA = "The Berlin Crisis", OptionB = "The Korean War", OptionC = "The Cuban Missile Crisis", OptionD = "The Suez Crisis", CorrectOption = "C", Order = 2 },
                    new Question { Text = "Which Soviet satellite became the first artificial object in space in 1957?", OptionA = "Vostok", OptionB = "Sputnik", OptionC = "Mir", OptionD = "Luna", CorrectOption = "B", Order = 3 },
                    new Question { Text = "In what year did the Berlin Wall fall?", OptionA = "1985", OptionB = "1987", OptionC = "1989", OptionD = "1991", CorrectOption = "C", Order = 4 },
                    new Question { Text = "In what year did the Soviet Union formally dissolve, ending the Cold War?", OptionA = "1989", OptionB = "1990", OptionC = "1991", OptionD = "1992", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 28 June 1914, in the Bosnian city of Sarajevo, a nineteen-year-old Bosnian Serb nationalist named Gavrilo Princip shot Archduke Franz Ferdinand, heir to the Austro-Hungarian throne, and his wife Sophie. It was the shot that started the First World War — though it took five weeks of diplomatic crisis, ultimatums and mobilisations for the war to actually begin. By the time it ended in November 1918, roughly 17 million people were dead and the old European order had been destroyed beyond recovery." },
            new { type = "fact", title = "The Alliance System", text = "Europe in 1914 was divided into two armed camps connected by a web of treaties. The Triple Alliance linked Germany, Austria-Hungary and Italy. The Triple Entente linked France, Russia and Britain. When Austria-Hungary declared war on Serbia, Russia mobilised to defend Serbia, Germany declared war on Russia, France was pulled in through her alliance with Russia, and Britain entered when Germany violated Belgian neutrality. A regional dispute became a world war in thirty-seven days." },
            new { type = "image", url = "/images/chapters/modern/trenches.png", caption = "The Western Front — 700 kilometres of trenches stretching from the English Channel to Switzerland." },
            new { type = "paragraph", text = "The war that both sides expected to be over by Christmas 1914 settled into four years of industrialised slaughter. The Western Front — a line of trenches stretching 700 kilometres from the English Channel to Switzerland — barely moved for years. New weapons of mass destruction appeared: poison gas, tanks, aircraft, long-range artillery. The Battle of the Somme in 1916 killed 57,470 British soldiers on its first day alone. By the war's end, the machine gun and barbed wire had made offensive warfare catastrophically expensive." },
            new { type = "timeline", date = "1 July 1916", evnt = "The first day of the Battle of the Somme — 57,470 British casualties, the bloodiest day in British military history" },
            new { type = "paragraph", text = "The war ended not with a decisive military victory but with exhaustion. Germany's allies collapsed one by one in autumn 1918. Revolution broke out in Germany itself. The Kaiser abdicated. On 11 November 1918, at 11 AM, the guns fell silent. The Treaty of Versailles imposed harsh terms on Germany — reparations, territorial losses, the humiliating war-guilt clause. Many historians argue that the seeds of the Second World War were sown at Versailles." },
            new { type = "fact", title = "The Spanish Flu", text = "The First World War was followed immediately by a pandemic that killed more people than the war itself. The Spanish Flu of 1918-20 infected an estimated 500 million people — a third of the world's population — and killed between 50 and 100 million. Weakened by war, malnutrition and the movement of millions of soldiers, populations across the world had little resistance. It remains the deadliest pandemic in recorded history after the Black Death." },
            new { type = "image", url = "/images/chapters/modern/somme.png", caption = "The Battle of the Somme — the bloodiest battle in British military history, a byword for the futility of industrial warfare." },
            new { type = "timeline", date = "11 November 1918", evnt = "Armistice Day — the guns of the Western Front fall silent at 11 AM, ending four years of the deadliest war in history to that point" },
            new { type = "quote", text = "They shall not grow old, as we that are left grow old. Age shall not weary them, nor the years condemn.", source = "Laurence Binyon, For the Fallen, 1914" },
            new { type = "paragraph", text = "The First World War destroyed four empires — the German, Austro-Hungarian, Ottoman and Russian. It redrew the map of Europe and the Middle East, creating nations that had never existed before. It introduced industrialised killing on a scale the world had never seen. It produced the Russian Revolution and, through the failures of Versailles, made the Second World War almost inevitable. It was, as the soldiers who fought it said, the war to end all wars — and it was not." },
            new { type = "image", url = "/images/chapters/modern/armistice.png", caption = "Armistice Day, 11 November 1918 — the moment the Great War ended, leaving a world transformed beyond recognition." }
        });

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In the summer of 1789, France was bankrupt, its harvests had failed, its people were hungry, and its king — Louis XVI — was convening a parliament that had not met in 175 years to solve a fiscal crisis. What began as a financial emergency became the most radical political revolution in history: a complete dismantling of the monarchy, the aristocracy, the Church and the entire social order of the ancien régime. The revolution that followed would inspire and terrify the world for two centuries." },
            new { type = "fact", title = "The Three Estates", text = "French society was divided into three Estates: the clergy (First Estate), the nobility (Second Estate) and everyone else — about 97% of the population — the Third Estate. The First and Second Estates paid virtually no taxes. The Third Estate paid almost all of them. When Louis XVI called the Estates-General to address the fiscal crisis, the Third Estate's representatives transformed themselves into a National Assembly and declared that sovereign power belonged to the nation, not the king. The revolution had begun." },
            new { type = "image", url = "/images/chapters/modern/bastille.png", caption = "The storming of the Bastille, 14 July 1789 — the moment the French Revolution became a popular uprising." },
            new { type = "paragraph", text = "The storming of the Bastille on 14 July 1789 became the symbolic moment of the revolution. The fortress-prison held only seven prisoners — but it represented royal tyranny, and its fall electrified France and the world. The Declaration of the Rights of Man and of the Citizen, adopted in August 1789, proclaimed liberty, equality and popular sovereignty as universal principles. The king accepted a constitutional monarchy. It seemed, briefly, as though a peaceful transformation was possible." },
            new { type = "timeline", date = "14 July 1789", evnt = "The storming of the Bastille — the symbolic beginning of the French Revolution and the end of the ancien régime" },
            new { type = "paragraph", text = "The revolution turned radical in 1792 when war with Austria and Prussia threatened to crush it. The monarchy was abolished; Louis XVI was tried for treason and guillotined in January 1793. Then came the Terror — a period of revolutionary paranoia in which the Committee of Public Safety, led by Maximilien Robespierre, executed approximately 17,000 people officially and perhaps 40,000 in total. Nobles, clergy, moderates, anyone suspected of counter-revolutionary sympathy went to the guillotine. Then Robespierre himself was arrested and guillotined in Thermidor 1794." },
            new { type = "fact", title = "Napoleon's Rise", text = "The revolution ended not with a return to monarchy but with something new: Napoleon Bonaparte, a Corsican artillery officer who had risen through the revolutionary army, seized power in a coup in 1799. He preserved many of the revolution's legal gains — the Napoleonic Code, religious tolerance, careers open to talent — while destroying its political ones. He made himself Emperor in 1804, crowning himself in Notre Dame Cathedral with the Pope watching. The revolution had produced a new kind of absolute ruler." },
            new { type = "image", url = "/images/chapters/modern/guillotine.png", caption = "The guillotine — the instrument of the Terror, which claimed 17,000 official victims including the king and eventually Robespierre himself." },
            new { type = "timeline", date = "21 January 1793", evnt = "Louis XVI is guillotined in the Place de la Révolution — the first European monarch to be publicly executed by his own people" },
            new { type = "quote", text = "The revolution devours its own children.", source = "Jacques Mallet du Pan, 1793" },
            new { type = "paragraph", text = "The French Revolution's legacy was enormous and contradictory. It gave the world the Declaration of the Rights of Man, modern nationalism, the metric system and the Napoleonic Code — a legal framework that still underlies the law of France, Spain, Italy, Belgium, Quebec and Louisiana. It also demonstrated that revolutions can devour their own ideals, that popular sovereignty can produce tyranny, and that the road from liberty to terror can be shorter than anyone imagines." },
            new { type = "image", url = "/images/chapters/modern/napoleon.png", caption = "Napoleon Bonaparte — the revolution's ultimate product, who preserved its legal legacy while destroying its political ideals." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "Sometime in the 1760s and 1770s, in the mills and workshops of northern England, something began that would transform the entire world within a century. The Industrial Revolution — powered by steam, coal and iron — replaced the muscle of humans and animals with machines, moved production from cottages to factories, and shifted populations from the countryside to cities at a speed and scale that no previous generation had experienced. It was the most consequential economic transformation in human history since the Agricultural Revolution ten thousand years before." },
            new { type = "fact", title = "Why Britain?", text = "The Industrial Revolution began in Britain for a convergence of reasons: abundant coal and iron deposits; a stable political system that protected property rights and patents; an empire that provided raw materials and markets; an agricultural revolution that had freed labour from the land; the world's largest free trade area; and a culture that rewarded practical tinkering and invention. James Watt's improved steam engine of 1769 — backed by entrepreneur Matthew Boulton — was the spark that lit the fire." },
            new { type = "image", url = "/images/chapters/modern/steam-engine.png", caption = "James Watt's steam engine — the machine that powered the Industrial Revolution and transformed the world." },
            new { type = "paragraph", text = "The steam engine unlocked energy on a previously inconceivable scale. It pumped water from mines, powered textile mills, drove printing presses and eventually propelled railway locomotives and steamships. The railway was the Industrial Revolution's most visible achievement: the Liverpool and Manchester Railway, opened in 1830, was the world's first intercity passenger line. By 1850, Britain had 6,000 miles of railway track. The country had been physically and economically transformed." },
            new { type = "timeline", date = "1830", evnt = "The Liverpool and Manchester Railway opens — the world's first intercity passenger railway, ushering in the age of rail" },
            new { type = "paragraph", text = "The human cost was enormous. Factory workers — including children as young as five — worked twelve to sixteen hours a day in dangerous conditions. Industrial cities grew so fast that they lacked sanitation, clean water or adequate housing. Life expectancy in Manchester in the 1840s was just 28 years. Engels described the condition of the working class with horror. Karl Marx, watching capitalism's birth pangs in London, developed the theory that would produce the twentieth century's defining political conflict." },
            new { type = "fact", title = "The Global Spread", text = "The Industrial Revolution spread from Britain to Belgium, France and Germany by the 1840s, to the United States and Japan by the 1870s, and eventually to the entire world. Countries that industrialised early gained enormous military and economic advantages over those that did not. The gap between industrialised and non-industrialised nations drove the imperialism of the nineteenth century — Britain, France, Germany, and America could project power globally in ways that earlier empires could not have imagined." },
            new { type = "image", url = "/images/chapters/modern/factory.png", caption = "A Victorian factory — the engine of industrial production that transformed goods, labour and society." },
            new { type = "timeline", date = "1848", evnt = "Marx and Engels publish The Communist Manifesto — a response to the conditions created by the Industrial Revolution that would shape the 20th century" },
            new { type = "quote", text = "The bourgeoisie has created more massive and more colossal productive forces than have all preceding generations together.", source = "Karl Marx and Friedrich Engels, The Communist Manifesto, 1848" },
            new { type = "paragraph", text = "The Industrial Revolution ultimately raised living standards enormously — but not before generating misery on a massive scale. It created the urban working class and the conditions for trade unionism, socialism and eventually the welfare state. It produced the wealth that funded science, education, medicine and art. It generated the carbon emissions that are now destabilising the climate. Every aspect of modern life — our cities, our economies, our politics, our environmental crisis — is a consequence of what began in those English mills." },
            new { type = "image", url = "/images/chapters/modern/victorian-city.png", caption = "Victorian Manchester — the first industrial city, a wonder of modernity and a catastrophe of living conditions simultaneously." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 1 September 1939, Germany invaded Poland. Two days later, Britain and France declared war on Germany. The Second World War had begun — a conflict that would last six years, involve every major nation on Earth, kill between 70 and 85 million people, and end with the world fundamentally and permanently changed. It was the deadliest conflict in human history, encompassing the deliberate murder of six million Jews in the Holocaust, and ending with the detonation of two atomic bombs." },
            new { type = "fact", title = "The Road to War", text = "The Second World War grew directly from the failures of the peace that ended the First. The Treaty of Versailles humiliated Germany without destroying her power to resist. The Great Depression of the 1930s created mass unemployment and political desperation. Adolf Hitler, who became German Chancellor in 1933, exploited these conditions brilliantly, promising national restoration and identifying Jews, communists and foreigners as enemies. The Western democracies, traumatised by the First World War, appeased him until it was too late." },
            new { type = "image", url = "/images/chapters/modern/ww2-europe.png", caption = "Europe in 1940 — Nazi Germany and its allies controlled most of the continent after France fell in six weeks." },
            new { type = "paragraph", text = "The war in Europe initially went catastrophically for the Allies. France fell in six weeks in 1940. Britain stood alone, sustained by Churchill's rhetoric and the RAF's narrow victory in the Battle of Britain. Germany invaded the Soviet Union in June 1941 — Operation Barbarossa — in the largest military operation in history. The Eastern Front would become the war's decisive theatre, killing perhaps 27 million Soviet citizens. Japan's attack on Pearl Harbor in December 1941 brought the United States into the war." },
            new { type = "timeline", date = "6 June 1944", evnt = "D-Day — 156,000 Allied troops land on the beaches of Normandy in the largest amphibious operation in history, beginning the liberation of Western Europe" },
            new { type = "paragraph", text = "The Holocaust was the systematic murder of six million Jews — two-thirds of European Jewry — by the Nazi regime. Jews were stripped of citizenship, deported to concentration and extermination camps, and murdered on an industrial scale. The camps at Auschwitz, Treblinka, Sobibor and Belzec killed millions. The Holocaust also murdered hundreds of thousands of Roma, disabled people, gay men, political prisoners and Soviet prisoners of war. It remains the defining atrocity of the modern era." },
            new { type = "fact", title = "The Atomic Bomb", text = "On 6 August 1945, the United States dropped an atomic bomb on the Japanese city of Hiroshima, killing approximately 80,000 people instantly and thousands more from radiation. Three days later, a second bomb destroyed Nagasaki. Japan surrendered on 15 August. The atomic bomb ended the war — but it also began the nuclear age, creating weapons capable of destroying civilisation and a forty-year standoff between the United States and Soviet Union that would define the second half of the twentieth century." },
            new { type = "image", url = "/images/chapters/modern/holocaust.png", caption = "The liberation of a concentration camp — the physical evidence of the Holocaust that shocked the world in 1945." },
            new { type = "timeline", date = "8 May 1945", evnt = "VE Day — Germany surrenders unconditionally. The war in Europe is over, revealing the full scale of Nazi atrocities" },
            new { type = "quote", text = "Never again.", source = "The founding principle of the United Nations, established in the aftermath of World War Two" },
            new { type = "paragraph", text = "The Second World War ended with a world that looked nothing like the one that had entered it. Europe was in ruins. The United States and Soviet Union had emerged as superpowers, dividing the world between them. The colonial empires of Britain, France and the Netherlands were fatally weakened. The United Nations was founded to prevent future wars. Israel was established as a Jewish state in 1948. The nuclear age had begun. And the shadow of the Holocaust would fall across every subsequent discussion of human rights, genocide and international law." },
            new { type = "image", url = "/images/chapters/modern/un-founding.png", caption = "The founding of the United Nations in 1945 — the world's attempt to build a system that would prevent another catastrophic war." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "The Second World War ended in 1945 with two superpowers standing over the ruins of the old world order. The United States and the Soviet Union had been wartime allies — but they were separated by irreconcilable differences of ideology, economics and political philosophy. Within two years of Germany's defeat, they were locked in a global competition that would last forty-five years, cost trillions of dollars, transform dozens of nations and bring the world several times to the brink of nuclear annihilation." },
            new { type = "fact", title = "The Nuclear Dimension", text = "What made the Cold War different from all previous great power rivalries was nuclear weapons. Both superpowers possessed enough warheads to destroy human civilisation many times over. This created what strategists called MAD — Mutually Assured Destruction: any nuclear exchange would destroy both sides. The knowledge that war meant annihilation prevented direct conflict between the superpowers — but it did not prevent dozens of proxy wars in Korea, Vietnam, Angola, Afghanistan and elsewhere, where millions died." },
            new { type = "image", url = "/images/chapters/modern/iron-curtain.png", caption = "The Berlin Wall — the physical embodiment of the Iron Curtain that divided Europe for 28 years." },
            new { type = "paragraph", text = "Europe was divided by what Churchill called the Iron Curtain — a line running from the Baltic to the Adriatic, separating Soviet-dominated Eastern Europe from the democratic West. Germany was split into two states. Berlin, deep inside East Germany, was itself divided — and became the flashpoint of multiple crises. The Berlin Blockade of 1948-49, the construction of the Berlin Wall in 1961, and the perpetual tension of a city divided by ideology and concrete made Berlin the symbolic heart of the Cold War." },
            new { type = "timeline", date = "October 1962", evnt = "The Cuban Missile Crisis — thirteen days in which the world came closest to nuclear war, resolved by secret diplomacy between Kennedy and Khrushchev" },
            new { type = "paragraph", text = "The Space Race was the Cold War's most spectacular theatre. When the Soviet Union launched Sputnik — the first artificial satellite — in October 1957, American confidence was shattered. The Soviets put the first human in space: Yuri Gagarin orbited the Earth in April 1961. President Kennedy responded by committing the United States to landing a man on the Moon before the end of the decade. On 20 July 1969, Neil Armstrong stepped onto the lunar surface. The United States had won the Space Race." },
            new { type = "fact", title = "The End of the Cold War", text = "The Cold War ended not with a bang but with a collapse. The Soviet economy, unable to compete with Western capitalism and exhausted by the arms race and the Afghan War, began to fail in the 1980s. Mikhail Gorbachev's reforms — glasnost and perestroika — unleashed forces he could not control. In 1989, the Berlin Wall fell as communist regimes across Eastern Europe collapsed one by one. On 25 December 1991, the Soviet Union itself was dissolved. The Cold War was over." },
            new { type = "image", url = "/images/chapters/modern/moon-landing.png", caption = "Neil Armstrong on the Moon, 20 July 1969 — the Cold War's most spectacular achievement, watched by 600 million people on television." },
            new { type = "timeline", date = "9 November 1989", evnt = "The fall of the Berlin Wall — the symbolic end of the Cold War and the beginning of German reunification" },
            new { type = "quote", text = "Mr Gorbachev, tear down this wall!", source = "President Ronald Reagan, speaking at the Berlin Wall, 12 June 1987" },
            new { type = "paragraph", text = "The Cold War shaped every aspect of the second half of the twentieth century. It drove decolonisation — both superpowers, for different reasons, opposed European empires. It produced the United Nations system, NATO and the European Union. It drove technological development: the internet, GPS and much modern computing were Cold War military projects. It killed millions in proxy conflicts from Korea to Vietnam to Angola. And its end — so sudden, so peaceful, so unexpected — created the world we live in now." },
            new { type = "image", url = "/images/chapters/modern/berlin-wall-fall.png", caption = "The fall of the Berlin Wall, November 1989 — the night the Cold War ended and Europe was reunited." }
        });
    }
}