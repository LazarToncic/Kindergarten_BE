using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Repositories;

public class DepartmentEmployeeRepository(IKindergartenDbContext dbContext) : IDepartmentEmployeeRepository
{
    public Task<bool> IsTeacherInDepartmentAsync(Guid departmentId, string userId, Guid kindergartenId, CancellationToken ct)
    {
        return dbContext.DepartmentEmployees
            .Include(de => de.Employee)
            .AnyAsync(de =>
                    de.DepartmentId               == departmentId &&
                    de.Employee.UserId            == userId &&
                    de.Employee.KindergartenId    == kindergartenId &&
                    de.RoleInDepartment           == EmployeeExtension.Teacher,
                ct);
    }
}