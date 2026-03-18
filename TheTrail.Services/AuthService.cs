using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheTrail.Domain.Entities;
using TheTrail.Services.Core.DTOs.Auth;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            ApplicationUser user = new ApplicationUser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.Email,
                CreatedOn = DateTime.UtcNow
            };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errors);
            }

            return GenerateAuthResponse(user);
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return null;

            bool passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid) return null;

            return GenerateAuthResponse(user);
        }

        private AuthResponseDto GenerateAuthResponse(ApplicationUser user)
        {
            string token = GenerateJwtToken(user);

            int expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"] ?? "60");

            return new AuthResponseDto
            {
                Token = token,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes)
            };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            string jwtKey = _configuration["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JWT SecretKey is not configured.");

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            SigningCredentials credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };

            int expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"] ?? "60");

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}