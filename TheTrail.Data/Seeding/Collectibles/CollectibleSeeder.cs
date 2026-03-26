using Microsoft.EntityFrameworkCore;
using TheTrail.Domain.Entities;
using TheTrail.Domain.Enums;

namespace TheTrail.Data.Seeding.Collectibles
{
    public static class CollectibleSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            if (await context.Collectibles.AnyAsync())
            {
                return;
            }

            // ── Common & Rare — linked to chapters ─────────────────────────
            List<(string ChapterTitle, string Name, string Description, string ArtworkUrl, Rarity Rarity)> chapterCollectibles = new()
            {
                // ── Prehistoric ────────────────────────────────────────────
                (
                    "The Age of Dinosaurs",
                    "T-Rex",
                    "The apex predator of the prehistoric era. Earned by those who walked the trail and proved their knowledge.",
                    "/images/collectibles/trex.jpg",
                    Rarity.Common
                ),
                (
                    "The Age of Dinosaurs",
                    "The Amber Fossil",
                    "A creature preserved in amber for 66 million years — awarded only to those who answered every question without a single error.",
                    "/images/collectibles/amber-fossil.jpg",
                    Rarity.Rare
                ),
                (
                    "The First Humans",
                    "The Oldowan Tool",
                    "The oldest known stone tool — a simple but revolutionary flint scraper carried by the first humans out of Africa.",
                    "/images/collectibles/oldowan-tool.jpg",
                    Rarity.Common
                ),
                (
                    "The First Humans",
                    "The Ochre Pendant",
                    "A piece of red ochre worn as jewellery by Homo sapiens 75,000 years ago — the oldest known evidence of symbolic thinking. Awarded to those with perfect knowledge.",
                    "/images/collectibles/ochre-pendant.jpg",
                    Rarity.Rare
                ),
                (
                    "The Ice Age",
                    "The Mammoth Tusk",
                    "A fragment of woolly mammoth tusk, preserved in permafrost for twenty thousand years — earned by those who understood the frozen world.",
                    "/images/collectibles/mammoth-tusk.jpg",
                    Rarity.Common
                ),
                (
                    "The Ice Age",
                    "The Cave Bear Claw",
                    "A claw from the great cave bear — hunted to extinction at the end of the Ice Age. Awarded to those whose knowledge of the frozen world is without flaw.",
                    "/images/collectibles/cave-bear-claw.jpg",
                    Rarity.Rare
                ),
                (
                    "The Agricultural Revolution",
                    "The First Seed",
                    "A grain of domesticated wheat — the seed that ended ten thousand years of nomadic life and built the foundations of civilisation.",
                    "/images/collectibles/first-seed.jpg",
                    Rarity.Common
                ),
                (
                    "The Agricultural Revolution",
                    "The Sickle Blade",
                    "A flint sickle blade from the Fertile Crescent, circa 9000 BCE — the tool that harvested the first crops. Given only to those with perfect mastery.",
                    "/images/collectibles/sickle-blade.jpg",
                    Rarity.Rare
                ),
                (
                    "The Bronze Age",
                    "The Bronze Axe",
                    "A cast bronze axe from the early Bronze Age — harder than stone, sharper than copper, and the weapon that changed warfare forever.",
                    "/images/collectibles/bronze-axe.jpg",
                    Rarity.Common
                ),
                (
                    "The Bronze Age",
                    "The Minoan Seal",
                    "A carved stone seal from the palace of Knossos — used to mark trade goods across the Bronze Age Mediterranean. Awarded to the most precise scholars.",
                    "/images/collectibles/minoan-seal.jpg",
                    Rarity.Rare
                ),

                // ── Ancient Civilizations ──────────────────────────────────
                (
                    "The Rise of Egypt",
                    "Scarab of the Pharaoh",
                    "The sacred scarab — symbol of resurrection and the rising sun, worn by pharaohs as protection in life and death.",
                    "/images/collectibles/scarab.jpg",
                    Rarity.Common
                ),
                (
                    "The Rise of Egypt",
                    "The Eye of Ra",
                    "The all-seeing eye of the sun god — bestowed upon those whose knowledge of ancient Egypt is without flaw.",
                    "/images/collectibles/eye-of-ra.jpg",
                    Rarity.Rare
                ),
                (
                    "Mesopotamia and the First Cities",
                    "The Clay Tablet",
                    "A cuneiform clay tablet from ancient Sumer — one of the earliest writing instruments in human history, earned by those who learned the story of the first cities.",
                    "/images/collectibles/clay-tablet.jpg",
                    Rarity.Common
                ),
                (
                    "Mesopotamia and the First Cities",
                    "The Lapis Cylinder",
                    "A lapis lazuli cylinder seal from Ur, circa 2500 BCE — used to mark royal documents. Awarded only to those who mastered every detail of the ancient world.",
                    "/images/collectibles/lapis-cylinder.jpg",
                    Rarity.Rare
                ),
                (
                    "The Greek Golden Age",
                    "The Athenian Owl Coin",
                    "A silver tetradrachm bearing the owl of Athena — the currency of the Athenian Empire, earned by those who understand the birth of democracy.",
                    "/images/collectibles/athenian-coin.jpg",
                    Rarity.Common
                ),
                (
                    "The Greek Golden Age",
                    "The Hemlock Cup",
                    "The cup from which Socrates drank hemlock — a symbol of intellectual integrity against political power. Given only to those who answered every question without error.",
                    "/images/collectibles/hemlock-cup.jpg",
                    Rarity.Rare
                ),
                (
                    "The Roman Republic",
                    "The Denarius",
                    "A silver denarius bearing the face of Julius Caesar — the most powerful man Rome ever produced, earned by those who walked the trail of the Republic.",
                    "/images/collectibles/denarius.jpg",
                    Rarity.Common
                ),
                (
                    "The Roman Republic",
                    "The Senate Ring",
                    "A gold senatorial ring from the late Republic — worn by those who held power over the greatest empire the world had seen. Awarded to perfect scholars.",
                    "/images/collectibles/senate-ring.jpg",
                    Rarity.Rare
                ),
                (
                    "The Persian Empire",
                    "The Daric Coin",
                    "A gold daric — the standard coin of the Persian Empire, bearing the image of the Great King, earned by those who understood the first great empire.",
                    "/images/collectibles/daric-coin.jpg",
                    Rarity.Common
                ),
                (
                    "The Persian Empire",
                    "The Cyrus Cylinder Fragment",
                    "A fragment of the Cyrus Cylinder — the world's first human rights charter, issued by the liberator of Babylon. Given only to those with perfect knowledge.",
                    "/images/collectibles/cyrus-fragment.jpg",
                    Rarity.Rare
                ),

                // ── Medieval ───────────────────────────────────────────────
                (
                    "The Black Death",
                    "The Plague Doctor's Mask",
                    "The beak-shaped mask worn by medieval physicians — a haunting symbol of humanity's darkest hour and its will to survive.",
                    "/images/collectibles/plague-mask.jpg",
                    Rarity.Common
                ),
                (
                    "The Black Death",
                    "The Survivor's Ring",
                    "A simple iron ring worn by one who lived through the Black Death — awarded to those who faced every question and did not falter.",
                    "/images/collectibles/survivors-ring.jpg",
                    Rarity.Rare
                ),
                (
                    "The Fall of Rome",
                    "The Broken Eagle",
                    "A fractured bronze Roman eagle standard — symbol of an empire that fell not in battle but in slow exhaustion. Earned by those who understood its end.",
                    "/images/collectibles/broken-eagle.jpg",
                    Rarity.Common
                ),
                (
                    "The Fall of Rome",
                    "The Last Aureus",
                    "A gold aureus of Romulus Augustulus — the last coin minted by the last Western Roman Emperor. Awarded only to those with complete mastery.",
                    "/images/collectibles/last-aureus.jpg",
                    Rarity.Rare
                ),
                (
                    "The Crusades",
                    "The Crusader Cross",
                    "A pilgrim's cross from the First Crusade — worn by those who marched to Jerusalem believing God willed it. Earned by students of the holy wars.",
                    "/images/collectibles/crusader-cross.jpg",
                    Rarity.Common
                ),
                (
                    "The Crusades",
                    "Saladin's Signet",
                    "The signet ring of Saladin, Sultan of Egypt and Syria — who retook Jerusalem with mercy where the Crusaders had used slaughter. For perfect scholars only.",
                    "/images/collectibles/saladin-signet.jpg",
                    Rarity.Rare
                ),
                (
                    "Magna Carta and the Birth of Rights",
                    "The Wax Seal",
                    "A wax seal of King John of England — pressed onto Magna Carta at Runnymede in 1215, the moment a king first bowed to the law.",
                    "/images/collectibles/wax-seal.jpg",
                    Rarity.Common
                ),
                (
                    "Magna Carta and the Birth of Rights",
                    "The Runnymede Parchment",
                    "A fragment of parchment from the age of Magna Carta — the material on which human rights were first written. Awarded to those who missed nothing.",
                    "/images/collectibles/runnymede-parchment.jpg",
                    Rarity.Rare
                ),
                (
                    "The Mongol Empire",
                    "The Mongol Arrowhead",
                    "A barbed Mongol arrowhead — the weapon that won the largest land empire in history, earned by those who studied the great Khan's conquests.",
                    "/images/collectibles/mongol-arrowhead.jpg",
                    Rarity.Common
                ),
                (
                    "The Mongol Empire",
                    "The Khan's Seal",
                    "The personal seal of Genghis Khan — whose name alone caused cities to surrender. Awarded to those whose mastery of the Mongol era is absolute.",
                    "/images/collectibles/khans-seal.jpg",
                    Rarity.Rare
                ),

                // ── Renaissance ────────────────────────────────────────────
                (
                    "Leonardo da Vinci",
                    "The Vitruvian Coin",
                    "A golden coin bearing the Vitruvian Man — awarded to those who think beyond the boundaries of a single discipline.",
                    "/images/collectibles/vitruvian-coin.jpg",
                    Rarity.Common
                ),
                (
                    "Leonardo da Vinci",
                    "Leonardo's Quill",
                    "The quill of the great master himself — given only to those whose mastery of his life and work is complete and without error.",
                    "/images/collectibles/leonardos-quill.jpg",
                    Rarity.Rare
                ),
                (
                    "The Printing Press",
                    "The Gutenberg Type",
                    "A piece of movable metal type from a 15th century printing press — one letter of the revolution that democratised knowledge.",
                    "/images/collectibles/gutenberg-type.jpg",
                    Rarity.Common
                ),
                (
                    "The Printing Press",
                    "The First Bible Page",
                    "A page from the Gutenberg Bible — the first book printed with movable type, the object that began the information age. For perfect scholars only.",
                    "/images/collectibles/bible-page.jpg",
                    Rarity.Rare
                ),
                (
                    "Michelangelo and the Sistine Chapel",
                    "The Painter's Brush",
                    "A brush of the kind used by Michelangelo on the Sistine ceiling — four years of solitary genius, earned by those who understand his achievement.",
                    "/images/collectibles/painters-brush.jpg",
                    Rarity.Common
                ),
                (
                    "Michelangelo and the Sistine Chapel",
                    "The Marble Fragment",
                    "A fragment of Carrara marble — the stone from which Michelangelo carved David and the Pietà. Awarded to those who answered every question without error.",
                    "/images/collectibles/marble-fragment.jpg",
                    Rarity.Rare
                ),
                (
                    "Copernicus and the Scientific Revolution",
                    "The Astrolabe",
                    "A brass astrolabe — the instrument that navigators and astronomers used to measure the heavens before the telescope existed.",
                    "/images/collectibles/astrolabe.jpg",
                    Rarity.Common
                ),
                (
                    "Copernicus and the Scientific Revolution",
                    "The Newton Prism",
                    "A glass prism of the kind Isaac Newton used to split white light into its spectrum — awarded to those who mastered the Scientific Revolution.",
                    "/images/collectibles/newton-prism.jpg",
                    Rarity.Rare
                ),
                (
                    "The Protestant Reformation",
                    "Luther's Seal",
                    "Martin Luther's personal seal — the Luther Rose, combining a cross, heart, rose and ring. Earned by students of the Reformation's origins.",
                    "/images/collectibles/luther-seal.jpg",
                    Rarity.Common
                ),
                (
                    "The Protestant Reformation",
                    "The Wittenberg Door Nail",
                    "A nail from the door of the Castle Church in Wittenberg — where Luther's 95 Theses were posted on 31 October 1517. For perfect scholars only.",
                    "/images/collectibles/wittenberg-nail.jpg",
                    Rarity.Rare
                ),

                // ── Age of Exploration ─────────────────────────────────────
                (
                    "The Voyages of Columbus",
                    "The Navigator's Compass",
                    "A 15th century mariner's compass — the instrument that guided Columbus across the Atlantic into the unknown. Earned by those who followed his trail.",
                    "/images/collectibles/navigator-compass.jpg",
                    Rarity.Common
                ),
                (
                    "The Voyages of Columbus",
                    "The Taíno Gold Mask",
                    "A small gold mask of the Taíno people — the first inhabitants Columbus encountered. A reminder of what was lost. Awarded to those with perfect knowledge.",
                    "/images/collectibles/taino-mask.jpg",
                    Rarity.Rare
                ),
                (
                    "Vasco da Gama and the Route to India",
                    "The Spice Jar",
                    "A Portuguese ceramic spice jar from the Age of Exploration — the object that motivated the search for the sea route to India.",
                    "/images/collectibles/spice-jar.jpg",
                    Rarity.Common
                ),
                (
                    "Vasco da Gama and the Route to India",
                    "The Cape of Good Hope Stone",
                    "A piece of granite from the Cape of Good Hope — the headland that stood between Europe and the riches of Asia. For those who missed nothing.",
                    "/images/collectibles/cape-stone.jpg",
                    Rarity.Rare
                ),
                (
                    "Magellan's Circumnavigation",
                    "The Magellan Astrolabe",
                    "A brass navigational astrolabe of the type carried by Magellan's fleet — the instrument that guided the first circumnavigation of the Earth.",
                    "/images/collectibles/magellan-astrolabe.jpg",
                    Rarity.Common
                ),
                (
                    "Magellan's Circumnavigation",
                    "The Victoria's Bell",
                    "The ship's bell of the Victoria — the sole vessel to complete Magellan's circumnavigation, returning with 18 survivors from 270. For perfect scholars.",
                    "/images/collectibles/victoria-bell.jpg",
                    Rarity.Rare
                ),
                (
                    "The Conquest of the Americas",
                    "The Conquistador Helmet",
                    "A Spanish morion helmet — the distinctive headgear of the conquistadors who toppled two great empires with a handful of soldiers.",
                    "/images/collectibles/conquistador-helmet.jpg",
                    Rarity.Common
                ),
                (
                    "The Conquest of the Americas",
                    "The Aztec Sun Stone Fragment",
                    "A fragment carved in the style of the Aztec Sun Stone — the calendar of a civilisation destroyed within a generation of first contact. For perfect scholars.",
                    "/images/collectibles/sun-stone-fragment.jpg",
                    Rarity.Rare
                ),
                (
                    "The Columbian Exchange",
                    "The Potato",
                    "A preserved potato from the New World — the crop that fed Europe, ended famines and changed the course of history. Earned by all who completed this chapter.",
                    "/images/collectibles/the-potato.jpg",
                    Rarity.Common
                ),
                (
                    "The Columbian Exchange",
                    "The Cacao Pod",
                    "A gilded cacao pod — chocolate was the currency and sacred drink of the Aztecs before Columbus carried it to Europe. Awarded to those with perfect mastery.",
                    "/images/collectibles/cacao-pod.jpg",
                    Rarity.Rare
                ),

                // ── Modern History ─────────────────────────────────────────
                (
                    "The First World War",
                    "The Poppy Medal",
                    "A red poppy cast in bronze — symbol of remembrance for the millions who fell on the fields of the Great War.",
                    "/images/collectibles/poppy-medal.jpg",
                    Rarity.Common
                ),
                (
                    "The First World War",
                    "The Trench Watch",
                    "A soldier's pocket watch, stopped at 11:00 AM on November 11, 1918 — the moment the guns fell silent. Earned by those who missed nothing.",
                    "/images/collectibles/trench-watch.jpg",
                    Rarity.Rare
                ),
                (
                    "The French Revolution",
                    "The Phrygian Cap",
                    "A red Phrygian cap — the symbol of liberty worn by French revolutionaries who stormed the Bastille and remade the world.",
                    "/images/collectibles/phrygian-cap.jpg",
                    Rarity.Common
                ),
                (
                    "The French Revolution",
                    "The Tricolour Cockade",
                    "The blue, white and red cockade worn by revolutionaries in 1789 — the emblem of liberty, equality and fraternity. Awarded to perfect scholars.",
                    "/images/collectibles/tricolour-cockade.jpg",
                    Rarity.Rare
                ),
                (
                    "The Industrial Revolution",
                    "The Steam Valve",
                    "A brass pressure valve from a 19th century steam engine — the component that made the Industrial Revolution safe enough to be useful.",
                    "/images/collectibles/steam-valve.jpg",
                    Rarity.Common
                ),
                (
                    "The Industrial Revolution",
                    "The Spinning Jenny Spindle",
                    "A spindle from a Spinning Jenny — James Hargreaves' invention that began the mechanisation of textile production and the Industrial Revolution.",
                    "/images/collectibles/spinning-spindle.jpg",
                    Rarity.Rare
                ),
                (
                    "The Second World War",
                    "The Dog Tag",
                    "A soldier's identification tag from the Second World War — worn by those who fought in the deadliest conflict in human history.",
                    "/images/collectibles/dog-tag.jpg",
                    Rarity.Common
                ),
                (
                    "The Second World War",
                    "The Enigma Rotor",
                    "A rotor from a German Enigma machine — the cipher device that Alan Turing cracked, shortening the war by an estimated two years. For perfect scholars.",
                    "/images/collectibles/enigma-rotor.jpg",
                    Rarity.Rare
                ),
                (
                    "The Cold War",
                    "The Berlin Wall Fragment",
                    "A piece of the Berlin Wall — the concrete barrier that divided a city, a nation and a continent for 28 years.",
                    "/images/collectibles/wall-fragment.jpg",
                    Rarity.Common
                ),
                (
                    "The Cold War",
                    "The Sputnik Antenna",
                    "A replica of Sputnik's radio antenna — the beep it transmitted on 4 October 1957 shook the world and started the Space Race. For perfect scholars.",
                    "/images/collectibles/sputnik-antenna.jpg",
                    Rarity.Rare
                ),

                // ── Digital Age ────────────────────────────────────────────
                (
                    "The Birth of the Internet",
                    "The First Packet",
                    "A crystallised data packet — commemorating the two letters 'LO' that began the greatest communication revolution in history.",
                    "/images/collectibles/data-packet.jpg",
                    Rarity.Common
                ),
                (
                    "The Birth of the Internet",
                    "The Golden Circuit",
                    "A circuit board cast entirely in gold — awarded to those who understood every facet of the digital revolution that connected the world.",
                    "/images/collectibles/golden-circuit.jpg",
                    Rarity.Rare
                ),
                (
                    "The Personal Computer Revolution",
                    "The Floppy Disk",
                    "A 5.25-inch floppy disk — the storage medium that carried the first personal computer software and launched the digital age.",
                    "/images/collectibles/floppy-disk.jpg",
                    Rarity.Common
                ),
                (
                    "The Personal Computer Revolution",
                    "The Apple Rainbow Chip",
                    "A microchip bearing the Apple rainbow logo — commemorating the company that made computing personal. Awarded to perfect scholars of the PC revolution.",
                    "/images/collectibles/apple-chip.jpg",
                    Rarity.Rare
                ),
                (
                    "The Space Race",
                    "The Lunar Dust Vial",
                    "A sealed vial containing a sample of simulated lunar regolith — commemorating the twelve men who walked on the Moon between 1969 and 1972.",
                    "/images/collectibles/lunar-dust.jpg",
                    Rarity.Common
                ),
                (
                    "The Space Race",
                    "The Mission Patch",
                    "The embroidered mission patch of Apollo 11 — worn by Armstrong, Aldrin and Collins on the greatest voyage in human history. For perfect scholars only.",
                    "/images/collectibles/apollo-patch.jpg",
                    Rarity.Rare
                ),
                (
                    "The Human Genome Project",
                    "The DNA Helix",
                    "A crystalline model of the DNA double helix — the molecule of life, fully sequenced by humanity in 2003 after thirteen years of work.",
                    "/images/collectibles/dna-helix.jpg",
                    Rarity.Common
                ),
                (
                    "The Human Genome Project",
                    "The CRISPR Scissors",
                    "A golden model of CRISPR-Cas9 molecular scissors — the gene-editing tool that allows humanity to rewrite the code of life. For perfect scholars.",
                    "/images/collectibles/crispr-scissors.jpg",
                    Rarity.Rare
                ),
                (
                    "Artificial Intelligence",
                    "The Neural Network Node",
                    "A glowing node from a neural network diagram — the basic unit of the artificial intelligence systems that are reshaping human civilisation.",
                    "/images/collectibles/neural-node.jpg",
                    Rarity.Common
                ),
                (
                    "Artificial Intelligence",
                    "The Turing Medal",
                    "A medal bearing Alan Turing's portrait — awarded to those who mastered the history of the thinking machine. Given only to those with perfect knowledge.",
                    "/images/collectibles/turing-medal.jpg",
                    Rarity.Rare
                ),
            };

