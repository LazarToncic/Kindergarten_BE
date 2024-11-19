using Kindergarten.Application.Common.Dto.Qualification;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Qualification;

[Mapper]
public static partial class QualificationMapper
{
    public static Domain.Entities.Qualification FromQualificationCreateEmployeeDtoToQualification(
        this QualificationCreateEmployeeDto dto, Guid qualificationTypeId)
    {
        return new Domain.Entities.Qualification
        {
            Name = dto.Qualification,
            Description = dto.Description,
            QualificationTypeId = qualificationTypeId
        };
    }
}