using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Repositories;
using Kindergarten.Domain.Entities;
using Kindergarten.Infrastructure.Configuration;
using Kindergarten.Infrastructure.Context;
using Kindergarten.Infrastructure.Identity;
using Kindergarten.Infrastructure.Repositories;
using Kindergarten.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
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
        
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddUserManager<ApplicationUserManager>()
                .AddEntityFrameworkStores<KindergartenDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IKindergartenDbContext>(provider => provider.GetRequiredService<KindergartenDbContext>());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IKindergartenService, KindergartenService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IQualificationService, QualificationService>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<IEmployeePositionService, EmployeePositionService>();
            services.AddScoped<ICoordinatorService, CoordinatorService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IChildrenService, ChildrenService>();
            services.AddScoped<IAllergyService, AllergyService>();
            services.AddScoped<IMedicalConditionService, MedicalConditionService>();
            services.AddScoped<IChildWithdrawalService, ChildWithdrawalService>();

            services.AddScoped<IDepartmentEmployeeRepository, DepartmentEmployeeRepository>();

        return services;
    }
}