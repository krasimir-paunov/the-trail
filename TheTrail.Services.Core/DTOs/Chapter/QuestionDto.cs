namespace TheTrail.Services.Core.DTOs.Chapter
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required string OptionA { get; set; }
        public required string OptionB { get; set; }
        public required string OptionC { get; set; }
        public required string OptionD { get; set; }
        public int Order { get; set; }
    }
}