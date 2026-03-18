using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Common;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class EraConfiguration : IEntityTypeConfiguration<Era>
    {
        public void Configure(EntityTypeBuilder<Era> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Era.NameMaxLength);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Era.DescriptionMaxLength);

            builder.Property(e => e.CoverImageUrl)
                .HasMaxLength(ValidationConstants.Era.CoverImageUrlMaxLength);

            builder.Property(e => e.ColorTheme)
                .HasMaxLength(ValidationConstants.Era.ColorThemeMaxLength);

            builder.HasMany(e => e.Chapters)
                .WithOne(c => c.Era)
                .HasForeignKey(c => c.EraId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}