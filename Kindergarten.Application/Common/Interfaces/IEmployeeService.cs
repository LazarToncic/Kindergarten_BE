using Kindergarten.Application.Common.Dto.Employee;

namespace Kindergarten.Application.Common.Interfaces;

public interface IEmployeeService
{
    Task CreateEmployee(CreateEmployeeDto dto, CancellationToken cancellationToken);
    Task UpdateEmployeePosition(UpdateEmployeePositionDto dto, CancellationToken cancellationToken);
}