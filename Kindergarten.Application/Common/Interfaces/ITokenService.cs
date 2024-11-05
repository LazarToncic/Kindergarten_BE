using Kindergarten.Application.Common.Dto.Auth;
using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Common.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(ApplicationUser user);
    Task<string> GenerateRefreshToken(ApplicationUser user);
    Task<LoginResponseDto> GenerateRefreshTokenAfterAccessIsExpired(string refreshToken);

    Task<RefreshToken?> CheckIfRefreshTokenIsRevoked(string sentRefreshToken);
}