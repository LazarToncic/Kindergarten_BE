using Kindergarten.Application.Common.Dto.Auth;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Kindergarten.Infrastructure.Exceptions;
using Kindergarten.Infrastructure.Identity;

namespace Kindergarten.Infrastructure.Services;

public class AuthService(ApplicationUserManager userManager, ITokenService tokenService) : IAuthService
{
    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingEmail = await userManager.FindByEmailAsync(dto.Email);
        if (existingEmail != null)
            throw new AuthException("User with this email already exists.");

        var existingUsername = await userManager.FindByNameAsync(dto.Username);
        if (existingUsername != null)
            throw new AuthException("User with this username already exists.");
    
        var user = new ApplicationUser
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            YearOfBirth = dto.YearOfBirth,
            UserName = dto.Username,
            PhoneNumber = dto.PhoneNumber
        };

        try
        {
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new AuthException("Could not create new user",
                    new { Errors = result.Errors.ToList() });
            }

            var rolesResult = await userManager.AddToRoleAsync(user, "User");
            if (!rolesResult.Succeeded)
            {
                await userManager.DeleteAsync(user);
                throw new AuthException("Could not add roles to the user",
                    new { Errors = rolesResult.Errors.ToList() });
            }
            
        }
        catch (Exception e)
        {
            await userManager.DeleteAsync(user);
            throw new AuthException("Could not register user", e);
        }
    }

    public async Task<LoginResponseDto> LoginAsync(LoginUserDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.EmailOrUsername);
        
        if (user == null)
        {
            user = await userManager.FindByNameAsync(dto.EmailOrUsername);
        }
        
        if (user == null)
        {
            throw new AuthException("User not found");
        }
        
        if (!await userManager.CheckPasswordAsync(user, dto.Password))
            throw new UnauthorizedAccessException("Invalid username or password");
        
        var accessToken = await tokenService.GenerateAccessToken(user);
        
        var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(20);
        
        
        string? refreshToken = null;
        DateTime? refreshTokenExpiresAt = null;

        if (dto.RememberMe)
        {
            refreshToken = await tokenService.GenerateRefreshToken(user);
            refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7); 
        }

        return new LoginResponseDto
        (
            accessToken,
            accessTokenExpiresAt,
            refreshToken,
            refreshTokenExpiresAt
        );

    }

    public async Task UserLogoutAsync(string refreshToken)
    {
        await tokenService.SetRefreshTokenForUserToExpired(refreshToken);
    }
}