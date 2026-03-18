using System.ComponentModel.DataAnnotations;
using TheTrail.Domain.Common;

namespace TheTrail.Services.Core.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(ValidationConstants.User.DisplayNameMaxLength)]
        public required string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public required string ConfirmPassword { get; set; }
    }
}