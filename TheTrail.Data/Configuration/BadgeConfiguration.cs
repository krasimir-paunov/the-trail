using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Common;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Badge.NameMaxLength);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Badge.DescriptionMaxLength);

            builder.Property(b => b.ArtworkUrl)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Badge.ArtworkUrlMaxLength);

            builder.Property(b => b.Rarity)
                .IsRequired()
                .HasConversion<string>();

            builder.HasMany(b => b.UserBadges)
                .WithOne(ub => ub.Badge)
                .HasForeignKey(ub => ub.BadgeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}