namespace TheTrail.Services.Core.DTOs.Chapter
{
    public class CheckAnswerDto
    {
        public int QuestionId { get; set; }
        public required string Answer { get; set; }
    }
}