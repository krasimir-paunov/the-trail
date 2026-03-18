using TheTrail.Services.Core.DTOs.Era;

namespace TheTrail.Services.Core.Interfaces
{
    public interface IEraService
    {
        Task<IEnumerable<EraDto>> GetAllAsync(string? userId);
        Task<EraDto?> GetByIdAsync(int id, string? userId);
        Task<EraDto> CreateAsync(CreateEraDto dto);
        Task<EraDto?> UpdateAsync(int id, UpdateEraDto dto);
        Task<bool> DeleteAsync(int id);
    }
}