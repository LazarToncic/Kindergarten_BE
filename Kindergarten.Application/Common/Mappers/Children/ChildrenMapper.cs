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

    public static GetUnassignedChildrenDto ChildToGetUnassignedChildrenDto(Domain.Entities.Child child)
    {
        return new GetUnassignedChildrenDto(
            child.Id,
            child.FirstName,
            child.LastName,
            child.YearOfBirth,
            child.ChildAllergies.Any(),
            child.ChildAllergies
                .Select(ca => ca.Allergy.Name)
                .ToList(),
            child.ChildMedicalConditions.Any(),
            child.ChildMedicalConditions
                .Select(cm => cm.MedicalCondition.Name)
                .ToList()
        );
    }

}