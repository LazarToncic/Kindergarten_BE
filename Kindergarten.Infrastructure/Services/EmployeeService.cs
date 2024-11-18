using Kindergarten.Application.Common.Dto.Employee;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Kindergarten.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class EmployeeService(IKindergartenDbContext dbContext, ApplicationUserManager userManager, IKindergartenService kindergartenService, 
    IDepartmentService departmentService) : IEmployeeService
{
    public async Task CreateEmployee(CreateEmployeeDto dto, CancellationToken cancellationToken)
    {
        var userId = await FindUserId(dto.EmailOrUsername);

        var employeePositionId = await GetEmployeePositionId(dto.EmployeePositionName);

        var kindergartenId = await kindergartenService.GetKindergartenId(dto.KindergartenName);

        var employee = new Employee
        {
            UserId = userId,
            EmployeePositionId = employeePositionId,
            KindergartenId = kindergartenId
        };

        dbContext.Employees.Add(employee);
        await dbContext.SaveChangesAsync(cancellationToken);

        var departmentEmployee = await departmentService.AssignNewEmployeeToDepartment(dto.DepartmentName, employee.Id, cancellationToken);

        if (!departmentEmployee)
            throw new ConflictException("this user cannot get a job", 
                new {});
        
        //TODO  ovde sad za qualifications i salary ali trebas da dodas da na osnovu qualificationoa mogu da budu zaposleni u tom departmanu
        //TODO i da kordinator moze samo u svom vrticu da zaposljava.
        //TODO nisi proverio dal ovo radi!!!!!!!!!
    }

    private async Task<string> FindUserId(string emailOrUsername)
    {
        var user = await userManager.FindByEmailAsync(emailOrUsername) 
                   ?? await userManager.FindByNameAsync(emailOrUsername);

        if (user == null)
        {
            throw new NotFoundException("User with this username or email doesn't exist.");
        }

        return user.Id;
    }

    private async Task<Guid> GetEmployeePositionId(string positionName)
    {
        var name = await dbContext.EmployeePositions
            .Where(x => x.Name.Equals(positionName, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefaultAsync();

        if (name == null)
            throw new NotFoundException("This employee positions doesnt exist",
                new {positionName});

        return name.Id;
    }
}