namespace Kindergarten.Application.Common.Dto.Employee;

public record UpdateEmployeePositionDto(Guid EmployeeId, Guid EmployeePositionId, string KindergartenName);