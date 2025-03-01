using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<ApplicationUser> Users { get; }
    DbSet<ApplicationRole> Roles { get; }
    DbSet<ApplicationUserRole> UserRoles { get; }
    DbSet<Domain.Entities.Kindergarten> Kindergartens { get; }
    DbSet<Domain.Entities.Department> Departments { get; }
    DbSet<KindergartenDepartment> KindergartenDepartments { get; }
    DbSet<Domain.Entities.QualificationType> QualificationTypes { get; }
    DbSet<EmployeePosition> EmployeePositions { get; }
    DbSet<Domain.Entities.Employee> Employees { get; }
    DbSet<DepartmentEmployee> DepartmentEmployees { get; }
    DbSet<Qualification> Qualifications { get; }
    DbSet<EmployeeQualification> EmployeeQualifications { get; }
    DbSet<Salary> Salaries { get; }
    DbSet<ParentRequest> ParentRequests { get; }
}