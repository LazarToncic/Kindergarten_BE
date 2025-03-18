using System.Reflection;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Kindergarten.Infrastructure.Context;

public class KindergartenDbContext(DbContextOptions<KindergartenDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, 
    ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options), IKindergartenDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=root;Database=Kindergarten");
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await this.Database.BeginTransactionAsync(cancellationToken);
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Kindergarten.Domain.Entities.Kindergarten> Kindergartens => Set<Kindergarten.Domain.Entities.Kindergarten>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<KindergartenDepartment> KindergartenDepartments => Set<KindergartenDepartment>();
    public DbSet<QualificationType> QualificationTypes  => Set<QualificationType>();
    public DbSet<EmployeePosition> EmployeePositions => Set<EmployeePosition>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<DepartmentEmployee> DepartmentEmployees => Set<DepartmentEmployee>();
    public DbSet<Qualification> Qualifications => Set<Qualification>();
    public DbSet<EmployeeQualification> EmployeeQualifications => Set<EmployeeQualification>();
    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<ParentRequest> ParentRequests => Set<ParentRequest>();
    public DbSet<Child> Children => Set<Child>();
    public DbSet<Parent> Parents => Set<Parent>();
}