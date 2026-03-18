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

        public ChapterService(IRepository<Chapter> chapterRepository,IRepository<UserChapterProgress> progressRepository)
        {
            _chapterRepository = chapterRepository;
            _progressRepository = progressRepository;
        }

        public async Task<IEnumerable<ChapterDto>> GetByEraAsync(int eraId, string? userId)
        {
            List<Chapter> chapters = await _chapterRepository
                .AllAsNoTracking()
                .Where(c => c.EraId == eraId && c.IsPublished)
                .OrderBy(c => c.Order)
                .Include(c => c.Collectible)
                .Include(c => c.Quiz)
                .ToListAsync();

            return chapters.Select(c => MapToDto(c, userId)).ToList();
        }

        public async Task<ChapterDto?> GetByIdAsync(int id, string? userId)
        {
            Chapter? chapter = await _chapterRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id && c.IsPublished)
                .Include(c => c.Collectible)
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

        private ChapterDto MapToDto(Chapter chapter, string? userId)
        {
            UserChapterProgress? progress = userId != null
                ? _progressRepository
                    .AllAsNoTracking()
                    .FirstOrDefault(p => p.UserId == userId
                                      && p.ChapterId == chapter.Id)
                : null;

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
                HasCollectible = chapter.Collectible != null,
                CollectibleRarity = chapter.Collectible?.Rarity
            };
        }
    }
}