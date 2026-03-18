using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class UserChapterProgressConfiguration : IEntityTypeConfiguration<UserChapterProgress>
    {
        public void Configure(EntityTypeBuilder<UserChapterProgress> builder)
        {
            builder.HasKey(ucp => new { ucp.UserId, ucp.ChapterId });

            builder.Property(ucp => ucp.QuizScore)
                .HasDefaultValue(0);

            builder.Property(ucp => ucp.ScrollCompleted)
                .HasDefaultValue(false);

            builder.Property(ucp => ucp.QuizPassed)
                .HasDefaultValue(false);

            builder.HasOne(ucp => ucp.User)
                .WithMany(u => u.ChapterProgresses)
                .HasForeignKey(ucp => ucp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ucp => ucp.Chapter)
                .WithMany(c => c.UserProgresses)
                .HasForeignKey(ucp => ucp.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}