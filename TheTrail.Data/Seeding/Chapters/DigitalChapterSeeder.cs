using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class DigitalChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            int eraId = await context.Eras
                .Where(e => e.Name == "Digital Age")
                .Select(e => e.Id)
                .FirstOrDefaultAsync();

            if (eraId == 0) return;

            await SeedChapter1Async(context, eraId);
            await SeedChapter2Async(context, eraId);
            await SeedChapter3Async(context, eraId);
            await SeedChapter4Async(context, eraId);
            await SeedChapter5Async(context, eraId);
        }

        // ── Chapter 1 — The Birth of the Internet ─────────────────────────
        private static async Task SeedChapter1Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Birth of the Internet")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Birth of the Internet",
                Subtitle = "How a Cold War defence project became the nervous system of civilisation",
                Content = BuildChapter1Content(),
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
                    new Question { Text = "What was the name of the early US military network that preceded the internet?", OptionA = "DARPANET", OptionB = "ARPANET", OptionC = "MILNET", OptionD = "USENET", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Who invented the World Wide Web?", OptionA = "Vint Cerf", OptionB = "Bill Gates", OptionC = "Tim Berners-Lee", OptionD = "Steve Jobs", CorrectOption = "C", Order = 2 },
                    new Question { Text = "In what year was the World Wide Web made publicly available?", OptionA = "1989", OptionB = "1991", OptionC = "1993", OptionD = "1995", CorrectOption = "B", Order = 3 },
                    new Question { Text = "What were the first two letters successfully transmitted over ARPANET in 1969?", OptionA = "HI", OptionB = "OK", OptionC = "LO", OptionD = "GO", CorrectOption = "C", Order = 4 },
                    new Question { Text = "What communication protocol forms the technical foundation of the internet?", OptionA = "HTTP", OptionB = "FTP", OptionC = "TCP/IP", OptionD = "HTML", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 2 — The Personal Computer Revolution ──────────────────
        private static async Task SeedChapter2Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Personal Computer Revolution")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Personal Computer Revolution",
                Subtitle = "How a machine that filled a room shrank to fit in your pocket",
                Content = BuildChapter2Content(),
                Order = 2,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Personal Computer Revolution")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Which company released the first commercially successful personal computer?", OptionA = "IBM", OptionB = "Apple", OptionC = "Microsoft", OptionD = "Altair", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Who co-founded Apple Computer with Steve Jobs?", OptionA = "Bill Gates", OptionB = "Steve Ballmer", OptionC = "Steve Wozniak", OptionD = "Paul Allen", CorrectOption = "C", Order = 2 },
                    new Question { Text = "What operating system did Microsoft supply to IBM for its 1981 personal computer?", OptionA = "Windows", OptionB = "MS-DOS", OptionC = "Unix", OptionD = "CP/M", CorrectOption = "B", Order = 3 },
                    new Question { Text = "In what year did Apple release the Macintosh with its revolutionary graphical interface?", OptionA = "1981", OptionB = "1982", OptionC = "1984", OptionD = "1986", CorrectOption = "C", Order = 4 },
                    new Question { Text = "Which law predicts that computing power doubles approximately every two years?", OptionA = "Newton's Law", OptionB = "Moore's Law", OptionC = "Gates's Law", OptionD = "Metcalfe's Law", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 3 — The Space Race ────────────────────────────────────
        private static async Task SeedChapter3Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Space Race")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Space Race",
                Subtitle = "The Cold War competition that took humanity beyond the atmosphere",
                Content = BuildChapter3Content(),
                Order = 3,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Space Race")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Which country launched the first artificial satellite, Sputnik, in 1957?", OptionA = "United States", OptionB = "Soviet Union", OptionC = "China", OptionD = "Germany", CorrectOption = "B", Order = 1 },
                    new Question { Text = "Who was the first human to travel to space?", OptionA = "Neil Armstrong", OptionB = "Alan Shepard", OptionC = "Yuri Gagarin", OptionD = "John Glenn", CorrectOption = "C", Order = 2 },
                    new Question { Text = "On what date did Apollo 11 land on the Moon?", OptionA = "20 July 1969", OptionB = "21 July 1969", OptionC = "20 July 1968", OptionD = "12 April 1961", CorrectOption = "A", Order = 3 },
                    new Question { Text = "What were Neil Armstrong's famous words as he stepped onto the Moon?", OptionA = "One small step for man, one giant leap for mankind", OptionB = "The eagle has landed", OptionC = "Houston, we have a problem", OptionD = "That's one small step for a man, one giant leap for mankind", CorrectOption = "D", Order = 4 },
                    new Question { Text = "Which Apollo mission famously experienced an oxygen tank explosion en route to the Moon?", OptionA = "Apollo 11", OptionB = "Apollo 12", OptionC = "Apollo 13", OptionD = "Apollo 14", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 4 — The Human Genome Project ─────────────────────────
        private static async Task SeedChapter4Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "The Human Genome Project")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "The Human Genome Project",
                Subtitle = "The day humanity read the instruction manual for human life",
                Content = BuildChapter4Content(),
                Order = 4,
                EstimatedMinutes = 8,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "The Human Genome Project")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "In what year was the Human Genome Project completed?", OptionA = "1999", OptionB = "2001", OptionC = "2003", OptionD = "2005", CorrectOption = "C", Order = 1 },
                    new Question { Text = "How many base pairs does the human genome contain?", OptionA = "Around 1 billion", OptionB = "Around 3 billion", OptionC = "Around 10 billion", OptionD = "Around 30 billion", CorrectOption = "B", Order = 2 },
                    new Question { Text = "Who co-discovered the structure of DNA in 1953?", OptionA = "Einstein and Bohr", OptionB = "Watson and Crick", OptionC = "Mendel and Darwin", OptionD = "Pasteur and Lister", CorrectOption = "B", Order = 3 },
                    new Question { Text = "Approximately how many protein-coding genes does the human genome contain?", OptionA = "Around 5,000", OptionB = "Around 20,000", OptionC = "Around 100,000", OptionD = "Around 500,000", CorrectOption = "B", Order = 4 },
                    new Question { Text = "What gene-editing technology, developed in the 2010s, allows precise modification of DNA?", OptionA = "PCR", OptionB = "CRISPR-Cas9", OptionC = "mRNA therapy", OptionD = "Recombinant DNA", CorrectOption = "B", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Chapter 5 — Artificial Intelligence ──────────────────────────
        private static async Task SeedChapter5Async(TheTrailDbContext context, int eraId)
        {
            if (await context.Chapters.AnyAsync(c => c.Title == "Artificial Intelligence")) return;

            context.Chapters.Add(new Chapter
            {
                Title = "Artificial Intelligence",
                Subtitle = "The machine that learned to think — and what happens next",
                Content = BuildChapter5Content(),
                Order = 5,
                EstimatedMinutes = 9,
                EraId = eraId,
                IsPublished = true,
                CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
            await context.SaveChangesAsync();

            int chapterId = await context.Chapters
                .Where(c => c.Title == "Artificial Intelligence")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (chapterId == 0) return;

            context.Quizzes.Add(new Quiz
            {
                ChapterId = chapterId,
                PassMarkPercent = 60,
                Questions = new List<Question>
                {
                    new Question { Text = "Who is considered the father of artificial intelligence?", OptionA = "John von Neumann", OptionB = "Alan Turing", OptionC = "Claude Shannon", OptionD = "Marvin Minsky", CorrectOption = "B", Order = 1 },
                    new Question { Text = "What test did Alan Turing propose to measure machine intelligence?", OptionA = "The Intelligence Quotient Test", OptionB = "The Imitation Game", OptionC = "The Turing Test", OptionD = "Both B and C", CorrectOption = "D", Order = 2 },
                    new Question { Text = "In what year did IBM's Deep Blue defeat world chess champion Garry Kasparov?", OptionA = "1993", OptionB = "1995", OptionC = "1997", OptionD = "1999", CorrectOption = "C", Order = 3 },
                    new Question { Text = "What type of AI architecture underlies modern large language models like GPT?", OptionA = "Recurrent Neural Networks", OptionB = "Convolutional Neural Networks", OptionC = "Transformer architecture", OptionD = "Expert systems", CorrectOption = "C", Order = 4 },
                    new Question { Text = "In what year did ChatGPT launch, sparking widespread public awareness of AI?", OptionA = "2020", OptionB = "2021", OptionC = "2022", OptionD = "2023", CorrectOption = "C", Order = 5 }
                }
            });
            await context.SaveChangesAsync();
        }

        // ── Content builders ──────────────────────────────────────────────
        private static string BuildChapter1Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On the night of 29 October 1969, researchers at UCLA sent a message to a computer at Stanford Research Institute over a new experimental network called ARPANET. They typed 'L', then 'O' — and the system crashed. Those two letters — 'LO' — were the first message ever transmitted over what would become the internet. It was, by accident, the most poetic beginning imaginable: 'lo', as in 'lo and behold'. The network that would connect all of humanity had spoken its first words." },
            new { type = "fact", title = "ARPANET's Purpose", text = "ARPANET was funded by the US Department of Defense's Advanced Research Projects Agency and designed to allow multiple computers to share resources and communicate. A popular myth holds that it was designed to survive nuclear war — this is only partly true. Its packet-switching architecture happened to be resilient, but its primary purpose was resource sharing among research computers. The internet's military origins shaped its decentralised, redundant architecture — which is why it has proved so difficult to censor or control." },
            new { type = "image", url = "/images/chapters/digital/arpanet.png", caption = "The original ARPANET map — four nodes in 1969 that would grow into the global internet." },
            new { type = "paragraph", text = "For its first two decades, the internet was the exclusive preserve of researchers, academics and the military. Then, in 1989, a British scientist named Tim Berners-Lee working at CERN in Geneva wrote a proposal for an information management system. His boss wrote 'vague but exciting' on the cover. Berners-Lee spent the next two years developing the World Wide Web — a system of hyperlinked documents accessible through a browser. In 1991 he made it publicly available, for free, without patent. The modern internet was born." },
            new { type = "timeline", date = "1991", evnt = "Tim Berners-Lee makes the World Wide Web publicly available — the moment the internet became accessible to ordinary people" },
            new { type = "paragraph", text = "The web's growth was explosive beyond any precedent. In 1993 there were 130 websites. By 1996, 250,000. By 2000, 17 million. By 2023, over 1.9 billion. The dot-com boom of the late 1990s created enormous fortunes and then destroyed them in the crash of 2000-01. From the wreckage emerged Google, Amazon, and the social media platforms that would define the 21st century. The internet had become the infrastructure of modern civilisation." },
            new { type = "fact", title = "The Scale of Connection", text = "By 2024, approximately 5.4 billion people — 67% of the world's population — use the internet. Every minute, users send 241 million emails, watch 500 hours of YouTube video, and conduct 8.5 million Google searches. The internet carries approximately 4.8 billion gigabytes of data every day. It has transformed commerce, politics, culture, science, journalism, friendship and war. No technology since the printing press has changed human communication so completely." },
            new { type = "image", url = "/images/chapters/digital/www.png", caption = "The World Wide Web — Tim Berners-Lee's gift to humanity, given freely and without patent." },
            new { type = "timeline", date = "2004", evnt = "Facebook launches at Harvard — the beginning of social media's transformation of human communication and politics" },
            new { type = "quote", text = "The web is more a social creation than a technical one. I designed it for a social effect — to help people work together — and not as a technical toy.", source = "Tim Berners-Lee" },
            new { type = "paragraph", text = "The internet has also created problems its inventors never anticipated. Misinformation spreads faster than truth. Authoritarian governments use it to surveil and control their populations. Social media algorithms optimise for engagement over accuracy, polarising societies. Cybercrime costs the global economy trillions annually. The question of how to govern the internet — who owns it, who regulates it, what it can and cannot do — is one of the defining political questions of the 21st century." },
            new { type = "image", url = "/images/chapters/digital/connected.png", caption = "A map of global internet connections — the neural network of human civilisation, lighting up the world." }
        });

        private static string BuildChapter2Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In 1943, the chairman of IBM, Thomas Watson, allegedly predicted: 'I think there is a world market for maybe five computers.' Whether he said it or not, it captured the consensus of the era: computers were vast, expensive machines for governments and corporations, filling entire rooms, requiring teams of specialists to operate. The idea that one day every person on Earth would carry a more powerful computer in their pocket would have seemed like science fiction. It took less than fifty years." },
            new { type = "fact", title = "The Microprocessor", text = "The revolution began with the microprocessor — an entire computer processor on a single chip of silicon. Intel's 4004, released in 1971, contained 2,300 transistors and could perform 60,000 operations per second. It was designed for a Japanese calculator. Its descendants power everything from smartphones to satellites. Moore's Law — Gordon Moore's 1965 observation that transistor density doubles roughly every two years — predicted the exponential growth in computing power that has driven the digital revolution." },
            new { type = "image", url = "/images/chapters/digital/apple-1.png", caption = "The Apple I — built by Steve Wozniak in a garage in 1976, the machine that began the personal computer revolution." },
            new { type = "paragraph", text = "The personal computer era began in a garage in Los Altos, California, in 1976. Steve Wozniak, a brilliant engineer, built a computer he called the Apple I. His friend Steve Jobs saw its commercial potential. Together they founded Apple Computer and sold it as a kit. The Apple II, released in 1977, was the first mass-market personal computer — easy to use, attractively designed, and successful enough to make Apple a multimillion dollar company within three years." },
            new { type = "timeline", date = "1984", evnt = "Apple releases the Macintosh with a graphical user interface — the computer that brought computing to ordinary people" },
            new { type = "paragraph", text = "The industry was transformed again in 1981 when IBM released its Personal Computer. IBM chose to use an outside operating system — MS-DOS from a small Seattle company called Microsoft, run by Bill Gates. IBM's decision to use off-the-shelf components and license its design meant that 'IBM-compatible' clones soon flooded the market. Microsoft, which owned the operating system that ran on all of them, became the most valuable company in the world. Gates became the richest man alive." },
            new { type = "fact", title = "The iPhone Moment", text = "On 9 January 2007, Steve Jobs walked onto a stage in San Francisco and announced: 'Today, Apple is going to reinvent the phone.' The iPhone combined a mobile phone, a music player and an internet browser into a single touchscreen device. It was not the first smartphone — but it was the first one that worked intuitively enough for anyone to use. Within a decade, 3.5 billion people owned smartphones. The personal computer had become personal in a way its inventors never imagined." },
            new { type = "image", url = "/images/chapters/digital/silicon-valley.png", caption = "Silicon Valley — the fifty-mile strip of California that became the capital of the digital age." },
            new { type = "timeline", date = "2007", evnt = "Apple releases the iPhone — transforming the mobile phone into a pocket computer and putting the internet in everyone's hands" },
            new { type = "quote", text = "The people who are crazy enough to think they can change the world are the ones who do.", source = "Apple's 'Think Different' advertising campaign, 1997" },
            new { type = "paragraph", text = "The personal computer revolution created the digital economy, the gig economy, the creator economy and the attention economy. It produced companies worth more than the GDP of most nations. It made knowledge instantly accessible and made misinformation instantly spreadable. It connected humanity in ways that have enabled both extraordinary collaboration and devastating polarisation. The machine that was supposed to serve its users has, in many ways, become their master." },
            new { type = "image", url = "/images/chapters/digital/smartphone.png", caption = "The smartphone — the personal computer in its ultimate form, carrying more computing power than sent men to the Moon." }
        });

        private static string BuildChapter3Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 4 October 1957, a metal sphere the size of a beach ball — 58 centimetres in diameter, weighing 83 kilograms — was launched into orbit by the Soviet Union. Sputnik 1 did nothing except emit a radio beep. But that beep was heard around the world, and it changed everything. The Space Age had begun. For the first time in human history, an object made by human hands was circling the Earth in space. The implications — military, scientific, psychological — were staggering." },
            new { type = "fact", title = "The American Response", text = "Sputnik shocked and terrified the United States. If the Soviets could put a satellite in orbit, they could put a nuclear warhead anywhere on Earth. Congress passed the National Defense Education Act, pouring money into science education. NASA was created in 1958. The Space Race was on — a competition as much about ideology as technology. Each launch, each first, was a statement about which system — capitalism or communism — was superior. Space became the ultimate Cold War theatre." },
            new { type = "image", url = "/images/chapters/digital/sputnik.png", caption = "Sputnik 1 — the metal sphere that shocked America and began the Space Age on 4 October 1957." },
            new { type = "paragraph", text = "The Soviets accumulated firsts at a dizzying rate: first satellite, first animal in space (the dog Laika in 1957), first human in space (Yuri Gagarin, 12 April 1961), first spacewalk (Alexei Leonov, 1965). President Kennedy, stung by these humiliations, made the boldest commitment in the history of exploration: the United States would land a man on the Moon and return him safely to Earth before the end of the decade. In 1961, with one astronaut having spent fifteen minutes in space, this seemed almost impossible." },
            new { type = "timeline", date = "12 April 1961", evnt = "Yuri Gagarin becomes the first human in space, completing one orbit of the Earth in 108 minutes" },
            new { type = "paragraph", text = "Apollo 11 launched on 16 July 1969. Four days later, the lunar module Eagle separated from the command module and descended toward the Sea of Tranquility. With 30 seconds of fuel remaining, Neil Armstrong manually guided the spacecraft past a boulder field and touched down. 'The Eagle has landed,' he reported. Six hours later, he stepped onto the lunar surface and spoke the words: 'That's one small step for a man, one giant leap for mankind.' An estimated 600 million people watched on television." },
            new { type = "fact", title = "The Technology Legacy", text = "The Apollo programme produced technological spinoffs that transformed civilian life: integrated circuits, CAT scanners, water filtration technology, scratch-resistant lenses, memory foam, freeze-dried food, cordless tools and the computer mouse all emerged from NASA research. The programme's computing requirements drove the development of integrated circuits that made personal computers possible. Every smartphone in existence owes something to the Apollo programme." },
            new { type = "image", url = "/images/chapters/digital/apollo.png", caption = "Apollo 11 landing on the Moon — the greatest adventure in human history, watched by 600 million people." },
            new { type = "timeline", date = "20 July 1969", evnt = "Apollo 11 lands on the Moon — humanity's greatest exploration achievement, fulfilling Kennedy's promise" },
            new { type = "quote", text = "That's one small step for a man, one giant leap for mankind.", source = "Neil Armstrong, Sea of Tranquility, 20 July 1969" },
            new { type = "paragraph", text = "The Space Race ended with the Moon landings — twelve Americans walked on the Moon between 1969 and 1972, and then humanity stopped going. Budget cuts, Vietnam and shifting priorities ended the Apollo programme. But the legacy of the Space Age persists: the International Space Station, the Hubble Space Telescope, GPS satellites, weather satellites, and now the new commercial space race led by SpaceX, Blue Origin and others. The universe awaits." },
            new { type = "image", url = "/images/chapters/digital/earthrise.png", caption = "Earthrise — photographed by Apollo 8 in 1968, this image of Earth from the Moon changed how humanity saw itself." }
        });

        private static string BuildChapter4Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "On 26 June 2000, in the East Room of the White House, President Bill Clinton stood between two scientists — Francis Collins of the publicly-funded Human Genome Project and Craig Venter of the private company Celera Genomics — and announced one of the greatest scientific achievements in history. The human genome had been sequenced. For the first time, humanity possessed a complete map of its own genetic blueprint — the 3 billion chemical base pairs that contain the instructions for building and operating a human being." },
            new { type = "fact", title = "What is a Genome?", text = "The human genome is the complete set of genetic information contained in human DNA — the molecule that carries the instructions for all living organisms. It consists of approximately 3.2 billion base pairs of DNA, organised into 23 pairs of chromosomes, containing around 20,000 protein-coding genes. If you printed the human genome in standard book format, the books would fill a library of 5,000 volumes. Reading it took 13 years, cost $3 billion and required the collaboration of scientists from 20 institutions in 6 countries." },
            new { type = "image", url = "/images/chapters/digital/dna.png", caption = "The double helix of DNA — the molecule that carries the instructions for all life, sequenced in full by 2003." },
            new { type = "paragraph", text = "The foundations of genomics were laid in 1953, when James Watson and Francis Crick published their model of DNA's double helix structure — building critically on X-ray crystallography data produced by Rosalind Franklin. The discovery revealed how genetic information is stored and copied. Four decades of molecular biology followed, developing the tools needed to read DNA sequences. The Human Genome Project, launched in 1990, was the culmination: an international effort to read the entire human genetic text." },
            new { type = "timeline", date = "2003", evnt = "The Human Genome Project is declared complete — the full sequence of human DNA published as a public resource available to every scientist on Earth" },
            new { type = "paragraph", text = "The applications of genomic knowledge have transformed medicine. Genetic testing can now identify inherited predispositions to cancer, heart disease and hundreds of other conditions. Pharmacogenomics tailors drug treatments to individual genetic profiles. The mRNA vaccines that protected billions from COVID-19 were possible only because of genomic science. Cancer treatment increasingly targets specific genetic mutations rather than simply killing rapidly dividing cells." },
            new { type = "fact", title = "CRISPR Revolution", text = "In 2012, Jennifer Doudna and Emmanuelle Charpentier developed CRISPR-Cas9 — a tool that can edit DNA with unprecedented precision, targeting specific sequences and cutting, deleting or inserting genetic material. It was described as 'molecular scissors'. By 2023, CRISPR therapies had cured patients of sickle cell disease and beta-thalassemia. The technique raises profound ethical questions: if we can edit the human genome, should we? And who decides?" },
            new { type = "image", url = "/images/chapters/digital/genome-map.png", caption = "The human genome map — the instruction manual for Homo sapiens, completed after 13 years of international collaboration." },
            new { type = "timeline", date = "2020", evnt = "Jennifer Doudna and Emmanuelle Charpentier win the Nobel Prize for developing CRISPR-Cas9 gene editing" },
            new { type = "quote", text = "We have caught the first glimpse of our own instruction book, previously known only to God.", source = "Francis Collins, Human Genome Project Director, 2000" },
            new { type = "paragraph", text = "The sequencing of the human genome was only the beginning. The cost of sequencing a human genome has fallen from $3 billion in 2003 to under $200 today — a fall faster than Moore's Law. Millions of human genomes have now been sequenced. Scientists are beginning to understand the complex interactions between genes and environment that determine health, disease, intelligence and personality. We stand at the beginning of an era in which humanity will be able to read, and perhaps rewrite, the text of life itself." },
            new { type = "image", url = "/images/chapters/digital/biotech.png", caption = "A modern biotechnology laboratory — where the insights of the Human Genome Project are being translated into medicine." }
        });

        private static string BuildChapter5Content() => JsonSerializer.Serialize(new object[]
        {
            new { type = "paragraph", text = "In 1950, the British mathematician Alan Turing published a paper titled 'Computing Machinery and Intelligence' that began with the question: 'Can machines think?' He proposed a test — now called the Turing Test — in which a human interrogator communicates through a terminal with either a human or a machine, and must determine which is which. If the machine could fool the interrogator, Turing suggested, it could be said to think. Seventy years later, machines can write poetry, compose music, paint pictures, write code and hold conversations indistinguishable from a human. The question Turing asked has never been more urgent." },
            new { type = "fact", title = "The History of AI", text = "The field of artificial intelligence was formally founded at a conference at Dartmouth College in 1956. Its early decades were marked by cycles of euphoric promise and bitter disappointment — what came to be called 'AI winters'. The breakthroughs came from machine learning: instead of programming rules, researchers trained algorithms on vast datasets, letting the machines find patterns themselves. The deep learning revolution of the 2010s — powered by neural networks, massive datasets and GPU computing — produced the AI systems we use today." },
            new { type = "image", url = "/images/chapters/digital/ai-brain.png", caption = "A visualisation of a neural network — the architecture that powers modern artificial intelligence." },
            new { type = "paragraph", text = "The milestones came rapidly. In 1997, IBM's Deep Blue defeated world chess champion Garry Kasparov — the first time a computer had beaten a reigning world champion. In 2011, IBM's Watson won Jeopardy! against champion human players. In 2016, DeepMind's AlphaGo defeated the world's best Go player — a game long thought immune to computational brute force, because its complexity exceeds the number of atoms in the universe. Each victory seemed impossible until the moment it happened." },
            new { type = "timeline", date = "November 2022", evnt = "ChatGPT launches — reaching 100 million users in two months and sparking a global debate about the future of artificial intelligence" },
            new { type = "paragraph", text = "The release of ChatGPT in November 2022 brought AI into mainstream consciousness with a speed that stunned even its creators. Within two months it had 100 million users — the fastest adoption of any technology in history. Suddenly, anyone could have a fluent conversation with a machine, generate images from text descriptions, write code, draft legal documents or compose essays. The implications for employment, education, creativity and truth itself began to be debated with an urgency that matched the pace of the technology." },
            new { type = "fact", title = "The Existential Question", text = "AI researchers are divided on the ultimate trajectory of artificial intelligence. Some believe we are approaching Artificial General Intelligence — a machine that can match or exceed human performance across all cognitive domains. Others believe such a goal is decades or centuries away, or fundamentally impossible. What is not in dispute is that the AI systems of today are transforming every industry, every profession and every aspect of human life at a speed that outpaces our ability to understand the implications." },
            new { type = "image", url = "/images/chapters/digital/future-ai.png", caption = "The age of artificial intelligence — a transformation of human capability whose ultimate consequences remain unknowable." },
            new { type = "timeline", date = "2023", evnt = "Multiple governments begin regulating AI — the EU AI Act passes, the first major legislation governing artificial intelligence" },
            new { type = "quote", text = "The development of full artificial intelligence could spell the end of the human race... or it could be the best thing ever to happen to us. We just don't know.", source = "Stephen Hawking" },
            new { type = "paragraph", text = "We are living through the earliest days of the most transformative technology in human history — or so its proponents claim. The truth is that no one knows. What we do know is that artificial intelligence is already transforming medicine, science, art, law, education and warfare. It is helping discover new drugs, predict protein structures, detect cancer, translate languages and generate content on a scale that dwarfs all previous human creative output. Whether it will ultimately prove to be humanity's greatest tool or its greatest threat is the question that will define the century to come. The trail of history continues — and where it leads, none can say." },
            new { type = "image", url = "/images/chapters/digital/horizon.png", caption = "The horizon of the digital age — a future being shaped by technologies whose implications we are only beginning to understand." }
        });
    }
}