            foreach (var c in chapterCollectibles)
            {
                int chapterId = await context.Chapters
                    .Where(ch => ch.Title == c.ChapterTitle)
                    .Select(ch => ch.Id)
                    .FirstOrDefaultAsync();

                if (chapterId == 0) continue;

                context.Collectibles.Add(new Collectible
                {
                    Name = c.Name,
                    Description = c.Description,
                    ArtworkUrl = c.ArtworkUrl,
                    Rarity = c.Rarity,
                    ChapterId = chapterId,
                    EraId = null
                });
            }

            await context.SaveChangesAsync();

            // ── Legendary — linked to eras ──────────────────────────────────
            List<(string EraName, string Name, string Description, string ArtworkUrl)> legendaryCollectibles = new()
            {
                (
                    "Prehistoric",
                    "Crown of the Ancient World",
                    "Forged before time was measured — a crown worn by no king, claimed only by those who mastered every chapter of Earth's first chapter.",
                    "/images/collectibles/legendary-prehistoric.jpg"
                ),
                (
                    "Ancient Civilizations",
                    "The Pharaoh's Cartouche",
                    "A golden cartouche bearing the name of the greatest explorer of antiquity. Inscribed only for those who walked every trail of the ancient world.",
                    "/images/collectibles/legendary-ancient.jpg"
                ),
                (
                    "Medieval",
                    "The Grandmaster's Seal",
                    "The wax seal of a medieval grandmaster — pressed once, in recognition of complete mastery over the age of faith and iron.",
                    "/images/collectibles/legendary-medieval.jpg"
                ),
                (
                    "Renaissance",
                    "The Codex Medallion",
                    "A medallion engraved with the first page of Leonardo's codex — bestowed upon those who mastered the era of humanity's greatest reawakening.",
                    "/images/collectibles/legendary-renaissance.jpg"
                ),
                (
                    "Age of Exploration",
                    "The Admiral's Astrolabe",
                    "A solid gold astrolabe — the instrument of the great admirals who sailed beyond the edge of the known world. Earned by those who mastered every chapter of the Age of Exploration.",
                    "/images/collectibles/legendary-exploration.jpg"
                ),
                (
                    "Modern History",
                    "The Iron Century",
                    "A sphere of polished iron containing echoes of two world wars — awarded only to those who comprehended the full weight of the modern age.",
                    "/images/collectibles/legendary-modern.jpg"
                ),
                (
                    "Digital Age",
                    "The Infinite Node",
                    "A luminous crystalline node representing every connection ever made across the internet — earned by those who mastered the age that linked the world.",
                    "/images/collectibles/legendary-digital.jpg"
                ),
            };

            foreach (var l in legendaryCollectibles)
            {
                int eraId = await context.Eras
                    .Where(e => e.Name == l.EraName)
                    .Select(e => e.Id)
                    .FirstOrDefaultAsync();

                if (eraId == 0) continue;

                context.Collectibles.Add(new Collectible
                {
                    Name = l.Name,
                    Description = l.Description,
                    ArtworkUrl = l.ArtworkUrl,
                    Rarity = Rarity.Legendary,
                    ChapterId = null,
                    EraId = eraId
                });
            }

            await context.SaveChangesAsync();
        }
    }
}