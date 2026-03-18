using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class UserCollectibleConfiguration : IEntityTypeConfiguration<UserCollectible>
    {
        public void Configure(EntityTypeBuilder<UserCollectible> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CollectibleId });

            builder.Property(uc => uc.EarnedOn)
                .IsRequired();

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserCollectibles)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uc => uc.Collectible)
                .WithMany(c => c.UserCollectibles)
                .HasForeignKey(uc => uc.CollectibleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}