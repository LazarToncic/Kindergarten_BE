namespace Kindergarten.Application.Common.Dto.Children;

public record GetChildrenQueryResponse(
        Guid ChildId,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string KindergartenName,
        string DepartmentName,
        Guid DepartmentId,
        string TeacherFullName,
        string ParentMotherFullName,
        string ParentFatherFullName,
        bool IsActive
    );