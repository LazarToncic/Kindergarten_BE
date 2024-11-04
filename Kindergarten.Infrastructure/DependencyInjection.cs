using Kindergarten.Infrastructure.Configuration;
using Kindergarten.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kindergarten.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfiguration = new PostgresDbConfiguration();
        configuration.GetSection("PostgresDbConfiguration").Bind(dbConfiguration);
        
            services.AddDbContext<KindergartenDbContext>(options =>
                options.UseNpgsql(dbConfiguration.ConnectionString(),
                    x => x.MigrationsAssembly(typeof(KindergartenDbContext).Assembly.FullName)));  
        

        return services;
    }
}