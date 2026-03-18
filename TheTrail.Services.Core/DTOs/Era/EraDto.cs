namespace TheTrail.Services.Core.DTOs.Era
{
    public class EraDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? ColorTheme { get; set; }
        public int Order { get; set; }
        public int ChapterCount { get; set; }
        public int CompletedChapterCount { get; set; }
        public bool IsGrandmasterUnlocked { get; set; }
    }
}