using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheTrail.Domain.Common;
using TheTrail.Domain.Entities;

namespace TheTrail.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Text)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Question.TextMaxLength);

            builder.Property(q => q.OptionA)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Question.OptionMaxLength);

            builder.Property(q => q.OptionB)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Question.OptionMaxLength);

            builder.Property(q => q.OptionC)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Question.OptionMaxLength);

            builder.Property(q => q.OptionD)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Question.OptionMaxLength);

            builder.Property(q => q.CorrectOption)
                .IsRequired()
                .HasMaxLength(ValidationConstants.Question.OptionMaxLength);
        }
    }
}