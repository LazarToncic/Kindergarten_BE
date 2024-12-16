using Kindergarten.Application.Common.Dto.Qualification;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
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
        
        if (qualifications.Contains(EmployeeQualificationsExtensions.HighSchool))
        {
            strongestRole = EmployeeQualificationsExtensions.HighSchool;
        } 
        
        if (qualifications.Contains(EmployeeQualificationsExtensions.Bachelor))
        {
            strongestRole = EmployeeQualificationsExtensions.Bachelor;
        } 
        
        if (qualifications.Contains(EmployeeQualificationsExtensions.Master))
        {
            strongestRole = EmployeeQualificationsExtensions.Master;
        }

        return strongestRole;
    }
}