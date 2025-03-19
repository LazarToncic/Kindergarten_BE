using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Children;

[Mapper]
public static partial class ChildrenMapper
{
    public static NewChildrenThroughParentRequestDto FromParentRequestChildDtoToNewChildrenThroughParentRequest(
        Guid childId,
        ParentRequestChildDto dto)
    {
        return new NewChildrenThroughParentRequestDto(childId, dto.HasAllergies, 
            dto.Allergies, dto.HasMedicalIssues, dto.MedicalConditions);
    }
}