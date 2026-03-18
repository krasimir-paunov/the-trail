namespace TheTrail.Services.Core.DTOs.Auth
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}