namespace TheTrail.Services.Core.DTOs.Profile
{
    public class ProfileDto
    {
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public int ChaptersRead { get; set; }
        public int QuizzesPassed { get; set; }
        public IEnumerable<EraProgressDto> EraProgress { get; set; } = new List<EraProgressDto>();
        public IEnumerable<CollectibleDto> EarnedCollectibles { get; set; } = new List<CollectibleDto>();
        public IEnumerable<CollectibleDto> AllCollectibles { get; set; } = new List<CollectibleDto>();
    }

    public class EraProgressDto
    {
        public int EraId { get; set; }
        public required string EraName { get; set; }
        public required string ColorTheme { get; set; }
        public int TotalChapters { get; set; }
        public int CompletedChapters { get; set; }
        public bool IsGrandmasterUnlocked { get; set; }
    }

    public class CollectibleDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public string Rarity { get; set; } = "Common";
        public bool IsEarned { get; set; }
        public int ChapterId { get; set; }
    }
}