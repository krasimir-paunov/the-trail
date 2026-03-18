using System.ComponentModel.DataAnnotations;
using TheTrail.Domain.Common;

namespace TheTrail.Services.Core.DTOs.Chapter
{
    public class CreateChapterDto
    {
        [Required]
        [MaxLength(ValidationConstants.Chapter.TitleMaxLength)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(ValidationConstants.Chapter.SubtitleMaxLength)]
        public required string Subtitle { get; set; }

        [Required]
        public required string Content { get; set; }

        [Url]
        [MaxLength(ValidationConstants.Chapter.CoverImageUrlMaxLength)]
        public string? CoverImageUrl { get; set; }

        public int Order { get; set; }

        [Range(ValidationConstants.Chapter.EstimatedMinutesMin,
               ValidationConstants.Chapter.EstimatedMinutesMax)]
        public int EstimatedMinutes { get; set; }

        [Required]
        public int EraId { get; set; }
    }
}