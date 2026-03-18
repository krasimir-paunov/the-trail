using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheTrail.Domain.Entities;

namespace TheTrail.Data
{
    public class TheTrailDbContext : IdentityDbContext<ApplicationUser>
    {
        public TheTrailDbContext(DbContextOptions<TheTrailDbContext> options)
            : base(options)
        {

        }

        public required DbSet<Era> Eras { get; set; }
        public required DbSet<Chapter> Chapters { get; set; }
        public required DbSet<Quiz> Quizzes { get; set; }
        public required DbSet<Question> Questions { get; set; }
        public required DbSet<Collectible> Collectibles { get; set; }
        public required DbSet<Badge> Badges { get; set; }
        public required DbSet<UserChapterProgress> UserChapterProgresses { get; set; }
        public required DbSet<UserCollectible> UserCollectibles { get; set; }
        public required DbSet<UserBadge> UserBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(TheTrailDbContext).Assembly);
        }
    }
}