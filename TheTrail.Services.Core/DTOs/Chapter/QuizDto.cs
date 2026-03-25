namespace TheTrail.Services.Core.DTOs.Chapter
{
    public class QuizDto
    {
        public int Id { get; set; }
        public int PassMarkPercent { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}