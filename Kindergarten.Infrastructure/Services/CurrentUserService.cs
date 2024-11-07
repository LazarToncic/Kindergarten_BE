using System.Security.Claims;
using Kindergarten.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Kindergarten.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public string? UserId { get; }
    public string? Email { get; }
    public List<string>? Roles { get; }
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        
        UserId = GetClaimValue(ClaimTypes.NameIdentifier);

        var identity = httpContextAccessor.HttpContext?.User.Identity;

        if (identity is not null && identity.IsAuthenticated)
        {
            var roles = GetRoleClaimValues();

            if (roles?.Count > 0)
            {
                Roles.AddRange(roles);
            }

            Email = GetClaimValue(ClaimTypes.Email);
        }
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value;
    }

    private List<string>? GetRoleClaimValues()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value)
            .ToList();
    }
}