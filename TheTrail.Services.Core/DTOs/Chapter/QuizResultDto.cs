namespace TheTrail.Services.Core.DTOs.Chapter
{
    public class QuizResultDto
    {
        public bool Passed { get; set; }
        public bool PerfectScore { get; set; }
        public bool LegendaryAwarded { get; set; }
        public string? LegendaryName { get; set; }
        public string? LegendaryDescription { get; set; }
        public string? LegendaryImageUrl { get; set; }
        public string? EraName { get; set; }
    }
}