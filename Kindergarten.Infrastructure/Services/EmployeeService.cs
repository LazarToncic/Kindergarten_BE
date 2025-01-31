using Kindergarten.Application.Common.Dto.Employee;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Kindergarten.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class EmployeeService(IKindergartenDbContext dbContext, ApplicationUserManager userManager, IKindergartenService kindergartenService, 
    IDepartmentService departmentService, IQualificationService qualificationService, ISalaryService salaryService, ICoordinatorService coordinatorService) : IEmployeeService
{
    public async Task CreateEmployee(CreateEmployeeDto dto, CancellationToken cancellationToken)
    {
        if (! await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten(dto.KindergartenName))
            throw new CoordinatorException("This Coordinator cant give jobs or make changes in Kindergarten in which he/she doesnt work");
        
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

        var setQualifications = await qualificationService.AssignQualificationToNewEmployee(dto.Qualifications, employee.Id, cancellationToken);

        var strongestQualification = await salaryService.CreateSalaryForNewEmployee(dto.Qualifications,dto.EmployeePositionName, employee.Id, cancellationToken);
        
        var setDepartmentEmployee = await departmentService.AssignNewEmployeeToDepartment(dto.DepartmentName, dto.EmployeePositionName, employee.Id, strongestQualification, cancellationToken);

        if (!setDepartmentEmployee)
            throw new ConflictException("this user cannot get a job", 
                new {});
        
    }

    public async Task UpdateEmployeePosition(UpdateEmployeePositionDto dto, CancellationToken cancellationToken)
    {
        if (!await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten(dto.KindergartenName))
            throw new CoordinatorException(
                "This Coordinator cant give jobs or make changes in Kindergarten in which he/she doesnt work");

        var employee = await dbContext.Employees
            .Include(x => x.EmployeePosition)
            .FirstOrDefaultAsync(x => x.Id.Equals(dto.EmployeeId), cancellationToken: cancellationToken);

        if (employee == null)
            throw new NotFoundException("This Employee doesnt exist");

        var newPositionName = await dbContext.EmployeePositions
            .Where(p => p.Id == dto.EmployeePositionId)
            .Select(p => p.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrEmpty(newPositionName))
            throw new NotFoundException("The specified Employee Position doesn't exist");
        
        switch (newPositionName)
        {
            case "Coordinator":
                if (employee.EmployeePosition.Name == "Teacher")
                {
                    await departmentService.DeleteDepartmentsForNewCoordinator(dto.EmployeeId, cancellationToken);
                    
                    await salaryService.CreateNewSalaryWhenEmployeeIsChangingPositions(dto.EmployeeId, dto.EmployeePositionId, cancellationToken);
                
                    employee.EmployeePositionId = dto.EmployeePositionId;

                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                break;

            case "Manager":
                // Logika za kada neko postane menadžer
                break;

            case "Driver":
                // Logika za kada neko postane vozač
                break;

            case "Cook":
                // Logika za kada neko postane kuvar
                break;

            default:
                // Opcionalno - ako nova pozicija ne zahteva posebne promene
                break;
        }

        
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
            .Where(x => x.Name.Equals(positionName))
            .FirstOrDefaultAsync();

        if (name == null)
            throw new NotFoundException("This employee positions doesnt exist",
                new {positionName});

        return name.Id;
    }
}