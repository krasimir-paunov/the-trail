using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Common;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.DisplayName)
                .IsRequired()
                .HasMaxLength(ValidationConstants.User.DisplayNameMaxLength);

            builder.Property(u => u.AvatarUrl)
                .HasMaxLength(ValidationConstants.User.AvatarUrlMaxLength);

            builder.Property(u => u.CreatedOn)
                .IsRequired();
        }
    }
}