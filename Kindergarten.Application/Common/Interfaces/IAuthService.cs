using Kindergarten.Application.Common.Dto.Auth;

namespace Kindergarten.Application.Common.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<LoginResponseDto> LoginAsync(LoginUserDto dto);
    Task UserLogoutAsync(string refreshToken);
}