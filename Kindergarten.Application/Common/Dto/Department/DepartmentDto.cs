namespace Kindergarten.Application.Common.Dto.Department;

public record DepartmentDto(
    Guid Id,
    int AgeGroup,
    int Capacity,
    string Name
    );