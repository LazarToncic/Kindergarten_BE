using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
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
    DbSet<Child> Children { get; }
    DbSet<ParentChild> ParentChildren { get; }
    DbSet<Domain.Entities.Parent> Parents { get; }
    DbSet<Allergy> Allergies { get; }
    DbSet<ChildAllergy> ChildAllergies { get; }
    DbSet<MedicalCondition> MedicalConditions { get; }
    DbSet<ChildMedicalCondition> ChildMedicalConditions { get; }
}