namespace Kindergarten.Application.Common.Dto.Department;

public record CreateDepartmentDto(string Name, int AgeGroup, Guid KindergartenId, int MaximumCapacity);