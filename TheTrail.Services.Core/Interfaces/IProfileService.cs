using TheTrail.Services.Core.DTOs.Profile;

namespace TheTrail.Services.Core.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto?> GetProfileAsync(string userId);
    }
}