using Kindergarten.Application.Common.Dto.ChildWithdrawalRequest;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.ChildWithdrawal;

[Mapper]
public static partial class ChildWithdrawalMapper
{
    public static WithdrawalRequestDtoResponse ToDto(this ChildWithdrawalRequest src)
    {
        // aktivno odeljenje
        var activeAssignment = src.Child.DepartmentAssignments
            .First(da => da.IsActive);
        var department = activeAssignment.Department;
        var kindergarten = activeAssignment.Kindergarten;

        // otac
        var fatherName = src.Child.ParentChildren
            .Where(pc => pc.Relationship == ParentChildRelationship.Father)
            .Select(pc => pc.Parent.User.FirstName + " " + pc.Parent.User.LastName)
            .FirstOrDefault() ?? string.Empty;

        // majka
        var motherName = src.Child.ParentChildren
            .Where(pc => pc.Relationship == ParentChildRelationship.Mother)
            .Select(pc => pc.Parent.User.FirstName + " " + pc.Parent.User.LastName)
            .FirstOrDefault() ?? string.Empty;

        // trenutni učitelj
        var teacherName = department.DepartmentEmployees
            .Where(de => de.RoleInDepartment == EmployeeExtension.Teacher)
            .Where(de => de.IsActive)
            .Select(de => de.Employee.User.FirstName + " " + de.Employee.User.LastName)
            .FirstOrDefault() ?? string.Empty;

        return new WithdrawalRequestDtoResponse(
            src.Id,
            // ChildFullName
            src.Child.FirstName + " " + src.Child.LastName,
            // FatherFullName
            fatherName,
            // MotherFullName
            motherName,
            // SubmittedByName
            src.RequestedByUser.FirstName + " " + src.RequestedByUser.LastName,
            // ReviewedByName
            src.ReviewedByUser != null
                ? src.ReviewedByUser.FirstName + " " + src.ReviewedByUser.LastName
                : null,
            // ReviewedAt
            src.ReviewedAt,
            // TeacherName
            teacherName,
            // DepartmentName
            department.Name,
            // KindergartenName
            kindergarten.Name,
            // Status
            src.Status,
            // CreatedAt
            src.RequestedAt,
            // Reason
            src.Reason
        );
    }

    public static WithdrawalRequestDtoResponseList ToDtoList(
        this IEnumerable<ChildWithdrawalRequest> src)
    {
        var list = src
            .Select(r => r.ToDto())
            .ToList();
        return new WithdrawalRequestDtoResponseList(list);
    }
}