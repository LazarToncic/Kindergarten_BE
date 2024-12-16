using Kindergarten.Application.Common.Dto.Employee;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Employee.Commands;

public record UpdateEmployeePositionCommand(UpdateEmployeePositionDto Dto) : IRequest;

public class UpdateEmployeePositionCommandHandler(IEmployeeService employeeService) : IRequestHandler<UpdateEmployeePositionCommand>
{
    public async Task Handle(UpdateEmployeePositionCommand request, CancellationToken cancellationToken)
    {
        await employeeService.UpdateEmployeePosition(request.Dto, cancellationToken);
    }
} 