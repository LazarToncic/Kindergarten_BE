using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Common.Interfaces;

public interface IKindergartenDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Domain.Entities.Kindergarten> Kindergartens { get; }
    DbSet<Domain.Entities.Department> Departments { get; }
    DbSet<KindergartenDepartment> KindergartenDepartments { get; }
}