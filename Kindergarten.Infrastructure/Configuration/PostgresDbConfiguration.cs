namespace Kindergarten.Infrastructure.Configuration;

public class PostgresDbConfiguration
{
    public string? DbHost { get; set; }
    public string? DbPort { get; set; }
    public string? DbName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }

    public string ConnectionString()
    {
        return $"HOST={DbHost}; Database={DbName}; Username={UserName}; Password={Password}";
    }
}