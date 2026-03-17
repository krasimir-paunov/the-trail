namespace TheTrail.Domain.Entities
{
    public class UserCollectible
    {
        public required string UserId { get; set; }

        public int CollectibleId { get; set; }

        public DateTime EarnedOn { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public Collectible Collectible { get; set; } = null!;
    }
}