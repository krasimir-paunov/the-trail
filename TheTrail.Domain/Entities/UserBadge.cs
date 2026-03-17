namespace TheTrail.Domain.Entities
{
    public class UserBadge
    {
        public required string UserId { get; set; }

        public int BadgeId { get; set; }

        public DateTime EarnedOn { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public Badge Badge { get; set; } = null!;
    }
}