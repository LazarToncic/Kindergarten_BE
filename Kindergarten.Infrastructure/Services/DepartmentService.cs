using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class DepartmentService(IKindergartenDbContext dbContext) : IDepartmentService
{
    public async Task<bool> AssignNewEmployeeToDepartment(string nameOfDepartment, Guid newEmployeeId, CancellationToken cancellationToken)
    {
        var departmentId = await GetDepartmentId(nameOfDepartment);

        var departmentEmployee = new DepartmentEmployee
        {
            DepartmentId = departmentId,
            EmployeeId = newEmployeeId
        };

        dbContext.DepartmentEmployees.Add(departmentEmployee);
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken);

        return dbResult > 0;
    }

    private async Task<Guid> GetDepartmentId(string departmentName)
    {
        var department = await dbContext.Departments
            .Where(x => x.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefaultAsync();

        if (department == null)
            throw new NotFoundException("Department with this name doesnt exist",
                new {departmentName});

        return department.Id;
    }
}