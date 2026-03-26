using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Common;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Chapter.TitleMaxLength);

            builder.Property(c => c.Subtitle)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Chapter.SubtitleMaxLength);

            builder.Property(c => c.Content)
                .IsRequired();

            builder.Property(c => c.CoverImageUrl)
                .HasMaxLength(ValidationConstants.Chapter.CoverImageUrlMaxLength);

            builder.HasOne(c => c.Quiz)
                .WithOne(q => q.Chapter)
                .HasForeignKey<Quiz>(q => q.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Collectibles)
                .WithOne(col => col.Chapter)
                .HasForeignKey(col => col.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.UserProgresses)
                .WithOne(up => up.Chapter)
                .HasForeignKey(up => up.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}