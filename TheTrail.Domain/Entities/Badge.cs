using TheTrail.Domain.Enums;

namespace TheTrail.Domain.Entities
{
    public class Badge
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string ArtworkUrl { get; set; }

        public Rarity Rarity { get; set; }

        public int EraId { get; set; }

        public Era Era { get; set; } = null!;

        public ICollection<UserBadge> UserBadges { get; set; } = new HashSet<UserBadge>();
    }
}