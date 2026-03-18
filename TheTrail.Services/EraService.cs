using Microsoft.EntityFrameworkCore;
using TheTrail.Data.Interfaces;
using TheTrail.Domain.Entities;
using TheTrail.Services.Core.DTOs.Era;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Services
{
    public class EraService : IEraService
    {
        private readonly IRepository<Era> _eraRepository;
        private readonly IRepository<UserChapterProgress> _progressRepository;

        public EraService(IRepository<Era> eraRepository,IRepository<UserChapterProgress> progressRepository)
        {
            _eraRepository = eraRepository;
            _progressRepository = progressRepository;
        }

        public async Task<IEnumerable<EraDto>> GetAllAsync(string? userId)
        {
            List<Era> eras = await _eraRepository
                .AllAsNoTracking()
                .Where(e => e.IsPublished)
                .OrderBy(e => e.Order)
                .Include(e => e.Chapters)
                .ToListAsync();

            return eras.Select(e => MapToDto(e, userId)).ToList();
        }

        public async Task<EraDto?> GetByIdAsync(int id, string? userId)
        {
            Era? era = await _eraRepository
                .AllAsNoTracking()
                .Where(e => e.Id == id && e.IsPublished)
                .Include(e => e.Chapters)
                .FirstOrDefaultAsync();

            if (era == null) return null;

            return MapToDto(era, userId);
        }

        public async Task<EraDto> CreateAsync(CreateEraDto dto)
        {
            Era era = new Era
            {
                Name = dto.Name,
                Description = dto.Description,
                CoverImageUrl = dto.CoverImageUrl,
                ColorTheme = dto.ColorTheme,
                Order = dto.Order,
                IsPublished = false,
                CreatedOn = DateTime.UtcNow
            };

            await _eraRepository.AddAsync(era);

            return MapToDto(era, null);
        }

        public async Task<EraDto?> UpdateAsync(int id, UpdateEraDto dto)
        {
            Era? era = await _eraRepository
                .All()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (era == null) return null;

            era.Name = dto.Name;
            era.Description = dto.Description;
            era.CoverImageUrl = dto.CoverImageUrl;
            era.ColorTheme = dto.ColorTheme;
            era.Order = dto.Order;
            era.IsPublished = dto.IsPublished;

            await _eraRepository.UpdateAsync(era);
            return MapToDto(era, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Era? era = await _eraRepository
                .All()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (era == null) return false;

            await _eraRepository.DeleteAsync(era);
            return true;
        }

        private EraDto MapToDto(Era era, string? userId)
        {
            int chapterCount = era.Chapters.Count;

            int completedCount = userId != null
                ? era.Chapters.Count(c =>
                    _progressRepository
                        .AllAsNoTracking()
                        .Any(p => p.UserId == userId
                               && p.ChapterId == c.Id
                               && p.QuizPassed))
                : 0;

            return new EraDto
            {
                Id = era.Id,
                Name = era.Name,
                Description = era.Description,
                CoverImageUrl = era.CoverImageUrl,
                ColorTheme = era.ColorTheme,
                Order = era.Order,
                ChapterCount = chapterCount,
                CompletedChapterCount = completedCount,
                IsGrandmasterUnlocked = chapterCount > 0 && completedCount == chapterCount
            };
        }
    }
}