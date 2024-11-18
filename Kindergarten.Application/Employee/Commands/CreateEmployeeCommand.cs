using Kindergarten.Application.Common.Dto.Employee;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Employee.Commands;

public record CreateEmployeeCommand(CreateEmployeeDto Dto) : IRequest;

public class CreateEmployeeCommandHandler(IEmployeeService employeeService) : IRequestHandler<CreateEmployeeCommand>
{
    public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await employeeService.CreateEmployee(request.Dto, cancellationToken);
    }
} 