namespace TheTrail.Domain.Entities
{
    public class Chapter
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Subtitle { get; set; }

        public required string Content { get; set; }

        public string? CoverImageUrl { get; set; }

        public int Order { get; set; }

        public int EstimatedMinutes { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreatedOn { get; set; }

        public int EraId { get; set; }

        public Era Era { get; set; } = null!;

        public Quiz? Quiz { get; set; }

        public Collectible? Collectible { get; set; }

        public ICollection<UserChapterProgress> UserProgresses { get; set; } = new HashSet<UserChapterProgress>();
    }
}