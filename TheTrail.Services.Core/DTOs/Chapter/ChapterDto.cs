using TheTrail.Domain.Enums;

namespace TheTrail.Services.Core.DTOs.Chapter
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Subtitle { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Order { get; set; }
        public int EstimatedMinutes { get; set; }
        public int EraId { get; set; }
        public bool ScrollCompleted { get; set; }
        public bool QuizPassed { get; set; }
        public bool HasQuiz { get; set; }
        public bool HasCollectible { get; set; }
        public Rarity? CollectibleRarity { get; set; }
    }
}