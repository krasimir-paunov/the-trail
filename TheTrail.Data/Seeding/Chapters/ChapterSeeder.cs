using TheTrail.Data.Seeding.Chapters;

namespace TheTrail.Data.Seeding.Chapters
{
    public static class ChapterSeeder
    {
        public static async Task SeedAsync(TheTrailDbContext context)
        {
            await PrehistoricChapterSeeder.SeedAsync(context);
            await AncientChapterSeeder.SeedAsync(context);
            await MedievalChapterSeeder.SeedAsync(context);
            await RenaissanceChapterSeeder.SeedAsync(context);
            await ModernChapterSeeder.SeedAsync(context);
            await DigitalChapterSeeder.SeedAsync(context);
        }
    }
}