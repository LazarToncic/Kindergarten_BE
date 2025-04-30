namespace Kindergarten.Application.Common.Dto.Children;

public record GetGetUnassignedChildrenQueryDto(Guid? KindergartenId, string? FirstName, string? LastName);