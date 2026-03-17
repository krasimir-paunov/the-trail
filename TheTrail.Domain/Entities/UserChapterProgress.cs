namespace TheTrail.Domain.Entities
{
    public class UserChapterProgress
    {
        public required string UserId { get; set; }

        public int ChapterId { get; set; }

        public bool ScrollCompleted { get; set; }

        public bool QuizPassed { get; set; }

        public int QuizScore { get; set; }

        public DateTime? CompletedOn { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public Chapter Chapter { get; set; } = null!;
    }
}