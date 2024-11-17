using Kindergarten.Application.Common.Dto.Department;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Department;

[Mapper]
public static partial class DepartmentMapper
{
    public static Domain.Entities.Department FromCreateDepartmentDtoToDepartment(this CreateDepartmentDto dto)
    {
        return new Domain.Entities.Department
        {
            Name = dto.Name,
            AgeGroup = dto.AgeGroup,
            Capacity = 0,
        };
    }
}