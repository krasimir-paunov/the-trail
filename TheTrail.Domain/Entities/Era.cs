namespace TheTrail.Domain.Entities
{
    public class Era
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public required string ColorTheme { get; set; }
        public int Order { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Chapter> Chapters { get; set; } = new HashSet<Chapter>();
        public ICollection<Collectible> Collectibles { get; set; } = new HashSet<Collectible>();
    }
}