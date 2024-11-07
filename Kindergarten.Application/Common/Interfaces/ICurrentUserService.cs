namespace Kindergarten.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? Email { get; }
    public List<string>? Roles { get; }
    
}