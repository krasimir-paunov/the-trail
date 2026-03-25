using Microsoft.EntityFrameworkCore;
using TheTrail.Data.Interfaces;
using TheTrail.Domain.Entities;
using TheTrail.Services.Core.DTOs.Profile;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<UserChapterProgress> _progressRepository;
        private readonly IRepository<Era> _eraRepository;
        private readonly IRepository<Collectible> _collectibleRepository;
        private readonly IRepository<UserCollectible> _userCollectibleRepository;

        public ProfileService(
            IRepository<ApplicationUser> userRepository,
            IRepository<UserChapterProgress> progressRepository,
            IRepository<Era> eraRepository,
            IRepository<Collectible> collectibleRepository,
            IRepository<UserCollectible> userCollectibleRepository)
        {
            _userRepository = userRepository;
            _progressRepository = progressRepository;
            _eraRepository = eraRepository;
            _collectibleRepository = collectibleRepository;
            _userCollectibleRepository = userCollectibleRepository;
        }

        public async Task<ProfileDto?> GetProfileAsync(string userId)
        {
            ApplicationUser? user = await _userRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            List<UserChapterProgress> progress = await _progressRepository
                .AllAsNoTracking()
                .Where(p => p.UserId == userId)
                .ToListAsync();

            List<Era> eras = await _eraRepository
                .AllAsNoTracking()
                .Include(e => e.Chapters)
                .OrderBy(e => e.Order)
                .ToListAsync();

            List<Collectible> allCollectibles = await _collectibleRepository
                .AllAsNoTracking()
                .ToListAsync();

            List<UserCollectible> earnedCollectibles = await _userCollectibleRepository
                .AllAsNoTracking()
                .Where(uc => uc.UserId == userId)
                .ToListAsync();

            HashSet<int> earnedIds = earnedCollectibles.Select(uc => uc.CollectibleId).ToHashSet();

            List<EraProgressDto> eraProgress = eras.Select(era => {
                int total = era.Chapters.Count;
                int completed = era.Chapters
                    .Count(c => progress.Any(p => p.ChapterId == c.Id && p.QuizPassed));
                return new EraProgressDto
                {
                    EraId = era.Id,
                    EraName = era.Name,
                    ColorTheme = era.ColorTheme ?? "prehistoric",
                    TotalChapters = total,
                    CompletedChapters = completed,
                    IsGrandmasterUnlocked = total > 0 && completed == total
                };
            }).ToList();

            return new ProfileDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email ?? string.Empty,
                ChaptersRead = progress.Count(p => p.ScrollCompleted),
                QuizzesPassed = progress.Count(p => p.QuizPassed),
                EraProgress = eraProgress,
                EarnedCollectibles = allCollectibles
                    .Where(c => earnedIds.Contains(c.Id))
                    .Select(c => MapCollectible(c, true)),
                AllCollectibles = allCollectibles
                    .Select(c => MapCollectible(c, earnedIds.Contains(c.Id)))
            };
        }

        private static CollectibleDto MapCollectible(Collectible c, bool isEarned) => new CollectibleDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageUrl = c.ArtworkUrl ?? string.Empty,
            Rarity = c.Rarity.ToString(),
            IsEarned = isEarned,
            ChapterId = c.ChapterId
        };
    }
}