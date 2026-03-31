using Microsoft.EntityFrameworkCore;
using TheTrail.Data.Interfaces;
using TheTrail.Domain.Entities;
using TheTrail.Services.Core.DTOs.Chapter;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Services
{
    public class ChapterService : IChapterService
    {
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<UserChapterProgress> _progressRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Collectible> _collectibleRepository;
        private readonly IRepository<UserCollectible> _userCollectibleRepository;

        public ChapterService(
            IRepository<Chapter> chapterRepository,
            IRepository<UserChapterProgress> progressRepository,
            IRepository<Question> questionRepository,
            IRepository<Collectible> collectibleRepository,
            IRepository<UserCollectible> userCollectibleRepository)
        {
            _chapterRepository = chapterRepository;
            _progressRepository = progressRepository;
            _questionRepository = questionRepository;
            _collectibleRepository = collectibleRepository;
            _userCollectibleRepository = userCollectibleRepository;
        }

        public async Task<QuizResultDto> SaveQuizResultAsync(int chapterId, string userId, bool passed, bool perfectScore)
        {
            var result = new QuizResultDto { Passed = passed, PerfectScore = perfectScore };

            // ── Update progress record ──────────────────────────────────────
            UserChapterProgress? progress = await _progressRepository
                .All()
                .FirstOrDefaultAsync(p => p.UserId == userId && p.ChapterId == chapterId);

            if (progress == null)
            {
                progress = new UserChapterProgress
                {
                    UserId = userId,
                    ChapterId = chapterId,
                    ScrollCompleted = true,
                    QuizPassed = passed
                };
                await _progressRepository.AddAsync(progress);
            }
            else
            {
                if (passed) progress.QuizPassed = true;
                await _progressRepository.UpdateAsync(progress);
            }

            // ── Award Common collectible (pass with 60%+) ───────────────────
            if (passed)
            {
                Collectible? commonCollectible = await _collectibleRepository
                    .AllAsNoTracking()
                    .FirstOrDefaultAsync(c => c.ChapterId == chapterId
                                           && c.Rarity == Domain.Enums.Rarity.Common);

                if (commonCollectible != null)
                {
                    bool alreadyEarned = await _userCollectibleRepository
                        .AllAsNoTracking()
                        .AnyAsync(uc => uc.UserId == userId
                                     && uc.CollectibleId == commonCollectible.Id);

                    if (!alreadyEarned)
                        await _userCollectibleRepository.AddAsync(new UserCollectible
                        {
                            UserId = userId,
                            CollectibleId = commonCollectible.Id,
                            EarnedOn = DateTime.UtcNow
                        });
                }
            }

            // ── Award Rare collectible (perfect score 5/5) ──────────────────
            if (perfectScore)
            {
                Collectible? rareCollectible = await _collectibleRepository
                    .AllAsNoTracking()
                    .FirstOrDefaultAsync(c => c.ChapterId == chapterId
                                           && c.Rarity == Domain.Enums.Rarity.Rare);

                if (rareCollectible != null)
                {
                    bool alreadyEarned = await _userCollectibleRepository
                        .AllAsNoTracking()
                        .AnyAsync(uc => uc.UserId == userId
                                     && uc.CollectibleId == rareCollectible.Id);

                    if (!alreadyEarned)
                        await _userCollectibleRepository.AddAsync(new UserCollectible
                        {
                            UserId = userId,
                            CollectibleId = rareCollectible.Id,
                            EarnedOn = DateTime.UtcNow
                        });
                }
            }

            // ── Check Legendary ─────────────────────────────────────────────
            Chapter? chapter = await _chapterRepository
                .AllAsNoTracking()
                .Include(c => c.Era)
                .FirstOrDefaultAsync(c => c.Id == chapterId);

            if (chapter != null)
            {
                bool wasAlreadyGrandmaster = await _userCollectibleRepository
                    .AllAsNoTracking()
                    .AnyAsync(uc => uc.UserId == userId
                                 && uc.Collectible!.EraId == chapter.EraId
                                 && uc.Collectible!.Rarity == Domain.Enums.Rarity.Legendary);

                if (!wasAlreadyGrandmaster)
                {
                    await AwardLegendaryIfEarnedAsync(chapter.EraId, userId);

                    bool nowGrandmaster = await _userCollectibleRepository
                        .AllAsNoTracking()
                        .AnyAsync(uc => uc.UserId == userId
                                     && uc.Collectible!.EraId == chapter.EraId
                                     && uc.Collectible!.Rarity == Domain.Enums.Rarity.Legendary);

                    if (nowGrandmaster)
                    {
                        Collectible? legendary = await _collectibleRepository
                            .AllAsNoTracking()
                            .FirstOrDefaultAsync(c => c.EraId == chapter.EraId
                                                   && c.Rarity == Domain.Enums.Rarity.Legendary);

                        result.LegendaryAwarded = true;
                        result.LegendaryName = legendary?.Name;
                        result.LegendaryDescription = legendary?.Description;
                        result.LegendaryImageUrl = legendary?.ArtworkUrl;
                        result.EraName = chapter.Era?.Name;
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<ChapterDto>> GetByEraAsync(int eraId, string? userId)
        {
            List<Chapter> chapters = await _chapterRepository
                .AllAsNoTracking()
                .Where(c => c.EraId == eraId && c.IsPublished)
                .OrderBy(c => c.Order)
                .Include(c => c.Collectibles)
                .Include(c => c.Quiz)
                .ToListAsync();

            return chapters.Select(c => MapToDto(c, userId)).ToList();
        }

        public async Task<ChapterDto?> GetByIdAsync(int id, string? userId)
        {
            Chapter? chapter = await _chapterRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id && c.IsPublished)
                .Include(c => c.Collectibles)
                .Include(c => c.Quiz)
                .FirstOrDefaultAsync();

            if (chapter == null) return null;

            return MapToDto(chapter, userId);
        }

        public async Task<ChapterDto> CreateAsync(CreateChapterDto dto)
        {
            Chapter chapter = new Chapter
            {
                Title = dto.Title,
                Subtitle = dto.Subtitle,
                Content = dto.Content,
                CoverImageUrl = dto.CoverImageUrl,
                Order = dto.Order,
                EstimatedMinutes = dto.EstimatedMinutes,
                EraId = dto.EraId,
                IsPublished = false,
                CreatedOn = DateTime.UtcNow
            };

            await _chapterRepository.AddAsync(chapter);
            return MapToDto(chapter, null);
        }

        public async Task<ChapterDto?> UpdateAsync(int id, UpdateChapterDto dto)
        {
            Chapter? chapter = await _chapterRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chapter == null) return null;

            chapter.Title = dto.Title;
            chapter.Subtitle = dto.Subtitle;
            chapter.Content = dto.Content;
            chapter.CoverImageUrl = dto.CoverImageUrl;
            chapter.Order = dto.Order;
            chapter.EstimatedMinutes = dto.EstimatedMinutes;
            chapter.IsPublished = dto.IsPublished;

            await _chapterRepository.UpdateAsync(chapter);
            return MapToDto(chapter, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Chapter? chapter = await _chapterRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chapter == null) return false;

            await _chapterRepository.DeleteAsync(chapter);
            return true;
        }

        public async Task<bool> CompleteScrollAsync(int chapterId, string userId)
        {
            UserChapterProgress? progress = await _progressRepository
                .All()
                .FirstOrDefaultAsync(p => p.UserId == userId
                                       && p.ChapterId == chapterId);

            if (progress == null)
            {
                progress = new UserChapterProgress
                {
                    UserId = userId,
                    ChapterId = chapterId,
                    ScrollCompleted = true
                };
                await _progressRepository.AddAsync(progress);
            }
            else
            {
                progress.ScrollCompleted = true;
                await _progressRepository.UpdateAsync(progress);
            }

            return true;
        }

        public async Task<QuizDto?> GetQuizAsync(int chapterId)
        {
            Quiz? quiz = await _chapterRepository
                .AllAsNoTracking()
                .Where(c => c.Id == chapterId)
                .Include(c => c.Quiz)
                .ThenInclude(q => q!.Questions)
                .Select(c => c.Quiz)
                .FirstOrDefaultAsync();

            if (quiz == null) return null;

            return new QuizDto
            {
                Id = quiz.Id,
                PassMarkPercent = quiz.PassMarkPercent,
                Questions = quiz.Questions
                    .OrderBy(q => q.Order)
                    .Select(q => new QuestionDto
                    {
                        Id = q.Id,
                        Text = q.Text,
                        OptionA = q.OptionA,
                        OptionB = q.OptionB,
                        OptionC = q.OptionC,
                        OptionD = q.OptionD,
                        Order = q.Order
                    })
            };
        }

        public async Task<string?> GetContentAsync(int chapterId)
        {
            return await _chapterRepository
                .AllAsNoTracking()
                .Where(c => c.Id == chapterId)
                .Select(c => c.Content)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckAnswerAsync(int questionId, string answer)
        {
            Question? question = await _questionRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null) return false;

            return question.CorrectOption.Equals(answer, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> CheckEraGrandmasterAsync(int eraId, string userId)
        {
            List<int> rareCollectibleIds = await _collectibleRepository
                .AllAsNoTracking()
                .Where(c => c.EraId == null
                         && c.Rarity == Domain.Enums.Rarity.Rare
                         && c.Chapter != null
                         && c.Chapter.EraId == eraId
                         && c.Chapter.IsPublished)
                .Select(c => c.Id)
                .ToListAsync();

            if (rareCollectibleIds.Count == 0) return false;

            int earnedCount = await _userCollectibleRepository
                .AllAsNoTracking()
                .CountAsync(uc => uc.UserId == userId
                               && rareCollectibleIds.Contains(uc.CollectibleId));

            return earnedCount >= rareCollectibleIds.Count;
        }

        public async Task AwardLegendaryIfEarnedAsync(int eraId, string userId)
        {
            bool isGrandmaster = await CheckEraGrandmasterAsync(eraId, userId);
            if (!isGrandmaster) return;

            Collectible? legendary = await _collectibleRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.EraId == eraId
                                       && c.Rarity == Domain.Enums.Rarity.Legendary);

            if (legendary == null) return;

            bool alreadyEarned = await _userCollectibleRepository
                .AllAsNoTracking()
                .AnyAsync(uc => uc.UserId == userId
                             && uc.CollectibleId == legendary.Id);

            if (!alreadyEarned)
            {
                await _userCollectibleRepository.AddAsync(new UserCollectible
                {
                    UserId = userId,
                    CollectibleId = legendary.Id,
                    EarnedOn = DateTime.UtcNow
                });
            }
        }

        private ChapterDto MapToDto(Chapter chapter, string? userId)
        {
            UserChapterProgress? progress = userId != null
                ? _progressRepository
                    .AllAsNoTracking()
                    .FirstOrDefault(p => p.UserId == userId && p.ChapterId == chapter.Id)
                : null;

            Collectible? common = chapter.Collectibles
                .FirstOrDefault(c => c.Rarity == Domain.Enums.Rarity.Common);

            Collectible? rare = chapter.Collectibles
                .FirstOrDefault(c => c.Rarity == Domain.Enums.Rarity.Rare);

            // ── Collectible earned status ───────────────────────────────────
            bool commonEarned = userId != null && common != null &&
                _userCollectibleRepository
                    .AllAsNoTracking()
                    .Any(uc => uc.UserId == userId && uc.CollectibleId == common.Id);

            bool rareEarned = userId != null && rare != null &&
                _userCollectibleRepository
                    .AllAsNoTracking()
                    .Any(uc => uc.UserId == userId && uc.CollectibleId == rare.Id);

            return new ChapterDto
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Subtitle = chapter.Subtitle,
                CoverImageUrl = chapter.CoverImageUrl,
                Order = chapter.Order,
                EstimatedMinutes = chapter.EstimatedMinutes,
                EraId = chapter.EraId,
                ScrollCompleted = progress?.ScrollCompleted ?? false,
                QuizPassed = progress?.QuizPassed ?? false,
                HasQuiz = chapter.Quiz != null,
                HasCollectible = chapter.Collectibles.Any(),
                CollectibleRarity = common?.Rarity,
                CollectibleName = common?.Name,
                CollectibleDescription = common?.Description,
                CollectibleImageUrl = common?.ArtworkUrl,
                RareCollectibleName = rare?.Name,
                RareCollectibleDescription = rare?.Description,
                RareCollectibleImageUrl = rare?.ArtworkUrl,
                CommonCollectibleEarned = commonEarned,
                RareCollectibleEarned = rareEarned,
            };
        }
    }
}