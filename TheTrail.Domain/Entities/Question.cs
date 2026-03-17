namespace TheTrail.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public required string Text { get; set; }

        public required string OptionA { get; set; }

        public required string OptionB { get; set; }

        public required string OptionC { get; set; }

        public required string OptionD { get; set; }

        public required string CorrectOption { get; set; }

        public int Order { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; } = null!;
    }
}