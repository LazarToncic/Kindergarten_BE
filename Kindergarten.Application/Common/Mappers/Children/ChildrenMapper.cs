using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Domain.Entities.Enums;
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

    public static GetChildrenQueryResponse ChildToGetChildrenQueryResponse(Domain.Entities.Child src)
    {
        var assignment = src.DepartmentAssignments
            .FirstOrDefault(da => da.IsActive);
        var dept = assignment?.Department;
        var kg   = dept?
            .KindergartenDepartments
            .FirstOrDefault()?
            .Kindergarten;

        var teacherUser = dept?
            .DepartmentEmployees
            .FirstOrDefault(de => de.RoleInDepartment == EmployeeExtension.Teacher)
            ?.Employee.User;

        var motherUser = src.ParentChildren
            .FirstOrDefault(pc => pc.Relationship == ParentChildRelationship.Mother)
            ?.Parent.User;
        var fatherUser = src.ParentChildren
            .FirstOrDefault(pc => pc.Relationship == ParentChildRelationship.Father)
            ?.Parent.User;

        return new GetChildrenQueryResponse(
            src.Id,
            src.FirstName,
            src.LastName,
            src.YearOfBirth,
            kg?.Name   ?? "Unknown",
            dept?.Name ?? "Unknown",
            assignment?.DepartmentId ?? Guid.Empty,
            teacherUser != null
                ? $"{teacherUser.FirstName} {teacherUser.LastName}"
                : "Unknown",
            motherUser != null
                ? $"{motherUser.FirstName} {motherUser.LastName}"
                : "Unknown",
            fatherUser != null
                ? $"{fatherUser.FirstName} {fatherUser.LastName}"
                : "Unknown",
            assignment?.IsActive ?? false
        );
    }

    public static GetChildrenQueryResponseList ToGetChildrenQueryResponseList(
        IEnumerable<Domain.Entities.Child> children)
    {
        var list = children
            .Select(ChildToGetChildrenQueryResponse)
            .ToList();
        return new GetChildrenQueryResponseList(list);
    }

}