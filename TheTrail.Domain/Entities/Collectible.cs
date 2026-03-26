using TheTrail.Domain.Enums;
namespace TheTrail.Domain.Entities
{
    public class Collectible
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ArtworkUrl { get; set; }
        public Rarity Rarity { get; set; }
        public int? ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
        public int? EraId { get; set; }
        public Era? Era { get; set; }
        public ICollection<UserCollectible> UserCollectibles { get; set; } = new HashSet<UserCollectible>();
    }
}