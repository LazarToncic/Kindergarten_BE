using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Kindergarten.Application.Common.Dto.Auth;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Kindergarten.Infrastructure.Exceptions;
using Kindergarten.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kindergarten.Infrastructure.Services;

public class TokenService(IConfiguration configuration, IKindergartenDbContext dbContext, ApplicationUserManager userManager) : ITokenService
{
    public async Task<string> GenerateAccessToken(ApplicationUser user)
    {
        
        var roles = await userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken(ApplicationUser user)
    {
        var existingRefreshToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == user.Id && !rt.IsRevoked);

        if (existingRefreshToken != null)
        {
            existingRefreshToken.IsRevoked = true;
            dbContext.RefreshTokens.Update(existingRefreshToken);
            await dbContext.SaveChangesAsync(new CancellationToken());
        }
        
        var randomNumber = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        var token = Convert.ToBase64String(randomNumber);

        var refreshTokenEntity = new RefreshToken
        {
            Token = token,
            IsRevoked = false,
            UserId = user.Id
        };

        await dbContext.RefreshTokens.AddAsync(refreshTokenEntity);
        await dbContext.SaveChangesAsync(new CancellationToken());

        return token; 
    }

    public async Task<LoginResponseDto> GenerateRefreshTokenAfterAccessIsExpired(string sentRefreshToken)
    {
        var refreshToken = await CheckIfRefreshTokenIsRevoked(sentRefreshToken);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }
        
        var user = await userManager.FindByIdAsync(refreshToken.UserId);
        
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        refreshToken.IsRevoked = true;
        dbContext.RefreshTokens.Update(refreshToken);
        await dbContext.SaveChangesAsync(new CancellationToken());
        
        var accessToken = await GenerateAccessToken(user);
        var newRefreshToken = await GenerateRefreshToken(user);
        
        return new LoginResponseDto
        (
            accessToken,
            DateTime.UtcNow.AddMinutes(20),
            newRefreshToken,
            DateTime.UtcNow.AddDays(7)
        );
    }

    public async Task<RefreshToken?> CheckIfRefreshTokenIsRevoked(string sentRefreshToken)
    {
        return await dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == sentRefreshToken && !rt.IsRevoked);
    }
}