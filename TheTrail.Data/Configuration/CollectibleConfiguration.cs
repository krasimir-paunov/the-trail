using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Common;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class CollectibleConfiguration : IEntityTypeConfiguration<Collectible>
    {
        public void Configure(EntityTypeBuilder<Collectible> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Collectible.NameMaxLength);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Collectible.DescriptionMaxLength);

            builder.Property(c => c.ArtworkUrl)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Collectible.ArtworkUrlMaxLength);

            builder.Property(c => c.Rarity)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(c => c.Chapter)
                .WithMany(ch => ch.Collectibles)
                .HasForeignKey(c => c.ChapterId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);


            builder.HasOne(c => c.Era)
                .WithMany(e => e.Collectibles)
                .HasForeignKey(c => c.EraId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(c => c.UserCollectibles)
                .WithOne(uc => uc.Collectible)
                .HasForeignKey(uc => uc.CollectibleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}