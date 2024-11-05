namespace Kindergarten.Infrastructure.Configuration;

public class JwtConfiguration
{
    public string? ValidAudience { get; set; }
    public string? ValidIssuer { get; set; }
    public string? Key { get; set; }
    public string? DurationInMinutes { get; set; }
}