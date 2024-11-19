using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.EmployeeQualification;

[Mapper]
public static partial class EmployeeQualificationMapper
{
    public static Domain.Entities.EmployeeQualification FromQualificationGuidsToEmployeeQualification(
        this Guid qualificationId, Guid employeeId, DateTime qualificationObtained)
    {
        return new Domain.Entities.EmployeeQualification
        {
            QualificationId = qualificationId,
            EmployeeId = employeeId,
            QualificationObtained = qualificationObtained
        };
    }
}