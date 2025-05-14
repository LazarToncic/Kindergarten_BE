using Kindergarten.Application.Common.Dto.Department;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class DepartmentService(IKindergartenDbContext dbContext, IChildrenService childrenService) : IDepartmentService
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

    public async Task<DepartmentsForUnassignedChildrenListDto> GetDepartmentsForUnassignedChildrenList(Guid childrenId, CancellationToken cancellationToken)
    {
        var childAge = await childrenService.GetChildrenAge(childrenId, cancellationToken);

        var departments = await dbContext.Departments
            .Where(x => x.AgeGroup == childAge)
            .Where(d => d.MaximumCapacity > d.Capacity)
            .Select(d => new DepartmentDto(
                d.Id,
                d.AgeGroup,
                d.Capacity,
                d.Name
            ))
            .ToListAsync(cancellationToken);

        return new DepartmentsForUnassignedChildrenListDto(departments);
    }

    public async Task CreateDepartment(Guid kindergartenId, int ageGroup, int maximumCapacity, string name,
        CancellationToken cancellationToken)
    {

        var department = new Department
        {
            AgeGroup = ageGroup,
            MaximumCapacity = maximumCapacity,
            Name = name,
            Capacity = 0
        };

    var existingDepartment = await dbContext.Departments
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingDepartment != null)
        {
            throw new ConflictException("Department with this name already exists.",
                new {existingDepartment.Name});
        }
        
        dbContext.Departments.Add(department);
        await dbContext.SaveChangesAsync(cancellationToken);

        var kindergartenDepartment = new KindergartenDepartment
        {
            DepartmentId = department.Id,
            KindergartenId = kindergartenId
        };

        dbContext.KindergartenDepartments.Add(kindergartenDepartment);
        
        await dbContext.SaveChangesAsync(cancellationToken);
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