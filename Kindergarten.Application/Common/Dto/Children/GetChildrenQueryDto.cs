namespace Kindergarten.Application.Common.Dto.Children;

public record GetChildrenQueryDto(
        Guid? KindergartenId,
        string? FirstName,
        string? LastName,
        DateOnly? BirthDate,
        bool? IsActive
    );