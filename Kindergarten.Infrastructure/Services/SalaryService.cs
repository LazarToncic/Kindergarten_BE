using Kindergarten.Application.Common.Dto.Qualification;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class SalaryService(IKindergartenDbContext dbContext, IQualificationService qualificationService) : ISalaryService
{
    public async Task<string> CreateSalaryForNewEmployee(List<QualificationCreateEmployeeDto> qualifications, string employeePositionName, Guid employeeId, CancellationToken cancellationToken)
    {
        var baseAmount = GetBaseAmountSalaryDependingOnPosition(employeePositionName);
        var typeOfQualifications = qualifications.Select(qualification => qualification.TypeOfQualification).ToList();

        var strongestQualification = qualificationService.GetStrongestQualificationForEmployee(typeOfQualifications);

        var finalAmount = GetTotalAmountOfSalaryWithQualification(baseAmount, strongestQualification);

        var salary = new Salary
        {
            EmployeeId = employeeId,
            Amount = finalAmount,
            Currency = "RSD",
            EmployeePosition = employeePositionName
        };

        dbContext.Salaries.Add(salary);
        await dbContext.SaveChangesAsync(cancellationToken);

        return strongestQualification;
    }

    public async Task CreateNewSalaryWhenEmployeeIsChangingPositions(Guid employeeId,Guid employeePositionId, CancellationToken cancellationToken)
    {
        var newPositionName = await dbContext.EmployeePositions
            .Where(x => x.Id.Equals(employeePositionId))
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);
        
        var baseAmount = GetBaseAmountSalaryDependingOnPosition(newPositionName);

        var qualifications = await dbContext.EmployeeQualifications
            .Where(x => x.EmployeeId.Equals(employeeId))
            .Select(x => x.Qualification.QualificationType.Name)
            .ToListAsync(cancellationToken);

        var strongestQualification = qualificationService.GetStrongestQualificationForEmployee(qualifications);
        
        var finalAmount = GetTotalAmountOfSalaryWithQualification(baseAmount, strongestQualification);
        
        var salary = new Salary
        {
            EmployeeId = employeeId,
            Amount = finalAmount,
            Currency = "RSD",
            EmployeePosition = newPositionName
        };

        dbContext.Salaries.Add(salary);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private int GetTotalAmountOfSalaryWithQualification(int baseAmount, string strongestQualification)
    {
        var newAmount = 0;

        if (strongestQualification == EmployeeQualificationsExtensions.Bachelor)
        {
            newAmount = baseAmount + 10000;
        } else if (strongestQualification == EmployeeQualificationsExtensions.Master)
        {
            newAmount = baseAmount + 16000;
        }
        
        return newAmount;
    }

    private int GetBaseAmountSalaryDependingOnPosition(string positionName)
    {
        var baseAmount = positionName switch
        {
            "Teacher" => 75000,
            "Cook" => 70000,
            "Coordinator" => 90000,
            "Driver" => 65000,
            _ => 70000
        };

        return baseAmount;
    }
}