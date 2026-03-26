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

            List<(string ChapterTitle, string Name, string Description, string ArtworkUrl, Rarity Rarity)> collectibles = new()
            {
                // ── Prehistoric ────────────────────────────────────────────────
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

                // ── Ancient Civilizations ──────────────────────────────────────
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

                // ── Medieval ───────────────────────────────────────────────────
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

                // ── Renaissance ────────────────────────────────────────────────
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

                // ── Modern History ─────────────────────────────────────────────
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

                // ── Digital Age ────────────────────────────────────────────────
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
                    "A circuit board cast in gold — awarded to those who understood every facet of the digital revolution that connected the world.",
                    "/images/collectibles/golden-circuit.jpg",
                    Rarity.Rare
                ),
            };

            foreach ((string ChapterTitle, string Name, string Description, string ArtworkUrl, Rarity Rarity) c in collectibles)
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
                });
            }

            await context.SaveChangesAsync();
        }
    }
}