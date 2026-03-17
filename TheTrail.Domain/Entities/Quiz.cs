namespace TheTrail.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }

        public int PassMarkPercent { get; set; }

        public int ChapterId { get; set; }

        public Chapter Chapter { get; set; } = null!;

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}