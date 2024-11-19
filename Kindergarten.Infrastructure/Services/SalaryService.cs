using Kindergarten.Application.Common.Dto.Qualification;
using Kindergarten.Application.Common.Interfaces;

namespace Kindergarten.Infrastructure.Services;

public class SalaryService(IKindergartenDbContext dbContext, IQualificationService qualificationService) : ISalaryService
{
    public Task<bool> CreateSalaryForNewEmployee(List<QualificationCreateEmployeeDto> qualifications, string employeePositionName, Guid employeeId, CancellationToken cancellationToken)
    {
        var baseAmount = GetBaseAmountSalaryDependingOnPosition(employeePositionName);
        var typeOfQualifications = qualifications.Select(qualification => qualification.TypeOfQualification).ToList();

        var strongestQualification = qualificationService.GetStrongestQualificationWhenCreatingNewEmployee(typeOfQualifications);

        var finalAmount = GetTotalAmountOfSalaryWithQualification(baseAmount, strongestQualification);
        
        
        throw new NotImplementedException();
    }

    private int GetTotalAmountOfSalaryWithQualification(int baseAmount, string strongestQualification)
    {
        var newAmount = 0;

        //TODO OVDE SI STAO!
        
        
        return 1;
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