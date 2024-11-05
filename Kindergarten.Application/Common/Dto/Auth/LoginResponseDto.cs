namespace Kindergarten.Application.Common.Dto.Auth;

public record LoginResponseDto(string AccessToken, DateTime AccessTokenExpiresAt, string? RefreshToken, DateTime? RefreshTokenExpiresAt);