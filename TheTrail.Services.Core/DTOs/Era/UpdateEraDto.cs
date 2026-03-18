using System.ComponentModel.DataAnnotations;
using TheTrail.Domain.Common;

namespace TheTrail.Services.Core.DTOs.Era
{
    public class UpdateEraDto
    {
        [Required]
        [MaxLength(ValidationConstants.Era.NameMaxLength)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.Era.DescriptionMaxLength)]
        public required string Description { get; set; }

        [Url]
        [MaxLength(ValidationConstants.Era.CoverImageUrlMaxLength)]
        public string? CoverImageUrl { get; set; }

        [MaxLength(ValidationConstants.Era.ColorThemeMaxLength)]
        public string? ColorTheme { get; set; }

        public int Order { get; set; }

        public bool IsPublished { get; set; }
    }
}