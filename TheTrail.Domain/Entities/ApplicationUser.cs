using Microsoft.AspNetCore.Identity;

namespace TheTrail.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string DisplayName { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<UserChapterProgress> ChapterProgresses { get; set; } = new HashSet<UserChapterProgress>();

        public ICollection<UserCollectible> UserCollectibles { get; set; } = new HashSet<UserCollectible>();

        public ICollection<UserBadge> UserBadges { get; set; } = new HashSet<UserBadge>();
    }
}