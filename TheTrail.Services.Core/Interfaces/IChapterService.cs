using TheTrail.Services.Core.DTOs.Chapter;

namespace TheTrail.Services.Core.Interfaces
{
    public interface IChapterService
    {
        Task<IEnumerable<ChapterDto>> GetByEraAsync(int eraId, string? userId);
        Task<ChapterDto?> GetByIdAsync(int id, string? userId);
        Task<ChapterDto> CreateAsync(CreateChapterDto dto);
        Task<ChapterDto?> UpdateAsync(int id, UpdateChapterDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> CompleteScrollAsync(int chapterId, string userId);
        Task<QuizDto?> GetQuizAsync(int chapterId);
        Task<string?> GetContentAsync(int chapterId);
        Task<bool> CheckAnswerAsync(int questionId, string answer);
        Task<QuizResultDto> SaveQuizResultAsync(int chapterId, string userId, bool passed, bool perfectScore);
        Task<bool> CheckEraGrandmasterAsync(int eraId, string userId);
        Task AwardLegendaryIfEarnedAsync(int eraId, string userId);
    }
}