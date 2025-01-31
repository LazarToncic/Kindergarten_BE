using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class DepartmentService(IKindergartenDbContext dbContext) : IDepartmentService
{
    public async Task<bool> AssignNewEmployeeToDepartment(string nameOfDepartment, string positionInDepartment, Guid newEmployeeId,string strongestQualification, CancellationToken cancellationToken)
    {
        var ageCheck = await CheckIfEmployeeCanWorkWithAgeGroup(nameOfDepartment, strongestQualification);

        if (!ageCheck)
            throw new EmployeeNotQualifiedException("Employee is not Qualified to work in this Department", 
                new {strongestQualification});
        
        var departmentId = await GetDepartment(nameOfDepartment);

        var departmentEmployee = new DepartmentEmployee
        {
            DepartmentId = departmentId.Id,
            EmployeeId = newEmployeeId,
            RoleInDepartment = positionInDepartment
        };

        dbContext.DepartmentEmployees.Add(departmentEmployee);
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken);

        return dbResult > 0;
    }

    public async Task<bool> CheckIfEmployeeCanWorkWithAgeGroup(string nameOfDepartment, string strongestQualification)
    {
        var department = await GetDepartment(nameOfDepartment);

        if (department.AgeGroup == 0 || department.AgeGroup == 1 || department.AgeGroup == 2)
        {
            return true;
        }
        
        if (department.AgeGroup == 3 || department.AgeGroup == 4 || department.AgeGroup == 5)
        {
            if (strongestQualification == EmployeeQualificationsExtensions.Bachelor || strongestQualification == EmployeeQualificationsExtensions.Master)
            {
                return true;
            }
        }

        if (department.AgeGroup == 6)
        {
            if (strongestQualification == EmployeeQualificationsExtensions.Master)
                return true;
        }

        return false;
    }

    public async Task DeleteDepartmentsForNewCoordinator(Guid employeeId, CancellationToken cancellationToken)
    {
        await dbContext.DepartmentEmployees
            .Where(x => x.EmployeeId.Equals(employeeId))
            .ExecuteDeleteAsync(cancellationToken);
    }

    private async Task<Department> GetDepartment(string departmentName)
    {
        var department = await dbContext.Departments
            .Where(x => x.Name.Equals(departmentName))
            .FirstOrDefaultAsync();

        if (department == null)
            throw new NotFoundException("Department with this name doesnt exist",
                new {departmentName});

        return department;
    }

    
}