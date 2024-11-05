namespace Kindergarten.Application.Common.Dto.Auth;

public record LoginUserDto(string EmailOrUsername, string Password, bool RememberMe);