using Kindergarten.Application.Common.Dto.Qualification;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.EmployeeQualification;
using Kindergarten.Application.Common.Mappers.Qualification;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class QualificationService(IKindergartenDbContext dbContext) : IQualificationService
{
    public async Task<bool> AssignQualificationToNewEmployee(List<QualificationCreateEmployeeDto> qualifications, Guid employeeId, CancellationToken cancellationToken)
    {
        foreach (var qualification in qualifications)
        {
            var qualificationType = await dbContext.QualificationTypes
                .Where(x => x.Name.Equals(qualification.TypeOfQualification))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (qualificationType == null)
                throw new NotFoundException("Qualification of this type doesnt exist",
                    new {qualification.TypeOfQualification});

            var newQualification = qualification.FromQualificationCreateEmployeeDtoToQualification(qualificationType.Id);

            dbContext.Qualifications.Add(newQualification);
            var newQualificationResult = await dbContext.SaveChangesAsync(cancellationToken);

            if (newQualificationResult <= 0)
                return false;

            var employeeQualification = newQualification.Id.FromQualificationGuidsToEmployeeQualification(employeeId, qualification.QualificationObtained);

            dbContext.EmployeeQualifications.Add(employeeQualification);
            await dbContext.SaveChangesAsync(cancellationToken);

        }

        return true;
    }

    public string GetStrongestQualificationWhenCreatingNewEmployee(List<string> qualifications)
    {
        var strongestRole = "";
        
        if (qualifications.Contains("Secondary School"))
        {
            strongestRole = "Secondary School";
        } 
        
        if (qualifications.Contains("Bachelor Degree"))
        {
            strongestRole = "Bachelor degree";
        } 
        
        if (qualifications.Contains("Master Degree"))
        {
            strongestRole = "Master Degree";
        }

        return strongestRole;
    }
}